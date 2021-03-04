using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TransportManagerLibrary.DAL;
using System.IO;
using TransportManagerLibrary.UtilityClass;
using CrystalDecisions.CrystalReports.Engine;

namespace TransportManagerUI.UI
{
    public partial class TripStatusReport : System.Web.UI.Page
    {
        #region Private Method

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
        private DataTable loadAllDealerInfo(string searchKey)
        {
            try
            {
                DataTable dt;
                DataTable dt2 = null;
                DataView view = new DataView();
                using (DealerGateway gatwayObj = new DealerGateway())
                {

                    dt = gatwayObj.Get_All_Dealer();
                    view = new DataView(dt);
                    dt2 = view.ToTable(false, "CustId", "CustName");

                    if (String.IsNullOrEmpty(searchKey))
                    {


                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("CustId").ToUpper().Contains(searchKey.ToUpper())
           || r.Field<String>("CustName").ToUpper().Contains(searchKey.ToUpper()));

                        dt2 = (filtered.CopyToDataTable());
                    }
                    return dt2;

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                return null;

            }
        }
        private DataTable GetVehicle(string searchKey)
        {
            try
            {
                DataTable dt;
                DataTable dt2 = null;
                DataView view = new DataView();

                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {


                    dt = gatwayObj.GetAllVehicle(1);
                    view = new DataView(dt);
                    dt2 = view.ToTable(false, "VehicleID", "VehicleNo");
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt2.AsEnumerable()
    .Where(r => r.Field<String>("VehicleID").Contains(searchKey) || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()));

                        dt2 = filtered.CopyToDataTable();

                    }
                    return dt2;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
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
                    ddlGhat.DataSource = dt;
                    ddlGhat.DataValueField = "StoreCode";
                    ddlGhat.DataTextField = "StoreName";
                    ddlGhat.DataBind();
                    //ddlGhat.Items.Add(new ListItem("All", "0"));

                    //ddlGhat.DataBind();
                   

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);

            }
        }
        private DataTable GetVehicle()
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;
                int getSelectedIndex=Convert.ToInt32(gvVehicleStatus.SelectedValue);
                string storewise = String.Empty;

                if (String.IsNullOrEmpty(ddlGhat.SelectedValue))
                {
                    storewise = String.Empty;
                }
                else
                {
                    int selectedGhat = Convert.ToInt32(ddlGhat.SelectedValue);
                    storewise = "AND GhatCode='" + selectedGhat + "'";
                }
                           
                    
                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {
                    dt2 = gatwayObj.GetVehicleCurrentStatus(1, getSelectedIndex);
                    DataRow[] foundRows=null;

                    // Use the Select method to find all rows matching the filter.
                    if(String.IsNullOrEmpty(storewise))
                    {
                        foundRows = dt2.Select();
                    }
                    else
                        foundRows = dt2.Select("[GhatCode]='" + ddlGhat.SelectedValue + "'");

                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();

                    return dt;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }
        private DataTable GetCurrentVehicleStatus()
        {
            try
            {
                DataTable dt;


                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {


                    dt = gatwayObj.GetVehicleCurrentStatus(1);

                    return dt;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }
        private DataTable GetCurrentVehicleStatusGhatwise()
        {
            try
            {
                DataTable dt;

                int ghatCode = Convert.ToInt32(ddlGhat.SelectedValue);
                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {


                    dt = gatwayObj.GetVehicleCurrentStatusGhatwise(1,ghatCode);

                    return dt;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }

       

        #endregion
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }
            isAuthorizeToPage();
            if ((!IsPostBack) && (!IsCallback))
            {
                getGhat();
                DataTable dt=GetCurrentVehicleStatus();
                gvVehicleStatus.DataSource = dt;
                gvVehicleStatus.DataBind();
                decimal totalVehicle =dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["NoofVehicles"]));
                

                gvVehicleStatus.FooterRow.Cells[0].Text = "Total";
                gvVehicleStatus.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                gvVehicleStatus.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                gvVehicleStatus.FooterRow.Cells[1].Text = totalVehicle.ToString("0");
               

            }
            
        }

        protected void ddlGhat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (String.IsNullOrEmpty(ddlGhat.SelectedValue))
            {
                dt=GetCurrentVehicleStatus();              

            }
            else
            {
                dt=GetCurrentVehicleStatusGhatwise();
            }
            gvVehicleStatus.DataSource = dt;
            gvVehicleStatus.DataBind();

            decimal totalVehicle = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["NoofVehicles"]));

            gvVehicleStatus.FooterRow.Cells[0].Text = "Total";
            gvVehicleStatus.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            gvVehicleStatus.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            gvVehicleStatus.FooterRow.Cells[1].Text = totalVehicle.ToString("0");
        }

        protected void gvVehicleStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                DataTable dt=GetVehicle();
                gvVehicleList.DataSource = dt;
                gvVehicleList.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvVehicleList.ClientID + "', 500,750, 30 ,true); </script>", false);
           
            //decimal totalVehicle = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["NoofVehicles"]));


            //gvVehicleStatus.FooterRow.Cells[0].Text = "Total";
            //gvVehicleStatus.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            //gvVehicleStatus.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            //gvVehicleStatus.FooterRow.Cells[1].Text = totalVehicle.ToString("0");

        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvVehicleList.ClientID + "', 500,750, 30 ,true); </script>", false);
            string SelectionFormula;
            int statuswise=0;
            try
            {
                if (gvVehicleStatus.SelectedValue != null)
                {
                    GridViewRow row = gvVehicleStatus.SelectedRow;
                    statuswise = Convert.ToInt32(gvVehicleStatus.DataKeys[gvVehicleStatus.SelectedIndex].Value);
                    if (String.IsNullOrEmpty(ddlGhat.SelectedValue))
                    {
                        SelectionFormula = "{VehicleInfo.VehicleStatus}=" + statuswise + "";
                    }
                    else
                        SelectionFormula = "{VehicleInfo.VehicleStatus}=" + statuswise + " AND {StoreLocation.StoreCode}='" + ddlGhat.SelectedValue + "'";
                }
                else
                {
                    if (String.IsNullOrEmpty(ddlGhat.SelectedValue))
                    {
                        SelectionFormula = "";
                    }
                    else
                        SelectionFormula = "{StoreLocation.StoreCode}='" + ddlGhat.SelectedValue + "'";
                }
                string strReportName;
                string strPath;
                CommonGateway cm = new CommonGateway();
                ////cryRpt.Close();

                //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                strReportName = "~//report//VehicleStatus.rpt";
                strPath = Server.MapPath(strReportName);
                //cryRpt.Load(strPath);
                if (String.IsNullOrEmpty(SelectionFormula)==false)
                {
                    Session["strPath"] = strPath;
                    Session["SelectionFormula"] = SelectionFormula;
                    Session["AllStatement"] = "1";
                    //Session["fromDate"] = fromValue;
                    //Session["ToDate"] = ToValue;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {

            gvVehicleStatus.SelectedIndex = -1;
            getGhat();
            DataTable dt = GetCurrentVehicleStatus();
            gvVehicleStatus.DataSource = dt;
            gvVehicleStatus.DataBind();
            decimal totalVehicle = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["NoofVehicles"]));


            gvVehicleStatus.FooterRow.Cells[0].Text = "Total";
            gvVehicleStatus.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            gvVehicleStatus.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            gvVehicleStatus.FooterRow.Cells[1].Text = totalVehicle.ToString("0");
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvVehicleList.ClientID + "', 500,750, 30 ,true); </script>", false);
        }

        protected void gvVehicleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = GetVehicle();
            gvVehicleList.PageIndex = e.NewPageIndex;
            gvVehicleList.DataSource = dt;
            gvVehicleList.DataBind();
            //decimal totalVehicle = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["NoofVehicles"]));


            //gvVehicleStatus.FooterRow.Cells[0].Text = "Total";
            //gvVehicleStatus.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            //gvVehicleStatus.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            //gvVehicleStatus.FooterRow.Cells[1].Text = totalVehicle.ToString("0");
        }

        protected void btnWorkShopReceived_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/VehicleMovmentEntry.aspx");
        }

        protected void btnPullReceived_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/PullReceive.aspx");
        }

        protected void btnAdvanceLoad_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/TripAdvInfo.aspx");
        }
    }
}