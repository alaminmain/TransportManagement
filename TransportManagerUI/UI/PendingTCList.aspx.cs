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
  .Where(r => r.Field<String>("TCNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("ProductName").Contains(searchKey.ToUpper())|| r.Field<String>("TCDate").Contains(searchKey.ToUpper())
           || r.Field<String>("DealerName").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("CustName").ToUpper().Contains(searchKey.ToUpper()));
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
        private void GetTotal(DataTable dt)
        {
            decimal Qty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
          

            decimal RowCount = dt.Rows.Count;

            gvlistofBasicData.FooterRow.Cells[1].Text = "Total";
            gvlistofBasicData.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvlistofBasicData.FooterRow.Cells[3].Text = RowCount.ToString("0");
            gvlistofBasicData.FooterRow.Cells[8].Text = string.Format("{0:n}", Qty); 
           

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


                if (dt != null)
                {
                    gvlistofBasicData.DataSource = dt;
                    gvlistofBasicData.DataBind();
                    GetTotal(dt);
                }
                else
                {
                    gvlistofBasicData.DataSource = null;
                    gvlistofBasicData.DataBind();
                }
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


            if (dt != null)
            {
                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                GetTotal(dt);
            }
            else
            {
                gvlistofBasicData.DataSource = null;
                gvlistofBasicData.DataBind();
            }
            upListofbasicData.Update();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTC(txtSearch.Text);


            if (dt != null)
            {
                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                GetTotal(dt);
            }
            else
            {
                gvlistofBasicData.DataSource = null;
                gvlistofBasicData.DataBind();
            }
            upListofbasicData.Update();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTC(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            GetTotal(dt);
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            //TransportManagerLibrary.DAL.CommonGateway cm = new CommonGateway();
           
            
            string strReportName;
            string strPath;

           
       
            string SelectionFormula;
           
            strReportName = "~//report//TCStatement.rpt"; 
            strPath = Server.MapPath(strReportName);
         

            SelectionFormula = "{TransContact.TCStatus} = 0";
        


            Session["strPath"] = strPath;
            Session["SelectionFormula"] = SelectionFormula;
            Session["AllStatement"] = "1";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);

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