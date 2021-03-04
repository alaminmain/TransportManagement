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
    public partial class MRR : System.Web.UI.Page
    {


        #region Private Methode


        private DataTable LoadPurchaseReceiveGrid(string searchKey)
        {
            try
            {
                DataTable dt;


                string sql = @"select PRC.PurRecNo,CONVERT(VARCHAR(10), PRC.RecDate, 103) AS [RecDate],PRC.InvNo,CONVERT(VARCHAR(10), PRC.InvDate, 103) AS [InvDate],PRC.Remarks, CAST((PRC.TotFOB) AS DECIMAL(18,2)) AS TotFOB,CAST((PRC.Freight) AS DECIMAL(18,2)) AS Freight,CAST((PRC.Insurance) AS DECIMAL(18,2)) AS Insurance,CAST((PRC.Others) AS DECIMAL(18,2)) AS Others,CAST((PRC.Discount) AS DECIMAL(18,2)) AS Discount,CAST((PRC.Deduction) AS DECIMAL(18,2)) AS Deduction,PRC.PurOrderNo,PRCD.OrderSLNo,PRCD.ProductCode,IT.ProductName,PRCD.RecQty,CAST((PRCD.PurPrice) AS DECIMAL(18,2)) AS PurPrice,CAST((PRCD.CostPrice) AS DECIMAL(18,2)) AS CostPrice,CAST(((PRCD.RecQty * PRCD.PurPrice)+PRCD.CostPrice) AS DECIMAL(18,2)) AS TotalPrics,PRCD.StoreCode,POD.ReceivedQty,POD.OrdQty from PurRec PRC inner join PurRecProd PRCD on PRC.PurRecNo=PRCD.PurRecNo
                            inner join Item IT on PRCD.ProductCode= IT.ProductCode and PRCD.StoreCode=IT.StoreCode
                            inner join PurOrderProd POD on PRC.PurOrderNo=POD.PurOrderNo And PRCD.StoreCode=PRCD.StoreCode and PRCD.ProductCode=POD.ProductCode
                            Order by PRC.RecDate desc,PRC.PurRecNo desc";

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];


                if (String.IsNullOrEmpty(searchKey))
                {


                }
                else
                {
                    var filtered = dt.AsEnumerable()
.Where(r => r.Field<String>("PurRecNo").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("RecDate").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();
                }
                return dt;

            }

            catch (Exception ex)
            {

                return null;

            }
        }

        private void loadPurchaseReceiveForm()
        {
            try
            {
                GridViewRow row = gvlistofBasicData.SelectedRow;
                string poNo = row.Cells[1].Text.ToString();


                DataTable dt = new DataTable();

                string sql = @"SELECT        PRC.PurRecNo, PRC.RecDate AS RecDate, PRC.InvDate AS InvDate, PRC.Remarks, CAST(PRC.TotFOB AS DECIMAL(18, 2)) AS TotFOB, CAST(PRC.Freight AS DECIMAL(18, 2)) AS Freight, CAST(PRC.Insurance AS DECIMAL(18, 2)) 
                         AS Insurance, CAST(PRC.Others AS DECIMAL(18, 2)) AS Others, CAST(PRC.Discount AS DECIMAL(18, 2)) AS Discount, CAST(PRC.Deduction AS DECIMAL(18, 2)) AS Deduction, PRCD.OrderSLNo, PRCD.ProductCode, 
                         IT.ProductName, PRCD.RecQty, CAST(PRCD.PurPrice AS DECIMAL(18, 2)) AS PurPrice, CAST(PRCD.CostPrice AS DECIMAL(18, 2)) AS CostPrice, CAST(PRCD.RecQty * PRCD.PurPrice + PRCD.CostPrice AS DECIMAL(18, 2)) 
                         AS TotalPrice, PRCD.StoreCode, POD.ReceivedQty, POD.OrdQty, PRC.InvNo, PRC.PurOrderNo,CONVERT(VARCHAR(10), PurOrder.OrderDate, 103) as OrderDate
FROM            PurRec AS PRC INNER JOIN
                         PurRecProd AS PRCD ON PRC.PurRecNo = PRCD.PurRecNo INNER JOIN
                         Item AS IT ON PRCD.StoreCode = IT.StoreCode INNER JOIN
                         PurOrderProd AS POD ON PRCD.StoreCode = PRCD.StoreCode INNER JOIN
                         PurOrder ON PRC.PurOrderNo = PurOrder.PurOrderNo AND POD.PurOrderNo = PurOrder.PurOrderNo
                            where PRC.PurRecNo=@PurRecNo";

                SqlParameter[] parameter ={
                                              new SqlParameter("@PurRecNo",poNo )
                                            };

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    lblMessage.Text = "";
                    lblPORecNo.Text= dt.Rows[0]["PurRecNo"].ToString();

                    txtRecDate_CalendarExtender.SelectedDate =Convert.ToDateTime(dt.Rows[0]["RecDate"].ToString());
                    txtInvDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["InvDate"].ToString());
                    txtPODate.Text = dt.Rows[0]["OrderDate"].ToString();

                    txtInvNo.Text = dt.Rows[0]["InvNo"].ToString();
                    txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                    
                    txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                    txtPurOrderNo.Text = dt.Rows[0]["PurOrderNo"].ToString();
                    txtDeduction.Text = dt.Rows[0]["Deduction"].ToString();
                    gvPurRecEdit.DataSource = dt;
                    gvPurRecEdit.DataBind();
                    CheckBox cb = (CheckBox)gvPurRecEdit.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();
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

      

        private void savePurchaseRecive()
        {
            try
            {
                int ComCode_1 = 1;

                string PurRecNo_2 = lblPORecNo.Text;
                if (string.IsNullOrEmpty(lblPORecNo.Text))
                {
                    this.lblPORecNo.Text = PurchaseRec_maxID();
                    PurRecNo_2 = lblPORecNo.Text.Trim();
                }
                else
                {
                    PurRecNo_2 = lblPORecNo.Text.Trim();
                }
                string RecDate_3 = txtRecDate.Text;
                string PurOrderNo_4 = txtPurOrderNo.Text;
                string InvNo_5 = txtInvNo.Text;
                string InvDate_6 = txtInvDate.Text;
                string Remarks_7;
                if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
                {
                    Remarks_7 = "";
                }
                else
                {
                    Remarks_7 = txtRemarks.Text;
                }
                decimal TotFOB_8=0;
              
                decimal Freight_9=0;
                
                decimal Insurance_10=0;
                
                decimal Others_11=0;
                
                decimal Discount_12;
                if (String.IsNullOrEmpty(txtDiscount.Text))
                {
                    Discount_12 = 0;
                }
                else
                {
                    Discount_12 = Convert.ToDecimal(txtDiscount.Text);
                }

                decimal Deduction;
                if (String.IsNullOrEmpty(txtDeduction.Text))
                {
                    Deduction = 0;
                }
                else
                {
                    Deduction = Convert.ToDecimal(txtDeduction.Text);
                }
                string userId = "";

                DataTable dt = new DataTable();
                dt = GetDataTable(gvPurRecEdit);
                if (dt.Rows.Count > 0)
                {
                    
                    string roweffected = (new PurReceiveGateway().InsertUpdatePurReceive(ComCode_1, PurRecNo_2, RecDate_3, PurOrderNo_4, InvNo_5, InvDate_6, Remarks_7, TotFOB_8, Freight_9, Insurance_10, Others_11, Discount_12, Deduction,userId,dt));
                    lblPORecNo.Text = roweffected;
                    lblMessage.Text = "Saved";
                }
                else
                    lblMessage.Text = "Cannot Save";
               

            }
            catch (Exception ex)
            {
                lblPORecNo.Text = (ex.Message);

            }
        }
        private DataTable GetDataTable(GridView dtg)
        {
            DataTable DTAddToList = new DataTable();

           
            DTAddToList.Columns.Add(new DataColumn("ProductCode"));
            DTAddToList.Columns.Add(new DataColumn("ProductName"));
            DTAddToList.Columns.Add(new DataColumn("StoreCode"));
            DTAddToList.Columns.Add(new DataColumn("OrdQty"));
            DTAddToList.Columns.Add(new DataColumn("ReceivedQty"));
            DTAddToList.Columns.Add(new DataColumn("RecQty"));
            DTAddToList.Columns.Add(new DataColumn("PurPrice"));
            DTAddToList.Columns.Add(new DataColumn("CostPrice"));
            DTAddToList.Columns.Add(new DataColumn("TotalPrice"));


            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                if ((row.FindControl("ck") as CheckBox).Checked)
                {
                    DataRow DRLocal;
                    DRLocal = DTAddToList.NewRow();




                    DRLocal["ProductCode"] = row.Cells[0].Text.ToString();
                    DRLocal["ProductName"] = row.Cells[1].Text.ToString();

                    DRLocal["StoreCode"] = row.Cells[2].Text.ToString();
                    DRLocal["OrdQty"] = row.Cells[3].Text.ToString();
                    DRLocal["ReceivedQty"] = row.Cells[4].Text.ToString();
                    DRLocal["RecQty"] = (row.FindControl("txtRecQTY") as TextBox).Text;
                    DRLocal["PurPrice"] = (row.FindControl("txtPrice") as TextBox).Text;
                    DRLocal["CostPrice"] = (row.FindControl("txtCost") as TextBox).Text;
                    DRLocal["TotalPrice"] = (row.FindControl("txtTotalPrice") as TextBox).Text;


                    DTAddToList.Rows.Add(DRLocal);
                }

            }

            return DTAddToList;
        }

        private string PurchaseRec_maxID()
        {
            string CurrentYear = DateTime.Today.ToString("yyyy");
            String sql = @"select ISNULL(Max(Convert(integer,Right(PurRecNo,6))), 0) + 1 from PurRec where ComCode='1'and year(RecDate)=" + CurrentYear + "";

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            int count = int.Parse(dt.Rows[0][0].ToString());
            string NewReceiveNo = count.ToString().PadLeft(6, '0');

            string MrrNo = "MRR" + "-" + CurrentYear + NewReceiveNo;
            return MrrNo;

        }

       

        


        private void ckSelectAllItem()
        {
            CheckBox cb = (CheckBox)gvPurRecEdit.HeaderRow.FindControl("ckAll");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvPurRecEdit.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvPurRecEdit.Rows[i].FindControl("ck");
                        chkboxSelect.Checked = true;

                    }
                }
                else
                {
                    for (int i = 0; i < gvPurRecEdit.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvPurRecEdit.Rows[i].FindControl("ck");
                        chkboxUnselect.Checked = false;

                    }
                }
            }
            this.CountTotalprice();
            this.CounttotalQTY();
            this.CounttotalAMT();
        }

        private bool isMandatoryFieldValidate()
        {
            //if (String.IsNullOrEmpty(.Text))
            //    return false;
            //else
            return true;
        }
        private DataTable LoadPurchaseOrder(string searchKey)
        {
            try
            {
                DataTable dt;
               

                string sql = @"Select PO.PurOrderNo, PO.PINo as ReqNo,CONVERT(VARCHAR(10), PO.PIIssDate, 103) AS [ReqDate],CONVERT(VARCHAR(10), PO.OrderDate, 103) AS [OrderDate],PO.PurOrderDesc 
								FROM            PurOrder AS PO INNER JOIN
                  PurOrderProd AS POP ON PO.ComCode = POP.ComCode AND PO.PurOrderNo = POP.PurOrderNo
WHERE(POP.OrdQty - POP.ReceivedQty > 0) order by PO.OrderDate desc,PO.PurOrderNo desc ";
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
        private void LoadPurchaseOrderbyId()
        {
            try
            {
                GridViewRow row = gvPurchase.SelectedRow;
                string poNo = row.Cells[1].Text.ToString();


                DataTable dt = new DataTable();

                string sql = @"Select po.PurOrderNo,POP.OrderSLNo,POP.ProductCode,IT.ProductName, POP.StoreCode,POP.OrdQty,CAST((POP.PurPrice) AS DECIMAL(18,2)) AS PurPrice,CAST((POP.OrdQty*POP.PurPrice) AS DECIMAL(18,2)) AS TotalPrice,POP.ReceivedQty,POP.ReceivedQty as RecQty,POP.OrdQty, PO.OrderDate,'' as CostPrice from PurOrder PO Inner Join PurOrderProd POP on PO.ComCode=POP.ComCode and PO.PurOrderNo=POP.PurOrderNo
                           inner join Item IT on POP.ProductCode=IT.ProductCode and POP.StoreCode=IT.StoreCode
                           where PO.PurOrderNo=@purOrderNo and (POP.OrdQty-POP.ReceivedQty)>0";

                SqlParameter[] parameter ={
                                              new SqlParameter("@purOrderNo",poNo )
                                            };

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    lblMessage.Text = "";
                    //.Text = dt.Rows[0][0].ToString().Trim();
                   txtPurOrderNo.Text= dt.Rows[0]["PurOrderNo"].ToString().Trim();
                    txtPODate.Text= dt.Rows[0]["OrderDate"].ToString().Trim();

                    gvPurRecEdit.DataSource = dt;
                    gvPurRecEdit.DataBind();

                    CheckBox cb = (CheckBox)gvPurRecEdit.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();
                }
                else
                {


                    gvPurRecEdit.DataSource = null;
                    gvPurRecEdit.DataBind();
                    lblMessage.Text = "Data not found.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CounttotalAMT()
        {
            decimal SubTotal = Convert.ToDecimal(txtSubTotal.Text.Trim());
            decimal Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
            try
            {
                decimal TotalAMT = SubTotal - Discount;
                txtTotalAMT.Text = TotalAMT.ToString();
            }
            catch (Exception ex)
            {
            }

        }
        private void CounttotalQTY()
        {

            decimal totalQTY = 0;
            decimal newTotalQTY = 0;
            for (int i = 0; i < gvPurRecEdit.Rows.Count; i++)
            {
                CheckBox isChecked = ((CheckBox)gvPurRecEdit.Rows[i].FindControl("ck"));

                if (isChecked.Checked)
                {

                    if (string.IsNullOrEmpty(((TextBox)(gvPurRecEdit.Rows[i].FindControl("txtRecQTY"))).Text))
                    {
                        newTotalQTY = 0;

                    }
                    else
                    {
                        newTotalQTY = Convert.ToDecimal(((TextBox)(gvPurRecEdit.Rows[i].FindControl("txtRecQTY"))).Text);
                    }

                    try
                    {
                        totalQTY = totalQTY + newTotalQTY;
                    }
                    catch { }
                }
            }
            txtTotalQTY.Text = totalQTY.ToString();
        }


        private void CountTotalprice()
        {
            try
            {

                decimal totalAMT = 0;
                decimal newTotalAMT = 0;
                for (int i = 0; i < gvPurRecEdit.Rows.Count; i++)
                {
                    CheckBox isChecked = ((CheckBox)gvPurRecEdit.Rows[i].FindControl("ck"));
                    if (isChecked.Checked)
                    {
                        if (string.IsNullOrEmpty(((TextBox)(gvPurRecEdit.Rows[i].FindControl("txtTotalPrice"))).Text))
                        {
                            newTotalAMT = 0;

                        }
                        else
                        {
                            newTotalAMT = Convert.ToDecimal(((TextBox)(gvPurRecEdit.Rows[i].FindControl("txtTotalPrice"))).Text);
                        }

                        try
                        {
                            totalAMT = totalAMT + newTotalAMT;
                        }
                        catch { }
                    }
                }
                txtSubTotal.Text = totalAMT.ToString();

            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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

            foreach (Control control in Panel4.Controls)
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
            txtRecDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
            txtInvDate_CalendarExtender.SelectedDate = DateTime.Now.Date;


            gvPurRecEdit.DataSource = null;
            gvPurRecEdit.DataBind();
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




                    txtRecDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                    txtInvDate_CalendarExtender.SelectedDate = DateTime.Now.Date;



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
            dt = LoadPurchaseReceiveGrid(txtSearch.Text);

            //gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            savePurchaseRecive();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string RepotrName = "PurchaseReceive";
            Response.Redirect("reportViewerWorkshop.aspx?RID=" + lblPORecNo.Text.Trim() + "&RN=" + RepotrName);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadPurchaseReceiveGrid(txtSearch.Text);

            //gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadPurchaseReceiveGrid(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPurchaseReceiveForm();
        }

        

        protected void btnPurSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadPurchaseOrder(txtSearch.Text);

            //gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvPurchase.DataSource = dt;
            gvPurchase.DataBind();
            upPurchase.Update();
            hfShowPurGrid_ModalPopupExtender.Show();
        }

        protected void gvPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPurchaseOrderbyId();
        }

        protected void gvPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadPurchaseOrder(txtSearch.Text);

            gvPurchase.PageIndex = e.NewPageIndex;
            gvPurchase.DataSource = dt;
            gvPurchase.DataBind();
            upPurchase.Update();
            hfShowPurGrid_ModalPopupExtender.Show();
        }

     
       protected void txtDisParcent_TextChanged(object sender, EventArgs e)
        {
            decimal subtotal = 0;
            decimal paecent = 0;




            try
            {
                subtotal = Convert.ToDecimal(txtSubTotal.Text);
                paecent = Convert.ToDecimal(txtDisParcent.Text);

                decimal disTotalparcent = (subtotal / 100) * paecent;
                txtDiscount.Text = disTotalparcent.ToString();
                this.CounttotalAMT();
            }
            catch (Exception ex)
            {

            }

        }
    

        protected void txtDeduction_TextChanged(object sender, EventArgs e)
        {
            this.CountTotalprice();
            this.CounttotalQTY();
            this.CounttotalAMT();
        }

        protected void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            ckSelectAllItem();
        }

        protected void ck_CheckedChanged(object sender, EventArgs e)
        {
            this.CountTotalprice();
            this.CounttotalQTY();
            this.CounttotalAMT();
        }

        protected void txtRecQTY_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;

            decimal ReqQty = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtRecQty"))).Text);
            decimal ReqPrice = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtPrice"))).Text);
            //decimal cost = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtCost"))).Text);


            TextBox txtTotalValue = ((TextBox)gvRow.FindControl("txtTotalPrice"));


            try
            {
                txtTotalValue.Text = (((ReqQty * ReqPrice)).ToString());
                this.CountTotalprice();
                this.CounttotalQTY();

            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtCost_TextChanged(object sender, EventArgs e)
        {
            //TextBox txt = (TextBox)sender;

            //GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;

            //decimal ReqQty = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtRecQty"))).Text);
            //decimal ReqPrice = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtPrice"))).Text);
            ////decimal cost = Convert.ToDecimal(((TextBox)(gvRow.FindControl("txtCost"))).Text);


            //TextBox txtTotalValue = ((TextBox)gvRow.FindControl("txtTotalPrice0"));


            //try
            //{
            //    txtTotalValue.Text = (((ReqQty * ReqPrice)).ToString());
            //    this.CountTotalprice();
            //    this.CounttotalQTY();

            //}

            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
        }

        protected void btnPurchaseOrder_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadPurchaseOrder(txtSearch.Text);

            //gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvPurchase.DataSource = dt;
            gvPurchase.DataBind();
            upPurchase.Update();
            hfShowPurGrid_ModalPopupExtender.Show();
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            this.CounttotalAMT();
        }
    }
}