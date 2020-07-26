using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI.AdminPanel
{
    public partial class AddUser : System.Web.UI.Page
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

       
        private void getGhat()
        {
            try
            {
                DataTable dt;


                using (StoreLocation gatwayObj = new StoreLocation())
                {


                    dt = gatwayObj.GetAllStoreLocation(1);
                    ddlGhat.DataSource = dt;
                    ddlGhat.DataValueField = "StoreCode";
                    ddlGhat.DataTextField = "StoreName";
                    ddlGhat.DataBind();

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);

            }
        }
        private void LoadRoles()
        {
            try
            {
                DataTable dt;


                using (UserGateway gatwayObj = new UserGateway())
                {

                    dt = gatwayObj.GetAllRoles(1);

                

                    ddlUserRole.DataSource = dt;
                    ddlUserRole.DataValueField = "RoleId";
                    ddlUserRole.DataTextField = "RoleName";

                    ddlUserRole.DataBind();
                    }
                   
                 }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                
            }
        }

        private DataTable GetUserList(string searchKey)
        {
            try
            {
                DataTable dt;


                using (UserGateway gatwayObj = new UserGateway())
                {


                    dt = gatwayObj.GetUserInfo(1);
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("UserName").Contains(searchKey) || r.Field<String>("RoleName").ToUpper().Contains(searchKey.ToUpper()));
                        dt = filtered.CopyToDataTable();

                    }
                    return dt;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }
            isAuthorizeToPage();
            if (!IsPostBack && !IsCallback)
            {
                getGhat();
                LoadRoles();
               
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetUserList(txtSearch.Text);
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
            dt = GetUserList(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string userName=txtUserName.Text;
                int comCode=1;
                string password = txtPassword.Text;
                int ghat=Convert.ToInt32(ddlGhat.SelectedValue);
                int role = Convert.ToInt32(ddlUserRole.SelectedValue);
                DataTable dt=new DataTable();
                dt=GetUserList(userName);
                if (dt!=null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('User Name Taken');", true);
                }
                else
                {
                    using (UserGateway gatwayObj = new UserGateway())
                    {
                        userName = gatwayObj.InsertUpdateUser(comCode, "0", userName, password, "0", role, ghat);
                        txtUserName.Text = userName;

                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
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
                string userName = rows.Cells[1].Text;
                txtUserName.Text = userName;
                DataTable dt = new DataTable();

                using (UserGateway gatwayObj = new UserGateway())
                {
                    dt = gatwayObj.GetUserInfo(1, userName);
                }
                //SELECT    ComCode, StoreCode, VehicleID, VehicleNo, ChesisNo, ModelNo, EngineNo, VehicleDesc, MobileNo, 
                //Capacity, KmPerLiter, FuelCode, IsHired, VehicleStatus FROM            VehicleInfo where VehicleID=@VehicleId and comCode=@ComCode
                if (dt != null)
                {

                    if (String.IsNullOrEmpty(dt.Rows[0]["Store_Id"].ToString()) == false)
                    {
                        ddlGhat.SelectedValue = dt.Rows[0]["Store_Id"].ToString();
                    }
                    if (String.IsNullOrEmpty(dt.Rows[0]["Store_Id"].ToString()))
                    {
                        ddlGhat.SelectedValue = "1";
                    }
                    else
                        ddlGhat.SelectedValue = dt.Rows[0]["Store_Id"].ToString();


                    if (String.IsNullOrEmpty(dt.Rows[0]["Role_Id"].ToString()) == false)
                    {
                        ddlUserRole.SelectedValue = dt.Rows[0]["Role_Id"].ToString();
                    }
                    if (String.IsNullOrEmpty(dt.Rows[0]["Role_Id"].ToString()))
                    {
                        ddlUserRole.SelectedValue = "1";
                    }
                    else
                        ddlUserRole.SelectedValue = dt.Rows[0]["Role_Id"].ToString();
                }
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
            dt = GetUserList(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }
    }
}