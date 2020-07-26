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
    public partial class AssignMenuToRole : System.Web.UI.Page
    {
        #region Private Method

        private DataTable RolesMenuDataTable()
        {
            
            List<string> menuIdList = new List<string>();
            string menuIds = String.Empty;
            

            DataTable productDt = new DataTable();
            productDt.Columns.Add("userRoleId");

            productDt.Columns.Add("MenuId");
            productDt.Columns.Add("ComCode");
          
            foreach (GridViewRow item in gvlistofBasicData.Rows)
            {
                if ((item.Cells[0].FindControl("chkIsActive") as CheckBox).Checked)
                {

                    menuIds = item.Cells[1].Text;
                    menuIdList.Add(menuIds);
                }


            }
            if (menuIdList != null)
            {
                foreach (string id in menuIdList)
                {
                    
                            DataRow dr = productDt.NewRow();
                            dr["userRoleId"] = Convert.ToInt32(ddlRoles.SelectedValue);
                            dr["MenuId"] = id;
                            dr["ComCode"] = 1;
                            
                            productDt.Rows.Add(dr);
                        }
               
            }
            return productDt;

        }

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
            foreach (Control control in Panel1.Controls)
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

        private DataTable LoadRolesMenu(string searchKey)
        {
            try
            {
                DataTable dt;


                using (UserGateway gatwayObj = new UserGateway())
                {

                    dt = gatwayObj.GetRolesMenuAll(1,Convert.ToInt32(ddlRoles.SelectedValue));

                    }
                    return dt;

                

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }

        public void loadRoleChk()
        {
            DataTable menu = new DataTable();
            menu = LoadMenus(txtSearch.Text);

            DataTable roleMenu = new DataTable();
            roleMenu = LoadRolesMenu(string.Empty);
            
            int menuId = 0;
            gvlistofBasicData.DataSource = menu;
            gvlistofBasicData.DataBind();
            for (int i = 0; i < this.gvlistofBasicData.Rows.Count; ++i)
            {
                CheckBox checkBox = (CheckBox)this.gvlistofBasicData.Rows[i].FindControl("chkIsActive");
                menuId = Convert.ToInt32(gvlistofBasicData.Rows[i].Cells[1].Text.ToString());
                if (roleMenu != null)
                {
                    if (roleMenu.Select().ToList().Exists(row => row["userMenuId"].ToString() == menuId.ToString()))
                        checkBox.Checked = true;
                }
                else
                    checkBox.Checked = false;
            }
            
            
        }


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Session["UserName"] == null)
                //{
                //    Session.Abandon();
                //    Response.Redirect("~/login.aspx");
                //}
                //isAuthorizeToPage();
                //lblMessage.Text = string.Empty;
                if (!IsPostBack && !IsCallback)
                {
                    DataTable roledt = new DataTable();
                    roledt = LoadRoles(String.Empty);

                    ddlRoles.DataSource = roledt;
                    ddlRoles.DataValueField = "RoleId";
                    ddlRoles.DataTextField = "RoleName";

                    ddlRoles.DataBind();

                    loadRoleChk();

                  
                   
                }
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRoleChk();
        }

        protected void gvlistofBasicData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = RolesMenuDataTable();
            using (UserGateway gatwayObj = new UserGateway())
            {
                gatwayObj.InsertUpdateRoleMenu(dt);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
        }
    }
}