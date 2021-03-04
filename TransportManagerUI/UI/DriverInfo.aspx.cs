using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;
using System.Text;
using System.IO;


namespace TransportManagerUI.UI
{
    public partial class DriverInfo : System.Web.UI.Page
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
                if ((!IsPostBack) && (!IsCallback))
                {
                    getGhat();

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
        private DataTable GetDriver(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (PersonalGateway gatwayObj = new PersonalGateway())
                {
                    dt2 = gatwayObj.GetAllDriver();
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("CardNo=1");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();


                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("EmpCode").Contains(searchKey) || r.Field<String>("EmpName").ToUpper().Contains(searchKey.ToUpper()));
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
        private bool isMandatoryFieldValidate()
        {
            if (String.IsNullOrEmpty(txtEmployeeName.Text) )
                return false;

            else
                return true;
        }
        #endregion
        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearch.Text);

            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearch.Text);

            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();

            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMandatoryFieldValidate())
                {
                    string EmpCode = lblEmpCode.Text;

                    string Cardno = "1";
                    string EmpName = txtEmployeeName.Text;
                    int ComCode = 1;
                    string FatherName = txtFatherName.Text;
                    string MotherName = txtMotherName.Text;
                    string BloodGroup = ddlBloodGroup.SelectedValue;
                    string Add1 = txtAddress1.Text;
                    string Add2 = txtAddress2.Text;
                    string Add3 = txtAddress3.Text;
                    string Mobile = txtMobilePhone.Text;
                    //int IsHired = 0;
                    int PStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                    string DrivingLicen = txtDrivingLisense.Text;
                    string userId = Session["UserName"].ToString();
                    int storeCode = Convert.ToInt32(ddlGhat.SelectedValue);
                    string nid = txtNID.Text;
                    string drivingCapacity = txtDrivingCapacity.Text;
                    using (PersonalGateway gatwayObj = new PersonalGateway())
                    {
                        string empcode = gatwayObj.InsertUpdatePersonal(EmpCode, Cardno, EmpName, ComCode, FatherName, MotherName, BloodGroup
                            , Add1, Add2, Add3, Mobile, PStatus, DrivingLicen, userId, storeCode, nid, drivingCapacity);
                        lblEmpCode.Text = empcode;
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

                using (PersonalGateway gatwayObj = new PersonalGateway())
                {
                    dt = gatwayObj.GetDriverById(1, code);
                }
                //EmpCode, Cardno, EmpName, ComCode,StoreCode, ShiftId, EmpType, FatherName, MotherName, BloodGroup, Add1, Add2, Add3, PostCode, Country, Phone, Mobile, Fax, Email, PStatus, BasicFinal, DrivingLicen,NID, DrivingCapacity 
                lblEmpCode.Text = dt.Rows[0]["EmpCode"].ToString();
                if(String.IsNullOrEmpty(dt.Rows[0]["StoreCode"].ToString()) ==false)
                    ddlGhat.SelectedValue = dt.Rows[0]["StoreCode"].ToString();
                txtEmployeeName.Text = dt.Rows[0]["EmpName"].ToString();
                txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                txtMotherName.Text = dt.Rows[0]["MotherName"].ToString();
                ddlBloodGroup.SelectedValue = dt.Rows[0]["BloodGroup"].ToString();
                txtAddress1.Text = dt.Rows[0]["Add1"].ToString();
                txtAddress2.Text = dt.Rows[0]["Add2"].ToString();
                txtAddress3.Text = dt.Rows[0]["Add3"].ToString();
                txtPostCode.Text = dt.Rows[0]["PostCode"].ToString();
                
                txtMobilePhone.Text = dt.Rows[0]["Mobile"].ToString();
                txtDrivingLisense.Text = dt.Rows[0]["DrivingLicen"].ToString();

                ddlStatus.SelectedValue = dt.Rows[0]["PStatus"].ToString();
                txtNID.Text = dt.Rows[0]["NID"].ToString();
                txtDrivingCapacity.Text = dt.Rows[0]["DrivingCapacity"].ToString();

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