using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using TransportManagerLibrary.UtilityClass;
using MadinaWorkshop.DLL.Gateway;
using System.IO;
namespace TransportManagerUI.UI.Workshop
{
    public partial class VehicleAdministative : System.Web.UI.Page
    {
        #region Private Method


       
       
        
        private DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ItemID"));

            dt.Columns.Add(new DataColumn("ExpDate"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("Comments"));
            


            // add the columns to the datatable            
            //if (dtg.HeaderRow != null)
            //{

            //    for (int i = 1; i < dtg.HeaderRow.Cells.Count; i++)
            //    {
            //        //if(i!=0)
            //        dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
            //    }
            //}

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                if ((row.FindControl("ck") as CheckBox).Checked)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    //for (int i = 0; i < row.Cells.Count; i++)
                    //{
                    //    if (i != 0)
                    dr["ItemID"] = row.Cells[0].Text;
                    dr["ExpDate"] = row.Cells[3].Text;
                    dr["Amount"] = row.Cells[4].Text; //(row.FindControl("txtIssuQTY") as TextBox).Text;
                    dr["Comments"] = row.Cells[5].Text; //(row.FindControl("txtPrice") as TextBox).Text;
                   
                    //}
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        private void AddtoList()
        {
            
                DataTable DTAddToList = new DataTable();



                DTAddToList.Columns.Add(new DataColumn("ItemID"));
                DTAddToList.Columns.Add(new DataColumn("ItemName"));
                DTAddToList.Columns.Add(new DataColumn("Dhara"));
                DTAddToList.Columns.Add(new DataColumn("ExpDate"));
                DTAddToList.Columns.Add(new DataColumn("Amount"));
                DTAddToList.Columns.Add(new DataColumn("Comments"));



                DataRow DRLocal = DTAddToList.NewRow();

            if (ddlWorkType.SelectedValue == "01")
            {
                
                string sql = @"Select ItemID,ItemName,Dhara from AdminItem where ItemID=@ItemId";
                DataTable dt = new DataTable();
                SqlParameter[] parameter ={
                                              new SqlParameter("@ItemId",ddlItemType.SelectedValue )
                                            };

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

                DRLocal["ItemID"] = ddlItemType.Text;
                DRLocal["ItemName"] = dt.Rows[0]["ItemName"].ToString();
                DRLocal["Dhara"] = dt.Rows[0]["Dhara"].ToString();
                DRLocal["ExpDate"] = txtExpareDate.Text;
                DRLocal["Amount"] = txtAmount.Text.Trim().ToString();
                DRLocal["Comments"] = txtComments.Text.Trim().ToString();

            }
            else
            {
                
                string sql = @"Select ItemID,ItemName from AdminItem where ItemID=@ItemId";
                DataTable dt = new DataTable();
                SqlParameter[] parameter ={
                                              new SqlParameter("@ItemId",ddlItemType.SelectedValue )
                                            };

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                

                DRLocal["ItemID"] = ddlItemType.Text;
                DRLocal["ItemName"] = dt.Rows[0][1].ToString();
                DRLocal["Dhara"] = "";
                DRLocal["ExpDate"] = txtExpareDate.Text;
                DRLocal["Amount"] = txtAmount.Text.Trim().ToString();
                DRLocal["Comments"] = txtComments.Text.Trim().ToString();
               

            }
                DTAddToList.Rows.Add(DRLocal);

                //DataTable DTLocal1 = DTAddToList.Clone();
                //for (int i = 0; i < DTAddToList.Rows.Count; i++)
                //{
                //    DTLocal1.ImportRow(DTAddToList.Rows[i]);
                //}

                DataTable DTLocal1 = DTAddToList.Clone();

                for (int k = 0; k < gvAdministration.Rows.Count; k++)
                {
                    DataRow DRLocal1 = DTLocal1.NewRow();


                    DRLocal1["ItemID"] = gvAdministration.Rows[k].Cells[0].Text;
                    DRLocal1["ItemName"] = gvAdministration.Rows[k].Cells[1].Text;
                    DRLocal1["Dhara"] = gvAdministration.Rows[k].Cells[2].Text;
                    DRLocal1["ExpDate"] = gvAdministration.Rows[k].Cells[3].Text;
                    DRLocal1["Amount"] = gvAdministration.Rows[k].Cells[4].Text;
                    DRLocal1["Comments"] = gvAdministration.Rows[k].Cells[5].Text;


                    DTLocal1.Rows.Add(DRLocal1);
                }

                for (int i = 0; i < DTAddToList.Rows.Count; i++)
                {
                    DTLocal1.ImportRow(DTAddToList.Rows[i]);
                }

                
                
                gvAdministration.DataSourceID = null;
                gvAdministration.DataBind();
                

                gvAdministration.DataSource = DTLocal1;
                
                gvAdministration.DataBind();

                txtAmount.Text = "0";
                txtExpareDate_CalendarExtender.SelectedDate = DateTime.Now.Date;

            }

        protected void gvAdministration_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string val = e.Row.Cells[2].ToString();  //check first cell value
                if (string.IsNullOrEmpty(val))
                {
                    gvAdministration.Columns[2].Visible = false;     //Hides First Column
                }

            }
        }
    
