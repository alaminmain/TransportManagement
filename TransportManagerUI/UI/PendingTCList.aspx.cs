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
using CrystalDecisions.CrystalReports.Engine;

namespace TransportManagerUI.UI
{
    public partial class PendingTCList : System.Web.UI.Page
    {
        #region Private Method
        private DataTable LoadAllTC(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (TransContactGateway gatwayObj = new TransContactGateway())
                {

                    dt2 = gatwayObj.GetAllTransContactWithBalance(1);


                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[TCStatus]= '0'");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();


                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
  .Where(r => r.Field<String>("TCNo").ToUpper().Contains(searchKey.ToUpper()) //|| r.Field<String>("OrderQty").Contains(searchKey.ToUpper())
           || r.Field<String>("DealerName").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("CustomerName").ToUpper().Contains(searchKey.ToUpper()));
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

                DataTable dt = new DataTable();
                dt = LoadAllTC(txtSearch.Text);


                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                upListofbasicData.Update();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);
            }
        }

        protected void btnTC_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/TransportContact.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTC(txtSearch.Text);


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTC(txtSearch.Text);


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTC(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            TransportManagerLibrary.DAL.CommonGateway cm = new CommonGateway();
            ReportDocument cryRpt;
            ReportDocument nreport;
            string strReportName;
            string strPath;

            string fromValue = Convert.ToDateTime(DateTime.Now.Date).ToString("yyyy,MM,dd,00,00,00");
            string ToValue = Convert.ToDateTime(DateTime.Now.Date).ToString("yyyy,MM,dd,00,00,00"); ;
            //cryRpt.Close();
            //string SelectionFormula;
            cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            strReportName = "~//report//TCStateGhat.rpt"; 
            strPath = Server.MapPath(strReportName);
            cryRpt.Load(strPath);


            //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
            //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

            cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

            //cryRpt.RecordSelectionFormula = SelectionFormula;


            nreport = cm.ConnectionInfo(cryRpt);
            Session["nreport"] = nreport;

            Response.Redirect("~/UI/ReportForStatement.aspx");

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void gvlistofBasicData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }
    }
}