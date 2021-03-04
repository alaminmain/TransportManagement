using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI
{
    public partial class PendingTripForVoucher : System.Web.UI.Page
    {
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
                    foundRows = dt2.Select("[TripStatus]= '1'");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("TripNo").Contains(searchKey) || r.Field<String>("TripDate").Contains(searchKey.ToUpper()) || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("EmpName").ToUpper().Contains(searchKey.ToUpper()));
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
        private DataTable LoadAllTripDriverwise(string searchKey)
        {
            try
            {
                DataTable dt = null;
                //DataTable dt2;


                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {


                    dt = gatwayObj.GetAllNotBilledTripDriverwise();

                    //DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    //foundRows = dt2.Select("[TripStatus]= '1'");
                    //if (foundRows.Length > 0)
                    //    dt = foundRows.CopyToDataTable();
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r=>r.Field<String>("Driver").ToUpper().Contains(searchKey.ToUpper()));
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

        private DataTable LoadAllTripVehiclewise(string searchKey)
        {
            try
            {
                DataTable dt = null;
               // DataTable dt2;


                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {


                    dt = gatwayObj.GetAllNotBilledTripVehiclewise();

                    //DataRow[] foundRows;

                    //// Use the Select method to find all rows matching the filter.
                    //foundRows = dt2.Select("[TripStatus]= '1'");
                    //if (foundRows.Length > 0)
                    //    dt = foundRows.CopyToDataTable();
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()));
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

        private void LoadDataInGridviews()
        {
            DataTable dt = new DataTable();
            if (ddlPendingType.SelectedValue == "0")
            {
                gvlistofBasicData.Visible = true;
                //Visibility False For other Grid
                gvlistofBasicData2.Visible = false;
                //gvlistofBasicData3.Visible = false;
                dt = LoadAllTrip(txtSearch.Text);


                gvlistofBasicData.DataSource = dt;

                gvlistofBasicData.DataBind();
                GetTotal(dt);
                upListofbasicData.Update();
                Session["PendingType"] = "0";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 39 ,true); </script>", false);
            }
            //Vehiclewise
            else if (ddlPendingType.SelectedValue == "1")
            {
                gvlistofBasicData.Visible = false;
                //Visibility False For other Grid
                gvlistofBasicData2.Visible = true;
                //gvlistofBasicData3.Visible = false;
                dt = LoadAllTripVehiclewise(txtSearch.Text);


                gvlistofBasicData2.DataSource = dt;

                gvlistofBasicData2.DataBind();

                upListofbasicData.Update();
                Session["PendingType"] = "1";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData2.ClientID + "', 410,1070, 39 ,true); </script>", false);
            }
            //Driverwise
            else if (ddlPendingType.SelectedValue == "2")
            {
                gvlistofBasicData.Visible = false;
                //Visibility False For other Grid
                gvlistofBasicData2.Visible = true;
                //gvlistofBasicData3.Visible = true;
                dt = LoadAllTripDriverwise(txtSearch.Text);


                gvlistofBasicData2.DataSource = dt;

                gvlistofBasicData2.DataBind();

                upListofbasicData.Update();
                
                Session["PendingType"] = "2";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData2.ClientID + "', 410,1070, 39 ,true); </script>", false);
            }
        }

        private void GetTotal(DataTable dt)
        {
           

            decimal RowCount = dt.Rows.Count;

            gvlistofBasicData.FooterRow.Cells[1].Text = "Total";
            gvlistofBasicData.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvlistofBasicData.FooterRow.Cells[3].Text = string.Format("{0:0}", RowCount);
           
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
                    if (Session["PendingType"] != null)
                        ddlPendingType.SelectedValue = (string)Session["PendingType"];
                    else
                        ddlPendingType.SelectedValue = "0";
                    gvlistofBasicData.Visible = false;
                    gvlistofBasicData2.Visible = false;
                    //gvlistofBasicData3.Visible = false;
                    LoadDataInGridviews();
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

      

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTrip(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;

            gvlistofBasicData.DataBind();
            GetTotal(dt);
            upListofbasicData.Update();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataInGridviews();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataInGridviews();
        }

        protected void txtNewVoucher_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Voucher.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 39 ,true); </script>", false);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData2.ClientID + "', 410,1070, 39 ,true); </script>", false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void ddlPendingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataInGridviews();
        }
    }
}