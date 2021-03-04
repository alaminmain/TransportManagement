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
    public partial class DatatablePendingDOList : System.Web.UI.Page
    {
        #region Private Method
        private DataTable LoadAllDO(string searchKey)
        {
            try
            {
                DataTable dt=null;
                DataTable dt2;


                using (SalesGateway gatwayObj = new SalesGateway())
                {

                    dt2 = gatwayObj.GetAllSalesWithAddress();
                    
                    
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[InvStatus]= '0'");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                    

                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
  .Where(r => r.Field<String>("InvNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("ProductName").ToUpper().Contains(searchKey.ToUpper())
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

        private void GetTotal(DataTable dt)
        {
            decimal Qty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
            decimal SOQty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["SOQty"]));
            decimal PendingQty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["PendingQty"]));
           
            decimal RowCount = dt.Rows.Count;
            
            gvlistofBasicData.FooterRow.Cells[1].Text = "Total";
            gvlistofBasicData.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvlistofBasicData.FooterRow.Cells[3].Text = RowCount.ToString("0");
            gvlistofBasicData.FooterRow.Cells[9].Text = Qty.ToString("0");
            gvlistofBasicData.FooterRow.Cells[10].Text = SOQty.ToString("0");
            gvlistofBasicData.FooterRow.Cells[11].Text = PendingQty.ToString("0");
           
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
            //if (Session["UserName"] == null)
            //{
            //    Session.Abandon();
            //    Response.Redirect("~/login.aspx");
            //}
            //isAuthorizeToPage();
            //lblMessage.Text = string.Empty;
            if ((!IsPostBack) && (!IsCallback))
            {

                DataTable dt = new DataTable();
                dt = LoadAllDO(txtSearch.Text);


                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                GetTotal(dt);
                upListofbasicData.Update();
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 100 ,true); </script>", false);

            }
        }

       
        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllDO(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            GetTotal(dt);
            upListofbasicData.Update();
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllDO(txtSearch.Text);

           
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            GetTotal(dt);
            upListofbasicData.Update();

           // ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 100 ,true); </script>", false);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllDO(txtSearch.Text);

            
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            GetTotal(dt);
            upListofbasicData.Update();
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);

        }

        protected void btnNewDO_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/DO.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            TransportManagerLibrary.DAL.CommonGateway cm = new CommonGateway();
            //ReportDocument //cryRpt;
            
            string strReportName;
            string strPath;

         
            strReportName = "~//report//DOStatement.rpt";
            strPath = Server.MapPath(strReportName);
           

            string SelectionFormula = "{SalesProducts.OrderQty}-{SalesProducts.OrderBalQty}>0";
          
         

            Session["strPath"] = strPath;
            Session["SelectionFormula"] = SelectionFormula;
          

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void gvlistofBasicData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes["onmouseover"] = "onMouseOver('" + (e.Row.RowIndex + 1) + "')";
            //    e.Row.Attributes["onmouseout"] = "onMouseOut('" + (e.Row.RowIndex + 1) + "')";
            //}
        }

        protected void gvlistofBasicData_PreRender(object sender, EventArgs e)
        {
            
        }
    }
}