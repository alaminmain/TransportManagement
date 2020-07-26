using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI
{
    public partial class FuelSlip : System.Web.UI.Page
    {
        #region Private Method
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
            dvTrip.DataSource = null;
            dvTrip.DataBind();
            txtSupplier.Text = "Madina Filling Station";

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
        private DataTable LoadAllPendingTrip(string searchKey)
        {
            try
            {
                DataTable dt=null;
                DataTable dt2;

                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {


                    dt2 = gatwayObj.GetAllTripInfoForGridView();
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[FuelSlipNo]= '' and [TripStatus]='0'");
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
        private DataTable LoadAllFuelSlip(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {

                    dt2 = gatwayObj.GetAllTripInfoForGridView();
                    DataRow[] foundRows;

                    foundRows = dt2.Select("FuelSlipNo<>null or FuelSlipNo<>''");

                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                   
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("TripNo").Contains(searchKey) );
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

        private void loadData()
        {
            GridViewRow row = gvlistofBasicData.SelectedRow;
            DataTable dt = new DataTable();
            using (TripInfoGateway gatwayObj = new TripInfoGateway())
            {
                dt = gatwayObj.GetTripInfo(row.Cells[1].Text);

            }

            dvTrip.DataSource = dt;
            dvTrip.DataBind();
            decimal kmPerLiter_10 = Convert.ToDecimal(dt.Rows[0]["KmPerLiter"]);

            hfStatus.Value = Convert.ToString(dt.Rows[0]["TripStatus"]);

            string Remarks_16 = Convert.ToString(dt.Rows[0]["Remarks"]); ;
            decimal totalKm_17 = Convert.ToDecimal(dt.Rows[0]["Totalkm"]); ;

            //txtAdditionalFuel.Text = Convert.ToString(dt.Rows[0]["Additionalkm"]);
            decimal additionalKm_18 = 0;
            additionalKm_18 = Convert.ToDecimal(dvTrip.Rows[8].Cells[1].Text);
            if (String.IsNullOrEmpty(dt.Rows[0]["AdjFuelQty"].ToString()))
            {
                txtAdjustedFuelQty.Text = "0";
            }
            else
            {
                txtAdjustedFuelQty.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows[0]["AdjFuelQty"]));
            }
            decimal fuelQty_15 = ((totalKm_17 + additionalKm_18) / kmPerLiter_10);// - Convert.ToDecimal(txtAdjustedFuelQty.Text);
            txtfuelQty.Text = String.Format("{0:0.00}", fuelQty_15);
            
            lblFuelSlipNo.Text = Convert.ToString(dt.Rows[0]["FuelSlipNo"]);
            hfShowTrip.Value = row.Cells[1].Text;
        }
        #endregion
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
            DataTable dt = new DataTable();
            dt = LoadAllFuelSlip(txtSearchTrip.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnTripSearch_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllPendingTrip(txtSearchTrip.Text);
            gvTrip.DataSource = dt;
            gvTrip.DataBind();
            hfShowTrip_ModalPopupExtender.Show();
        }

        protected void gvTrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvTrip.SelectedRow;
            DataTable dt = new DataTable();
            using (TripInfoGateway gatwayObj = new TripInfoGateway())
            {
                dt = gatwayObj.GetTripInfo(row.Cells[1].Text);

            }

            dvTrip.DataSource = dt;
            dvTrip.DataBind();
            decimal kmPerLiter_10 = Convert.ToDecimal(dt.Rows[0]["KmPerLiter"]);


            hfStatus.Value = Convert.ToString(dt.Rows[0]["TripStatus"]);
            string Remarks_16 = Convert.ToString(dt.Rows[0]["Remarks"]); ;
            decimal totalKm_17 = Convert.ToDecimal(dt.Rows[0]["Totalkm"]); ;

            //txtAdditionalFuel.Text = Convert.ToString(dt.Rows[0]["Additionalkm"]);
            decimal additionalKm_18 = 0;
            additionalKm_18= Convert.ToDecimal(dvTrip.Rows[8].Cells[1].Text);
            if (String.IsNullOrEmpty(dt.Rows[0]["AdjFuelQty"].ToString()))
            {
                txtAdjustedFuelQty.Text = "0";
            }
            else
            {
                txtAdjustedFuelQty.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows[0]["AdjFuelQty"]));
            }
            decimal fuelQty_15 = ((totalKm_17 + additionalKm_18) / kmPerLiter_10);
            txtfuelQty.Text = String.Format("{0:0.00}", fuelQty_15);

            lblFuelSlipNo.Text = Convert.ToString(dt.Rows[0]["FuelSlipNo"]);
            hfShowTrip.Value = row.Cells[1].Text;
        }

        protected void gvTrip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllPendingTrip(txtSearchTrip.Text);

            gvTrip.PageIndex = e.NewPageIndex;
            gvTrip.DataSource = dt;
            gvTrip.DataBind();
            upTrip.Update();
            hfShowTrip_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
         
            int comcode_1 = 1;

            string tripNo_2 = hfShowTrip.Value;
                string fuelSlipNo_3= lblFuelSlipNo.Text;
             

                string supplierName_4 = txtSupplier.Text;


            
                decimal? fuelQty_5 = Convert.ToDecimal(txtfuelQty.Text);
                decimal? adjFuelQty_6 = Convert.ToDecimal(txtAdjustedFuelQty.Text);
                int tripStatus_7 = 1;
                if (Convert.ToInt32(hfStatus.Value) != 0)
                tripStatus_7 = Convert.ToInt32(hfStatus.Value);
                
                string userCode_8 = Session["UserName"].ToString();


            using (TripInfoGateway gatwayObj = new TripInfoGateway())
            {
                string fuelSlipNo = gatwayObj.InsertUpdateFuelInfo(comcode_1, tripNo_2, fuelSlipNo_3, supplierName_4, fuelQty_5, adjFuelQty_6, tripStatus_7,
                    userCode_8);

                lblFuelSlipNo.Text = fuelSlipNo;
            }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('record Saved');", true);

            
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string fuelno = lblFuelSlipNo.Text;

            string reporton = "FuelSlip";

            Session["paramData"] = fuelno;
            Session["reportOn"] = reporton;
            //btnReport.PostBackUrl = "~/UI/reportViewer.aspx";
            Response.Redirect("~/UI/reportViewer.aspx");
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllFuelSlip(txtSearchTrip.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void txtAdditionalFuel_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        protected void txtAdjustedFuelQty_TextChanged(object sender, EventArgs e)
        {

            //decimal kmPerLiter_10 = Convert.ToDecimal(dvTrip.Rows[6].Cells[1].Text.ToString());
            //decimal totalKm_17 = Convert.ToDecimal(dvTrip.Rows[7].Cells[1].Text.ToString()); ;
            //decimal additionalKm_18 = 0;
            //decimal adjustedFuelQty = 0;
            //if (Decimal.TryParse(txtAdditionalFuel.Text, out additionalKm_18) && (Decimal.TryParse(txtAdjustedFuelQty.Text, out adjustedFuelQty)))
            //{
            //    decimal? fuelQty_15 = ((totalKm_17 + additionalKm_18) / kmPerLiter_10) - adjustedFuelQty;
            //    txtfuelQty.Text = String.Format("{0:0.00}", fuelQty_15);
            //}
            //else
            //{
            //    decimal? fuelQty_15 = ((totalKm_17 + additionalKm_18) / kmPerLiter_10) - adjustedFuelQty;
            //    txtfuelQty.Text = String.Format("{0:0.00}", fuelQty_15);
            //}
        }
    }
       
    }