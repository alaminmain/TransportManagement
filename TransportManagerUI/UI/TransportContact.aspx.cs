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
    public partial class TransportContact : System.Web.UI.Page
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
                    txtTCDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                    
                    txtTCTime.Text = DateTime.Now.ToString("hh:mm:ss tt");

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
            try
            {
                DataTable dt = new DataTable();
                dt = LoadTransportContact(txtSearch.Text);
                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                
                hfShowList_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnSearchOrder_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllDO(txtSearch.Text);
            gvListofDO.DataSource = dt;
            gvListofDO.DataBind();

            hidSelectDO_ModalPopupExtender.Show();
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
            txtTCDate_CalendarExtender.SelectedDate = DateTime.Now.Date;

            txtTCTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            gvListofDOProduct.DataSource = null;
            gvListofDOProduct.DataBind();
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
        private void  DtinSession()
        {
            DataTable dt = new DataTable();
            List<string> DOIdList = new List<string>();
            string doId = String.Empty;
            dt.Columns.Add("DONo");

            DataTable productDt = new DataTable();
            productDt.Columns.Add("InvNo");
            productDt.Columns.Add("ProductCode");
            productDt.Columns.Add("ProductName");
            productDt.Columns.Add("DOQty");
            productDt.Columns.Add("OrderQty");
            
            productDt.Columns.Add("Rent");
            productDt.Columns.Add("TotalPrice");
            foreach (GridViewRow item in gvListofDO.Rows)
            {
                if ((item.Cells[0].FindControl("cbSelect") as CheckBox).Checked)
                {
                    DataRow dr = dt.NewRow();
                    doId = item.Cells[1].Text;
                    DOIdList.Add(doId);
                }


            }
            if (DOIdList != null)
            {
                foreach (string id in DOIdList)
                {
                    DataTable DoDetail = new DataTable();
                    using (SalesProductGateway gatwayObj = new SalesProductGateway())
                    {
                        DoDetail = gatwayObj.GetSalesProductBySales(1,id);

                        foreach (DataRow r in DoDetail.Rows)
                        {
                            DataRow dr = productDt.NewRow();
                            dr["InvNo"] = r["InvNo"].ToString();
                            dr["ProductCode"] = r["ProductCode"].ToString();
                            dr["ProductName"] = Convert.ToString(r["ProductName"]);
                            dr["DOQty"] = r["Quantity"].ToString();
                            dr["OrderQty"] = r["Quantity"].ToString();
                            dr["Rent"] = r["UnitPrice"].ToString();
                            dr["TotalPrice"] = Convert.ToDecimal(r["Quantity"]) * Convert.ToDecimal(r["UnitPrice"]);
                            productDt.Rows.Add(dr);
                        }
                    }
                }
            }
            Session["myDatatable"] = productDt;
            

        }
        DataTable GetProductDataTable(GridView dtg)
        {
            try
            {
                DataTable dt = new DataTable();

                // add the columns to the datatable            
                if (dtg.HeaderRow != null)
                {

                    for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
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
                    string lblqty= (row.FindControl("lblDoQty") as Label).Text;
                    string OrdQty = (row.FindControl("TextBox1") as TextBox).Text;
                    if (Convert.ToDecimal(OrdQty)<=Convert.ToDecimal(lblqty)  )
                    {
                        //for (int i = 0; i < row.Cells.Count; i++)
                        //{
                        //    if (i != 0)
                        //row.FindControl("")
                        dr["DoNo"] = row.Cells[1].Text;
                        dr["ProductCode"] = row.Cells[2].Text;
                        dr["ProductName"] = row.Cells[3].Text;
                        dr["DOQty"] = (row.FindControl("lblDoQty") as Label).Text;
                        dr["OrderQty"] = (row.FindControl("TextBox1") as TextBox).Text; //row.Cells[4].Text; ;
                        dr["Rent"] = (row.FindControl("txtRent") as TextBox).Text; //row.Cells[5].Text;
                                                                                   //dr["TotalPrice"] = String.Empty;

                        //}
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Order Qty is Greater Then DO Qty');", true);
                    }
                }
                return dt;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        private DataTable ProductList()
        {
            DataTable dt = new DataTable();
            List<string> DOIdList = new List<string>();
            string doId = String.Empty;
            dt.Columns.Add("DONo");

            DataTable productDt = new DataTable();
            productDt.Columns.Add("InvNo");
            productDt.Columns.Add("ProductCode");
            productDt.Columns.Add("ProductName");
            productDt.Columns.Add("DOQty");
            productDt.Columns.Add("OrderQty");
            productDt.Columns.Add("Rent");
            productDt.Columns.Add("TotalPrice");

            foreach (GridViewRow item in gvListofDO.Rows)
            {
                if ((item.Cells[0].FindControl("cbSelect") as CheckBox).Checked)
                {
                    DataRow dr = dt.NewRow();
                    doId = item.Cells[1].Text;
                    DOIdList.Add(doId);
                }


            }
            if (DOIdList != null)
            {
                foreach (string id in DOIdList)
                {
                    DataTable DoDetail = new DataTable();
                    using (SalesProductGateway gatwayObj = new SalesProductGateway())
                    {
                        DoDetail = gatwayObj.GetSalesProductBySales(1, id);

                        foreach (DataRow r in DoDetail.Rows)
                        {
                            DataRow dr = productDt.NewRow();
                            dr["InvNo"] = r["InvNo"].ToString();
                            dr["ProductCode"] = r["ProductCode"].ToString();
                            dr["ProductName"] = Convert.ToString(r["ProductName"]);
                            dr["DOQty"] = r["PendingQty"].ToString();
                            dr["OrderQty"] = r["PendingQty"].ToString();
                            dr["Rent"] = r["UnitPrice"].ToString();
                            dr["TotalPrice"] = Convert.ToDecimal(r["Quantity"]) * Convert.ToDecimal(r["UnitPrice"]);
                            productDt.Rows.Add(dr);
                        }
                    }
                }
            }
            //Session["myDatatable"] = productDt;
            return productDt;

        }
        private DataTable ProductList(DataTable Detail)
        {
            DataTable dt = new DataTable();
            List<string> DOIdList = new List<string>();
            string doId = String.Empty;
            dt.Columns.Add("DONo");

            DataTable productDt = new DataTable();
            productDt.Columns.Add("InvNo");
            productDt.Columns.Add("ProductCode");
            productDt.Columns.Add("ProductName");
            productDt.Columns.Add("DOQty");
            productDt.Columns.Add("OrderQty");
            productDt.Columns.Add("Rent");
            productDt.Columns.Add("TotalPrice");

            foreach (DataRow item in Detail.Rows)
            {

                    doId = item["InvNo"].ToString();
                    DOIdList.Add(doId);
             
            }
            if (DOIdList != null)
            {
                foreach (string id in DOIdList)
                {
                    DataTable DoDetail = new DataTable();
                    using (SalesProductGateway gatwayObj = new SalesProductGateway())
                    {
                        DoDetail = gatwayObj.GetSalesProductBySales(1, id);

                        foreach (DataRow r in DoDetail.Rows)
                        {
                            foreach (DataRow item in Detail.Rows)
                            {
                                DataRow dr = productDt.NewRow();
                                if(r["InvNo"].ToString()== item["InvNo"].ToString() && r["ProductCode"].ToString()== item["ProductCode"].ToString())
                                {
                                    dr["InvNo"] = item["InvNo"].ToString();
                                    dr["ProductCode"] = item["ProductCode"].ToString();
                                    dr["ProductName"] = Convert.ToString(item["ProductName"]);
                                    dr["DOQty"] = r["PendingQty"].ToString();
                                    dr["OrderQty"] = item["OrderQty"].ToString();
                                    dr["Rent"] = item["Rent"].ToString();
                                    dr["TotalPrice"] = Convert.ToDecimal(item["OrderQty"]) * Convert.ToDecimal(item["Rent"]);
                                    productDt.Rows.Add(dr);
                                }
                               
                                
                            }
                          
                            
                        }
                    }
                }
            }
            //Session["myDatatable"] = productDt;
            return productDt;

        }
        private DataTable LoadTransportContact(string searchKey)
        {
            try
            {
                DataTable dt;


                using (TransContactGateway gatwayObj = new TransContactGateway())
                {

                    dt = gatwayObj.GetAllTransContactForGridView(1);

                    if (String.IsNullOrEmpty(searchKey))
                    {
                        
                       
                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("TCNo").Contains(searchKey) ||
    r.Field<String>("CustName").Contains(searchKey));
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

        private DataTable loadAllCustInfo(string searchKey)
        {
            try
            {
                DataTable dt;


                using (DealerGateway gatwayObj = new DealerGateway())
                {

                    dt = gatwayObj.Get_All_Customer();

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
        private DataTable loadAllCustInfo(string searchKey,string DealerId)
        {
            try
            {
                DataTable dt;


                using (DealerGateway gatwayObj = new DealerGateway())
                {

                    dt = gatwayObj.Get_All_CustomerByDealerId(DealerId);

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
        private DataTable LoadAllDO(string searchKey)
        {
            try
            {
                DataTable dt;


                using (SalesGateway gatwayObj = new SalesGateway())
                {

                    dt = gatwayObj.GetAllSalesByDealer(1,lblDealerCode.Text);


                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("InvNo").Contains(searchKey)
    || r.Field<String>("DealerName").Contains(searchKey));
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

        private bool isMandatoryFieldValidate()
        {
            if (String.IsNullOrEmpty(lblDealerCode.Text) || String.IsNullOrEmpty(lblCustomerCode.Text))
                return false;
            else if (gvListofDOProduct.Rows.Count <= 0)
              return false;
            

            else
                return true;
        }
        
        #endregion

        protected void btnSearchD_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadTransportContact(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofTC_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            LoadTransportContact(txtSearch.Text);
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSearchDo_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllDO(txtSearch.Text);
            gvListofDO.DataSource = dt;
            gvListofDO.DataBind();
            hidSelectDO_ModalPopupExtender.Show();
            
        }

        protected void btnSelectCustomer_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtSearchCustomer.Text,lblDealerCode.Text);
            gvCustomer.DataSource = dt;
            gvCustomer.DataBind();
            hidSearchdCustomer_ModalPopupExtender.Show();

        }
        protected void btnDealerShow_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllDealerInfo(txtDealerSearch.Text);
            gvDealerSearch.DataSource = dt;
            gvDealerSearch.DataBind();
            hidSearchdDealer_ModalPopupExtender.Show();
        }

        protected void gvDealerSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvDealerSearch.SelectedRow;
            lblDealerName.Text = row.Cells[2].Text;
            lblDealerCode.Text = row.Cells[1].Text;
        }

        protected void gvCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvCustomer.SelectedRow;
            lblCustomerName.Text = row.Cells[2].Text;
            lblCustomerCode.Text = row.Cells[1].Text;
        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtSearchCustomer.Text,lblDealerCode.Text);

            gvCustomer.PageIndex = e.NewPageIndex;
            gvCustomer.DataSource = dt;
            gvCustomer.DataBind();
            upCustomer.Update();
            hidSearchdCustomer_ModalPopupExtender.Show();

            
        }

        protected void gvDealerSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

                DataTable dt = new DataTable();
                dt = loadAllDealerInfo(txtDealerSearch.Text);

                gvDealerSearch.PageIndex = e.NewPageIndex;
                gvDealerSearch.DataSource = dt;
                gvDealerSearch.DataBind();
                UpdatePanel3.Update();
                hidSearchdDealer_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadTransportContact(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnDealerSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllDealerInfo(txtDealerSearch.Text);
            gvDealerSearch.DataSource = dt;
            gvDealerSearch.DataBind();
            hidSearchdDealer_ModalPopupExtender.Show();
        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtSearchCustomer.Text,lblDealerCode.Text);
            gvCustomer.DataSource = dt;
            gvCustomer.DataBind();
            hidSearchdCustomer_ModalPopupExtender.Show();
        }
       
       
        protected void btnDoOk_Click(object sender, EventArgs e)
        {
                Session["myDatatable"] = null;
                hidSelectDO_ModalPopupExtender.Hide();
                //DtinSession();
                //DataTable dt = (DataTable)Session["myDatatable"];
                DataTable dt = ProductList();
                gvListofDOProduct.DataSource = dt;
                gvListofDOProduct.DataBind();
                decimal total = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
            gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
            gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            TextBox txttotal = (TextBox)gvListofDOProduct.FooterRow.FindControl("txtTotalQty");
           txttotal.Text= total.ToString("0.00");
           
            //gvListofDOProduct.FooterRow.Cells[4].Text = total.ToString("0.00");
        }

        protected void gvListofDO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadTransportContact(txtSearch.Text);

            gvListofDO.PageIndex = e.NewPageIndex;
            gvListofDO.DataSource = dt;
            gvListofDO.DataBind();
            UpdatePanel1.Update();
            hidSelectDO_ModalPopupExtender.Show();
        }

       

        protected void gvListofDOProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //try
            //{
            //    gvListofDOProduct.EditIndex = e.NewEditIndex;
            //    DataTable dt = (DataTable)Session["myDatatable"];
            //    DataTable dt2 = new DataTable();
            //    DataView dv = new DataView(dt);
            //    dt2 = dv.ToTable(false, "InvNo", "ProductCode", "ProductName", "OrderQty", "UnitPrice", "TotalPrice");
               
            //    gvListofDOProduct.DataSource = dt2;
            //    gvListofDOProduct.DataBind();

            //    decimal total = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
            //    gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
            //    gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            //    gvListofDOProduct.FooterRow.Cells[2].Text = total.ToString("N2");
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }


        protected void gvListofDOProduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //gvListofDOProduct.EditIndex = -1;
            //    DataTable dt = (DataTable)Session["myDatatable"];
            ////Session["myDatatable"] = dt;
            //gvListofDOProduct.DataSource = dt;
            //gvListofDOProduct.DataBind();
        }

      

        protected void gvListofDOProduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)gvListofDOProduct.Rows[e.RowIndex];
                int DoQty= Convert.ToInt32(row.Cells[4].Text);
                string updateQty = (row.FindControl("TextBox1") as TextBox).Text;//(row.Cells[5].Controls[0] as TextBox).Text;

                DataTable dt = (DataTable)Session["myDatatable"];
                dt.Rows[row.RowIndex]["OrderQty"] = updateQty;
                if (Convert.ToInt32(updateQty) <= DoQty)
                {
                    Session["myDatatable"] = dt;
                    gvListofDOProduct.EditIndex = -1;

                    gvListofDOProduct.DataSource = dt;
                    gvListofDOProduct.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Order qty can not be greater than DO Qty');", true);
                    dt.Rows[row.RowIndex]["OrderQty"] = DoQty.ToString();
                }
                decimal totalqty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"])); 
                
                gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
                gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                TextBox txttotal = (TextBox)gvListofDOProduct.FooterRow.FindControl("txtTotalQty");
                txttotal.Text = totalqty.ToString("0.00");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                
            }
        }

        protected void gvListofDOProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = ProductList();
           

            dt.Rows[e.RowIndex].Delete();


            //decimal total = Convert.ToDecimal(dt.Compute("Sum(TotalPrice)", ""));
            //txtTotalAmount.Text = total.ToString();


            gvListofDOProduct.DataSource = dt;
            gvListofDOProduct.DataBind();
            decimal total = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
            gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
            gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            TextBox txttotal = (TextBox)gvListofDOProduct.FooterRow.FindControl("txtTotalQty");
            txttotal.Text = total.ToString("0.00");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMandatoryFieldValidate())
                {
                    int comCode_1 = 1;
                    string tC_No_2 = lblTcNo.Text;
                    string Time = DateTime.Now.ToString("hh:mm:ss tt");
                    string tcDate_3 = txtTCDate.Text + " " + Time;
                    int storeCode_4 = Convert.ToInt32(ddlGhatList.SelectedValue);
                    string dealerId_5 = lblDealerCode.Text;
                    string custId_6 = lblCustomerCode.Text;
                    int payMode_7 = Convert.ToInt32(ddlPaymentMode.SelectedValue);
                    string Remarks_8 = txtRemarks.Text;
                    int tcStatus_9 = Convert.ToInt32(ddlStatus.SelectedValue);
                    string user_code_10 = Session["UserName"].ToString();//it will replace with Session User
                    DataTable transContactDetail = new DataTable();
                    transContactDetail = GetProductDataTable(gvListofDOProduct);
                    if (transContactDetail.Rows.Count > 0)
                    {
                        decimal totalQty = transContactDetail.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                        gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
                        gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        TextBox txttotal = (TextBox)gvListofDOProduct.FooterRow.FindControl("txtTotalQty");
                        txttotal.Text = totalQty.ToString("0.00");

                        using (TransContactGateway gatwayObj = new TransContactGateway())
                        {
                            string tc_no = gatwayObj.InsertUpdateTransContact(comCode_1, tC_No_2, tcDate_3, storeCode_4, dealerId_5, custId_6, payMode_7, Remarks_8, tcStatus_9, user_code_10, totalQty, transContactDetail);
                            lblTcNo.Text = tc_no;
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('record Saved');", true);
                        //clearAll();
                    }
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Mandatory Field');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearAll();
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string tc = rows.Cells[1].Text;
                DataTable dt = new DataTable();

                using (TransContactGateway gatwayObj = new TransContactGateway())
                {
                    dt = gatwayObj.GetAllTransContact(1, tc);
                }
                //ComCode, TCNo, TCDate, StoreCode, DealerId, CustId, Paymode, Remarks, TCStatus
                lblTcNo.Text = dt.Rows[0]["TCNo"].ToString();
                txtTCDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["TCDate"]).Date;
                txtTCTime.Text = Convert.ToDateTime(dt.Rows[0]["TCDate"]).ToString("hh:mm:ss tt");
                //txtTCDate.Text = Convert.ToDateTime(dt.Rows[0]["TCDate"]).Date.ToString("dd/MMM/yyyy");
                lblDealerCode.Text = dt.Rows[0]["DealerId"].ToString();
                lblDealerName.Text = dt.Rows[0]["DealerName"].ToString();
                lblCustomerCode.Text = dt.Rows[0]["CustId"].ToString();
                lblCustomerName.Text = dt.Rows[0]["CustomerName"].ToString();
                ddlPaymentMode.SelectedValue = dt.Rows[0]["Paymode"].ToString();
                ddlGhatList.SelectedValue = dt.Rows[0]["StoreCode"].ToString();
                txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                ddlStatus.SelectedValue = dt.Rows[0]["TCStatus"].ToString();
                Session.Remove("myDatatable");
                using (TransContactDetailGateway gatwayObj = new TransContactDetailGateway())
                {
                    dt = gatwayObj.GetTransContactDetail(1, tc);
                }
                dt=ProductList(dt);
                gvListofDOProduct.DataSource = dt;
                gvListofDOProduct.DataBind();
                
                decimal total = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
                gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                TextBox txttotal = (TextBox)gvListofDOProduct.FooterRow.FindControl("txtTotalQty");
                txttotal.Text = total.ToString("0.00");
                //gvListofDOProduct.FooterRow.Cells[4].Text = total.ToString("N2");
                //Session["myDatatable"] = dt;
                hfShowList_ModalPopupExtender.Hide();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblTcNo.Text)==false)
            {
                Session["paramData"] = null;
                Session["reportOn"] = null;
                string tcno = lblTcNo.Text;

                string reporton = "TC";

                Session["paramData"] = tcno;
                Session["reportOn"] = reporton;
                //btnReport.PostBackUrl = "~/UI/reportViewer.aspx";
                Response.Redirect("~/UI/reportViewer.aspx");
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Please Select TC No ');", true);
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
            hfState.Value = "1";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}