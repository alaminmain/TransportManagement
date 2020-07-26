using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;
using System.IO;

namespace TransportManagerUI.UI.AdminPanel
{
    public partial class Roles : System.Web.UI.Page
    {
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

        private DataTable LoadRoles(string searchKey)
        {
            try
            {
                DataTable dt;


                using (UserGateway gatwayObj = new UserGateway())
                {

                    dt = gatwayObj.GetAllRoles(1);

                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("RoleId").ToUpper().Contains(searchKey.ToUpper())
           || r.Field<String>("RoleName").ToUpper().Contains(searchKey.ToUpper()));

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
                    dt = gatwayObj.GetRolesByRoleId(1, Convert.ToInt16(code));
                }

                lblRoleId.Text = dt.Rows[0]["RoleId"].ToString();
                txtRoleName.Text = dt.Rows[0]["RoleName"].ToString();
                txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
               

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
            dt = LoadRoles(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadRoles(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int comCode1 = 1;
            int roleCode = Convert.ToInt16(lblRoleId.Text);
            string roleName = txtRoleName.Text;
            string remarks = txtRemarks.Text;
          
            string userId = Session["UserName"].ToString();

            using (UserGateway gatwayObj = new UserGateway())
            {
                string rCode = gatwayObj.InsertUpdateRoles(1,comCode1,roleCode,roleName,remarks);
                lblRoleId.Text = rCode;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadRoles(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}