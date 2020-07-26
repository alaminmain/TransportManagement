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
    public partial class VehicleInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }
            isAuthorizeToPage();
            if (!IsPostBack && !IsCallback)
            {
                getGhat();
                LoadFuelType();
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

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
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

        }
        private DataTable GetVehicle(string searchKey)
        {
            try
            {
                DataTable dt;


                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {


                    dt = gatwayObj.GetAllVehicle(1);
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
       
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int comCode = 1;
                string VehicleID_1 = lblVehicleId.Text;
                string VehicleNo_2 = txtVehicleNo.Text;
                string ChesisNo_3 = txtChesisNo.Text;
                string ModelNo_4 = txtModelNo.Text;
                string EngineNo_5 = txtEngineNo.Text;
                string EngineVolume_16 = txtEngineVolume.Text;
                string PurchaseDate_17 = txtPurchaseDate.Text;
                string VehicleDesc_6 = txtVehicleDescription.Text;
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
                
                    IsHired_12 = 0;
               
                int VehicleStatus_13 = Convert.ToInt32(ddlStatus.SelectedValue);
                string userId = Session["UserName"].ToString();
                string capacityUnit = ddlCapacityUnit.SelectedItem.Text;
                string Remarks_18 = txtRemarks.Text;
                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {
                    VehicleID_1 = gatwayObj.InsertUpdateVehicle(comCode,VehicleID_1, VehicleNo_2, ChesisNo_3, ModelNo_4, EngineNo_5, VehicleDesc_6,
                        Mobile_7, Capacity_8, KmPerLiter_9, FuelCode_10, StoreCode_11, IsHired_12, VehicleStatus_13, userId,capacityUnit,EngineVolume_16,PurchaseDate_17,Remarks_18);
                    lblVehicleId.Text = VehicleID_1;

                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearAll();

                GridViewRow rows = gvlistofBasicData.SelectedRow;
                var textbox = rows.FindControl("Label1") as Label;
                string code = textbox.Text;
                DataTable dt = new DataTable();

                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {
                    dt = gatwayObj.GetVehicleById(1, code);
                }
                //SELECT    ComCode, StoreCode, VehicleID, VehicleNo, ChesisNo, ModelNo, EngineNo, VehicleDesc, MobileNo, 
                //Capacity, KmPerLiter, FuelCode, IsHired, VehicleStatus FROM            VehicleInfo where VehicleID=@VehicleId and comCode=@ComCode
                if (dt != null)
                {
                    lblVehicleId.Text = dt.Rows[0]["VehicleID"].ToString();
                    txtVehicleNo.Text = dt.Rows[0]["VehicleNo"].ToString();
                    txtChesisNo.Text = dt.Rows[0]["ChesisNo"].ToString();
                    txtModelNo.Text = dt.Rows[0]["ModelNo"].ToString();
                    if (String.IsNullOrEmpty(dt.Rows[0]["StoreCode"].ToString()) == false)
                    {
                        ddlGhatList.Text = dt.Rows[0]["StoreCode"].ToString();
                    }
                     if (String.IsNullOrEmpty(dt.Rows[0]["StoreCode"].ToString()))
                    {
                        ddlGhatList.SelectedValue = "1";
                     }
                     else
                         ddlGhatList.SelectedValue = dt.Rows[0]["StoreCode"].ToString();
                   
                    
                    txtEngineNo.Text = dt.Rows[0]["EngineNo"].ToString();
                    txtEngineVolume.Text= dt.Rows[0]["EngineVolume"].ToString();
                    txtPurchaseDate_CalendarExtender.SelectedDate= Convert.ToDateTime(dt.Rows[0]["PurchaseDate"].ToString());
                    txtVehicleDescription.Text = dt.Rows[0]["VehicleDesc"].ToString();
                    txtMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                    txtCapacity.Text = dt.Rows[0]["Capacity"].ToString();

                    txtKmPerLitre.Text = dt.Rows[0]["KmPerLiter"].ToString();
                    ddlFuelType.SelectedValue = dt.Rows[0]["FuelCode"].ToString();
                    if(String.IsNullOrEmpty(dt.Rows[0]["CapacityUnit"].ToString())==false)
                    {
                      ddlCapacityUnit.SelectedItem.Text = dt.Rows[0]["CapacityUnit"].ToString();
                    }


                   
                    txtRemarks.Text= dt.Rows[0]["Remarks"].ToString();
                    ddlStatus.SelectedValue = dt.Rows[0]["VehicleStatus"].ToString();
                }
                hfShowList_ModalPopupExtender.Hide();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}