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
    public partial class TripAdvInfo : System.Web.UI.Page
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
                //if (Session[KGC_CONSTANTS.CURRENT_USER] == null)
                //{
                //    Session.Abandon(); Response.Redirect("NotLoggedIn.htm");
                //}
                //lblMessage.Text = string.Empty;
                if (!IsPostBack && !IsCallback)
                {
                    getGhat();
                    LoadVehicleAgent();
                    txtTripDate_CalendarExtender.SelectedDate = DateTime.Now;
                    loadAllProduct();
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
        
        private DataTable GetDriver(string searchKey)
        {
            try
            {
                DataTable dt;


                using (PersonalGateway gatwayObj = new PersonalGateway())
                {


                    dt = gatwayObj.GetAllDriver();
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

        private DataTable GetVehicle(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {
                    
                    int vehicleHiredStatus=0;
                    if (ddlAgent.SelectedValue != "00001")
                        vehicleHiredStatus=1;
                    else
                       vehicleHiredStatus=0;
                    dt2 = gatwayObj.GetAllVehicle(1);
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[VehicleStatus]= '0' AND [IsHired]='"+vehicleHiredStatus+ "'");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("VehicleID").Contains(searchKey) || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()));
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

     
        private void GetVehicle(int vehicleId)
        {
            try
            {
                DataTable dt;


                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {


                    dt = gatwayObj.GetAllVehicle(1);


                    gvVehicleList.DataSource = dt;

                    gvVehicleList.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);

            }
        }

        private DataTable LoadAllTrip(string searchKey)
        {
            try
            {
                DataTable dt;


                using (TripAdvInfoGateway gatwayObj = new TripAdvInfoGateway())
                {


                    dt = gatwayObj.GetAllTripInfoForGridView();
                    if (String.IsNullOrEmpty(searchKey))
                    {
                        
                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("TripAdvNo").Contains(searchKey));
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

        public void loadTripInfo(string tripNo)
        {
            try
            {
                DataTable dtTrip = new DataTable();
                DataTable dt = new DataTable();
                using (TripAdvInfoGateway gatwayObj = new TripAdvInfoGateway())
                {
                    dtTrip = gatwayObj.GetTripAdvInfo(tripNo);
                    dt = gatwayObj.GetTripDetailByTrip(1, tripNo);
                }
                //SELECT TripAdvInfo.ComCode, TripAdvInfo.StoreCode, TripAdvInfo.TripAdvNo, TripAdvInfo.TripAdvDate, TripAdvInfo.TransportBy, TripAdvInfo.AgentID, TripAdvInfo.VehicleID, TripAdvInfo.EmpCode, "
                //  + " TripAdvInfo.Capacity, TripAdvInfo.KmPerLiter, TripAdvInfo.FuelSlipNo, TripAdvInfo.SupplierName, TripAdvInfo.FuelCode, TripAdvInfo.FuelRate, TripAdvInfo.FuelQty,TripAdvInfo.AdjFuelQty, TripAdvInfo.Remarks, "
                //  + " TripAdvInfo.Totalkm, TripAdvInfo.Additionalkm, TripAdvInfo.CapacityBal, TripAdvInfo.TripStatus, VehicleInfo.VehicleNo, Personal.EmpName,Personal.Mobile
                lblTripNo.Text = dtTrip.Rows[0]["TripAdvNo"].ToString();
                txtTripDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dtTrip.Rows[0]["TripAdvDate"]).Date;
                ddlTransportBy.SelectedValue = dtTrip.Rows[0]["TransportBy"].ToString();
               
                hfVehicleSearch.Value= dtTrip.Rows[0]["VehicleID"].ToString();
                
                lblCapacity.Text = dtTrip.Rows[0]["VehicleID"].ToString()+" "+ dtTrip.Rows[0]["VehicleNo"].ToString();
                lblDriverCode.Text = dtTrip.Rows[0]["EmpCode"].ToString();
                lblDriverName.Text = dtTrip.Rows[0]["EmpName"].ToString();
                ddlAgent.SelectedValue = dtTrip.Rows[0]["AgentID"].ToString();

                txtRemarks.Text = dtTrip.Rows[0]["Remarks"].ToString();
                ddlStatus.SelectedValue = dtTrip.Rows[0]["TripStatus"].ToString();
                //if(String.IsNullOrEmpty(dtTrip.Rows[0]["Hired"].ToString()))
                //    chkIsHired.Checked=false;
                //else if(dtTrip.Rows[0]["Hired"].ToString()=="1")
                //    chkIsHired.Checked=true;
                //else
                //    chkIsHired.Checked=false;
                grdListOfProduct.DataSource = dt;
                    grdListOfProduct.DataBind();
                    Session["myDatatable"] = dt;
                
                decimal totalqty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Quantity"]));
                decimal totalAmount = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]));
                grdListOfProduct.FooterRow.Cells[1].Text = "Total";
                grdListOfProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                grdListOfProduct.FooterRow.Cells[3].Text = totalqty.ToString("0");
                grdListOfProduct.FooterRow.Cells[5].Text = totalAmount.ToString("0.00");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
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
        private void LoadVehicleAgent()
        {
            try
            {
                DataTable dt;


                using (VehicleAgentGateway gatwayObj = new VehicleAgentGateway())
                {


                    dt = gatwayObj.GetAllAgent();
                    ddlAgent.DataSource = dt;
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataTextField = "AgentName";
                    ddlAgent.DataBind();

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                
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
                grdListOfProduct.DataSource = null;
                grdListOfProduct.DataBind();

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
                dr["Rent"] = row.Cells[4].Text;
                dr["TotalPrice"] = (row.FindControl("Label2") as Label).Text;
                //}
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private bool isMandatoryFieldValidate()
        {
            if (String.IsNullOrEmpty(hfVehicleSearch.Value))
                return false;
            else if (String.IsNullOrEmpty(lblDriverCode.Text))
                return false;
            else if (grdListOfProduct.Rows.Count <= 0)
                return false;
            //else if (ddlStatus.SelectedValue!="0")
            //    return false;
            else
                return true;
        }
        #endregion
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMandatoryFieldValidate())
                {
                    DataTable dtVehicle = new DataTable();
                    DataTable fuel = new DataTable();
                    GridViewRow row = gvVehicleList.SelectedRow;
                    int comcode_1 = 1;
                    int storeCode_2 = Convert.ToInt32(ddlGhatList.SelectedValue);
                    string tripAdvNo_3 = lblTripNo.Text;
                    string tripAdvDate_4 = txtTripDate.Text+" "+DateTime.Now.ToShortTimeString();
                    int transportBy_5 = Convert.ToInt32(ddlTransportBy.SelectedValue);
                    string agentId_6 = ddlAgent.SelectedValue;
                    string vehicleId_7 = hfVehicleSearch.Value;
                    string empCode_8 = lblDriverCode.Text;
                    //vehicle Fuel Info
                    using (VehicleInfoGateway vf = new VehicleInfoGateway())
                    {
                        dtVehicle = vf.GetVehicleById(1, vehicleId_7);
                    }
                    string capacity_9 = dtVehicle.Rows[0]["Capacity"].ToString(); //row.Cells[3].Text;
                    decimal kmPerLiter_10 = Convert.ToDecimal(dtVehicle.Rows[0]["KmPerLiter"]); //Convert.ToDecimal(row.Cells[4].Text);
                   


                    string fuelCode_11 = dtVehicle.Rows[0]["FuelCode"].ToString();
                    using (FuelInfoGateway fi = new FuelInfoGateway())
                    {
                        fuel = fi.get_Fuel_Info(fuelCode_11);
                    }
                    decimal? fuelRate_12 = Convert.ToDecimal(fuel.Rows[0]["Rate"]);
                   
                    string Remarks_13 = txtRemarks.Text;
                  
                    int tripStatus_14 = Convert.ToInt32(ddlStatus.SelectedValue);
                    string userCode_15 = Session["UserName"].ToString();
                    
                   
                    //DataTable dt = new DataTable();
                    //dt = LoadAllTrip(lblTripNo.Text);

                    DataTable dt = new DataTable();
                    dt = GetDataTable(grdListOfProduct);
                    int CapacityBal_17 = (int)dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Quantity"]));
                    using (TripAdvInfoGateway gatwayObj = new TripAdvInfoGateway())
                    {
                        string tripno = gatwayObj.InsertUpdateTripAdvInfo(comcode_1, storeCode_2, tripAdvNo_3, tripAdvDate_4, transportBy_5,
                    agentId_6, vehicleId_7, empCode_8, capacity_9, kmPerLiter_10, fuelCode_11, fuelRate_12, Remarks_13,
                     tripStatus_14, userCode_15, CapacityBal_17, dt);

                        lblTripNo.Text = tripno;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
                    //clearAll();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Mandatory Field');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = LoadAllTrip(txtSearch.Text);

                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();

                hfShowList_ModalPopupExtender.Show();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTrip(txtSearch.Text);

            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();

            hfShowList_ModalPopupExtender.Show();

        }

        protected void btnVehicle_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtVehicleSearch.Text);

            gvVehicleList.DataSource = dt;
            gvVehicleList.DataBind();
            
            hfVehicleSearch_ModalPopupExtender.Show();
        }

        protected void btnDriver_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearchDriver.Text);

            gvDriverList.DataSource = dt;
            gvDriverList.DataBind();
            hfDriverSearch_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTrip(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnVehicleSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtVehicleSearch.Text);

            gvVehicleList.DataSource = dt;
            gvVehicleList.DataBind();

            hfVehicleSearch_ModalPopupExtender.Show();
        }

        protected void btnSearchDriver_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearchDriver.Text);

            gvDriverList.DataSource = dt;
            gvDriverList.DataBind();
            hfDriverSearch_ModalPopupExtender.Show();
        }

        protected void gvVehicleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvVehicleList.SelectedRow;
            lblCapacity.Text =" "+ row.Cells[1].Text + " " + row.Cells[2].Text + " " + row.Cells[3].Text;
            hfTotalCapacity.Value = row.Cells[4].Text;
            hfVehicleSearch.Value = row.Cells[1].Text;
        }

        protected void gvDriverList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvDriverList.SelectedRow;
            lblDriverCode.Text = row.Cells[1].Text;
            lblDriverName.Text=row.Cells[2].Text;
        }

        protected void gvDriverList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearchDriver.Text);

            gvDriverList.PageIndex = e.NewPageIndex;
            gvDriverList.DataSource = dt;
            gvDriverList.DataBind();
            UpdatePanel2.Update();
            hfDriverSearch_ModalPopupExtender.Show();
        }

        protected void gvVehicleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtVehicleSearch.Text);

            gvVehicleList.PageIndex = e.NewPageIndex;
            gvVehicleList.DataSource = dt;
            gvVehicleList.DataBind();
            UpdatePanel1.Update();
            hfVehicleSearch_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow r=gvlistofBasicData.SelectedRow;
            string tripNo = r.Cells[1].Text;
            loadTripInfo(tripNo);
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        
        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblTripNo.Text) == false)
            {
            Session["paramData"] = null;
            Session["reportOn"] = null;
            string tripNo = lblTripNo.Text;

            string reporton = "TripAdvanceAdvice";

            Session["paramData"] = tripNo;
            Session["reportOn"] = reporton;
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/reportViewer.aspx" + "','_blank')", true);
            }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Please Select Trip No');", true);
        }
          
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
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
                dt.Columns.Add("Rent");

                dt.Columns.Add("TotalPrice");

                if (grdListOfProduct.Rows.Count <= 0)
                {

                    DataRow dr = dt.NewRow();
                    dr["ProductCode"] = ddlproductName.SelectedValue.ToString();
                    dr["ProductName"] = ddlproductName.SelectedItem.ToString();
                    qty = (String.IsNullOrEmpty(txtQuantity.Text)) ? qty = 0 : qty = Convert.ToDecimal(txtQuantity.Text);
                    rate = (String.IsNullOrEmpty(txtRate.Text)) ? rate = 0 : rate = Convert.ToDecimal(txtRate.Text);
                    dr["Quantity"] = qty;
                    dr["Rent"] = rate;
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
                    //lblTotalAmount.Text = String.Format("{0:N2}", Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Rent"]))));

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
                    dr["Rent"] = rate;
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
                    //decimal total = Convert.ToDecimal(dt.Compute("Sum(Rent)", ""));
                    //txtTotalAmount.Text = total.ToString();

                }

                ddlproductName.ClearSelection();
                txtRate.Text = String.Empty;
                txtQuantity.Text = String.Empty;
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Quantity Cannot be Null or 0');", true);

        }

        protected void grdListOfProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdListOfProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDataTable(grdListOfProduct);

            dt.Rows[e.RowIndex].Delete();


            //decimal total = Convert.ToDecimal(dt.Compute("Sum(Rent)", ""));
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
            //lblTotalAmount.Text = String.Format("{0:N2}", Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Rent"]))));

        }

        protected void ddlproductName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}