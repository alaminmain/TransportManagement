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
    public partial class PurchaseOrder : System.Web.UI.Page
    {

      
        #region Private Methode

      
        private DataTable LoadPurchaseInfoGrid(string searchKey)
        {
            try
            {
                DataTable dt;


                string sql = @"Select PO.PurOrderNo, PO.PINo as ReqNo,CONVERT(VARCHAR(10), PO.PIIssDate, 103) AS [ReqDate],CONVERT(VARCHAR(10), PO.OrderDate, 103) AS [OrderDate],PO.PurOrderDesc 
								from PurOrder PO  Order by PO.OrderDate desc,PO.PurOrderNo desc";

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];


                if (String.IsNullOrEmpty(searchKey))
                {


                }
                else
                {
                    var filtered = dt.AsEnumerable()
.Where(r => r.Field<String>("PONo").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("ReqDate").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();
                }
                return dt;

            }

            catch (Exception ex)
            {

                return null;

            }
        }

        private void loadPurchaseOrderForm()
        {
            try
            {
                GridViewRow row = gvlistofBasicData.SelectedRow;
                string poNo = row.Cells[1].Text.ToString();


                DataTable dt = new DataTable();

                string sql = @"SELECT        PO.PurOrderNo, PO.PINo, PO.PIIssDate AS ReqDate, PO.OrderDate AS OrderDate, PO.PurOrderDesc, PO.SuplierID, PRD.ProductCode, PRD.StoreCode, IT.ProductName, PRD.OrderQty, POD.OrdQty, POD.PurPrice, 
                         CAST(POD.PurPrice AS DECIMAL(18, 2)) AS ReqPrice, CAST(POD.OrdQty * POD.PurPrice AS DECIMAL(18, 2)) AS TotalAMT, PRD.ReqQty
FROM            PurOrder AS PO RIGHT OUTER JOIN
                         PurOrderProd AS POD ON PO.ComCode = POD.ComCode AND PO.PurOrderNo = POD.PurOrderNo INNER JOIN
                         PurRequisition AS PR ON PO.PINo = PR.ReqNo INNER JOIN
                         PurRequisitionDTL AS PRD ON PR.ComCode = PRD.ComCode AND PR.ReqNo = PRD.ReqNo AND POD.ProductCode = PRD.ProductCode INNER JOIN
                         Item AS IT ON POD.ProductCode = IT.ProductCode AND POD.StoreCode = IT.StoreCode
                                where PO.PurOrderNo=@PurOrderNo";

                SqlParameter[] parameter ={
                                              new SqlParameter("@PurOrderNo",poNo )
                                            };

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    lblMessage.Text = "";
                    lblPONO.Text= dt.Rows[0]["PurOrderNo"].ToString().Trim();
                    txtReqNo.Text = dt.Rows[0]["PINo"].ToString().Trim();

                    //txtRecDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["RecDate"].ToString());
                    txtPurOrderDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["OrderDate"].ToString());

                    txtReqDate.Text = dt.Rows[0]["ReqDate"].ToString().Trim();
                    //txtPurOrderDate.Text = dt.Rows[0]["OrderDate"].ToString();
                    txtPurOrderDesc.Text = dt.Rows[0]["PurOrderDesc"].ToString();
                    ddlSupplierID.SelectedValue= dt.Rows[0]["SuplierID"].ToString();

                    gvPurchaseEdit.DataSource = dt;
                    gvPurchaseEdit.DataBind();
                }
                else
                {
                    lblMessage.Text = "Data not found.";
                }

            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SavePurchaseOrder()
        {
            try
            {
                int comcode = 1;

                string PurOrderNo_1;
                if (string.IsNullOrEmpty(lblPONO.Text))
                {
                    PurOrderNo_1 = Purchase_maxID();
                    
                }
                else
                {
                    PurOrderNo_1 = lblPONO.Text;
                }
                string OrderDate_2 = txtPurOrderDate.Text;
                string PINo_3 = txtReqNo.Text;
                string PIIssDate_4 = txtReqDate.Text;
                string PICurrency_5 = "Taka";
                string SuplierID_6 = ddlSupplierID.SelectedValue;
                string PurOrderDesc_7 = txtPurOrderDesc.Text;
                int OrdStatus_8 = 0;
                decimal TotOrderQty_9 = 0;
                string userId = "";
                DataTable dt = new DataTable();
                dt = GetDataTable(gvPurchaseEdit);
                if (dt.Rows.Count > 0)
                {
                    TotOrderQty_9 = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                    string roweffected = (new PuechaseOrderGateway().insertUpdatePurchaseOrder(comcode, PurOrderNo_1, OrderDate_2, PINo_3, PIIssDate_4, PICurrency_5, SuplierID_6, PurOrderDesc_7, OrdStatus_8, TotOrderQty_9, userId, dt));
                    lblPONO.Text = roweffected;
                    lblMessage.Text = "Saved";
                }
                else
                    lblMessage.Text = "Cannot Save";
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = (ex.Message);
            }
        }
        private DataTable GetDataTable(GridView dtg)
        {
            DataTable DTAddToList = new DataTable();

            DTAddToList.Columns.Add(new DataColumn("ReqSLNo"));
            DTAddToList.Columns.Add(new DataColumn("ProductCode"));
            DTAddToList.Columns.Add(new DataColumn("ProductName"));
            DTAddToList.Columns.Add(new DataColumn("StoreCode"));
            DTAddToList.Columns.Add(new DataColumn("ReqQty"));
            DTAddToList.Columns.Add(new DataColumn("PreviousOrderQty"));
            DTAddToList.Columns.Add(new DataColumn("OrderQTY"));
            DTAddToList.Columns.Add(new DataColumn("PurPrice"));
            DTAddToList.Columns.Add(new DataColumn("TotalAMT"));
           


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




                    DRLocal["ReqSLNo"] = row.Cells[0].Text.ToString();
                    DRLocal["ProductCode"] = row.Cells[1].Text.ToString();

                    DRLocal["ProductName"] = row.Cells[2].Text.ToString();
                    DRLocal["StoreCode"] = row.Cells[3].Text.ToString();
                    DRLocal["ReqQty"] = row.Cells[4].Text.ToString();
                    DRLocal["PreviousOrderQty"] = row.Cells[5].Text.ToString();
                    DRLocal["OrderQTY"] = (row.FindControl("txtOrderQTY") as TextBox).Text;
                    DRLocal["PurPrice"] = (row.FindControl("txtPurPrice") as TextBox).Text;
                    DRLocal["TotalAMT"] = (row.FindControl("txtTotalAMT") as TextBox).Text;
                   

                    DTAddToList.Rows.Add(DRLocal);
                }

            }

            return DTAddToList;
        }

        private string Purchase_maxID()
        {
            string CurrentYear = DateTime.Today.ToString("yyyy");
            String sql = @"select ISNULL(Max(Convert(integer,Right(PurOrderNo,6))), 0) + 1 from PurOrder where ComCode='1'and year(OrderDate)=" + CurrentYear + "";

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            int count = int.Parse(dt.Rows[0][0].ToString());
            string NewPurOrderNo = count.ToString().PadLeft(6, '0');

            string PurchaseNo = "PO" + "-" + CurrentYear + NewPurOrderNo;

            return PurchaseNo;

        }

        private void loadddlSupplier()
        {
            string sql = @"Select SupplierName,SupplierID from Supplier";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlSupplierID.DataSource = dt;
                ddlSupplierID.DataTextField = "SupplierName";
                ddlSupplierID.DataValueField = "SupplierID";
                ddlSupplierID.DataBind();
            }
        }

        private bool check_Create()     //***Check Create function*************
        {

            if (ddlSupplierID.SelectedIndex < 0)
            {
                lblMessage.Text = ("please select Supplier.");
                ddlSupplierID.Focus();

                return false;
            }
           

            else
                return true;
        }
       
       

        private void ckSelectAllItem()
        {
            CheckBox cb = (CheckBox)gvPurchaseEdit.HeaderRow.FindControl("ckAll");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvPurchaseEdit.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvPurchaseEdit.Rows[i].FindControl("ck");
                        chkboxSelect.Checked = true;

                    }
                }
                else
                {
                    for (int i = 0; i < gvPurchaseEdit.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvPurchaseEdit.Rows[i].FindControl("ck");
                        chkboxUnselect.Checked = false;

                    }
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
        private DataTable LoadRequsition(string searchKey)
        {
            try
            {
                DataTable dt;


                string sql = @"SELECT     PurRequisition.ComCode, PurRequisition.ReqNo, PurRequisition.CateCode, PurRequisition.ProdSubCatCode, CONVERT(VARCHAR(10), PurRequisition.ReqDate, 103) AS [ReqDate], 
                      PurRequisition.ImergencRreqStatus, PurRequisition.Status, PurRequisition.Remark, Item_Category.CateName, ItemSubCategory.ProdSubCatName
                      FROM         PurRequisition INNER JOIN
                      Item_Category ON PurRequisition.CateCode = Item_Category.CateCode INNER JOIN
                      ItemSubCategory ON PurRequisition.CateCode = ItemSubCategory.CateCode AND PurRequisition.ProdSubCatCode = ItemSubCategory.ProdSubCatCode and PurRequisition.Status in ('0','1','2') 
                        Order by PurRequisition.ReqDate desc,PurRequisition.ReqNo desc ";

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
        private void loadRequistionDataBySelect()
        {
            try
            {
                GridViewRow row = gvRequisition.SelectedRow;
                string reqNo = row.Cells[1].Text.ToString();

                string sql = @"select PRS.ReqNo,CONVERT(VARCHAR(10), PRS.ReqDate, 103) AS [ReqDate],PRSD.ReqSLNo,PRSD.ProductCode,PRSD.StoreCode,IT.ProductName,PRSD.ReqQty,CAST((PRSD.ReqPrice) AS DECIMAL(18,2)) AS PurPrice,CAST((PRSD.ReqQty * PRSD.ReqPrice) AS DECIMAL(18,2)) as TotalAMT ,PRSD.ReqQty as OrdQty, PRSD.OrderQty  from PurRequisition PRS inner join PurRequisitionDTL PRSD on PRS.ReqNo=PRSD.ReqNo
                                inner join Item IT on PRSD.ProductCode=IT.ProductCode and PRSD.StoreCode=IT.StoreCode
                                where PRS.ReqNo=@ReqNo and (PRSD.ReqQty-PRSD.OrderQty)>0 and PRSD.Status='0'";
                DataTable dt = new DataTable();
                SqlParameter[] parameter ={
                                              new SqlParameter("@ReqNo",reqNo )
                                            };

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                if (dt.Rows.Count > 0)
                {
                   
                    lblMessage.Text = "";
                    //.Text = dt.Rows[0][0].ToString().Trim();
                    txtReqDate.Text = dt.Rows[0]["ReqDate"].ToString().Trim();
                    txtReqNo.Text = dt.Rows[0]["ReqNo"].ToString().Trim();
                    

                    gvPurchaseEdit.DataSource = dt;
                    gvPurchaseEdit.DataBind();

                    CheckBox cb = (CheckBox)gvPurchaseEdit.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();
                    }
                else
                {

                    
                    gvPurchaseEdit.DataSource = null;
                    gvPurchaseEdit.DataBind();
                    lblMessage.Text = "Data not found.";
                }
            }
            catch(Exception ex)
            {
                throw ex;   
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
            txtPurOrderDate_CalendarExtender.SelectedDate = DateTime.Now.Date;

           
            gvPurchaseEdit.DataSource = null;
            gvPurchaseEdit.DataBind();
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

                    loadddlSupplier();
                    txtPurOrderDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
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
            dt = LoadPurchaseInfoGrid(txtSearch.Text);

            //gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SavePurchaseOrder();
        }
       
        protected void txtPurPrice_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;

            decimal PurQty = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtOrderQTY"))).Text);
            decimal PurPrice = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtPurPrice"))).Text);


            TextBox txtTotalValue = ((TextBox)gvRow.FindControl("txtTotalAMT"));

            try
            {
                txtTotalValue.Text = ((PurQty * PurPrice).ToString());
                            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string RepotrName = "PurchaseOrder";
            Response.Redirect("ReportViewer.aspx?RID=" + lblPONO.Text.Trim() + "&RN=" + RepotrName);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadPurchaseInfoGrid(txtSearch.Text);

            //gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPurchaseOrderForm();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadPurchaseInfoGrid(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnReqSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadRequsition(txtSearch.Text);

            //gvRequisition.PageIndex = e.NewPageIndex;
            gvRequisition.DataSource = dt;
            gvRequisition.DataBind();
            upReqsition.Update();
            hfShowReqGrid_ModalPopupExtender.Show();
        }

        protected void gvRequisition_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRequistionDataBySelect();
        }

        protected void gvRequisition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadRequsition(txtSearch.Text);

            gvRequisition.PageIndex = e.NewPageIndex;
            gvRequisition.DataSource = dt;
            gvRequisition.DataBind();
            upReqsition.Update();
            hfShowReqGrid_ModalPopupExtender.Show();
        }

        protected void btnRequistionSearch_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadRequsition(txtSearch.Text);

            //gvRequisition.PageIndex = e.NewPageIndex;
            gvRequisition.DataSource = dt;
            gvRequisition.DataBind();
            upReqsition.Update();
            hfShowReqGrid_ModalPopupExtender.Show();
        }

        protected void ckSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            ckSelectAllItem();
        }

        protected void txtOrderQTY_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;

            decimal PurQty = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtOrderQty"))).Text);
            decimal PurPrice = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtPurPrice"))).Text);


            TextBox txtTotalValue = ((TextBox)gvRow.FindControl("txtTotalAMT"));


            try
            {
                txtTotalValue.Text = ((PurQty * PurPrice).ToString());


            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}