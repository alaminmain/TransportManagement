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
    public partial class TripInfo : System.Web.UI.Page
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
                    btnAddHiredVehicle.Enabled = false;
                    btnAddHiredVehicle.Visible = false;
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

        DataTable GetProductDataTable(GridView dtg)
        {
            try
            {
                DataTable productDt = new DataTable();

                // add the columns to the datatable            
                productDt.Columns.Add("TCNo");

                productDt.Columns.Add("ProductCode");
                productDt.Columns.Add("ProductName");
                productDt.Columns.Add("OrderQty");
                productDt.Columns.Add("Rent");
                productDt.Columns.Add("TotalAmount");

                //  add each of the data rows to the table
                foreach (GridViewRow row in dtg.Rows)
                {

                    DataRow dr;
                    dr = productDt.NewRow();
                  
                   
                    dr["TCNo"] = row.Cells[1].Text;
                    dr["ProductCode"] = row.Cells[2].Text;
                    dr["ProductName"] = row.Cells[3].Text;
                    dr["OrderQty"] = (row.FindControl("Label1") as Label).Text;
                    dr["Rent"] = row.Cells[5].Text;//(row.FindControl("Rent") as TextBox).Text; //row.Cells[4].Text; ;
                    dr["TotalAmount"] = (row.FindControl("Label2") as Label).Text; //row.Cells[5].Text;
                                                                               //dr["TotalPrice"] = String.Empty;

                    //}
                    productDt.Rows.Add(dr);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Order Qty is Greater Then DO Qty');", true);
                    //}
                }
                return productDt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private DataTable ProductList()
        {
            
            try
            {
                DataTable PreviousDt = GetProductDataTable(gvListofTCProduct);
                
                
                    var ids = PreviousDt.AsEnumerable().Select(r => r.Field<string>("TCNo")).ToList();
               

                DataTable dt = new DataTable();


                List<string> DOIdList = new List<string>();
                string tcId = String.Empty;


                DataTable productDt = new DataTable();
                productDt.Columns.Add("TCNo");
                productDt.Columns.Add("ProductCode");
                productDt.Columns.Add("ProductName");
                productDt.Columns.Add("OrderQty");
                productDt.Columns.Add("Rent");
                productDt.Columns.Add("TotalAmount");

                //Add Previous Table
                foreach (DataRow r in PreviousDt.Rows)
                {
                    DataRow dr = productDt.NewRow();
                    dr["TCNo"] = r["tcNo"].ToString();

                    dr["ProductCode"] = r["ProductCode"].ToString();
                    dr["ProductName"] = Convert.ToString(r["ProductName"]);
                    dr["OrderQty"] = r["OrderQty"].ToString();
                    dr["Rent"] = String.Format("{0:0.00}", r["Rent"]);
                    dr["TotalAmount"] = String.Format("{0:0.00}", r["TotalAmount"]);
                    productDt.Rows.Add(dr);
                }

                //Add Selected Value
                foreach (GridViewRow item in gvTC.Rows)
                {
                    if ((item.Cells[0].FindControl("cbSelectTc") as CheckBox).Checked)
                    {

                        tcId = item.Cells[1].Text;
                        
                        var match = ids.FirstOrDefault(stringToCheck => stringToCheck.Contains(tcId));

                        if (match == null)
                        {
                            DataTable tcDetail = new DataTable();
                            using (TransContactGateway gatwayObj = new TransContactGateway())
                            {
                                tcDetail = gatwayObj.GetAllTransContactDetail(1, tcId);

                                foreach (DataRow r in tcDetail.Rows)
                                {
                                    DataRow dr = productDt.NewRow();
                                    dr["TCNo"] = r["TCNo"].ToString();

                                    dr["ProductCode"] = r["ProductCode"].ToString();
                                    dr["ProductName"] = Convert.ToString(r["ProductName"]);
                                    dr["OrderQty"] = r["OrderQty"].ToString();
                                    dr["Rent"] = String.Format("{0:0.00}", r["Rent"]); 
                                    dr["TotalAmount"] = String.Format("{0:0.00}", r["TotalAmount"]);
                                    productDt.Rows.Add(dr);
                                }

                            }

                        }

                    }

                }

                
               
                
                //Session["myDatatable"] = productDt;
                return productDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void tcDataTable()
        {

            DataTable dt = new DataTable();
            List<string> tcIdList = new List<string>();
            string tcno = String.Empty;
            dt.Columns.Add("DONo");

            DataTable productDt = new DataTable();
            productDt.Columns.Add("TCNo");
          
            productDt.Columns.Add("ProductCode");
            productDt.Columns.Add("ProductName");
            productDt.Columns.Add("OrderQty");
            productDt.Columns.Add("Rent");
            productDt.Columns.Add("TotalAmount");
            foreach (GridViewRow item in gvTC.Rows)
            {
                if ((item.Cells[0].FindControl("cbSelectTc") as CheckBox).Checked)
                {
                    DataRow dr = dt.NewRow();
                    tcno = item.Cells[1].Text;
                    tcIdList.Add(tcno);
                }


            }
            if (tcIdList != null)
            {
                foreach (string id in tcIdList)
                {
                    DataTable tcDetail = new DataTable();
                    using (TransContactGateway gatwayObj = new TransContactGateway())
                    {
                        tcDetail = gatwayObj.GetAllTransContactDetail(1, id);

                        foreach (DataRow r in tcDetail.Rows)
                        {
                            DataRow dr = productDt.NewRow();
                            dr["TCNo"] = r["TCNo"].ToString();
                         
                            dr["ProductCode"] = r["ProductCode"].ToString();
                            dr["ProductName"] = Convert.ToString(r["ProductName"]);
                            dr["OrderQty"] = r["OrderQty"].ToString();
                            dr["Rent"] = Convert.ToDecimal(r["Rent"]).ToString("0.00");
                            dr["TotalAmount"] =Convert.ToDecimal(r["TotalAmount"]).ToString("0.00");
                            productDt.Rows.Add(dr);
                        }
                    }
                }
            }
            Session["myDatatable"] = productDt;

        }
        private DataTable GetDriver(string searchKey)
        {
            try
            {
                DataTable dt = null;
               // DataTable dt2;


                using (PersonalGateway gatwayObj = new PersonalGateway())
                {

                    //int HiredStatus = 0;
                    //if (ddlAgent.SelectedValue != "00001")
                    //    vehicleHiredStatus = 1;
                    //else
                    //    vehicleHiredStatus = 0;
                    //dt2 = gatwayObj.GetAllVehicle(1);
                    //DataRow[] foundRows;

                    //// Use the Select method to find all rows matching the filter.
                    //foundRows = dt2.Select("[VehicleStatus] IN ('0','4') AND [IsHired]='" + vehicleHiredStatus + "' AND StoreCode=" + ddlGhatList.SelectedValue);
                    //if (foundRows.Length > 0)
                    //    dt = foundRows.CopyToDataTable();

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
                    foundRows = dt2.Select("[VehicleStatus] IN ('0','4') AND [IsHired]='"+vehicleHiredStatus+ "' AND StoreCode="+ddlGhatList.SelectedValue);
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
                DataTable dt=null;
                DataTable dt2;

                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {

                    dt2 = gatwayObj.GetAllTripInfoForGridView(); ;
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("IsOtherTrip=0");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                    //dt = gatwayObj.GetAllTripInfoForGridView();
                    if (String.IsNullOrEmpty(searchKey))
                    {
                        
                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("TripNo").Contains(searchKey));
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

        private void getHGhat()
        {
            try
            {
                DataTable dt;


                using (StoreLocation gatwayObj = new StoreLocation())
                {
                    dt = gatwayObj.GetAllStoreLocation(1);
                    ddlHGhat.DataSource = dt;
                    ddlHGhat.DataValueField = "StoreCode";
                    ddlHGhat.DataTextField = "StoreName";
                    ddlHGhat.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);

            }
        }

        private void LoadFuelType()
        {
            try
            {
                DataTable dt;


                using (FuelInfoGateway gatwayObj = new FuelInfoGateway())
                {


                    dt = gatwayObj.get_Fuel_Info();

                    ddlFuelType.DataSource = dt;
                    ddlFuelType.DataTextField = "FuelName";
                    ddlFuelType.DataValueField = "FuelCode";
                    DataBind();

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);

            }
        }

        

        public void loadTripInfo(string tripNo)
        {
            try
            {
                DataTable dtTrip = new DataTable();
                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {
                    dtTrip = gatwayObj.GetTripInfo(tripNo);
                }
                //ComCode, StoreCode, TripNo, TripDate, TransportBy, AgentID, VehicleID, EmpCode, Capacity, 
                //KmPerLiter, FuelSlipNo, SupplierName, FuelCode, FuelRate, FuelQty, Remarks, Totalkm, Additionalkm, 
                //CapacityBal,  TripStatus
                lblTripNo.Text = dtTrip.Rows[0]["TripNo"].ToString();
                txtTripDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dtTrip.Rows[0]["TripDate"]).Date;
                ddlTransportBy.SelectedValue = dtTrip.Rows[0]["TransportBy"].ToString();
                lblDealerCode.Text = dtTrip.Rows[0]["DealerId"].ToString();
                lblDealerName.Text = dtTrip.Rows[0]["CustName"].ToString();
                hfVehicleSearch.Value= dtTrip.Rows[0]["VehicleID"].ToString();
                hfTotalCapacity.Value = dtTrip.Rows[0]["Capacity"].ToString();
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
                DataTable dt = new DataTable();
                using (TripDetail td = new TripDetail())
                {
                    dt = td.GetTripDetailByTrip(1, lblTripNo.Text);
                    if (dt.Rows.Count > 0)
                    {
                        gvListofTCProduct.DataSource = dt;
                        gvListofTCProduct.DataBind();
                        //Session["myDatatable"] = dt;

                        decimal totalqty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                        decimal totalAmount = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalAmount"]));
                        gvListofTCProduct.FooterRow.Cells[1].Text = "Total";
                        gvListofTCProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        gvListofTCProduct.FooterRow.Cells[3].Text = totalqty.ToString("0");
                        gvListofTCProduct.FooterRow.Cells[5].Text = totalAmount.ToString("0.00");
                    }
                    else
                    {
                        gvListofTCProduct.DataSource = dt;
                        gvListofTCProduct.DataBind();
                    }
                }
              
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private DataTable LoadTransportContact(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (TransContactGateway gatwayObj = new TransContactGateway())
                {

                    dt2 = gatwayObj.GetAllTransContactForGridView(1);
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[DealerId]= '" + lblDealerCode.Text + "' and [TCStatus]=0 ");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                  

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
                gvListofTCProduct.DataSource = null;
                gvListofTCProduct.DataBind();

            }

           
        }

        private void clearHirdVehicleEntryForm()
        {
            foreach (Control control in plHiredVehicle.Controls)
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
        private void clearHirdDriverEntryForm()
        {
            foreach (Control control in pnHDriver.Controls)
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
            if (String.IsNullOrEmpty(hfVehicleSearch.Value))
                return false;
            else if (String.IsNullOrEmpty(lblDriverCode.Text))
                return false;
            //else if (gvListofTCProduct.Rows.Count <= 0)
            //    return false;
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
                    string tripNo_3 = lblTripNo.Text;
                    string tripDate_4 = txtTripDate.Text+" "+DateTime.Now.ToShortTimeString();
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
                    string dealerId_16 = lblDealerCode.Text;
                   
                    DataTable dt = new DataTable();
                    dt = LoadAllTrip(lblTripNo.Text);

                    DataTable tripdetail = GetProductDataTable(gvListofTCProduct);
                    int CapacityBal_17 = (int)tripdetail.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                    using (TripInfoGateway gatwayObj = new TripInfoGateway())
                    {
                        string tripno = gatwayObj.InsertUpdateTripInfo(comcode_1, storeCode_2, tripNo_3, tripDate_4, transportBy_5,
                    agentId_6, vehicleId_7, empCode_8, capacity_9, kmPerLiter_10, fuelCode_11, fuelRate_12, Remarks_13,
                     tripStatus_14, userCode_15, dealerId_16, CapacityBal_17, tripdetail);

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
            DataTable dt = new DataTable();
            dt = LoadAllTrip(txtSearch.Text);

            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();

            hfShowList_ModalPopupExtender.Show();
        }

        //protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //    try
        //    {
        //        DataTable dt;


        //        using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
        //        {

        //            dt = gatwayObj.GetVehicleById(ddlVehicle.SelectedValue);

        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("VehicleNo: ");
        //            sb.Append(dt.Rows[0]["VehicleNo"].ToString());
        //            sb.Append("&nbsp");
        //            sb.Append("Capaicity: ");
        //            sb.Append(dt.Rows[0]["Capacity"].ToString());
        //            sb.Append("&nbsp");
        //            sb.Append("KmPerLiter: ");
        //            sb.Append(dt.Rows[0]["KmPerLiter"].ToString());
        //            lblCapacity.Text = sb.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError(ex.ToString(), new object[0]);

        //    }
        //}

       
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

        protected void btnTCProduct_Click(object sender, EventArgs e)
        {
            //Session["myDatatable"] = null;
            
            // tcDataTable();
            DataTable PreviousDt = GetProductDataTable(gvListofTCProduct);
            DataTable dt = ProductList();
            
            decimal totalqty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
            decimal totalAmount = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalAmount"]));

            if (totalqty > Convert.ToDecimal(hfTotalCapacity.Value))
            {
                gvListofTCProduct.DataSource = PreviousDt;
                gvListofTCProduct.DataBind();
                totalqty = PreviousDt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                totalAmount = PreviousDt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalAmount"]));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Vehicle Capacity Overloaded');", true);

            }
            else
            {
                gvListofTCProduct.DataSource = dt;
                gvListofTCProduct.DataBind();

            }
            gvListofTCProduct.FooterRow.Cells[1].Text = "Total";
            gvListofTCProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvListofTCProduct.FooterRow.Cells[3].Text = totalqty.ToString("0");

            
            gvListofTCProduct.FooterRow.Cells[5].Text = totalAmount.ToString("0.00");
            
                
                //Session["myDatatable"] = null;
                
                
            
            hfTC_ModalPopupExtender.Hide();
        }

        protected void btnSearchTC_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = LoadTransportContact(txtSearchTC.Text);
                gvTC.DataSource = dt;
                gvTC.DataBind();

                string tcId = String.Empty;
                if (gvListofTCProduct.Rows.Count > 0)
                {
                    foreach (GridViewRow item in gvListofTCProduct.Rows)
                    {
                        tcId = item.Cells[1].Text;
                        foreach (GridViewRow dolist in gvTC.Rows)
                        {
                            if (dolist.Cells[1].Text == tcId)
                            {
                                (dolist.Cells[0].FindControl("cbSelectTc") as CheckBox).Checked = true;
                            }


                        }

                    }
                }

                hfTC_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnSearchDo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = LoadTransportContact(txtSearchTC.Text);
                gvTC.DataSource = dt;
                gvTC.DataBind();

                hfTC_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void gvTC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadTransportContact(txtSearchTC.Text);

            gvTC.PageIndex = e.NewPageIndex;
            gvTC.DataSource = dt;
            gvTC.DataBind();
            upTC.Update();
            hfTC_ModalPopupExtender.Show();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblTripNo.Text) == false)
            {
            Session["paramData"] = null;
            Session["reportOn"] = null;
            string tripNo = lblTripNo.Text;

            string reporton = "TripAdvice";

            Session["paramData"] = tripNo;
            Session["reportOn"] = reporton;
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/reportViewer.aspx" + "','_blank')", true);
            }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Please Select Trip No');", true);
        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtDealerSearch.Text);
            gvDealerSearch.DataSource = dt;
            gvDealerSearch.DataBind();


            hfDealer1_ModalPopupExtender.Show();
        }
       
        protected void gvDealerSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvDealerSearch.SelectedRow;
            lblDealerCode.Text = row.Cells[1].Text;
            lblDealerName.Text = row.Cells[2].Text;

            
        }

        protected void gvDealerSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtDealerSearch.Text);

            gvDealerSearch.PageIndex = e.NewPageIndex;
            gvDealerSearch.DataSource = dt;
            gvDealerSearch.DataBind();
            UpdatePanel3.Update();
            hfDealer1_ModalPopupExtender.Show();
        }

        protected void btnDealerSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtDealerSearch.Text);

           
            gvDealerSearch.DataSource = dt;
            gvDealerSearch.DataBind();
            UpdatePanel3.Update();
            hfDealer1_ModalPopupExtender.Show();
        }

        protected void btnShowDealer_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllCustInfo(txtDealerSearch.Text);
            gvDealerSearch.DataSource = dt;
            gvDealerSearch.DataBind();
            UpdatePanel3.Update();

            hfDealer1_ModalPopupExtender.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnAddHiredVehicle_Click(object sender, EventArgs e)
        {
             LoadFuelType();
            getHGhat();
            hfInsertHVehicle_ModalPopupExtender.Show();
            clearHirdVehicleEntryForm();
        }

        protected void btnHiredVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtVehicleNo.Text) == false)
                {
                    int comCode = 1;
                    string VehicleType = ddlVehicleType.SelectedValue.ToString();
                    string VehicleID_1 = lblVehicleId.Text;
                    string VehicleNo_2 = txtVehicleNo.Text;
                    string ChesisNo_3 = String.Empty;// txtChesisNo.Text;
                    string ModelNo_4 = String.Empty;//txtModelNo.Text;
                    string EngineNo_5 = String.Empty;//txtEngineNo.Text;
                    string EngineVolume_16 = String.Empty;//txtEngineVolume.Text;
                    string PurchaseDate_17 = String.Empty;//txtPurchaseDate.Text;
                    string VehicleDesc_6 = String.Empty;
                    string Mobile_7 = txtMobileNo.Text;
                    string Capacity_8 = txtCapacity.Text;
                    decimal KmPerLiter_9;
                    if (string.IsNullOrEmpty(txtKmPerLitre.Text))
                        KmPerLiter_9 = 0;
                    else
                        KmPerLiter_9 = Convert.ToDecimal(txtKmPerLitre.Text);

                    int FuelCode_10 = Convert.ToInt32(ddlFuelType.SelectedValue);
                    int StoreCode_11 = Convert.ToInt32(ddlGhatList.SelectedValue);
                    int IsHired_12;

                    IsHired_12 = 1;

                    int VehicleStatus_13 = Convert.ToInt32(ddlStatus.SelectedValue);
                    string userId = Session["UserName"].ToString();
                    string capacityUnit = ddlCapacityUnit.SelectedItem.Text;
                    string Remarks_18 = String.Empty;
                    using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                    {
                        VehicleID_1 = gatwayObj.InsertUpdateVehicle(comCode, VehicleType, VehicleID_1, VehicleNo_2, ChesisNo_3, ModelNo_4, EngineNo_5, VehicleDesc_6,
                           Mobile_7, Capacity_8, KmPerLiter_9, FuelCode_10, StoreCode_11, IsHired_12, VehicleStatus_13, userId, capacityUnit, EngineVolume_16, PurchaseDate_17, Remarks_18);
                        lblVehicleId.Text = VehicleID_1;

                    }
                }
                if (String.IsNullOrEmpty(lblVehicleId.Text) == false)
                    lblMessage.Text = "Data Saved";
                else
                    lblMessage.Text = "Could'nt save!!!";
                hfInsertHVehicle_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnHiredVehicleCancel_Click(object sender, EventArgs e)
        {
            clearHirdVehicleEntryForm();
            hfInsertHVehicle_ModalPopupExtender.Hide();
        }

        protected void ddlTransportBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransportBy.SelectedValue == "2")
            {
                btnAddHiredVehicle.Enabled = true;
                btnAddHiredVehicle.Visible = true;
            }
            else
            {
                btnAddHiredVehicle.Enabled = false;
                btnAddHiredVehicle.Visible = false;
            }
        }

        protected void gvListofTCProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DataTable dt = GetProductDataTable(gvListofTCProduct);


                dt.Rows[e.RowIndex].Delete();


                //decimal total = Convert.ToDecimal(dt.Compute("Sum(TotalPrice)", ""));
                //txtTotalAmount.Text = total.ToString();


                gvListofTCProduct.DataSource = dt;
                gvListofTCProduct.DataBind();
                decimal totalqty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                decimal totalAmount = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalAmount"]));

                gvListofTCProduct.FooterRow.Cells[1].Text = "Total";
                gvListofTCProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvListofTCProduct.FooterRow.Cells[3].Text = totalqty.ToString("0");


                gvListofTCProduct.FooterRow.Cells[5].Text = totalAmount.ToString("0.00");
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtEmployeeName.Text)==false||String.IsNullOrEmpty(txtMobilePhone.Text)==false)
                {
                    string EmpCode = lblEmpCode.Text;

                    string Cardno = "3";
                    string EmpName = txtEmployeeName.Text;
                    int ComCode = 1;
                    string FatherName = txtFatherName.Text;
                    string MotherName = String.Empty;
                    string BloodGroup = String.Empty;
                    string Add1 = String.Empty;
                    string Add2 = String.Empty;
                    string Add3 = String.Empty;
                    string Mobile = txtMobilePhone.Text;
                    //int IsHired = 0;
                    int PStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                    string DrivingLicen = txtDrivingLisense.Text;
                    string userId = Session["UserName"].ToString();
                    int storeCode = Convert.ToInt32(ddlGhatList.SelectedValue);
                    string nid = txtNID.Text;
                    string drivingCapacity = String.Empty;
                    using (PersonalGateway gatwayObj = new PersonalGateway())
                    {
                        string empcode = gatwayObj.InsertUpdatePersonal(EmpCode, Cardno, EmpName, ComCode, FatherName, MotherName, BloodGroup
                            , Add1, Add2, Add3, Mobile, PStatus, DrivingLicen, userId, storeCode, nid, drivingCapacity);
                        lblEmpCode.Text = empcode;
                    }
                    lblMessageDriver.Text = "Driver Added";
                }
                else
                    lblMessageDriver.Text = "Cannot Save!!!";
                hfInsertDriver_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAddHDriver_Click(object sender, EventArgs e)
        {
            clearHirdDriverEntryForm();
            hfInsertDriver_ModalPopupExtender.Show();
        }

        protected void btnCancelAddDriver_Click(object sender, EventArgs e)
        {
            clearHirdDriverEntryForm(); hfInsertDriver_ModalPopupExtender.Hide();
        }
    }
}