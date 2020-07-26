using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI.AdminPanel
{
    public partial class Menu : System.Web.UI.Page
    {
        #region private Method
        private void isAuthorizeToPage()
        {
            string url = Path.GetFileName(Request.Path);
            DataTable dt = new DataTable();
            if (Session["RoleMenu"] != null)
            {
                dt = (DataTable)(Session["RoleMenu"]);
                var filtered = dt.AsEnumerable().Where(r => r.Field<String>("menuUrl").Contains(url)).ToList();
                if (filtered.Count <= 0)
                {
                    Response.Redirect("~/UI/404.aspx");
                }

            }

        }
        private void clearAll()
        {
            foreach (Control control in Panel2.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    txt.Text = "";
                }
                if (control is Label)
                {
                    Label txt = (Label)control;
                    txt.Text = "";
                }
                if (control is DropDownList)
                {
                    DropDownList txt = (DropDownList)control;
                    txt.ClearSelection();
                }
                if (control is GridView)
                {
                    GridView txt = (GridView)control;
                    txt.DataSource = null;
                    txt.DataBind();
                }

            }

            foreach (Control control in Panel6.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    txt.Text = "";
                }
                if (control is Label)
                {
                    Label txt = (Label)control;
                    txt.Text = "";
                }
                if (control is DropDownList)
                {
                    DropDownList txt = (DropDownList)control;
                    txt.ClearSelection();
                }
                if (control is GridView)
                {
                    GridView txt = (GridView)control;
                    txt.DataSource = null;
                    txt.DataBind();
                }

            }

        }

        private DataTable LoadMenus(string searchKey)
        {
            try
            {
                DataTable dt;


                using (UserGateway gatwayObj = new UserGateway())
                {

                    dt = gatwayObj.GetAllMenu(1);

                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("menuId").ToUpper().Contains(searchKey.ToUpper())
           || r.Field<String>("menuName").ToUpper().Contains(searchKey.ToUpper()));

                        dt = filtered.CopyToDataTable();
                    }
                    return dt;

                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                }
                isAuthorizeToPage();
                //lblMessage.Text = string.Empty;
                if (!IsPostBack && !IsCallback)
                {

                }
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadMenus(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadMenus(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int comCode1 = 1;
            string code = lblMenuId.Text;
            string menuName = txtMenuName.Text;
            string menuUrl = txtMenuUrl.Text;
            string isActive;
            if (cbIsActive.Checked)
            {
                isActive = "0";
            }
            else
                isActive = "1";

            

            using (UserGateway gatwayObj = new UserGateway())
            {
                string rCode = gatwayObj.InsertUpdateMenu(1, comCode1, code, menuName, menuUrl,isActive);
                lblMenuId.Text = rCode;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearAll();
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string code = rows.Cells[1].Text;
                DataTable dt = new DataTable();

                using (UserGateway gatwayObj = new UserGateway())
                {
                    dt = gatwayObj.GetMenuByMenuId(1, Convert.ToInt16(code));
                }

                lblMenuId.Text = dt.Rows[0]["menuId"].ToString();
                txtMenuName.Text = dt.Rows[0]["menuName"].ToString();
                txtMenuUrl.Text = dt.Rows[0]["menuUrl"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["isActive"].ToString()) || dt.Rows[0]["isActive"].ToString() == "0")
                {
                    cbIsActive.Checked = true;
                }
                else
                    cbIsActive.Checked = false;


                hfShowList_ModalPopupExtender.Hide();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadMenus(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }
    }
}