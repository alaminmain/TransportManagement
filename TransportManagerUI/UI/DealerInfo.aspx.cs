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
    public partial class DealerInfo : System.Web.UI.Page
    {
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
                    //ClearSession();
                    //FillChalanTypDDL();
                    //LoadChartofAccounts("");
                    //if (ddlDestination.Items.Count > 0)//&& ddlOrigin.Items.Count > 0 && ddlChalanTyp.Items.Count > 0 && 
                    //{
                    //    ClearAllCtl();
                    //}
                    //txtChalanSearch.Focus();
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
            dt = loadAllDealerInfo(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
            
        }


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
        private DataTable loadAllDealerInfo(string searchKey)
        {
            try
            {
                DataTable dt;


                using (DealerGateway gatwayObj = new DealerGateway())
                {

                    dt = gatwayObj.Get_All_Dealer();

                    if (String.IsNullOrEmpty(searchKey))
                    {


                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("CustId").ToUpper().Contains(searchKey.ToUpper())
           || r.Field<String>("CustName").ToUpper().Contains(searchKey.ToUpper()));
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
        private bool isMandatoryFieldValidate()
        {
            if (String.IsNullOrEmpty(txtCustomerName.Text) || String.IsNullOrEmpty(txtLocDistance.Text))
                return false;
            
            else
                return true;
        }
        #endregion

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllDealerInfo(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllDealerInfo(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMandatoryFieldValidate())
                {
                    int ComCode = 1;
                    string CustId = lblCustomerId.Text;
                    string CustName = txtCustomerName.Text;
                    string CustNameBangla = txtCustomerNameBangla.Text;
                    string CustAddressBang = txtAddressBangla.Text;
                    string DealerId = String.Empty;
                    string CustType = "1";
                    string ContactPer = txtContactPerson.Text;
                    string Add1 = txtAddres1.Text;
                    string Add2 = txtAddress2.Text;
                    string Add3 = txtAddress3.Text;
                    string Phone = txtPhone.Text;
                    string Mobile = txtMobile.Text;
                    string Location = String.Empty;
                    int locDistance = Convert.ToInt32(txtLocDistance.Text);
                    int status = Convert.ToInt32(ddlStatus.SelectedValue);
                    string userId = Session["UserName"].ToString();//Add in session

                    using (DealerGateway gatwayObj = new DealerGateway())
                    {
                        string custCode = gatwayObj.InsertUpdateCustomer(ComCode, CustId, CustName, CustNameBangla, CustAddressBang, DealerId, CustType,
                            ContactPer, Add1, Add2, Add3, Phone, Mobile, Location, locDistance, status, userId);
                        lblCustomerId.Text = custCode;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Mandatory Field');", true);
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

                using (DealerGateway gatwayObj = new DealerGateway())
                {
                    dt = gatwayObj.GetCustomerById(1, code);
                }
                //ComCode, CustId, CustName, CustNameBangla, CustAddressBang, DealerId, ContactPer, Designation, CustType, AgreeDate, Add1, Add2, Add3, Phone, Mobile, Email, FAX, Location, LocDistance
                lblCustomerId.Text = dt.Rows[0]["CustId"].ToString();
                txtCustomerName.Text = dt.Rows[0]["CustName"].ToString();
                txtCustomerNameBangla.Text = dt.Rows[0]["CustNameBangla"].ToString();
                txtAddressBangla.Text = dt.Rows[0]["CustAddressBang"].ToString();
                txtContactPerson.Text = dt.Rows[0]["ContactPer"].ToString();
                txtAddres1.Text = dt.Rows[0]["Add1"].ToString();
                txtAddress2.Text = dt.Rows[0]["Add2"].ToString();
                txtAddress3.Text = dt.Rows[0]["Add3"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtLocDistance.Text = dt.Rows[0]["LocDistance"].ToString();
                ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();


                hfShowList_ModalPopupExtender.Hide();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}