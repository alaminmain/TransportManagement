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
using CrystalDecisions.CrystalReports.Engine;

namespace TransportManagerUI.UI
{
    public partial class PendingTripInfo : System.Web.UI.Page
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
                    foundRows = dt2.Select("[TripStatus]= '0'");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("TripNo").Contains(searchKey) || r.Field<String>("TripDate").Contains(searchKey.ToUpper())
           || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("CustName").ToUpper().Contains(searchKey.ToUpper()));
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
                
                if ((!IsPostBack) && (!IsCallback))
                {

                    DataTable dt = new DataTable();
                    dt = LoadAllTrip(txtSearch.Text);


                    gvlistofBasicData.DataSource = dt;
                    gvlistofBasicData.DataBind();
                    upListofbasicData.Update();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 30 ,true); </script>", false);
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
            upListofbasicData.Update();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTrip(txtSearch.Text);


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 30 ,true); </script>", false);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTrip(txtSearch.Text);


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 20 ,true); </script>", false);
        }

        protected void txtNewTrip_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/TripInfo.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            //ReportDocument //cryRpt;
            
            string strReportName;
            string strPath;
            
            ////cryRpt.Close();
            string SelectionFormula;

            //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            strReportName = "~//report//TripStatement.rpt";
            strPath = Server.MapPath(strReportName);
            //cryRpt.Load(strPath);
            SelectionFormula = "{TripInfo.TripStatus} = 0";

           
           
            Session["strPath"] = strPath;
            Session["SelectionFormula"] = SelectionFormula;
         
            
            Session["AllStatement"] = "1";
            //Session["nreport"] = //cryRpt;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}