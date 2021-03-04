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
            productDt.Columns.Add("IsView");
            productDt.Columns.Add("IsInsert");
            productDt.Columns.Add("IsUpdate");
            productDt.Columns.Add("IsDelete");
            productDt.Columns.Add("IsPrint");
            productDt.Columns.Add("Status");

            foreach (GridViewRow item in gvRolePermission.Rows)
            {
                if ((item.Cells[0].FindControl("chkSelectUnique") as CheckBox).Checked)
                {

                    
                    
                    DataRow dr = productDt.NewRow();
                    dr["userRoleId"] = Convert.ToInt32(ddlRoles.SelectedValue);
                    dr["MenuId"] = gvRolePermission.DataKeys[item.RowIndex].Values[0].ToString(); 
                    dr["ComCode"] = 1;
                    CheckBox cbView = (CheckBox)item.FindControl("cbIsView");
                    CheckBox cbInsert = (CheckBox)item.FindControl("cbIsInsert");
                    CheckBox cbUpdate = (CheckBox)item.FindControl("cbIsUpdate");
                    CheckBox cbDelete = (CheckBox)item.FindControl("cbIsDelete");
                    CheckBox cbPrint = (CheckBox)item.FindControl("cbIsPrint");

                    if (cbView.Checked) dr["IsView"] = "Y"; else dr["IsView"] = "N";
                    if (cbInsert.Checked) dr["IsInsert"] = "Y"; else dr["IsInsert"] = "N";
                    if (cbUpdate.Checked) dr["IsUpdate"] = "Y"; else dr["IsUpdate"] = "N";
                    if (cbDelete.Checked) dr["IsDelete"] = "Y"; else dr["IsDelete"] = "N";
                    if (cbPrint.Checked) dr["IsPrint"] = "Y"; else dr["IsPrint"] = "N";
                    dr["Status"] = 0;
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
            gvRolePermission.DataSource = menu;
            gvRolePermission.DataBind();
            for (int i = 0; i < this.gvRolePermission.Rows.Count; ++i)
            {
                CheckBox checkBox = (CheckBox)this.gvRolePermission.Rows[i].FindControl("chkSelectUnique");
                menuId = Convert.ToInt32(this.gvRolePermission.DataKeys[i].Value);
                CheckBox cbView = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsView");
                CheckBox cbInsert = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsInsert");
                CheckBox cbUpdate = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsUpdate");
                CheckBox cbDelete = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsDelete");
                CheckBox cbPrint = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsPrint");
                if (roleMenu.Select().ToList().Exists(row => row["userMenuId"].ToString() == menuId.ToString()))
                {
                    checkBox.Checked = true;
                    DataRow[] rows = roleMenu.Select("userMenuId=" + menuId.ToString());

                    if (rows[0]["IsView"].ToString() == "Y")
                        cbView.Checked = true;
                    if (rows[0]["IsInsert"].ToString() == "Y")
                        cbInsert.Checked = true;
                    if (rows[0]["IsUpdate"].ToString() == "Y")
                        cbUpdate.Checked = true;
                    if (rows[0]["IsDelete"].ToString() == "Y")
                        cbDelete.Checked = true;
                    if (rows[0]["IsPrint"].ToString() == "Y")
                        cbPrint.Checked = true;
                }
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
            string userCode= Session["UserName"].ToString();
            using (UserGateway gatwayObj = new UserGateway())
            {
                gatwayObj.InsertUpdateRoleMenu(dt,userCode);
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

        //protected void btnSave_Click(object sender, EventArgs e)
        //{

        //    int menuId;
        //    int roleId;
        //    int appId;
           
        //    roleId = Convert.ToInt32(ddlRoles.SelectedValue.ToString());

        //    string IsInsert = string.Empty;
        //    string IsDelete = string.Empty;
        //    string IsView = string.Empty;
        //    string IsUpdate = string.Empty;
        //    string IsPrint = string.Empty;

        //    if ((appId != 0) && (roleId != 0))
        //    {

        //        // update checkbox value if role found in checkbox


        //        //load Role permission datatable
        //        DataTable dtRolePermission = null;
        //        using (UserGateway gatwayObj = new UserGateway())
        //        {

        //            dtRolePermission = gatwayObj.GetRolesMenuAll(1, Convert.ToInt32(ddlRoles.SelectedValue));

        //        }

        //        for (int i = 0; i < this.gvRolePermission.Rows.Count; ++i)
        //        {
        //            CheckBox checkBox = (CheckBox)this.gvRolePermission.Rows[i].FindControl("chkSelectUnique");
        //            menuId = Convert.ToInt32(this.gvRolePermission.DataKeys[i].Value);

        //            if (checkBox.Checked)
        //            {
        //                CheckBox cbView = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsView");
        //                CheckBox cbInsert = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsInsert");
        //                CheckBox cbUpdate = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsUpdate");
        //                CheckBox cbDelete = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsDelete");
        //                CheckBox cbPrint = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsPrint");

        //                if (cbView.Checked) IsView = "Y"; else IsView = "N";
        //                if (cbInsert.Checked) IsInsert = "Y"; else IsInsert = "N";
        //                if (cbUpdate.Checked) IsUpdate = "Y"; else IsUpdate = "N";
        //                if (cbDelete.Checked) IsDelete = "Y"; else IsDelete = "N";
        //                if (cbPrint.Checked) IsPrint = "Y"; else IsPrint = "N";


        //                //new select role is already exists in old user role if not found then prepare data for insert
        //                if (!dtRolePermission.Select().ToList().Exists(row => row["MENU_ID"].ToString() == menuId.ToString()))
        //                {
        //                    // if (dtUserAllRoles.Select().ToList().Exists(row => row["USER_ID"].ToString() == userId.ToString()))

        //                    //add new permission
        //                    rolePermissionInsertList.Add(
        //                    new RolePermission
        //                    {

        //                        RoleId = roleId,
        //                        MenuId = menuId,
        //                        IsInsert = IsInsert,
        //                        IsUpdate = IsUpdate,
        //                        IsDelete = IsDelete,
        //                        IsView = IsView,
        //                        IsPrint = IsPrint,
        //                        PermissionStatus = "Y"

        //                    });
        //                }
        //                else
        //                {
        //                    //check any role is update like isInsert, Is_update, Is_delete
        //                    DataRow[] rows = dtRolePermission.Select("menu_id=" + menuId.ToString());

        //                    if ((rows[0]["IS_INSERT"].ToString() != IsInsert) || (rows[0]["IS_VIEW"].ToString() != IsView) ||
        //                        (rows[0]["IS_UPDATE"].ToString() != IsUpdate) || (rows[0]["IS_DELETE"].ToString() != IsDelete) || (rows[0]["IS_PRINT"].ToString() != IsPrint))

        //                        rolePermissionUpdateList.Add(
        //                        new RolePermission
        //                        {

        //                            RoleId = roleId,
        //                            MenuId = menuId,
        //                            IsInsert = IsInsert,
        //                            IsUpdate = IsUpdate,
        //                            IsDelete = IsDelete,
        //                            IsView = IsView,
        //                            IsPrint = IsPrint,
        //                            PermissionStatus = "Y"

        //                        });
        //                }
        //            }
        //            else
        //            {
        //                if (dtRolePermission.Select().ToList().Exists(row => row["MENU_ID"].ToString() == menuId.ToString()))

        //                    // if (dtUserAllRoles.Select().ToList().Exists(row => row["USER_ID"].ToString() == userId.ToString()))
        //                    rolePermissionDeleteList.Add(
        //                    new RolePermission
        //                    {

        //                        RoleId = roleId,
        //                        MenuId = menuId,
        //                    });

        //            }
        //        } //end for loop

        //        if (rolePermissionInsertList.Count > 0 || rolePermissionDeleteList.Count > 0 || rolePermissionUpdateList.Count > 0)
        //        {

        //            objRole.SaveRolePermission(rolePermissionInsertList, rolePermissionUpdateList, rolePermissionDeleteList, 1);

        //            ErrorMessage.Text = "Change Successfully.";
        //            LoadRolePermissionDataGrid();

        //        }
        //    }
        //}

        private void LoadRolePermissionDataGrid()
        {
            int menuId;
            int roleId;
            
            roleId = Convert.ToInt32(ddlRoles.SelectedValue.ToString());
            if ((roleId != 0))
            {
               
                //clear gridview  
                for (int i = 0; i < this.gvRolePermission.Rows.Count; ++i)
                {
                    CheckBox checkBox = (CheckBox)this.gvRolePermission.Rows[i].FindControl("chkSelectUnique");
                    checkBox.Checked = false;
                }


                // update checkbox value if role found in checkbox
                DataTable dtRolePermission = null;
                using (UserGateway gatwayObj = new UserGateway())
                {

                    dtRolePermission = gatwayObj.GetRolesMenuAll(1, Convert.ToInt32(ddlRoles.SelectedValue));

                }
                //load Role permission datatable
               

                if (dtRolePermission.Rows.Count > 0)
                {
                    for (int i = 0; i <= this.gvRolePermission.Rows.Count; ++i)
                    {
                        //find control of all check box
                        CheckBox checkBox = (CheckBox)this.gvRolePermission.Rows[i].FindControl("chkSelectUnique");
                        menuId = Convert.ToInt32(this.gvRolePermission.DataKeys[i].Value);
                        CheckBox cbView = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsView");
                        CheckBox cbInsert = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsInsert");
                        CheckBox cbUpdate = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsUpdate");
                        CheckBox cbDelete = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsDelete");
                        CheckBox cbPrint = (CheckBox)this.gvRolePermission.Rows[i].FindControl("cbIsPrint");
                        if (dtRolePermission.Select().ToList().Exists(row => row["MENU_ID"].ToString() == menuId.ToString()))
                        {
                            checkBox.Checked = true;
                            DataRow[] rows = dtRolePermission.Select("userMenuId=" + menuId.ToString());

                            if (rows[0]["IsView"].ToString() == "Y")
                                cbView.Checked = true;
                            if (rows[0]["IsInsert"].ToString() == "Y")
                                cbInsert.Checked = true;
                            if (rows[0]["IsUpdate"].ToString() == "Y")
                                cbUpdate.Checked = true;
                            if (rows[0]["IsDelete"].ToString() == "Y")
                                cbDelete.Checked = true;
                            if (rows[0]["IsPrint"].ToString() == "Y")
                                cbPrint.Checked = true;
                        }
                    }
                }// if any role found in role permission

            }  //end if

        }

        protected void chkSelectUnique_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}