using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using CrystalDecisions.CrystalReports.Engine;

namespace TransportManagerUI.UI
{
    public partial class PendingMovementInfo : System.Web.UI.Page
    {
        #region Private Method

        private DataTable loadAllMovement(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (TransportMasterGateway gatwayObj = new TransportMasterGateway())
                {
                    
                    
                    dt2 = gatwayObj.GetAllMovementAll(1);


                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select();
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("MovementNo").Contains(searchKey) || r.Field<String>("TripNo").ToUpper().Contains(searchKey.ToUpper())
     || r.Field<String>("DealerName").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("TransportDate").Contains(searchKey.ToUpper())
      || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("EmpName").ToUpper().Contains(searchKey.ToUpper()));
                        dt = filtered.CopyToDataTable();

                    }
                    return dt;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                throw ex;
                //return null;
            }
        }

        private void GetTotal(DataTable dt)
        {
          

            decimal RowCount = dt.Rows.Count;

            gvlistofBasicData.FooterRow.Cells[1].Text = "Total";
            gvlistofBasicData.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvlistofBasicData.FooterRow.Cells[3].Text = string.Format("{0:0}", RowCount);
         

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
                dt = loadAllMovement(txtSearch.Text);


                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                GetTotal(dt);
                upListofbasicData.Update();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 39 ,true); </script>", false);
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllMovement(txtSearch.Text);


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            GetTotal(dt);
            upListofbasicData.Update();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 39 ,true); </script>", false);
        }

        protected void btnMovement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/MovmentInfo.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            TransportManagerLibrary.DAL.CommonGateway cm = new CommonGateway();
            //ReportDocument //cryRpt;

            string strReportName;
            string strPath;

         
            string SelectionFormula;
            
            strReportName = "~//report//rptTripMovementStatement.rpt";
            strPath = Server.MapPath(strReportName);
            //cryRpt.Load(strPath);

            SelectionFormula = "{TripInfo.TripStatus} = 0";
                      
            Session["strPath"] = strPath;
            Session["SelectionFormula"] = SelectionFormula;
            //Session["fromDate"] = fromValue;
            //Session["ToDate"] = ToValue;
            Session["AllStatement"] = "1";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);

            
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 39 ,true); </script>", false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllMovement(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllMovement(txtSearch.Text);


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            GetTotal(dt);
            upListofbasicData.Update();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 39 ,true); </script>", false);
        }
    }
}