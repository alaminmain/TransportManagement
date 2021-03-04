using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;
using System.IO;
using System.Net;
using System.Text;

namespace TransportManagerUI.UI
{
    public partial class MovmentInfo : System.Web.UI.Page
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
                    getGhat();
                    txtMovmentDate_CalendarExtender.SelectedDate = DateTime.Now;
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

            foreach (Control control in Panel3.Controls)
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
            dvTrip.DataSource = null;
            dvTrip.DataBind();
            gvTrip.DataSource = null;
            gvTrip.DataBind();
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
        private DataTable LoadTCProduct(string TCNo)
        {
            try
            {
                DataTable dt;


                using (TransContactDetailGateway gatwayObj = new TransContactDetailGateway())
                {


                    dt = gatwayObj.GetTransContactDetail(1,TCNo);
                   
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }
        private DataTable LoadAllTrip(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {


                    dt2 = gatwayObj.GetAllTripInfoForGridView();

                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[TripStatus]= '0' AND IsOtherTrip=0");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
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
        private DataTable LoadTransportContact(string searchKey)
        {
            try
            {
                DataTable dt=null;
                DataTable dt2;


                using (TransContactGateway gatwayObj = new TransContactGateway())
                {

                    //dt = gatwayObj.GetAllTransContactForGridView(1,hfDealer.Value);

                    dt2 = gatwayObj.GetAllTransContactByTripNo(1,hfTrip.Value);
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[TCStatus]= '1'");

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

        private DataTable LoadTransportContactWhenEdit(string searchKey)
        {
            try
            {
                DataTable dt;


                using (TransContactGateway gatwayObj = new TransContactGateway())
                {

                    //dt = gatwayObj.GetAllTransContactForGridView(1,hfDealer.Value);
                    dt = gatwayObj.GetAllTransContactByTripNo(1, hfTrip.Value);
                                   
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

        private DataTable loadAllMovement(string searchKey)
        {
            try
            {
                DataTable dt;


                using (TransportMasterGateway gatwayObj = new TransportMasterGateway())
                {


                    dt = gatwayObj.GetMovmentforGridview(1);
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("MovementNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("TripNo").ToUpper().Contains(searchKey.ToUpper()));
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

        private void loadMovementUI()
        {
            try
            {
                GridViewRow row = gvlistofBasicData.SelectedRow;
                //Load Movment Related Info
                DataTable dt = new DataTable();
                using (TransportMasterGateway gatwayObj = new TransportMasterGateway())
                {
                    dt = gatwayObj.GetAllMovement(1, row.Cells[1].Text);
                }
                lblMovement.Text = dt.Rows[0]["MovementNo"].ToString();
                txtMovmentDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["TransportDate"]).Date;
                // lblCapacity.Text = dt.Rows[0][""].ToString();
                txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                ddlStatus.SelectedValue = dt.Rows[0]["TranStatus"].ToString();
                ddlGhatList.SelectedValue = dt.Rows[0]["StoreCode"].ToString();
                //load Trip Info
                DataTable TripInfo = new DataTable();
                TripInfo = LoadAllTrip(dt.Rows[0]["TripNo"].ToString());
                hfTrip.Value = dt.Rows[0]["TripNo"].ToString();
                DataTable loadTrip = new DataTable();
                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {
                    loadTrip = gatwayObj.GetTripInfo(hfTrip.Value);
                }

                dvTrip.DataSource = loadTrip;
                dvTrip.DataBind();

                lblCustomerCode.Text = dt.Rows[0]["DealerId"].ToString();
                lblCustomerName.Text = dt.Rows[0]["DealerName"].ToString();
                lblDealerMobile.Text = loadTrip.Rows[0]["DealerMobile"].ToString();
                //lblsnmMobileNo.Text = loadTrip.Rows[0]["Phone"].ToString();

                hfDealer.Value = dt.Rows[0]["DealerId"].ToString();

                hfCustomerId.Value = dt.Rows[0]["CustId"].ToString();
                //Load TC Info
                DataTable TCinfo = new DataTable();
                TCinfo = LoadTransportContactWhenEdit(dt.Rows[0]["TCNo"].ToString());
                hfTC.Value = dt.Rows[0]["TCNo"].ToString();
                lblTCInfo.Text = "TC No- " + TCinfo.Rows[0]["TCNo"].ToString() + " Date " + Convert.ToDateTime(TCinfo.Rows[0]["TCDate"]).Date.ToString("dd/MMM/yyyy");
                lblCustomerCode1.Text = TCinfo.Rows[0]["CustId"].ToString();
                lblCustomerName1.Text = TCinfo.Rows[0]["CustName1"].ToString();
                lblCustomerMobile.Text = TCinfo.Rows[0]["CustomerMobile"].ToString();
                lblsnmMobileNo.Text = TCinfo.Rows[0]["Phone"].ToString();
                //Load Dealer Info

                //lblDealerInfo.Text=tc

                DataTable movementDetail = new DataTable();
                using (TransportDetailGateway gatwayObj = new TransportDetailGateway())
                {
                    movementDetail = gatwayObj.get_Transport_Detail(row.Cells[1].Text);
                }
                gvListofDOProduct.DataSource = movementDetail;
                gvListofDOProduct.DataBind();
                decimal totalQty = movementDetail.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                decimal totalAmount = movementDetail.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]));
                gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
                gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvListofDOProduct.FooterRow.Cells[2].Text = totalQty.ToString("N2");
                gvListofDOProduct.FooterRow.Cells[4].Text = totalAmount.ToString("N2");
                txtSearch.Text = String.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool isMandatoryFieldValidate()
        {
            if (String.IsNullOrEmpty(hfTrip.Value))
                return false;
            else if (String.IsNullOrEmpty(hfTC.Value))
                return false;
            else if (gvListofDOProduct.Rows.Count <= 0)
                return false;
            else
                return true;
        }
            
        #endregion

        protected void btnTripSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = LoadAllTrip(txtSearchTrip.Text);
                gvTrip.DataSource = dt;
                gvTrip.DataBind();

                hfTrip_ModalPopupExtender.Show();
            }
            catch(Exception)
            {
                throw;
            }

        }

        protected void btnDealerSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadAllDealerInfo(txtDealerSearch.Text);
                gvDealer.DataSource = dt;
                gvDealer.DataBind();
                hfDealer_ModalPopupExtender.Show();
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnTcNoSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = LoadTransportContact(txtTCSearch.Text);
                gvTC.DataSource = dt;
                gvTC.DataBind();

                hfTC_ModalPopupExtender.Show();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #region forGridView
        protected void gvTrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvTrip.SelectedRow;
                   
               //lblTripInfo.Text = "Trip No- "+ row.Cells[1].Text + " " + row.Cells[2].Text;
            hfTrip.Value = row.Cells[1].Text;
            DataTable loadTrip = new DataTable();
            using (TripInfoGateway gatwayObj = new TripInfoGateway())
            {
                loadTrip = gatwayObj.GetTripInfo(row.Cells[1].Text);
            }
            lblCustomerCode.Text = loadTrip.Rows[0]["DealerId"].ToString();
            lblCustomerName.Text = loadTrip.Rows[0]["CustName"].ToString();
            lblDealerMobile.Text = loadTrip.Rows[0]["DealerMobile"].ToString();
            
            hfDealer.Value = lblCustomerCode.Text;
            dvTrip.DataSource = loadTrip;
            dvTrip.DataBind();

        }

        protected void gvTrip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTrip(txtSearchTrip.Text);

            gvTrip.PageIndex = e.NewPageIndex;
            gvTrip.DataSource = dt;
            gvTrip.DataBind();
            upTrip.Update();
            hfTrip_ModalPopupExtender.Show();
        }

        protected void gvDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvDealer.SelectedRow;

            lblCustomerCode.Text = row.Cells[1].Text;
            lblCustomerName.Text = row.Cells[2].Text;
                
            hfDealer.Value = row.Cells[1].Text;
            hfTC.Value = String.Empty;
            lblTCInfo.Text = String.Empty;
                
                //gvDealerInfo.DataSource = filtered.CopyToDataTable();
                //gvDealerInfo.DataBind();

        }

        protected void gvTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvTC.SelectedRow;

                lblTCInfo.Text = "TC No- " + row.Cells[1].Text + " TC Date-" + row.Cells[2].Text;
                DataTable loadTC=new DataTable();
                string tcno = row.Cells[1].Text;
                hfTC.Value = tcno;
                using (TransContactGateway gatwayObj = new TransContactGateway())
                {
                    loadTC = gatwayObj.GetAllTransContact(1,row.Cells[1].Text);
                }
                
                hfCustomerId.Value = row.Cells[4].Text;
                lblCustomerCode1.Text = row.Cells[4].Text;
                lblCustomerName1.Text = row.Cells[5].Text;
                lblCustomerMobile.Text = loadTC.Rows[0]["Mobile"].ToString();
                lblsnmMobileNo.Text= loadTC.Rows[0]["Phone"].ToString();
                DataTable dt = new DataTable();
                dt = LoadTCProduct(tcno);
                //lblCurrentCapacity.Text = Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"])));
                gvListofDOProduct.DataSource = dt;
                gvListofDOProduct.DataBind();
                decimal totalQty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
                decimal totalAmount = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]));
                gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
                gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvListofDOProduct.FooterRow.Cells[2].Text = totalQty.ToString("0");
                gvListofDOProduct.FooterRow.Cells[4].Text = totalAmount.ToString("0.00");
                hfTC_ModalPopupExtender.Hide();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        protected void gvTC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadTransportContact(txtTCSearch.Text);

            gvTC.PageIndex = e.NewPageIndex;
            gvTC.DataSource = dt;
            gvTC.DataBind();
            upTC.Update();
            hfTC_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMandatoryFieldValidate())
                {

                    int ComCode_1 = 1;
                    string TripNo_2 = hfTrip.Value;
                    string MovementNo_3 = lblMovement.Text;
                    string TransportDate_4 = txtMovmentDate.Text + " " + DateTime.Now.ToString("hh:mm:ss tt");
                    string TCNo_5 = hfTC.Value;
                    string DealerId_6 = hfDealer.Value;
                    string CustId_7 = hfCustomerId.Value;

                    string Remarks_8 = txtRemarks.Text;
                    int TranStatus_9 = Convert.ToInt32(ddlStatus.SelectedValue);
                    string User_Code_10 = Session["UserName"].ToString();
                    int StoreCode_11 = Convert.ToInt32(ddlGhatList.SelectedValue);
                    DataTable dt = new DataTable();
                    dt = LoadTCProduct(TCNo_5);

                    using (TransportMasterGateway gatwayObj = new TransportMasterGateway())
                    {
                        string movNo = gatwayObj.InsertUpdateTransport(ComCode_1, TripNo_2, MovementNo_3, TransportDate_4, TCNo_5, DealerId_6,
                            CustId_7, Remarks_8, TranStatus_9, User_Code_10, StoreCode_11, dt);
                        lblMovement.Text = movNo;
                    }
                    DataTable custdt = new DataTable();
                    //Cust Id Related 
                    string custLocDistance;
                    using (DealerGateway gt = new DealerGateway())
                    {
                        custdt = gt.GetCustomerById(1, CustId_7);
                        custLocDistance = custdt.Rows[0]["LocDistance"].ToString();
                        if (String.IsNullOrEmpty(custLocDistance))
                            custLocDistance = "0";
                    }
                    DataTable dtTrip = new DataTable();

                    using (TripInfoGateway gatwayObj = new TripInfoGateway())
                    {
                        dtTrip = gatwayObj.GetTripInfo(TripNo_2);
                        //ComCode, StoreCode, TripNo, TripDate, TransportBy, AgentID, VehicleID, EmpCode, Capacity, KmPerLiter, 
                        //FuelSlipNo, SupplierName, FuelCode, FueiRate, FuelQty, Remarks, Totalkm, Additionalkm, CapacityBal,  TripStatus
                       // Convert.ToInt32(dtTrip.Rows[0]["Capacity"]) + Convert.ToInt32(lblCurrentCapacity.Text);

                        decimal totalKm_17 = 0;

                        if (String.IsNullOrEmpty(dtTrip.Rows[0]["Totalkm"].ToString()) == false || dtTrip.Rows[0]["Totalkm"].ToString()=="0")
                        {
                            totalKm_17 = Convert.ToDecimal(dtTrip.Rows[0]["Totalkm"]);
                        }
                        else
                            totalKm_17 = 0;

                        if (Convert.ToDecimal(custLocDistance)>= totalKm_17)
                        {
                            totalKm_17 = Convert.ToDecimal(custLocDistance);
                            string updateInof = gatwayObj.UpdateCapacityBalance(TripNo_2, totalKm_17);
                        }
                    }


                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
                    //clearAll();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Madatory Field');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
       

    #endregion

   

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            
            dt = loadAllMovement(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvDealer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllDealerInfo(txtDealerSearch.Text);

            gvDealer.PageIndex = e.NewPageIndex;
            gvDealer.DataSource = dt;
            gvDealer.DataBind();
            upDearler.Update();
            hfDealer_ModalPopupExtender.Show();
        }

       

        protected void btnTCok_Click(object sender, EventArgs e)
        {
            GridViewRow row = gvTC.SelectedRow;
            string tcno = row.Cells[1].Text;
            DataTable dt = new DataTable();
            dt = LoadTCProduct(tcno);
            gvListofDOProduct.DataSource = dt;
            gvListofDOProduct.DataBind();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblMovement.Text) == false)
            {
                Session["paramData"] = null;
                Session["reportOn"] = null;

                string movementno = lblMovement.Text;

                string reporton = "MovementChalan";

                Session["Movement"] = movementno;
                Session["reportOn"] = reporton;
                //btnReport.PostBackUrl = "~/UI/reportViewer.aspx";
               
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/reportViewer.aspx" + "','_blank')", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Please Select Movement No');", true);
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadMovementUI();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllMovement(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnDealerOk_Click(object sender, EventArgs e)
        {

        }

        protected void btnDearlerSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllDealerInfo(txtDealerSearch.Text);
            gvDealer.DataSource = dt;
            gvDealer.DataBind();
            hfDealer_ModalPopupExtender.Show();

        }

        protected void btnSearchdTrip_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = LoadAllTrip(txtSearchTrip.Text);
                gvTrip.DataSource = dt;
                gvTrip.DataBind();

                hfTrip_ModalPopupExtender.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnTCSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = LoadTransportContact(txtTCSearch.Text);
                gvTC.DataSource = dt;
                gvTC.DataBind();

                hfTC_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnSendSms_Click(object sender, EventArgs e)
        {
           

            List<string> mobileNo = new List<string>();
                if (String.IsNullOrEmpty(lblCustomerMobile.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('No Mobile No');", true);
                    
                }
                else
                {
                 
                    mobileNo.Add(lblCustomerMobile.Text);
                }

                if (String.IsNullOrEmpty(lblDealerMobile.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('No Mobile No');", true);
                }
                else
                {

                mobileNo.Add(lblDealerMobile.Text);
                }
                if (String.IsNullOrEmpty(lblsnmMobileNo.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('No Mobile No');", true);
                }
                else
                {
                 mobileNo.Add(lblsnmMobileNo.Text);
                }

            SendSMS(mobileNo);
        }


        private void SendSMS(List<string> ToPhoneNo)
        {
           

            //string smsBody = "Dear Concern, Delivery Info: Product:"+dgvProduct[1, 0].Value.ToString()+" Qty:"+dgvProduct[2, 0].Value.ToString()+" Cargo:" + txtVehicle.Text + " Driver :"  +txtDriver.Text+
            //    " Out Time:"+DateTime.Now.ToShortTimeString()+" Transport Division(MCIL)";
            DataTable dtDriver = new DataTable();
            DataTable product = new DataTable();
            
            try
            {

               
                using (PersonalGateway gateway = new PersonalGateway())
                {
                    dtDriver = gateway.GetDriverById(1, dvTrip.Rows[3].Cells[1].Text);
                }
                using (ProductGateway pd = new ProductGateway())
                {
                  product=pd.GetProductById(gvListofDOProduct.Rows[0].Cells[0].Text);
                }
                string ShortProductName = product.Rows[0]["ProductName1"].ToString();
                //string ProductName = gvListofDOProduct.Rows[0].Cells[1].Text;
                string Quantity = Convert.ToDecimal(gvListofDOProduct.FooterRow.Cells[2].Text).ToString("0");
                string CustomerName = lblCustomerName.Text;
                string VehicleNo = dvTrip.Rows[2].Cells[1].Text;
                string DriverName = dvTrip.Rows[4].Cells[1].Text;
                string driverMobileNo = Convert.ToString(dtDriver.Rows[0]["Mobile"]);

                string smsBody = "Dear Sir," + " \nRetailer: " + CustomerName +" \n"+ ShortProductName + " " + Quantity + " bags"  + " \nCargo No: " + VehicleNo + " \nDriver: " + DriverName +
                     " \nMob: " + driverMobileNo + " \nThanks by MCIL-L&D";
                int count = 0;
                foreach (var mono in ToPhoneNo)
                {

                    string mo = "+88" +mono;
                string remoteUri ="https://vas.banglalinkgsm.com/sendSMS/sendSMS?msisdn="+mo.Trim()+"&message="+smsBody.Trim()+"&userID=McementBLsms&passwd=c8a8495667aae60f4b1f42d816889b05&sender=Madina Cement";
                    WebClient MyClient = new WebClient();
                    byte[] myDataBuffer = MyClient.DownloadData(remoteUri);

                    // Display the downloaded data.
                    string download = Encoding.ASCII.GetString(myDataBuffer);
                    //Stream MyStream = MyClient.OpenRead(remoteUri);
                    //StreamReader MyReader = new StreamReader(MyStream);
                    if(download== "Success Count : 1 and Fail Count : 0")
                    {
                        count++;
                    }
                    //string page = MyReader.ReadLine();
                    //MyStream.Close();
                    //MyStream.Dispose();
                }
                if (count>0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('"+count+"' Message Sent Successfull!');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Messages Sent UnSuccessfull!!');", true); //MessageBox.Show(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllMovement(txtSearch.Text);

            
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            DataTable loadTC = new DataTable();
          
            using (TransContactGateway gatwayObj = new TransContactGateway())
            {
                loadTC = gatwayObj.GetAllTransContact(1, hfTC.Value);
            }

            hfCustomerId.Value = loadTC.Rows[0]["CustId"].ToString(); 
            lblCustomerCode1.Text = loadTC.Rows[0]["CustId"].ToString(); 
            lblCustomerName1.Text = loadTC.Rows[0]["CustomerName"].ToString();
            lblCustomerMobile.Text = loadTC.Rows[0]["Mobile"].ToString();
            lblsnmMobileNo.Text = loadTC.Rows[0]["Phone"].ToString();
            DataTable dt = new DataTable();
            dt = LoadTCProduct(hfTC.Value);
            //lblCurrentCapacity.Text = Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"])));
            gvListofDOProduct.DataSource = dt;
            gvListofDOProduct.DataBind();
            decimal totalQty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
            decimal totalAmount = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["TotalPrice"]));
            gvListofDOProduct.FooterRow.Cells[1].Text = "Total";
            gvListofDOProduct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvListofDOProduct.FooterRow.Cells[2].Text = totalQty.ToString("0");
            gvListofDOProduct.FooterRow.Cells[4].Text = totalAmount.ToString("0.00");
        }

       
    }
}