using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;
using System.IO;

namespace TransportManagerUI.UI
{
    public partial class DO : System.Web.UI.Page
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
                    //loadAllCustomer();
                    loadAllProduct();
                    getGhat();
                    lblInvoiceDate_CalendarExtender.SelectedDate = DateTime.Now;
                    //lblInvoiceDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    //ClearSession();
                    //FillChalanTypDDL();
                    //LoadAllDO("");
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQuantity.Text) == false && Convert.ToDecimal(txtQuantity.Text) > 0)
            {
                decimal qty = 0;
                decimal rate = 0;
                DataTable dt = new DataTable();
                DataTable pdt = new DataTable();
                dt.Columns.Add("ProductCode");
                dt.Columns.Add("ProductName");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("UnitPrice");
                
                dt.Columns.Add("TotalPrice");

                if (grdListOfProduct.Rows.Count <= 0)
                {

                    DataRow dr = dt.NewRow();
                    dr["ProductCode"] = ddlproductName.SelectedValue.ToString();
                    dr["ProductName"] = ddlproductName.SelectedItem.ToString();
                    qty = (String.IsNullOrEmpty(txtQuantity.Text)) ? qty = 0 : qty = Convert.ToDecimal(txtQuantity.Text);
                    rate = (String.IsNullOrEmpty(txtRate.Text)) ? rate = 0 : rate = Convert.ToDecimal(txtRate.Text);
                    dr["Quantity"] = qty;
                    dr["UnitPrice"] = rate;                    
                    dr["TotalPrice"] = (rate * qty);


                    dt.Rows.Add(dr);

                    grdListOfProduct.DataSource = dt;
                    grdListOfProduct.DataBind();
                    decimal Qty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Quantity"]));
                    decimal Amount = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]));

                    grdListOfProduct.FooterRow.Cells[1].Text = "Total";
                    grdListOfProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    grdListOfProduct.FooterRow.Cells[3].Text = Qty.ToString("N2");
                    grdListOfProduct.FooterRow.Cells[5].Text = Amount.ToString("N2");
                    //lblTotalQty.Text = String.Format("{0:N2}", Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Quantity"]))));
                    //lblTotalAmount.Text = String.Format("{0:N2}", Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]))));

                }
                else
                {
                    pdt = GetDataTable(grdListOfProduct);

                    DataRow dr = pdt.NewRow();
                    dr["ProductCode"] = ddlproductName.SelectedValue.ToString();
                    dr["ProductName"] = ddlproductName.SelectedItem.ToString();
                    qty = (String.IsNullOrEmpty(txtQuantity.Text)) ? qty = 0 : qty = Convert.ToDecimal(txtQuantity.Text);
                    rate = (String.IsNullOrEmpty(txtRate.Text)) ? rate = 0 : rate = Convert.ToDecimal(txtRate.Text);
                    dr["Quantity"] = qty;
                    dr["UnitPrice"] = rate;                    
                    dr["TotalPrice"] = (rate * qty);

                    pdt.Rows.Add(dr);
                    grdListOfProduct.DataSource = pdt;
                    grdListOfProduct.DataBind();

                    decimal Qty = pdt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Quantity"]));
                    decimal Amount = pdt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]));

                    grdListOfProduct.FooterRow.Cells[1].Text = "Total";
                    grdListOfProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    grdListOfProduct.FooterRow.Cells[3].Text = Qty.ToString("N2");
                    grdListOfProduct.FooterRow.Cells[5].Text = Amount.ToString("N2");
                    //decimal total = Convert.ToDecimal(dt.Compute("Sum(TotalPrice)", ""));
                    //txtTotalAmount.Text = total.ToString();

                }

                ddlproductName.ClearSelection();
                txtRate.Text = String.Empty;
                txtQuantity.Text = String.Empty;
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Quantity Cannot be Null or 0');", true);

         }

        protected void grdListOfProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDataTable(grdListOfProduct);

            dt.Rows[e.RowIndex].Delete();


            //decimal total = Convert.ToDecimal(dt.Compute("Sum(TotalPrice)", ""));
            //txtTotalAmount.Text = total.ToString();


            grdListOfProduct.DataSource = dt;
            grdListOfProduct.DataBind();

            decimal Qty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Quantity"]));
            decimal Amount = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]));

            grdListOfProduct.FooterRow.Cells[1].Text = "Total";
            grdListOfProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            grdListOfProduct.FooterRow.Cells[3].Text = Qty.ToString("N2");
            grdListOfProduct.FooterRow.Cells[5].Text = Amount.ToString("N2");
            //lblTotalQty.Text = String.Format("{0:N2}", Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Quantity"]))));
            //lblTotalAmount.Text = String.Format("{0:N2}", Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]))));


        }

        #region Private Methods
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
        DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 1; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    //if(i!=0)
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                
                DataRow dr;
                dr = dt.NewRow();

                //for (int i = 0; i < row.Cells.Count; i++)
                //{
                //    if (i != 0)
                dr["ProductCode"] = row.Cells[1].Text;
                dr["ProductName"] = row.Cells[2].Text;
                dr["Quantity"] = (row.FindControl("Label1") as Label).Text;
                dr["UnitPrice"] = row.Cells[4].Text;
                dr["TotalPrice"] = (row.FindControl("Label2") as Label).Text;
                //}
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void getGhat()
        {
            try
            {
                DataTable dt;


                using (StoreLocation gatwayObj = new StoreLocation())
                {


                    dt = gatwayObj.GetAllStoreLocation(1);
                    ddlGhatList.DataSource = dt;
                    ddlGhatList.DataValueField = "StoreCode";
                    ddlGhatList.DataTextField = "StoreName";
                    ddlGhatList.DataBind();

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);

            }
        }
        private DataTable loadAllCustInfo(string searchKey)
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

       
        /// <summary>
        /// Load All Product From Database
        /// </summary>
        private void loadAllProduct()
        {
            try
            {
                DataTable dt;


                using (ProductGateway gatwayObj = new ProductGateway())
                {

                    dt = gatwayObj.GetAllProduct();

                    ddlproductName.DataSource = dt;
                    ddlproductName.DataValueField = "ProductCode";
                    ddlproductName.DataTextField = "ProductName";
                    ddlproductName.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);

            }
        }
        /*
        private void loadProductInfo(int productCode)
        {
            try
            {
                DataTable dt;


                using (ProductGateway gatwayObj = new ProductGateway())
                {

                    dt = gatwayObj.GetProductById(productCode);

                    lblUnitPrice.Text = dt.Rows[0][];
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);

            }
        }*/

        private void LoadStatus()
        {
            Array itemValues = System.Enum.GetValues(typeof(Status));
            Array itemNames = System.Enum.GetNames(typeof(Status));

            for (int i = 0; i <= itemNames.Length - 1; i++)
            {
                ListItem item = new ListItem(itemNames.GetValue(i).ToString(), itemValues.GetValue(i).ToString());
                ddlStatus.Items.Add(item);
            }
        }

        private DataTable LoadAllDO(string searchKey)
        {
            try
            {
                DataTable dt;


                using (SalesGateway gatwayObj = new SalesGateway())
                {

                    dt = gatwayObj.GetAllSales();
                   

                    if (String.IsNullOrEmpty(searchKey))
                    {
                       
                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
  .Where(r => r.Field<String>("InvNo").ToUpper().Contains(searchKey.ToUpper())
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
            grdListOfProduct.DataSource = null;
            grdListOfProduct.DataBind();
           
        }
        private bool isMandatoryFieldValidate()
        {
            if (String.IsNullOrEmpty(lblCustomerCode.Text) || grdListOfProduct.Rows.Count<=0 ||ddlStatus.SelectedValue!="0")
                return false;

            else
                return true;
        }
        #endregion

        protected void ddlproductName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdListOfProduct_DataBound(object sender, EventArgs e)
        {
         //   double total = grdListOfProduct.Rows.Cast<GridViewRow>()
         //.Sum(r => double.Parse(((Label)r.FindControl("TotalPrice")).Text));
         //   txtTotalAmount.Text = total.ToString();
        }

       

        protected void btnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtSearchCustomer.Text);
            gvCustomer.DataSource = dt;
            gvCustomer.DataBind();


            hidCustomerData_ModalPopupExtender.Show();
        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtSearchCustomer.Text);

            gvCustomer.PageIndex = e.NewPageIndex;
            gvCustomer.DataSource = dt;
            gvCustomer.DataBind();
            upCustomer.Update();
            hidCustomerData_ModalPopupExtender.Show();
        }

        protected void gvCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvCustomer.SelectedRow;
            lblCustomerCode.Text = row.Cells[1].Text;
            lblCustomerName.Text = row.Cells[2].Text;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt=LoadAllDO(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        //decimal gvSumProductQty, gvSumAmount;
        protected void grdListOfProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    gvSumProductQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantity"));
            //    gvSumAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalPrice"));
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblqty = (Label)e.Row.FindControl("lblSumQty");
            //    Label lblAmount = (Label)e.Row.FindControl("lblTotalAmount");
            //    lblqty.Text = gvSumProductQty.ToString();
            //    lblAmount.Text = gvSumAmount.ToString();
            //}
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllDO(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllDO(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtSearchCustomer.Text);
            gvCustomer.DataSource = dt;
            gvCustomer.DataBind();
            
            hidCustomerData_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMandatoryFieldValidate())
                {
                    int dmlType = 1;
                    int ComCode_1 = 1;
                    int storeCode = Convert.ToInt32(ddlGhatList.SelectedValue);
                    string invNo_2 = lblInvoiceNo.Text;
                    string DoNo = txtManualDoNo.Text;
                    string invDate_3 = lblInvoiceDate.Text;
                    string chalanNo_4 = String.Empty;

                    string chalanDate_5 = DateTime.Now.ToShortDateString();
                    string custId_6 = lblCustomerCode.Text;
                    decimal invAmount_7 = Convert.ToDecimal(grdListOfProduct.FooterRow.Cells[5].Text);
                    decimal discount_8 = 0;
                    int payMode_9 = Convert.ToInt32(ddlPaymentMode.SelectedValue);
                    string remarks_10 = txtRemarks.Text;
                    int invStatus_11 = Convert.ToInt32(ddlStatus.SelectedValue);
                    string userCode_12 = Session["UserName"].ToString();
                    decimal totalQty_14 = Convert.ToDecimal(grdListOfProduct.FooterRow.Cells[3].Text);
                    DataTable dt = new DataTable();
                    dt = GetDataTable(grdListOfProduct);
                    dt.Columns.Add("Discount");
                    using (SalesGateway gatwayObj = new SalesGateway())
                    {
                        string doNo = gatwayObj.InsertUpdateSales(dmlType, ComCode_1, storeCode, invNo_2, invDate_3,
                            chalanNo_4, chalanDate_5, custId_6, invAmount_7, discount_8, payMode_9, remarks_10, invStatus_11, userCode_12, DoNo, totalQty_14, dt);
                        lblInvoiceNo.Text = doNo;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('record Save');", true);
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Mandatory Field');", true);
                //clearAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = gvlistofBasicData.SelectedRow;
            string doNo=rows.Cells[1].Text;
            DataTable dt = new DataTable();
            using (SalesGateway gatwayObj=new SalesGateway())
            {
                dt = gatwayObj.GetSalesById(1, doNo);
            }

            lblInvoiceNo.Text = dt.Rows[0]["InvNo"].ToString();
            txtManualDoNo.Text = dt.Rows[0]["DONo"].ToString();
            lblInvoiceDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["InvDate"]).Date;
            lblCustomerCode.Text = dt.Rows[0]["CustId"].ToString();
            lblCustomerName.Text = dt.Rows[0]["CustName"].ToString();
            ddlPaymentMode.SelectedValue = dt.Rows[0]["Paymode"].ToString();
            ddlGhatList.SelectedValue = dt.Rows[0]["StoreCode"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            ddlStatus.SelectedValue = dt.Rows[0]["InvStatus"].ToString();

            DataTable loadDoDetail = new DataTable();
            using (SalesProductGateway gatwayObj = new SalesProductGateway())
            {
                loadDoDetail = gatwayObj.GetSalesProductBySales(1, doNo);
            }
            grdListOfProduct.DataSource = loadDoDetail;
            grdListOfProduct.DataBind();
            decimal Qty = loadDoDetail.AsEnumerable().Sum(x => Convert.ToDecimal(x["Quantity"]));
            decimal Amount = loadDoDetail.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]));

            grdListOfProduct.FooterRow.Cells[1].Text = "Total";
            grdListOfProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            grdListOfProduct.FooterRow.Cells[3].Text = Qty.ToString("0");
            grdListOfProduct.FooterRow.Cells[5].Text = Amount.ToString("N2");
            
            hfShowList_ModalPopupExtender.Hide();
            
        }

        protected void btnSearchOk_Click(object sender, EventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblInvoiceNo.Text)==false)
            {
                Session["paramData"] = null;
                Session["reportOn"] = null;
                string dono = lblInvoiceNo.Text;

                string reporton = "DO";

                Session["paramData"] = dono;
                Session["reportOn"] = reporton;
                //btnReport.PostBackUrl = "~/UI/reportViewer.aspx";
                Response.Redirect("~/UI/reportViewer.aspx");
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Please Select DO No');", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}