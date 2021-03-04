using MadinaWorkshop.DLL.Gateway;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI.Workshop
{
    public partial class Technician : System.Web.UI.Page
    {
        #region Private Method
        private DataTable GetAll(string searchKey)
        {
            try
            {
                DataTable dt = null;
                //DataTable dt2;
                string sql = @"Select EmpCode, EmpName,FatherName,Mobile from Personal where  Cardno='2' order by (EmpCode)asc ";

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];

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
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }
        private void loadddlSubDepartment()
        {
            string sql = @"select ProdSubCatCode,ProdSubCatName from ItemSubCategory where CateCode='02'";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlSubDepartment.DataSource = dt;
                ddlSubDepartment.DataTextField = "ProdSubCatName";
                ddlSubDepartment.DataValueField = "ProdSubCatCode";
                ddlSubDepartment.DataBind();
            }
        }
        private void loadddlDesignation()
        {
            string sql = @"Select DesignationID,DesignationName from Designation";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataValueField = "DesignationID";
                ddlDesignation.DataBind();
            }
        }
        private string Technician_maxID()
        {
            String sql = @"select ISNULL(Max(Convert(integer,SUBSTRING( EmpCode,5,LEN(EmpCode)-4))), 0)+1 from Personal where Cardno='2'";

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            int count = int.Parse(dt.Rows[0][0].ToString());
            string NewTechnicianID = count.ToString().PadLeft(4, '0');

            return NewTechnicianID;

        }
        public void InsertUpdateEmployee()
        {
            try
            {


                string EmpCode;
                if (String.IsNullOrEmpty(txtTechnicianId.Text))
                {
                    EmpCode = Technician_maxID();

                }
                else
                {
                    EmpCode = txtTechnicianId.Text;
                }
                string Cardno = "2";

                string EmpName = "";
                if (txtName.Text == string.Empty)
                {

                    txtName.Focus();
                }
                else
                {
                    EmpName = txtName.Text;
                }
                int ComCode = 1;
                string FatherName = txtFatherName.Text;
                string MotherName = txtMotherName.Text;
                string BloodGroup = ddlBloodGroup.SelectedValue.ToString();
                string ProdSubCatCode_5 = ddlSubDepartment.SelectedValue;
                int DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                string Add1;
                if (string.IsNullOrEmpty(txtAddress1.Text))
                {
                    Add1 = "";
                }
                else
                {
                    Add1 = txtAddress1.Text;
                }
                string Add2;
                if (string.IsNullOrEmpty(txtAddress2.Text))
                {
                    Add2 = "";
                }
                else
                {
                    Add2 = txtAddress2.Text;
                }
                string Add3;
                if (string.IsNullOrEmpty(txtAddress3.Text))
                {
                    Add3 = "";
                }
                else
                {
                    Add3 = txtAddress3.Text;
                }

                //string Add2 = txtAddress2.Text;
                //string Add3 = txtAddress3.Text;

                string Mobile = txtMobile.Text;

                int PStatus = ddlStatus.SelectedIndex;
                string DrivingLicen = null;
                string userId="";
                //PersonalGateway personalGateway = new PersonalGateway();
                int rowAffected = new PersonalTechnicianGateway().InsertUpdatePersonal(EmpCode, Cardno, EmpName, ComCode, FatherName, MotherName, BloodGroup, ProdSubCatCode_5, DesignationID, Add1, Add2, Add3, Mobile, PStatus, DrivingLicen,userId);
                if (rowAffected > 0)
                {
                    LblMsg.Text = "Saved";
                }
                //txtEmpCode.Text = EmpCode;
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;

                //MessageBox.Show(ex.ToString());
            }

        }
        private void LoadTechnician()
        {

            GridViewRow rows = gvlistofBasicData.SelectedRow;
            string code = rows.Cells[1].Text;
            string sql = @"Select EmpCode, EmpName,FatherName,MotherName,Add1,Add2,Add3,Mobile,ProdSubCatCode,DesignationID,BloodGroup,PStatus from Personal Where EmpCode=@EmpCode and Cardno='2'";
            DataTable dt;
            SqlParameter[] parameter ={
                                              new SqlParameter("@EmpCode", code),
                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtTechnicianId.Text = dt.Rows[0]["EmpCode"].ToString();
                txtName.Text = dt.Rows[0]["EmpName"].ToString();
                txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                txtMotherName.Text = dt.Rows[0]["MotherName"].ToString();
                txtAddress1.Text = dt.Rows[0]["Add1"].ToString();
                txtAddress2.Text = dt.Rows[0]["Add2"].ToString();
                txtAddress3.Text = dt.Rows[0]["Add3"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                ddlBloodGroup.SelectedValue = dt.Rows[0]["BloodGroup"].ToString();
                ddlDesignation.SelectedValue = dt.Rows[0]["DesignationID"].ToString();
                ddlSubDepartment.SelectedValue = dt.Rows[0]["ProdSubCatCode"].ToString();
                ddlStatus.SelectedValue = dt.Rows[0]["PStatus"].ToString();

            }

            else
                clearAll();
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
            if (String.IsNullOrEmpty(txtName.Text))
                return false;
            else
                return true;
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
                    loadddlSubDepartment();
                    loadddlDesignation();                    
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

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetAll(txtSearch.Text);

            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertUpdateEmployee();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetAll(txtSearch.Text);

            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();

        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTechnician();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetAll(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;

            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }
    }
}