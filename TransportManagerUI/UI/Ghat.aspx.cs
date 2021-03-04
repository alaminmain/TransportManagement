using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;
using System.IO;

namespace TransportManagerUI.UI
{
    public partial class Ghat : System.Web.UI.Page
    {
        #region Private Method
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
        private DataTable loadAllGhatInfo(string searchKey)
        {
            try
            {
                DataTable dt;


                using (StoreLocation gatwayObj = new StoreLocation())
                {

                    dt = gatwayObj.GetAllStoreLocation(1);

                    if (String.IsNullOrEmpty(searchKey))
                    {


                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<Int32>("StoreCode").Equals(searchKey.ToUpper())
           || r.Field<String>("StoreName").ToUpper().Contains(searchKey.ToUpper()));
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
                if ((!IsPostBack) && (!IsCallback))
                {
                   

                }
            }
            catch (Exception ex)
            {
                throw;
                //Utilities.ShowMessageBox(
                //    ex.Data[Constant.CUSTOMMESSAGE] != null ? ex.Data[Constant.CUSTOMMESSAGE].ToString() : ex.Message,
                //    lblMessage, Color.Red);
            }
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllGhatInfo(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearAll();
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                int code = Convert.ToInt32(rows.Cells[1].Text);
                DataTable dt = new DataTable();

                using (StoreLocation gatwayObj = new StoreLocation())
                {
                    dt = gatwayObj.GetStoreLocation(1, code);
                }
                //ComCode, StoreCode, StoreName, Add1, Add2, Add3, PostCode, Phone, Fax, Mobile, Email, WebAddress, StoreStatus
                lblStoreCode.Text = dt.Rows[0]["StoreCode"].ToString();
                txtGhat.Text = dt.Rows[0]["StoreName"].ToString();
                txtAddres1.Text = dt.Rows[0]["Add1"].ToString();
                txtAddress2.Text = dt.Rows[0]["Add2"].ToString();
                txtAddress3.Text = dt.Rows[0]["Add3"].ToString();
                txtPostCode.Text = dt.Rows[0]["Add3"].ToString();
                txtFax.Text = dt.Rows[0]["Fax"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtWebAddress.Text = dt.Rows[0]["WebAddress"].ToString();
               
                ddlStatus.SelectedValue = dt.Rows[0]["StoreStatus"].ToString();


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
            dt = loadAllGhatInfo(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //ComCode, StoreCode, StoreName, Add1, Add2, Add3, PostCode, Phone, Fax, Mobile, Email, WebAddress, StoreStatus
                int ComCode = 1;
                string StoreCode = lblStoreCode.Text;
                string StoreName = txtGhat.Text;
                
                string Add1 = txtAddres1.Text;
                string Add2 = txtAddress2.Text;
                string Add3 = txtAddress3.Text;
                string PostCode = txtPostCode.Text;
                string Phone = txtPhone.Text;
                string Fax = txtFax.Text;
                
                string Mobile = txtMobile.Text;
                string Email = txtEmail.Text;
                string WebAddress = txtWebAddress.Text;
               int StoreStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                string userId = Session["UserName"].ToString();//Add in session

                using (StoreLocation gatwayObj = new StoreLocation())
                {
                    string storeCode = gatwayObj.InsertUpdateStoreLocation(ComCode, StoreCode, StoreName, Add1, Add2, Add3,
                        PostCode, Phone, Fax, Mobile, Email, WebAddress, StoreStatus, userId);
                lblStoreCode.Text=storeCode.ToString();
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllGhatInfo(txtSearch.Text);

            
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}