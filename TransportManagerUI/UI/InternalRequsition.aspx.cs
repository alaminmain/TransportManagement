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
    public partial class InternalRequsition : System.Web.UI.Page
    {
        #region Private Method


        private bool check_Grid()     //***Check Create function*************
        {
            int count = gvEditVMF.Rows.Count;



            if (count <= 0)
            {
                lblMsg.Text = ("Please select at last one Record.");
                return false;
            }
            else

                return true;
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
        private void Load_ddlProdSubCategory()
        {
            string sql = @"select CateCode,ProdSubCatCode,ProdSubCatName from ItemSubCategory where CateCode=@CateCode";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={
                                              new SqlParameter("@CateCode", ddlDepartment.SelectedValue)
                                              
                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

            if (dt.Rows.Count > 0)
            {
                ddlSubDepartment.DataSource = dt;
                ddlSubDepartment.DataTextField = "ProdSubCatName";
                ddlSubDepartment.DataValueField = "ProdSubCatCode";
                ddlSubDepartment.DataBind();
            }

        }
        private DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ProductCode"));

            dt.Columns.Add(new DataColumn("StoreCode"));
            dt.Columns.Add(new DataColumn("ProductQty"));
            dt.Columns.Add(new DataColumn("PurPrice"));
            dt.Columns.Add(new DataColumn("Comment"));


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
                    dr["ProductCode"] = row.Cells[0].Text;
                    dr["StoreCode"] = row.Cells[3].Text;
                    dr["ProductQty"] = (row.FindControl("txtIssuQTY") as TextBox).Text;
                    dr["PurPrice"] = (row.FindControl("txtPrice") as TextBox).Text; 
                    dr["Comment"] = (row.FindControl("txtComment") as TextBox).Text;
                    //}
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        private void AddtoList()
        {
            try
            {
                DataTable DTAddToList = new DataTable();



                DTAddToList.Columns.Add(new DataColumn("ProductCode"));
                DTAddToList.Columns.Add(new DataColumn("ProductName"));
                DTAddToList.Columns.Add(new DataColumn("ProductBName"));
                DTAddToList.Columns.Add(new DataColumn("StoreCode"));
                DTAddToList.Columns.Add(new DataColumn("IssQty"));
                DTAddToList.Columns.Add(new DataColumn("PurPrice"));
                DTAddToList.Columns.Add(new DataColumn("TotalAMT"));
                //DTAddToList.Columns.Add(new DataColumn("EmpCode"));
                DTAddToList.Columns.Add(new DataColumn("Comment"));



                DataRow DRLocal = DTAddToList.NewRow();

                string sql = @"select ProductName,ProductCode,ProductBName,StoreCode from Item where ProductCode=@ProductCode and CateCode='02'and ProdSubCatCode=@SubDepartmentId";
                DataTable dt = new DataTable();
                SqlParameter[] parameter ={
                                              new SqlParameter("@ProductCode", ddlProductCode.SelectedValue),
                                              new SqlParameter("@SubDepartmentId", ddlSubDepartment.SelectedValue)
                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

                //dt = dc.SelectDs(sql).Tables[0];


                decimal ReqQTY = Convert.ToDecimal(txtIssuQty.Text.Trim());
                decimal ReqPrice = Convert.ToDecimal(txtPrice.Text.Trim());
                decimal total = 0;
                if (ReqQTY == 0)
                    total = ReqPrice;
                else
                    total = ReqQTY * ReqPrice;


                DRLocal["ProductCode"] = ddlProductCode.SelectedValue;
                DRLocal["ProductName"] = dt.Rows[0][0].ToString();
                DRLocal["ProductBName"] = dt.Rows[0][2].ToString();
                DRLocal["StoreCode"] = dt.Rows[0][3].ToString();
                DRLocal["IssQty"] = txtIssuQty.Text.Trim().ToString();
                DRLocal["PurPrice"] = txtPrice.Text.Trim().ToString();
                DRLocal["TotalAMT"] = total.ToString();
                //DRLocal["EmpCode"] = ddlTechnicianID.SelectedValue;
                DRLocal["Comment"] = txtComment.Text.Trim();

                DTAddToList.Rows.Add(DRLocal);

                //DataTable DTLocal1 = DTAddToList.Clone();
                //for (int i = 0; i < DTAddToList.Rows.Count; i++)
                //{
                //    DTLocal1.ImportRow(DTAddToList.Rows[i]);
                //}

                DataTable DTLocal1 = DTAddToList.Clone();

                for (int k = 0; k < gvEditVMF.Rows.Count; k++)
                {
                    DataRow DRLocal1 = DTLocal1.NewRow();


                    DRLocal1["ProductCode"] = gvEditVMF.Rows[k].Cells[0].Text;
                    DRLocal1["ProductName"] = gvEditVMF.Rows[k].Cells[1].Text;
                    DRLocal1["ProductBName"] = gvEditVMF.Rows[k].Cells[2].Text;
                    DRLocal1["StoreCode"] = gvEditVMF.Rows[k].Cells[3].Text;
                    DRLocal1["IssQty"] = ((TextBox)gvEditVMF.Rows[k].FindControl("txtIssuQTY")).Text;
                    DRLocal1["PurPrice"] = ((TextBox)gvEditVMF.Rows[k].FindControl("txtPrice")).Text;
                    DRLocal1["TotalAMT"] = ((TextBox)gvEditVMF.Rows[k].FindControl("txtTotalAMT")).Text;
                    //DRLocal1["EmpCode"] = gvEditVMF.Rows[k].Cells[7].Text;
                    DRLocal1["Comment"] = ((TextBox)gvEditVMF.Rows[k].FindControl("txtComment")).Text; ;


                    DTLocal1.Rows.Add(DRLocal1);


                }

                for (int i = 0; i < DTAddToList.Rows.Count; i++)
                {
                    DTLocal1.ImportRow(DTAddToList.Rows[i]);
                }
                
                gvEditVMF.DataSourceID = null;
                gvEditVMF.DataSource = DTLocal1;
                gvEditVMF.DataBind();

                decimal Qty = DTLocal1.AsEnumerable().Sum(x => Convert.ToDecimal(x["IssQty"]));
                decimal Amount = DTLocal1.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalAMT"]));

                
                gvEditVMF.FooterRow.Cells[1].Text = "Total";
                gvEditVMF.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvEditVMF.FooterRow.Cells[3].Text = Qty.ToString("0");
                gvEditVMF.FooterRow.Cells[5].Text = Amount.ToString("N2");
                ClearInsertedGridData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void saveInternalReusition()
        {

            try
            {
                int comcode = 1;
                string InternalReqNo;
                if (string.IsNullOrEmpty(lblIRNo.Text))
                {
                    lblIRNo.Text = IssuStock_maxID();
                    InternalReqNo = lblIRNo.Text.Trim();
                }
                else
                {
                    InternalReqNo = lblIRNo.Text.Trim();
                }
                string IssVouchNo_1 = txtIssVoucherNo.Text;


                string IssDate_2 = txtIssDate.Text.Trim();
                string VehicleID_3 = ddlVehicleID.SelectedValue;
                string ModelNo_4 = txtModelNo.Text.Trim();
                string DriverID_5 = ddlDriverID.SelectedValue;
                string ProdSubCatCode_6 = ddlSubDepartment.SelectedValue;
                string CateCode_7 = "02";
                string TechnicianID_12 = ddlTechnicianID.SelectedValue.Trim();
                string Remarks_13;
                if (string.IsNullOrEmpty(txtRemark.Text))
                {
                    Remarks_13 = "";
                }
                else
                {
                    Remarks_13 = txtRemark.Text.Trim();
                }
                string FromDate_14 = txtFromDate.Text.Trim();
                string ToDate_15 = txtToDate.Text.Trim();
                int IssuStatus_16 = 0;
                int ServiceStatus;
                if (ckWithoutParts.Checked)
                {
                    ServiceStatus = 1;
                }
                else
                {
                    ServiceStatus = 0;
                }

                DataTable dt = new DataTable();
                dt = GetDataTable(gvEditVMF);
                string userId = "";

                string roweffected = (new StockIssuGateway().InsertUpdateStockIssu(comcode, InternalReqNo, IssVouchNo_1, IssDate_2, VehicleID_3, ModelNo_4, DriverID_5, ProdSubCatCode_6, CateCode_7, TechnicianID_12, Remarks_13, FromDate_14, ToDate_15, IssuStatus_16, ServiceStatus,userId,dt));
                if (String.IsNullOrEmpty(roweffected) == false)
                {
                    txtIssVoucherNo.Text = IssVouchNo_1;
                    lblMsg.Text = "Successfully Save.";
                }
            }
            catch (Exception ex)
            {
                //this.lblMsg.Text = (ex.Message);
            }

        }
        private void ckSelectAllItem()
        {
            CheckBox cb = (CheckBox)gvEditVMF.HeaderRow.FindControl("ckAll");
            decimal Qty = 0;
            decimal amount = 0;
            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvEditVMF.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvEditVMF.Rows[i].FindControl("ck");
                        chkboxSelect.Checked = true;

                        Qty = Qty + Convert.ToDecimal(((TextBox)gvEditVMF.Rows[i].FindControl("txtIssuQTY")).Text);
                        amount = amount +
                                 Convert.ToDecimal(((TextBox) gvEditVMF.Rows[i].FindControl("txtTotalAMT")).Text);
                    }
                }
                else
                {
                    for (int i = 0; i < gvEditVMF.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvEditVMF.Rows[i].FindControl("ck");
                        chkboxUnselect.Checked = false;
                        Qty = 0;
                        amount = 0;

                    }
                }
                txtTotalQTY.Text = Qty.ToString("0");
                txtTotalAMT.Text = amount.ToString("0.00");
            }
        }
        private DataTable loadAllInternalReq(string searchKey)
        {
            try
            {
                string sql = @"SELECT     StockIssue.ComCode, StockIssue.InternalReqNo, StockIssue.IssVouchNo, CONVERT(VARCHAR(10), StockIssue.IssDate, 103) AS [IssDate], StockIssue.VehicleID, VMFIssue.VehicleID AS Expr1, 
                      VehicleInfo.VehicleNo FROM         StockIssue INNER JOIN
                      VMFIssue ON StockIssue.IssVouchNo = VMFIssue.IssVouchNo INNER JOIN
                      VehicleInfo ON VMFIssue.VehicleID = VehicleInfo.VehicleID
                        WHERE      (StockIssue.IssVouchNo <> '')
                        ORDER BY StockIssue.IssDate DESC, StockIssue.InternalReqNo DESC";
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
                if (String.IsNullOrEmpty(searchKey))
                {


                }
                else
                {
                    var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("InternalReqNo").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("IssVouchNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();
                }
                return dt;

            }

            catch (Exception ex)
            {

                return null;

            }
        }
        private void loadInternalReqByID()
        {
            try
            {
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string code = rows.Cells[1].Text;
                string sql = @"Select SI.InternalReqNo, SI.IssVouchNo,SI.IssDate AS [IssDate],SI.ModelNo,
                            SI.FromDate AS [FromDate],SI.ToDate AS [ToDate],SI.Remarks,
                            SI.TechnitianID,SI.EmpCode,PS.EmpName,PES.EmpName As TechnicianName,SI.CateCode,SI.VehicleID,VI.VehicleNo,SID.IssQty,
                            SID.ProductCode,SID.StoreCode,CAST((SID.IssQty) AS DECIMAL(18,2)) AS IssQty,CAST((SID.PurPrice) AS DECIMAL(18,2)) AS PurPrice,CAST((SID.PurPrice * SID.IssQty) AS DECIMAL(18,2)) AS TotalAMT,IT.ProductName,IT.ProductBName,SID.EmpCode,SID.Comment,SI.ProdSubCatCode,ITS.ProdSubCatName  
							from StockIssue SI inner join Personal PS on SI.EmpCode=PS.EmpCode
                            inner join VehicleInfo VI on SI.VehicleID=VI.VehicleID 
                            inner join StockIssueDetail SID on SI.ComCode=SID.ComCode and SI.InternalReqNo=SID.InternalReqNo 
                            inner join Personal PES on SI.TechnitianID=PES.EmpCode
                            inner join Item IT on SID.ProductCode=IT.ProductCode and SID.StoreCode=IT.StoreCode
                            inner join ItemSubCategory ITS on SI.ProdSubCatCode=ITS.ProdSubCatCode and SI.CateCode=ITS.CateCode
                            where SI.InternalReqNo=@InternalReqNo  and      (IssVouchNo IS NOT NULL) AND (IssVouchNo <> '') ";  //VI.IssuStatus='0'


                DataTable dt = new DataTable();

                SqlParameter[] parameter ={
                                              new SqlParameter("@InternalReqNo", code)
                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                //dt = dc.SelectDs(sql).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    lblIRNo.Text= dt.Rows[0]["InternalReqNo"].ToString();
                    txtIssVoucherNo.Text = dt.Rows[0]["IssVouchNo"].ToString().Trim();

                    txtIssDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["IssDate"].ToString()).Date;
                    txtFromDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString());
                    txtToDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString());

                    txtModelNo.Text = dt.Rows[0]["ModelNo"].ToString().Trim();
                  
                    txtRemark.Text = dt.Rows[0]["Remarks"].ToString().Trim();
                   
                    

                    ddlDriverID.SelectedValue = dt.Rows[0]["EmpCode"].ToString().Trim();

                    ddlVehicleID.SelectedValue = dt.Rows[0]["VehicleID"].ToString().Trim();



                    
                    ddlDepartment.SelectedValue = dt.Rows[0]["CateCode"].ToString().Trim();



                    ddlSubDepartment.SelectedValue = dt.Rows[0]["ProdSubCatCode"].ToString().Trim();

                    loadDDLTechnician();
                    ddlTechnicianID.SelectedValue = dt.Rows[0]["TechnitianID"].ToString().Trim();

                    gvEditVMF.DataSource = dt;
                    gvEditVMF.DataBind();

                    CheckBox cb = (CheckBox)gvEditVMF.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();

                    //if (servicestatus == "1")
                    //{
                    //    ckWithoutParts.Checked = true;
                    //    this.loadDDLServiceProduct();

                    //    txtIssuQty.Text = "0";
                    //    txtIssuQty.Enabled = false;

                    //}
                    //else
                    //{
                    //    ckWithoutParts.Checked = false;

                    //    txtIssuQty.Text = "";
                    //    txtIssuQty.Enabled = true;
                    //}

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private DataTable loadAllVMFInfo(string searchKey)
        {
            try
            {


                string sql = @"SELECT    VMFIssue.ComCode, VMFIssue.IssVouchNo, CONVERT(VARCHAR(10), VMFIssue.IssDate, 103) AS [IssDate], VMFIssue.VehicleID, VMFIssue.ModelNo, VMFIssue.EmpCode, VMFIssue.CateCode, 
                      VMFIssue.ProdSubCatCode, VMFIssue.TechnitianID, VMFIssue.Remarks, VMFIssue.FromDate, VMFIssue.ToDate, VMFIssue.ServiceStatus, VMFIssue.IssuStatus, 
                      VMFIssue.OutDate, VehicleInfo.VehicleNo, Personal.EmpName AS DriverName, Personal_1.EmpName AS TechnicianName
FROM         VMFIssue INNER JOIN
                      VehicleInfo ON VMFIssue.VehicleID = VehicleInfo.VehicleID INNER JOIN
                      Personal ON VMFIssue.EmpCode = Personal.EmpCode INNER JOIN
                      Personal AS Personal_1 ON VMFIssue.ComCode = Personal_1.ComCode AND VMFIssue.TechnitianID = Personal_1.EmpCode
                          and VMFIssue.IssuStatus=3 order By VMFIssue.IssDate desc, VMFIssue.IssVouchNo desc ";

                DataTable dt = new DataTable();

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];


                //dt = dc.SelectDs(sql).Tables[0];




                if (String.IsNullOrEmpty(searchKey))
                {


                }
                else
                {
                    var filtered = dt.AsEnumerable()
.Where(r => r.Field<String>("IssVouchNo").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("ModelNo").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();
                }
                return dt;


            }
            catch (Exception ex)
            {

                return null;

            }
        }
        private void loadVMFDatabyVMFID()
        {
            try
            {
                GridViewRow rows = gvVFM.SelectedRow;
                string code = rows.Cells[1].Text;
                string sql = @"Select VI.IssVouchNo,CONVERT(VARCHAR(10), VI.IssDate, 103) AS [IssDate],VI.ModelNo,CONVERT(VARCHAR(10),VI.FromDate, 103) AS [FromDate],CONVERT(VARCHAR(10),VI.ToDate, 103) AS [ToDate],VI.Remarks,VI.TechnitianID,VI.EmpCode,PS.EmpName,VI.CateCode,VI.VehicleID,VE.VehicleNo,PSR.EmpCode AS TechnicianID,VI.ServiceStatus,PSR.EmpName As TechnicianName,VI.ProdSubCatCode,ITS.ProdSubCatName,VI.IssuStatus  
							from VMFIssue VI inner join Personal PS on VI.EmpCode=PS.EmpCode
							inner join Personal PSR on VI.TechnitianID=PSR.EmpCode
                            inner join VehicleInfo VE on VI.VehicleID=VE.VehicleID 
                            inner join  dbo.ItemSubCategory  ITS on VI.CateCode=its.CateCode and VI.ProdSubCatCode=ITS.ProdSubCatCode
                            where VI.IssVouchNo=@IssVoucherNo And  VI.CateCode='02' ";  //VI.IssuStatus='0'


                DataTable dt = new DataTable();

                SqlParameter[] parameter ={
                                              new SqlParameter("@IssVoucherNo", code)
                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                //dt = dc.SelectDs(sql).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtIssVoucherNo.Text = dt.Rows[0]["IssVouchNo"].ToString().Trim();
                    txtIssDate.Text = dt.Rows[0]["IssDate"].ToString().Trim();

                    txtModelNo.Text = dt.Rows[0]["ModelNo"].ToString().Trim();

                    txtRemark.Text = dt.Rows[0]["Remarks"].ToString().Trim();
                    txtFromDate.Text = dt.Rows[0]["FromDate"].ToString().Trim();
                    txtToDate.Text = dt.Rows[0]["ToDate"].ToString().Trim();

                    ddlDriverID.SelectedValue = dt.Rows[0]["EmpCode"].ToString().Trim();

                    ddlVehicleID.SelectedValue = dt.Rows[0]["VehicleID"].ToString().Trim();




                    ddlDepartment.SelectedValue = dt.Rows[0]["CateCode"].ToString().Trim();



                    ddlSubDepartment.SelectedValue = dt.Rows[0]["ProdSubCatCode"].ToString().Trim();

                    loadDDLTechnician();
                    //string test = ddlTechnicianID.SelectedValue;
                    ddlTechnicianID.SelectedValue = dt.Rows[0]["TechnitianID"].ToString().Trim();
                    string servicestatus= dt.Rows[0]["ServiceStatus"].ToString().Trim();

                    if (servicestatus == "1")
                    {
                        ckWithoutParts.Checked = true;
                        this.loadDDLServiceProduct();

                        txtIssuQty.Text = "0";
                        txtIssuQty.Enabled = false;

                    }
                    else
                    {
                        ckWithoutParts.Checked = false;

                        txtIssuQty.Text = "";
                        txtIssuQty.Enabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void loadVMFDetailGrid()
        {
            try
            {
                GridViewRow rows = gvVFM.SelectedRow;
                string code = rows.Cells[1].Text;
                string sql = @"SELECT     VID.IssVouchNo, VID.ProductCode, CAST(VID.IssQty AS DECIMAL(18, 2)) AS IssQty, CAST(VID.PurPrice AS DECIMAL(18, 2)) AS PurPrice, 
                      CAST(VID.PurPrice * VID.IssQty AS DECIMAL(18, 2)) AS TotalAMT, IT.ProductName, IT.ProductBName, VID.StoreCode, VID.Comment
FROM         VMFIssueDetail AS VID INNER JOIN
                      Item AS IT ON VID.StoreCode = IT.StoreCode
WHERE     (VID.IssVouchNo = @IssVoucherNo)";
                DataTable dt = new DataTable();

                SqlParameter[] parameter ={
                                              new SqlParameter("@IssVoucherNo",code)
                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                //dt = dc.SelectDs(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvEditVMF.DataSource = dt;
                    gvEditVMF.DataBind();

                    CheckBox cb = (CheckBox)gvEditVMF.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        private void clear_Grid()
        {


            gvEditVMF.DataSource = null;
            gvEditVMF.DataBind();
        }

        private void ClearInsertedGridData()
        {
            //txtProdSlNO.Text = "";
            txtIssuQty.Text = "";
            txtComment.Text = "";
            txtPrice.Text = "";
        }

        private void Clear_Form()
        {
            lblMsg.Text = "";
            
            txtIssVoucherNo.Text = "";
            txtIssDate.Text = "";
            txtModelNo.Text = "";

            txtToDate.Text = "";
            txtFromDate.Text = "";
            txtTotalQTY.Text = "";
            txtTotalAMT.Text = "";
            txtPrice.Text = "";

            txtIssuQty.Text = "";
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
            }

        }
        private void loadDDLTechnician()
        {


            string sql = @"Select EmpCode,EmpName from Personal where Cardno='2' and DesignationID=1 ";
            DataTable dt = new DataTable();
            //SqlParameter[] parameter ={
            //                                  new SqlParameter("@ProdSubCatCode", ddlSubDepartment.SelectedValue)
            //                                };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlTechnicianID.DataSource = dt;
                ddlTechnicianID.DataTextField = "EmpName";
                ddlTechnicianID.DataValueField = "EmpCode";
                ddlTechnicianID.DataBind();
            }



        }
        private void loadDDLVehicle()
        {
            //if (txtSearchVehicle.Text.Length > 3)
            //{
            
            string sql = @"Select VehicleID,VehicleNo from dbo.VehicleInfo"; //where VehicleNo like'%" + txtSearchVehicle.Text.Trim() + "'";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlVehicleID.DataSource = dt;
                ddlVehicleID.DataTextField = "VehicleNo";
                ddlVehicleID.DataValueField = "VehicleID";
                ddlVehicleID.DataBind();
            }
            //}
            //else
            //    lblMsg.Text = "Please Give Minimum 4 charecter.";

        }
        private void loadDDLProduct()
        {
            
            string sql = @"Select ProductCode,ProductName from Item where CateCode='02'and ProdSubCatCode=@ProdSubCatCode and  Not ProductCode='00000'";
            SqlParameter[] parameter ={
                                              new SqlParameter("@ProdSubCatCode", ddlSubDepartment.SelectedValue)
                                            };
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlProductCode.DataSource = null;
                ddlProductCode.DataBind();
                ddlProductCode.DataSource = dt;
                ddlProductCode.DataTextField = "ProductName";
                ddlProductCode.DataValueField = "ProductCode";
                ddlProductCode.DataBind();
            }


        }

        private void loadDDLServiceProduct()
        {

            
            string sql = @"Select ProductCode,ProductName from Item where CateCode='02'and ProdSubCatCode=@ProdSubCatCode and ProductCode='00000'";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={
                                              new SqlParameter("@ProdSubCatCode", ddlSubDepartment.SelectedValue)
                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
            //dt = dc.SelectDs(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlProductCode.DataSource = null;
                ddlProductCode.DataBind();

                ddlProductCode.DataSource = dt;
                ddlProductCode.DataTextField = "ProductName";
                ddlProductCode.DataValueField = "ProductCode";
                ddlProductCode.DataBind();
            }


        }

        //private void totalQtycount()
        //{
        //    try
        //    {
        //        decimal totalQty = Convert.ToDecimal(gvEditVMF.Rows.Count);

        //        txtTotalQTY.Text = totalQty.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //}

        //private void totalAMTcount()
        //{
        //    try
        //    {
        //        decimal totalAMT = 0;
        //        for (int i = 0; i < gvEditVMF.Rows.Count; i++)
        //        {
        //            string newtotalAMT = gvEditVMF.Rows[i].Cells[6].Text.Trim().ToString();
        //            decimal newtotalAMT_1 = Convert.ToDecimal(newtotalAMT);
        //            totalAMT = totalAMT + newtotalAMT_1;

        //        }
        //        txtTotalAMT.Text = totalAMT.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //}
        private string IssuStock_maxID()
        {

           string CurrentYear = DateTime.Today.ToString("yyyy");
            String sql = @"select ISNULL(Max(Convert(integer,Right(InternalReqNo,6))), 0) + 1 from StockIssue where ComCode='1'and year(IssDate)=" + CurrentYear + "";

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            int count = int.Parse(dt.Rows[0][0].ToString());
            string NewIssVoucherNo = count.ToString().PadLeft(6, '0');
            string InternalReq = "IR" + "-" + CurrentYear + NewIssVoucherNo;
            return InternalReq;


        }
      



        private void ProductUnitPrice()
        {
            try
            {
                string sql = @"select CAST((DistPrice)as decimal(18,2)) from Item where ProductCode=@ProductCode and CateCode='02'and ProdSubCatCode=@SubDepartmentId";
                DataTable dt = new DataTable();
                SqlParameter[] parameter ={
                                              new SqlParameter("@ProductCode", ddlProductCode.SelectedValue),
                                              new SqlParameter("@SubDepartmentId", ddlSubDepartment.SelectedValue)
                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                //dt = dc.SelectDs(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtPrice.Text = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
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
                txtIssDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
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
                    Load_ddlProdSubCategory();
                    loadDDLProduct();
                    loadDDLVehicle();
                    loadDDLDriver();
                    loadDDLTechnician();
                    txtIssDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
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
            dt = loadAllInternalReq(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveInternalReusition();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";


            string Department = ddlDepartment.SelectedValue;
            string RepotrName = "InternalRequisition2";
                Response.Redirect("reportViewerWorkshop.aspx?RID=" + lblIRNo.Text.Trim() + "&RN=" + RepotrName + "&Dpt=" + Department);
           
          
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllInternalReq(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInternalReqByID();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllInternalReq(txtSearch.Text);
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

        protected void btnVMFSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllVMFInfo(txtSearch.Text);
            //gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvVFM.DataSource = dt;
            gvVFM.DataBind();
            upVMF.Update();
            hfShowVFMGrid_ModalPopupExtender.Show();
        }

        protected void gvVFM_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadVMFDatabyVMFID();
            loadVMFDetailGrid();
        }

        protected void gvVFM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllVMFInfo(txtSearch.Text);
            gvVFM.PageIndex = e.NewPageIndex;
            gvVFM.DataSource = dt;
            gvVFM.DataBind();
            upVMF.Update();
            hfShowVFMGrid_ModalPopupExtender.Show();
        }

        protected void ck_CheckedChanged(object sender, EventArgs e)
        {
            decimal Qty = 0;
            decimal amount = 0;
            foreach (GridViewRow row in gvEditVMF.Rows)
            {
                if ((row.FindControl("ck") as CheckBox).Checked)
                {
                    Qty = Qty + Convert.ToDecimal(((TextBox) row.FindControl("txtIssuQTY")).Text);
                    amount = amount +
                             Convert.ToDecimal(((TextBox) row.FindControl("txtTotalAMT")).Text);
                }
            }
            txtTotalQTY.Text = Qty.ToString("0");
            txtTotalAMT.Text = amount.ToString("0.00");
        }

        protected void btnVMF_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllVMFInfo(txtSearch.Text);
            //gvVFM.PageIndex = e.NewPageIndex;
            gvVFM.DataSource = dt;
            gvVFM.DataBind();
            upVMF.Update();
            hfShowVFMGrid_ModalPopupExtender.Show();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_ddlProdSubCategory();
            loadDDLProduct();
        }

        protected void ddlSubDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDDLProduct();
        }

        protected void txtIssuQty_TextChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            string sql = @"select ItemStock.PhyStock,ItemStock.ProductCode from ItemStock inner join Item on ItemStock.ProductCode=Item.ProductCode and item.StoreCode=ItemStock.StoreCode and Item.CateCode='02'and Item.ProdSubCatCode=@SubDepartmentId where ItemStock.ProductCode=@ProductCode";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={

                                              new SqlParameter("@SubDepartmentId", ddlSubDepartment.SelectedValue),
                                              new SqlParameter("@ProductCode", ddlProductCode.SelectedValue)
                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                decimal balance = Convert.ToDecimal(dt.Rows[0][0].ToString());
                if (Convert.ToDecimal(txtIssuQty.Text.Trim()) > balance)
                {
                    lblMsg.Text = "Stock Balance Is Not Available Emergency purchase!";
                    txtIssuQty.Text = "";
                    txtIssuQty.Focus();
                }
                else
                {
                    this.ProductUnitPrice();
                    txtPrice.Focus();
                }
            }
            else
                lblMsg.Text = "Select Another Product!";
        }

        protected void ckWithoutParts_CheckedChanged(object sender, EventArgs e)
        {
            if (ckWithoutParts.Checked)
            {

                this.loadDDLServiceProduct();


                txtIssuQty.Text = "1";
                txtIssuQty.Enabled = false;
            }
            else
            {

                txtIssuQty.Text = "";
                txtIssuQty.Enabled = true;

                ddlProductCode.DataSource = null;
                ddlProductCode.DataBind();
                this.loadDDLProduct();



            }
        }

        protected void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            ckSelectAllItem();
            //totalAMTcount();
            //totalQtycount();

        }
    }
}