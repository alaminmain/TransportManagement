using MadinaWorkshop.DLL.Gateway;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI.Workshop
{
    public partial class SPRApproved1 : System.Web.UI.Page
    {
        #region Private Methode
        private bool check_CreateDTL()     //***Check Create function*************
        {

            if (ddlProductCode.Text.Length == 0)
            {
                lblMessage.Text = ("Invalid Product.");


                return false;
            }
            else if (txtReqQTY.Text.Length == 0)
            {

                lblMessage.Text = ("Invalid Present Requisition.");
                txtReqQTY.Focus();
                return false;
            }
            else if (txtReqDay.Text.Length == 0)
            {
                lblMessage.Text = ("Invalid Require Day.");
                txtReqDay.Focus();
                return false;
            }
            else if (txtReqPrice.Text.Length == 0)
            {
                lblMessage.Text = ("Invalid Estimated Price.");
                txtReqPrice.Focus();
                return false;
            }
            else if (txtPerMonthUse.Text.Length == 0)
            {
                lblMessage.Text = ("Invalid PerMonth Used");
                txtPerMonthUse.Focus();
                return false;
            }
            else if (txtComments.Text.Length == 0)
            {
                lblMessage.Text = ("Invalid Remarks");
                txtComments.Focus();
                return false;
            }
            else

                return true;
        }

        private void ClearTheForm()
        {
            txtReqQTY.Text = "";
            txtReqPrice.Text = "";
            txtReqDay.Text = "";
            txtComments.Text = "";
            txtPerMonthUse.Text = "";
        }

        private void loadDDLProduct()
        {

            lblMessage.Text = "";
            string sql = @"Select ProductCode,ProductName from Item where ProdSubCatCode=@ProdSubCatCode and CateCode=@CateCode and  ProductCode<>'00000'";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={

                                              new SqlParameter("@ProdSubCatCode", ddlSubDepartment.SelectedValue),
                                               new SqlParameter("@CateCode", ddlDepartment.SelectedValue )
                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

            // dt = dc.SelectDs(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlProductCode.DataSource = dt;
                ddlProductCode.DataTextField = "ProductName";
                ddlProductCode.DataValueField = "ProductCode";
                ddlProductCode.DataBind();
            }



        }
        private void loadSubProduct()
        {
            string sql = @"select ProdSubCatCode,ProdSubCatName from ItemSubCategory where CateCode=@CateCode";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={
                                              new SqlParameter("@CateCode", ddlDepartment.SelectedValue )
                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
            // dt = dc.SelectDs(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlSubDepartment.DataSource = dt;
                ddlSubDepartment.DataTextField = "ProdSubCatName";
                ddlSubDepartment.DataValueField = "ProdSubCatCode";
                ddlSubDepartment.DataBind();
            }



        }
        private void loadddlDepartment()
        {
            string sql = @"select CateCode,CateName from Item_Category";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "CateName";
                ddlDepartment.DataValueField = "CateCode";
                ddlDepartment.DataBind();
            }



        }
        private DataTable GetDataTable(GridView dtg)
        {
            DataTable DTAddToList = new DataTable();

            DTAddToList.Columns.Add(new DataColumn("ProductName"));
            DTAddToList.Columns.Add(new DataColumn("ProductCode"));
            DTAddToList.Columns.Add(new DataColumn("ProductBName"));
            DTAddToList.Columns.Add(new DataColumn("StoreCode"));
            DTAddToList.Columns.Add(new DataColumn("PhyStock"));
            DTAddToList.Columns.Add(new DataColumn("UnitType"));
            DTAddToList.Columns.Add(new DataColumn("ReqQty"));
            DTAddToList.Columns.Add(new DataColumn("RequireDay"));
            DTAddToList.Columns.Add(new DataColumn("ReqPrice"));
            DTAddToList.Columns.Add(new DataColumn("Comments"));
            DTAddToList.Columns.Add(new DataColumn("TotalAMT"));
            DTAddToList.Columns.Add(new DataColumn("UsedPerMonth"));


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
                    DataRow DRLocal;
                    DRLocal = DTAddToList.NewRow();




                    DRLocal["ProductCode"] = row.Cells[0].Text.ToString();
                    DRLocal["ProductName"] = row.Cells[1].Text.ToString();

                    DRLocal["ProductBName"] = row.Cells[2].Text.ToString();
                    DRLocal["StoreCode"] = row.Cells[3].Text.ToString();
                    DRLocal["PhyStock"] = row.Cells[4].Text.ToString();
                    DRLocal["UnitType"] = row.Cells[5].Text.ToString();
                    DRLocal["ReqQty"] = (row.FindControl("txtPresentReq") as TextBox).Text;
                    DRLocal["RequireDay"] = (row.FindControl("txtReqDay") as TextBox).Text;
                    DRLocal["ReqPrice"] = (row.FindControl("txtEstmitedValue") as TextBox).Text;
                    DRLocal["Comments"] = (row.FindControl("txtRemarks") as TextBox).Text;
                    DRLocal["TotalAMT"] = (row.FindControl("txtTotalValue") as TextBox).Text;
                    DRLocal["UsedPerMonth"] = (row.FindControl("txtPerMonthUse") as TextBox).Text; ;

                    DTAddToList.Rows.Add(DRLocal);
                }

            }

            return DTAddToList;
        }
        private DataTable LoadBasicGrid(string searchKey)
        {
            try
            {
                DataTable dt;


                string sql = @"SELECT     PurRequisition.ComCode, PurRequisition.ReqNo, PurRequisition.CateCode, PurRequisition.ProdSubCatCode, CONVERT(VARCHAR(10), PurRequisition.ReqDate, 103) AS ReqDate, 
                      PurRequisition.ImergencRreqStatus, PurRequisition.Status, PurRequisition.Remark, Item_Category.CateName, ItemSubCategory.ProdSubCatName
                      FROM         PurRequisition INNER JOIN Item_Category ON PurRequisition.CateCode = Item_Category.CateCode INNER JOIN
                      ItemSubCategory ON PurRequisition.CateCode = ItemSubCategory.CateCode AND PurRequisition.ProdSubCatCode = ItemSubCategory.ProdSubCatCode and(PurRequisition.Status='0' or PurRequisition.Status='1') 
                        order by PurRequisition.ReqDate desc,PurRequisition.ReqNo desc";

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];


                if (String.IsNullOrEmpty(searchKey))
                {


                }
                else
                {
                    var filtered = dt.AsEnumerable()
.Where(r => r.Field<String>("ReqNo").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("Remark").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();
                }
                return dt;

            }

            catch (Exception ex)
            {

                return null;

            }
        }


        private void LoadDetailGrid()
        {

            string sql1 = @"SELECT     IT.CateCode, IT.ProdSubCatCode, IT.ProductCode, IT.ProductName, IT.ProductBName, abs(ITS.PhyStock-IT.Reorder)as ReqQTY, IT.Reorder, IT.ProductDiscription, IT.DealPrice, IT.Discontinued, 
                      IT.DistPrice AS ReqPrice, IT.OperPrice, IT.UnitType, '' AS RequireDay, CAST(abs(ITS.PhyStock-IT.Reorder) * IT.DistPrice AS DECIMAL(18, 2)) AS TotalAMT, '' AS UsedPerMonth, 
                      ITS.StoreCode, ITS.PhyStock, ''as Comments
FROM         Item AS IT INNER JOIN
                      ItemStock AS ITS ON IT.ProductCode = ITS.ProductCode AND IT.StoreCode = ITS.StoreCode AND IT.Reorder > ITS.PhyStock + ITS.ReqQTY
WHERE     (IT.Discontinued = 'false') and it.ProdSubCatCode=@ProdSubCatCode and it.CateCode=@CateCode";
            DataTable dt1 = new DataTable();
            SqlParameter[] parameter1 ={

                                              new SqlParameter("@ProdSubCatCode", ddlSubDepartment.SelectedValue),
                                               new SqlParameter("@CateCode", ddlDepartment.SelectedValue )
                                            };
            dt1 = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql1, parameter1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lblMessage.Text = "";
                gvPurRequisition.DataSource = dt1;
                gvPurRequisition.DataBind();

                CheckBox cb = (CheckBox)gvPurRequisition.HeaderRow.FindControl("ckAll");
                cb.Checked = true;
                ckSelectAllItem();
            }
            else
            {
                gvPurRequisition.DataSourceID = null;
                gvPurRequisition.DataSource = dt1;
                gvPurRequisition.DataBind();

                lblMessage.Text = "Data Not Found.";
            }


        }

        private void loadSPRForm()
        {
            try
            {
                GridViewRow row = gvlistofBasicData.SelectedRow;
                string reqNo = row.Cells[1].Text.ToString();

                string sql = @"SELECT     PR.ComCode, PR.ReqNo, PR.ReqDate AS ReqDate, PR.Remark, PR.CateCode, IT.ProdSubCatCode, PRD.ReqSLNo, PRD.ProductCode, 
                      PRD.ReqQty, CAST(PRD.ReqPrice AS DECIMAL(18, 2)) AS ReqPrice, PRD.RequireDay, PRD.PhyStock, PRD.Comments, IT.ProductCode AS Expr1, IT.ProductName, 
                      IT.ProductBName, IT.UnitType, IST.PhyStock AS Expr2, IST.StoreCode, CAST(PRD.ReqQty * PRD.ReqPrice AS DECIMAL(18, 2)) AS TotalAMT, PRD.UsedPerMonth, 
                      PR.ImergencRreqStatus
FROM         Item AS IT INNER JOIN       PurRequisition AS PR INNER JOIN
                      PurRequisitionDTL AS PRD ON PR.ReqNo = PRD.ReqNo INNER JOIN
                      ItemStock AS IST ON PRD.StoreCode = IST.StoreCode ON IT.StoreCode = PRD.StoreCode
                            where PR.ReqNo=@ReqNo order by(PRD.ReqSLNo)asc";
                DataTable dt = new DataTable();

                SqlParameter[] parameter ={   new SqlParameter("@ReqNo", reqNo ),
                                            
                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    lblMessage.Text = "";

                    txtReqNo.Text = dt.Rows[0]["ReqNo"].ToString();
                    CalendarExtender2.SelectedDate = Convert.ToDateTime(dt.Rows[0]["ReqDate"].ToString()).Date;

                    txtRemark.Text = dt.Rows[0]["Remark"].ToString().Trim();
                    //txtDeptName.Text = dt.Rows[0][5].ToString().Trim();

                    //DateTime MyDate = Convert.ToDateTime(dt.Rows[0][2].ToString().Trim());
                    ddlDepartment.SelectedValue = dt.Rows[0]["CateCode"].ToString();
                    ddlSubDepartment.SelectedValue = dt.Rows[0]["ProdSubCatCode"].ToString();

                    int ImergencyREQ = Convert.ToInt32(dt.Rows[0]["ImergencRreqStatus"].ToString());
                    

                    gvPurRequisition.DataSource = dt;
                    gvPurRequisition.DataBind();
                    CheckBox cb = (CheckBox)gvPurRequisition.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SavePurRequisition()
        {
            int ComCode = 1;
            string ReqNo_1;
            if (string.IsNullOrEmpty(txtReqNo.Text.Trim()))
            {
                this.txtReqNo.Text = Requisition_maxID();
                ReqNo_1 = txtReqNo.Text.ToString();
            }
            else
            {
                ReqNo_1 = txtReqNo.Text.ToString();
            }

            string ReqDate_2 = txtReqDate.Text.Trim();
            int emergencRreqStatus = 0;
            //if (ckEmergencyReq.Checked)
            //    emergencRreqStatus = 0;
            //else
            //    emergencRreqStatus = 1;
            string CateCode = ddlDepartment.SelectedValue;
            string ProdSubCatCode = ddlSubDepartment.SelectedValue;

            string Remark_3 = txtRemark.Text.Trim();
            int status_4 =1;

            string userId = "";
            DataTable detail = new DataTable();
            detail = GetDataTable(gvPurRequisition);

            //string ReqSubmitedDept="'"+ddlDepartment.Text.ToString()+"'"+" / "+"'"+ddlSubDepartment.Text.ToString()+"'";
            int roweffected = (new PurRequisitionGateway().InsertUpdatePurRequisition(ComCode, ReqNo_1, ReqDate_2, emergencRreqStatus, CateCode, ProdSubCatCode, Remark_3, status_4));
            deletePurRequisitionDTL();
            SavePurRequisitionDTLStock();

        }
        public void deletePurRequisitionDTL()
        {

            int ComCode_1 = 1;
            string ReqNo_2 = txtReqNo.Text;
            int rowEffected = new PurRequisitionDTL().deletePurRequisitionDTL(ComCode_1, ReqNo_2);
        }
        public void SavePurRequisitionDTLStock()
        {
            try
            {
                if (String.IsNullOrEmpty(txtReqNo.Text))
                {

                }
                else if (gvPurRequisition.Rows.Count < 0)
                {

                }
                else
                {
                    int rowEffected = 0;
                    int ReqSLNo_2 = 0;

                    decimal ReqQty_4 = 0;
                    string ReqQty;
                    decimal ReqPrice_5 = 0;
                    string ReqPrice;
                    string PhyStock;
                    decimal PhyStock_6 = 0;

                    string UsedPerMonth;
                    decimal UsedPerMonth_7 = 0;
                    int RequireDay_8 = 0;
                    string RequireDay;
                    string Comments_9;

                    for (int i = 0; i < gvPurRequisition.Rows.Count; i++)
                    {
                        CheckBox isChecked = ((CheckBox)gvPurRequisition.Rows[i].FindControl("ck"));

                        if (isChecked.Checked)
                        {

                            int comcode = 1;
                            string ReqNo_1 = txtReqNo.Text.Trim();

                            ReqSLNo_2 = (gvPurRequisition.Rows.Count) + i + 1;
                            string ProductCode_3 = gvPurRequisition.Rows[i].Cells[0].Text.ToString().Trim();
                            string StoreCode = gvPurRequisition.Rows[i].Cells[3].Text.ToString().Trim();
                            ReqQty = ((TextBox)(gvPurRequisition.Rows[i].FindControl("txtPresentReq"))).Text;
                            //gvPurApproved.Rows[i].Cells[2].Text.ToString().Trim();
                            ReqQty_4 = Convert.ToDecimal(ReqQty);
                            ReqPrice = ((TextBox)(gvPurRequisition.Rows[i].FindControl("txtEstmitedValue"))).Text;
                            ReqPrice_5 = Convert.ToDecimal(ReqPrice);
                            PhyStock = gvPurRequisition.Rows[i].Cells[4].Text.ToString().Trim();
                            PhyStock_6 = Convert.ToDecimal(PhyStock);
                            UsedPerMonth = ((TextBox)(gvPurRequisition.Rows[i].FindControl("txtPerMonthUse"))).Text;
                            UsedPerMonth_7 = Convert.ToDecimal(UsedPerMonth);
                            RequireDay = ((TextBox)(gvPurRequisition.Rows[i].FindControl("txtReqDay"))).Text;
                            RequireDay_8 = Convert.ToInt32(RequireDay);
                            Comments_9 = ((TextBox)(gvPurRequisition.Rows[i].FindControl("txtRemarks"))).Text;

                            int Status = 0;


                            rowEffected = new PurRequisitionDTL().InsertUpdatePurRequisitionDTL(comcode, ReqNo_1, ReqSLNo_2, ProductCode_3, StoreCode, ReqQty_4, ReqPrice_5, PhyStock_6, UsedPerMonth_7, RequireDay_8, Comments_9, Status);

                            rowEffected = rowEffected + 1;


                        }
                        if (rowEffected > 0)
                        {
                            lblMessage.Text = "Successfully Inserted Data";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                this.lblMessage.Text = (ex.Message);
            }
        }

      

        
        private string Requisition_maxID()
        {
            string CurrentYear = DateTime.Today.ToString("yyyy");
            String sql = @"select ISNULL(Max(Convert(integer,Right(ReqNo,6))), 0) + 1 from PurRequisition where ComCode='1'and year(ReqDate)=" + CurrentYear.ToString() + "";

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            int count = int.Parse(dt.Rows[0][0].ToString());
            string NewReqNo = count.ToString().PadLeft(6, '0');

            string ReqNo = "SPR" + CurrentYear + NewReqNo;
            return ReqNo;

        }




        private void clearForm()
        {
            txtReqNo.Text = "";
            txtRemark.Text = "";
            
           
            gvPurRequisition.DataSource = null;
            gvPurRequisition.DataBind();
        }
        private bool check_Create()     //***Check Create function*************
        {

            if (ddlDepartment.SelectedIndex < 0)
            {
                lblMessage.Text = ("please select Department.");
                ddlDepartment.Focus();

                return false;
            }
            else if (ddlSubDepartment.SelectedIndex < 0)
            {
                lblMessage.Text = ("please select SubDepartment.");
                ddlSubDepartment.Focus();

                return false;
            }
            else if (txtRemark.Text.Length == 0)
            {
                lblMessage.Text = ("Invalid Remarks.");
                txtRemark.Focus();

                return false;
            }

            else
                return true;
        }

        private bool check_Grid()     //***Check Create function*************
        {
            int count = 0;
            foreach (GridViewRow gvrow in gvPurRequisition.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("ck");

                if (chk != null && chk.Checked)
                {
                    count = count + 1;
                }
            }
            foreach (GridViewRow gvrow in gvPurRequisition.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("ck");

                if (chk != null && chk.Checked)
                {
                    if (((TextBox)(gvrow.FindControl("txtPresentReq"))).Text.Length == 0)
                    {

                        lblMessage.Text = "Invalid Present Requisition.";
                        return false;

                    }
                    else if (((TextBox)(gvrow.FindControl("txtEstmitedValue"))).Text.Length == 0)
                    {

                        lblMessage.Text = "Invalid Estmited Value.";
                        return false;

                    }
                    else if (((TextBox)(gvrow.FindControl("txtPerMonthUse"))).Text.Length == 0)
                    {

                        lblMessage.Text = "Invalid Per Month Use.";
                        return false;

                    }
                    else if (((TextBox)(gvrow.FindControl("txtReqDay"))).Text.Length == 0)
                    {

                        lblMessage.Text = "Invalid Require Day.";
                        return false;

                    }
                    else if (((TextBox)(gvrow.FindControl("txtRemarks"))).Text.Length == 0)
                    {

                        lblMessage.Text = "Invalid Remarks.";
                        return false;

                    }
                }
            }

            if (count <= 0)
            {
                lblMessage.Text = ("Please select at last one Record.");
                return false;
            }
            else

                return true;
        }
        private void ckSelectAllItem()
        {
            CheckBox cb = (CheckBox)gvPurRequisition.HeaderRow.FindControl("ckAll");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvPurRequisition.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvPurRequisition.Rows[i].FindControl("ck");
                        chkboxSelect.Checked = true;

                    }
                }
                else
                {
                    for (int i = 0; i < gvPurRequisition.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvPurRequisition.Rows[i].FindControl("ck");
                        chkboxUnselect.Checked = false;

                    }
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

                    loadddlDepartment();
                    loadSubProduct();
                    loadDDLProduct();
                    btnAddtoList.Enabled = true;
                    txtReqDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

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
            clearForm();
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadBasicGrid(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (check_Create())
            {
                SavePurRequisition();
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string RepotrName = "Requisition";
            Response.Redirect("reportViewerWorkshop.aspx?RID=" + txtReqNo.Text.Trim() + "&RN=" + RepotrName);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadBasicGrid(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSPRForm();
            ckSelectAllItem();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadBasicGrid(txtSearch.Text);
            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }


        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.LoadGrid();
            loadSubProduct();
            this.loadDDLProduct();

        }

        protected void ckStorePurchase_CheckedChanged(object sender, EventArgs e)
        {
            LoadDetailGrid();
        }



        protected void btnAddtoList_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable DTAddToList = new DataTable();


                DTAddToList.Columns.Add(new DataColumn("ProductName"));
                DTAddToList.Columns.Add(new DataColumn("ProductCode"));
                DTAddToList.Columns.Add(new DataColumn("ProductBName"));
                DTAddToList.Columns.Add(new DataColumn("StoreCode"));
                DTAddToList.Columns.Add(new DataColumn("PhyStock"));
                DTAddToList.Columns.Add(new DataColumn("UnitType"));
                DTAddToList.Columns.Add(new DataColumn("ReqQty"));
                DTAddToList.Columns.Add(new DataColumn("RequireDay"));
                DTAddToList.Columns.Add(new DataColumn("ReqPrice"));
                DTAddToList.Columns.Add(new DataColumn("Comments"));
                DTAddToList.Columns.Add(new DataColumn("TotalAMT"));
                DTAddToList.Columns.Add(new DataColumn("UsedPerMonth"));

                DataRow DRLocal = DTAddToList.NewRow();

                string sql = @"select IT.ProductName,IT.ProductCode,IT.ProductBName,ITS.StoreCode,ITS.PhyStock,IT.UnitType from Item IT inner join ItemStock ITS on IT.ProductCode=ITS.ProductCode and IT.StoreCode=ITS.StoreCode
                            where IT.ProductCode=@ProductCode and it.ProdSubCatCode=@ProdSubCatCode and it.CateCode=@CateCode";
                DataTable dt = new DataTable();

                SqlParameter[] parameter ={
                                              new SqlParameter("@ProductCode", ddlProductCode.SelectedValue),
                                              new SqlParameter("@ProdSubCatCode", ddlSubDepartment.SelectedValue),
                                               new SqlParameter("@CateCode", ddlDepartment.SelectedValue )
                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                //dt = dc.SelectDs(sql).Tables[0];

                decimal ReqQTY = Convert.ToDecimal(txtReqQTY.Text.Trim().ToString());
                decimal ReqPrice = Convert.ToDecimal(txtReqPrice.Text.Trim().ToString());
                decimal total = ReqQTY * ReqPrice;

                DRLocal["ProductCode"] = dt.Rows[0][1].ToString().Trim();
                DRLocal["ProductName"] = dt.Rows[0][0].ToString().Trim();

                DRLocal["ProductBName"] = dt.Rows[0][2].ToString().Trim();
                DRLocal["StoreCode"] = dt.Rows[0][3].ToString().Trim();
                DRLocal["PhyStock"] = dt.Rows[0][4].ToString().Trim();
                DRLocal["UnitType"] = dt.Rows[0][5].ToString().Trim();
                DRLocal["ReqQty"] = txtReqQTY.Text.Trim().ToString();
                DRLocal["RequireDay"] = txtReqDay.Text.Trim().ToString();
                DRLocal["ReqPrice"] = txtReqPrice.Text.Trim().ToString();
                DRLocal["Comments"] = txtComments.Text.Trim().ToString();
                DRLocal["TotalAMT"] = total.ToString();
                DRLocal["UsedPerMonth"] = txtPerMonthUse.Text.Trim().ToString();

                DTAddToList.Rows.Add(DRLocal);



                DataTable DTLocal1 = DTAddToList.Clone();
                if (gvPurRequisition.Rows.Count > 0)
                {
                    for (int k = 0; k < gvPurRequisition.Rows.Count; k++)
                    {
                        DataRow DRLocal1 = DTLocal1.NewRow();

                        DRLocal1["ProductCode"] = gvPurRequisition.Rows[k].Cells[0].Text;
                        DRLocal1["ProductName"] = gvPurRequisition.Rows[k].Cells[1].Text;

                        DRLocal1["ProductBName"] = gvPurRequisition.Rows[k].Cells[2].Text;
                        DRLocal1["StoreCode"] = gvPurRequisition.Rows[k].Cells[3].Text;
                        DRLocal1["PhyStock"] = gvPurRequisition.Rows[k].Cells[4].Text;
                        DRLocal1["UnitType"] = gvPurRequisition.Rows[k].Cells[5].Text;
                        DRLocal1["ReqQty"] = ((TextBox)(gvPurRequisition.Rows[k].Cells[6].FindControl("txtPresentReq"))).Text;
                        DRLocal1["RequireDay"] = ((TextBox)(gvPurRequisition.Rows[k].Cells[7].FindControl("txtReqDay"))).Text;
                        DRLocal1["ReqPrice"] = ((TextBox)(gvPurRequisition.Rows[k].Cells[8].FindControl("txtEstmitedValue"))).Text;
                        DRLocal1["Comments"] = ((TextBox)(gvPurRequisition.Rows[k].Cells[9].FindControl("txtRemarks"))).Text;
                        DRLocal1["TotalAMT"] = ((TextBox)(gvPurRequisition.Rows[k].Cells[10].FindControl("txtTotalValue"))).Text;
                        DRLocal1["UsedPerMonth"] = ((TextBox)(gvPurRequisition.Rows[k].Cells[11].FindControl("txtPerMonthUse"))).Text;


                        DTLocal1.Rows.Add(DRLocal1);


                    }
                }
                for (int i = 0; i < DTAddToList.Rows.Count; i++)
                {
                    DTLocal1.ImportRow(DTAddToList.Rows[i]);
                }


                gvPurRequisition.DataSource = DTLocal1;
                gvPurRequisition.DataBind();
                ClearTheForm();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlSubDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDDLProduct();
        }

        protected void txtEstmitedValue_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;

            decimal ReqQty = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtPresentReq"))).Text);
            decimal ReqPrice = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtEstmitedValue"))).Text);


            TextBox txtTotalValue = ((TextBox)gvRow.FindControl("txtTotalValue"));


            try
            {
                txtTotalValue.Text = ((ReqQty * ReqPrice).ToString());


            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ckALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)gvPurRequisition.HeaderRow.FindControl("ckAll");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvPurRequisition.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvPurRequisition.Rows[i].FindControl("ck");
                        chkboxSelect.Checked = true;

                    }
                }
                else
                {
                    for (int i = 0; i < gvPurRequisition.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvPurRequisition.Rows[i].FindControl("ck");
                        chkboxUnselect.Checked = false;

                    }
                }
            }
        }

        protected void btnReorderLevel_Click(object sender, EventArgs e)
        {
            LoadDetailGrid();
        }
    }
}