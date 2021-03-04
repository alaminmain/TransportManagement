using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TransportManagerLibrary.UtilityClass;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.SqlClient;

namespace TransportManagerUI.UI.Workshop
{
    public partial class StoreStatus : System.Web.UI.Page
    {
        #region Private Method

        private void loadddlDepartment()
        {
            string sql = @"select CateCode,CateName from Item_Category";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "CateName";
                ddlDepartment.DataValueField = "CateCode";
                ddlDepartment.DataBind();
            }
        }
        private void Load_ddlProdSubCategory()
        {
            string sql = @"select CateCode,ProdSubCatCode,ProdSubCatName from ItemSubCategory where CateCode=@CateCode";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={
                                              new SqlParameter("@CateCode", ddlDepartment.SelectedValue)
                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

            if (dt.Rows.Count > 0)
            {
                ddlSubDepartment.DataSource = dt;
                ddlSubDepartment.DataTextField = "ProdSubCatName";
                ddlSubDepartment.DataValueField = "ProdSubCatCode";
                ddlSubDepartment.DataBind();
            }

        }
        private DataTable loadAllStorestatus(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;



                string sql = @"select IT.ProductCode,IT.ProductName,IT.ProductBName,IT.Reorder,IT.DistPrice,IST.StoreCode,IST.PhyStock,CAST((IT.DistPrice*IST.PhyStock) AS DECIMAL(18,2))as Value ,
                                Case  when IST.PhyStock<IT.Reorder then'Quantity Sort' else '-' end 'Stutas' 
                                from Item IT Inner join ItemSubCategory ITS on IT.ProdSubCatCode=ITS.ProdSubCatCode and IT.CateCode=ITS.CateCode
                               inner join Item_Category ITC on ITS.CateCode=ITC.CateCode
                               inner join ItemStock IST on IT.ProductCode=IST.ProductCode and IT.StoreCode=IST.StoreCode 
                               where ITS.ProdSubCatCode=@ProdSubCatCode and ITC.CateCode=@CateCode order by (IT.ProductName)asc";
                
                SqlParameter[] parameter ={

                                              new SqlParameter("@ProdSubCatCode", ddlSubDepartment.SelectedValue ),
                                              new SqlParameter("@CateCode", ddlDepartment.SelectedValue ),
                                              

                                            };
                dt2 = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];



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
.Where(r => r.Field<String>("IssVouchNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("IssDate").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()) || r.Field<String>("VMFStatus").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();

                }
                return dt;

            }

            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }

        //private void GetTotal(DataTable dt)
        //{
        //    decimal Qty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["OrderQty"]));
        //    decimal SOQty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["SOQty"]));
        //    decimal PendingQty = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["PendingQty"]));

        //    decimal RowCount = dt.Rows.Count;

        //    gvlistofBasicData.FooterRow.Cells[1].Text = "Total";
        //    gvlistofBasicData.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        //    gvlistofBasicData.FooterRow.Cells[3].Text = string.Format("{0:n}", RowCount);
        //    gvlistofBasicData.FooterRow.Cells[9].Text = string.Format("{0:n}", Qty);
        //    gvlistofBasicData.FooterRow.Cells[10].Text = string.Format("{0:n}", SOQty);
        //    gvlistofBasicData.FooterRow.Cells[11].Text = string.Format("{0:n}", PendingQty);

        //}

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
            //isAuthorizeToPage();
            //lblMessage.Text = string.Empty;
            if ((!IsPostBack) && (!IsCallback))
            {

                DataTable dt = new DataTable();
                dt = loadAllStorestatus(txtSearch.Text);
                loadddlDepartment();
                Load_ddlProdSubCategory();
                if (dt != null)
                {
                    gvlistofBasicData.DataSource = dt;
                    gvlistofBasicData.DataBind();
                    //GetTotal(dt);
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


        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllStorestatus(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            //GetTotal(dt);
            upListofbasicData.Update();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllStorestatus(txtSearch.Text);


            if (dt != null)
            {
                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                //GetTotal(dt);
            }
            else
            {
                gvlistofBasicData.DataSource = null;
                gvlistofBasicData.DataBind();
            }
            upListofbasicData.Update();

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllStorestatus(txtSearch.Text);


            if (dt != null)
            {
                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                //GetTotal(dt);
            }
            else
            {
                gvlistofBasicData.DataSource = null;
                gvlistofBasicData.DataBind();
            }
            upListofbasicData.Update();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);

        }

        protected void btnNewDO_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/VMFIssue.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            ////TransportManagerLibrary.DAL.CommonGateway cm = new CommonGateway();
            ////ReportDocument //cryRpt;

            //string strReportName;
            //string strPath;


            //strReportName = "~//report//DOStatement.rpt";
            //strPath = Server.MapPath(strReportName);


            //string SelectionFormula = "{SalesProducts.OrderQty}-{SalesProducts.OrderBalQty}>0";



            //Session["strPath"] = strPath;
            //Session["SelectionFormula"] = SelectionFormula;

            //Session["AllStatement"] = "1";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 40 ,true); </script>", false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}