    private void saveAdministrativeTask()
        {

            try
            {
                int comcode = 1;
                string AdministrativNo_1;
                if (string.IsNullOrEmpty(txtAdminNo.Text))
                {
                    txtAdminNo.Text = Administrativ_maxID();
                    AdministrativNo_1 = txtAdminNo.Text.Trim();
                }
                else
                {
                    AdministrativNo_1 = txtAdminNo.Text.Trim();
                }

                string VehicleID_2 = ddlVehicleID.SelectedValue;
                string ModelNo_3 = txtModelNo.Text.Trim();
                string IssDate_4 = txtIssDate.Text;
                string CateID = ddlWorkType.SelectedValue;
                string EmpCode_5="";
                
                    EmpCode_5 = ddlDriverID.SelectedValue;
                
                int IssuStatus_6 = 0;
                string AttendDate = "";

                string SergeantName;
                
                    SergeantName = txtSergeantName.Text;

               
                string Area;
                
                    Area = txtArea.Text;

                
                string Remarks_7 = txtRemarks.Text.Trim();
                string userId = "";
                DataTable dt = new DataTable();
                dt = GetDataTable(gvAdministration);
                if (dt.Rows.Count > 0)
                {
                    
                    string roweffected = (new AdministrativWorkGateway().InsertAdministrativWork(comcode, AdministrativNo_1, VehicleID_2, ModelNo_3, IssDate_4, CateID, EmpCode_5, IssuStatus_6, AttendDate, SergeantName, Area, Remarks_7,userId,dt));
                    txtAdminNo.Text = roweffected;
                    lblMsg2.Text = "Saved";
                }
                else
                    lblMsg2.Text = "Cannot Save";

               
            }
            catch (Exception ex)
            {
                this.lblMsg2.Text = (ex.Message);
            }


        }
        private string Administrativ_maxID()
        {
            string CurrentYear = DateTime.Today.ToString("yyyy");
            String sql = @"select ISNULL(Max(Convert(integer,Right(AdministrativNo,6))), 0) + 1 from AdministrativWork where ComCode='1'and year(IssDate)=" + CurrentYear + "";

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            int count = int.Parse(dt.Rows[0][0].ToString());
            string NewAdministrativNo = count.ToString().PadLeft(6, '0');

            string AdministrativNo = "AD" + "-" + CurrentYear + NewAdministrativNo;

            return AdministrativNo;
        }
        private void ckSelectAllItem()
        {
            CheckBox cb = (CheckBox)gvAdministration.HeaderRow.FindControl("ckAll");
            
            decimal amount = 0;
            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvAdministration.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvAdministration.Rows[i].FindControl("ck");
                        chkboxSelect.Checked = true;
                        amount = amount + Convert.ToDecimal(gvAdministration.Rows[i].Cells[4].Text);

                    }
                }
                else
                {
                    for (int i = 0; i < gvAdministration.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvAdministration.Rows[i].FindControl("ck");
                        chkboxUnselect.Checked = false;
                        amount = 0;

                    }
                }
              
            }
            txtTotalAMT.Text = amount.ToString("0.00");
        }
        private DataTable loadBasicData(string searchKey)
        {
            try
            {
                string sql = @"SELECT        AdministrativWork.ComCode, AdministrativWork.AdministrativNo, AdministrativWork.VehicleID, AdministrativWork.ModelNo, CONVERT(VARCHAR(10), AdministrativWork.IssDate, 103) as IssDate, AdministrativWork.CateID, AdministrativWork.EmpCode, 
                         AdministrativWork.AttendDate, AdministrativWork.SergeantName, AdministrativWork.Area, AdministrativWork.Discount, AdministrativWork.Status, AdministrativWork.Remarks, VehicleInfo.VehicleNo, AdminItemCate.CateName, 
                         Personal.EmpName
FROM            AdministrativWork INNER JOIN
                         AdminItemCate ON AdministrativWork.CateID = AdminItemCate.CateID LEFT OUTER JOIN
                         Personal ON AdministrativWork.EmpCode = Personal.EmpCode LEFT OUTER JOIN
                         VehicleInfo ON AdministrativWork.VehicleID = VehicleInfo.VehicleID order by AdministrativWork.IssDate desc,AdministrativWork.AdministrativNo desc ";
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
                if (String.IsNullOrEmpty(searchKey))
                {


                }
                else
                {
                    var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("AdministrativNo").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("IssDate").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();
                }
                return dt;

            }

            catch (Exception ex)
            {

                return null;

            }
        }
        private void CateCodeOption()
        {
            string cateID = ddlWorkType.SelectedValue;
            if (cateID == "01")
            {
               
                lblItemId.Text = "Dhara:";
                lblDateType.Text = "Widraw Date:";

                
                ddlDriverID.Enabled = true;
                txtArea.Enabled = true;
                txtSergeantName.Enabled = true;

            }
            else if (cateID == "02")
            {
                
                lblItemId.Text = "Paper Renewal Type:";
                lblDateType.Text = "Expire Date:";
                ddlDriverID.SelectedValue = "";
                ddlDriverID.Enabled = false;
                txtArea.Text = String.Empty;
                txtSergeantName.Text = String.Empty;
                txtArea.Enabled = false;
                txtSergeantName.Enabled = false;

            }
            else if (cateID == "03")
            {
               
                lblItemId.Text = "Accident";
                lblDateType.Text = "Accident Date:";
               
                ddlDriverID.Enabled = true;
                             
                txtArea.Enabled = true;
                txtSergeantName.Text = String.Empty;
                txtSergeantName.Enabled = false;

            }
            else
            {
                
                lblItemId.Text = "Others";
                lblDateType.Text = "Event Date:";
               
                ddlDriverID.Enabled = true;
            }
            loadAdminProduct();
        }
        private void loadBasicDataByID()
        {
            try
            {
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string code = rows.Cells[1].Text;
                string sql = @"select AW.ComCode,AW.AdministrativNo,AW.IssDate AS IssDate ,AW.CateID,
                AW.EmpCode,AW.VehicleID,AW.ModelNo,AW.Status,AW.Remarks,AWD.ItemID,convert(varchar(10),AWD.ExpDate,103)AS ExpDate,
                CAST((AWD.Amount)AS DECIMAL(18,2))as Amount,AWD.Comments,AI.ItemName,AI.Dhara,CONVERT(varchar(10),AW.AttendDate,103)as AttendDate,
                AW.SergeantName,AW.Area  
                        from AdministrativWork AW inner join AdministrativWorkDTL AWD  on AW.AdministrativNo=AWD.AdministrativNo and AW.ComCode=AW.ComCode
                           inner join AdminItem AI on AWD.ItemID=AI.ItemID 
                           where AW.AdministrativNo=@AdministrativNo";

                DataTable dt2 = new DataTable();
                SqlParameter[] parameter ={
                                              new SqlParameter("@AdministrativNo",code )
                                            };

                dt2 = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    ddlWorkType.SelectedValue= dt2.Rows[0]["CateID"].ToString();
                    txtAdminNo.Text = dt2.Rows[0]["AdministrativNo"].ToString();
                    txtIssDate0_CalendarExtender.SelectedDate = Convert.ToDateTime(dt2.Rows[0]["IssDate"].ToString());
                    
                    //txtIssDate.Text = dt2.Rows[0]["IssDate"].ToString();
                    txtModelNo.Text = dt2.Rows[0]["ModelNo"].ToString();

                    if (String.IsNullOrEmpty(dt2.Rows[0]["EmpCode"].ToString()))
                    {
                        ddlDriverID.SelectedValue = "";
                    }
                    else
                        ddlDriverID.SelectedValue = dt2.Rows[0]["EmpCode"].ToString();


                    if (String.IsNullOrEmpty(dt2.Rows[0]["VehicleID"].ToString()))
                    {
                        ddlVehicleID.SelectedValue = "";
                    }
                    else
                        ddlVehicleID.SelectedValue = dt2.Rows[0]["VehicleID"].ToString();

                    txtAttendDate.Text = dt2.Rows[0]["AttendDate"].ToString();
                    txtSergeantName.Text = dt2.Rows[0]["SergeantName"].ToString();
                    txtArea.Text = dt2.Rows[0]["Area"].ToString();
                    txtRemarks.Text = dt2.Rows[0]["Remarks"].ToString();
                    
                    CateCodeOption();

                    gvAdministration.DataSource = null;
                    gvAdministration.DataBind();
                    gvAdministration.DataSource = dt2;
                    gvAdministration.DataBind();
                    CheckBox cb = (CheckBox)gvAdministration.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();

                }
                else
                {
                    lblMsg2.Text = "Data not Found.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private void loadDDLDriver()
        {


            string sql = @"Select EmpCode,EmpName from Personal where Cardno=1"; //AND EmpCode like'%"+txtSearchDriver.Text.Trim()+"'";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlDriverID.DataSource = dt;
                ddlDriverID.DataTextField = "EmpName";
                ddlDriverID.DataValueField = "EmpCode";
                ddlDriverID.DataBind();
                ListItem df = new ListItem();
                df.Value = "";
                df.Text = "None";
                ddlDriverID.Items.Add(df);
            }

        }
       
        private void loadDDLVehicle()
        {
           

            string sql = @"Select VehicleID,VehicleNo from dbo.VehicleInfo"; 
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlVehicleID.DataSource = dt;
                ddlVehicleID.DataTextField = "VehicleNo";
                ddlVehicleID.DataValueField = "VehicleID";
                ddlVehicleID.DataBind();
                ListItem df = new ListItem();
                df.Value = "";
                df.Text = "None";
                ddlVehicleID.Items.Add(df);


            }
           
          
        }
        private void loadAdminProduct()
        {

            string sql = @"SELECT ItemID, CateID, ItemName, Dhara, Amount FROM            AdminItem where CateID=@CateId";
           SqlParameter[] parameter ={
                                              new SqlParameter("@CateId", ddlWorkType.SelectedValue)
                                            };
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

            if (dt.Rows.Count > 0)
            {
                ddlItemType.DataSource = null;
                ddlItemType.DataBind();


                ddlItemType.DataSource = dt;
                if (ddlWorkType.SelectedValue == "01")
                {
                    ddlItemType.DataTextField = "Dhara";
                }
                else
                    ddlItemType.DataTextField = "ItemName";
                ddlItemType.DataValueField = "ItemID";

                ddlItemType.DataBind();
            }


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
            CateCodeOption();

        }
        private bool isMandatoryFieldValidate()
        {
            //if (String.IsNullOrEmpty(.Text))
            //    return false;
            //else
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

                    CateCodeOption();
                    loadDDLVehicle();
                    loadDDLDriver();
                    loadAdminProduct();

                    txtIssDate0_CalendarExtender.SelectedDate = DateTime.Now.Date;
                    txtExpareDate_CalendarExtender.SelectedDate = DateTime.Now.Date;

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
            dt = loadBasicData(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveAdministrativeTask();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (ddlWorkType.SelectedValue=="01" && String.IsNullOrEmpty(txtAdminNo.Text)==false)
            {
                string RepotrName = "AdministrativWorkCase";
                Response.Redirect("reportViewerWorkshop.aspx?RID=" + txtAdminNo.Text + "&RN=" + RepotrName);

            }
            else if (ddlWorkType.SelectedValue == "02" && String.IsNullOrEmpty(txtAdminNo.Text) == false)
            {
                string RepotrName = "AdministrativWorkPaper";
                Response.Redirect("reportViewerWorkshop.aspx?RID=" + txtAdminNo.Text + "&RN=" + RepotrName);

            }
            else if (ddlWorkType.SelectedValue == "03" || ddlWorkType.SelectedValue == "04" && String.IsNullOrEmpty(txtAdminNo.Text) == false)
            {
                string RepotrName = "AdministrativWork";
                Response.Redirect("reportViewerWorkshop.aspx?RID=" + txtAdminNo.Text + "&RN=" + RepotrName);
            }
            else
            {
                lblMsg2.Text = "Please Search Administrative No.";
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadBasicData(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBasicDataByID();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadBasicData(txtSearch.Text);
            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }


        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            AddtoList();
        }

       

        protected void ck_CheckedChanged(object sender, EventArgs e)
        {
            decimal Qty = 0;
            decimal amount = gvAdministration.Rows.Cast<GridViewRow>()
                .Where(row => (row.FindControl("ck") as CheckBox).Checked)
                .Aggregate<GridViewRow, decimal>(0, (current, row) => current + Convert.ToDecimal(row.Cells[4].Text));

            txtTotalAMT.Text = amount.ToString("0.00");
        }

        

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            loadAdminProduct();
        }

        protected void ddlSubDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAdminProduct();
        }
            

        protected void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            ckSelectAllItem();
            //totalAMTcount();
            //totalQtycount();

        }

        protected void ddlWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CateCodeOption();
            loadAdminProduct();
        }

        protected void btnAddtoList_Click(object sender, EventArgs e)
        {
            AddtoList();
        }

        
    }
}