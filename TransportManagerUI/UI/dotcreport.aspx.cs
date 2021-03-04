using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;

using CrystalDecisions.Shared;
using TransportManagerLibrary.DAL;
using CrystalDecisions.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using TransportManagerLibrary.UtilityClass;
namespace TransportManagerUI.UI
{
    public partial class dotcreport : System.Web.UI.Page
    {
        #region Private Method
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
        private DataTable GetGhat(string searchKey)
        {
            try
            {
                DataTable dt;
                DataTable dt2 = null;
                DataView view = new DataView();

                using (StoreLocation gatwayObj = new StoreLocation())
                {


                    dt = gatwayObj.GetAllStoreLocation(1);
                    view = new DataView(dt);
                    dt2 = view.ToTable(false, "StoreCode", "StoreName");
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt2.AsEnumerable()
    .Where(r => r.Field<String>("StoreCode").Contains(searchKey) || r.Field<String>("StoreName").ToUpper().Contains(searchKey.ToUpper()));

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
        private void ReportsPanelOption()
        {
            if (rblDoReports.SelectedValue == "DoStatmentDealerwise")
            {
                plOption1.Visible = true;
                plOption1.GroupingText = "Search Dealer";
                plOption2.Visible = false;
                plOption2.GroupingText = "";
            }
            else if (rblDoReports.SelectedValue == "DoStatmentGhatwise")
            {
                plOption1.Visible = false;
                plOption1.GroupingText = "";
                plOption2.Visible = true;
                plOption2.GroupingText = "Search Ghat";
            }

            else if (rblDoReports.SelectedValue == "DoStatmentProductwise")
            {
                plOption1.Visible = false;
                plOption1.GroupingText = "";
                plOption2.Visible = false;
                plOption2.GroupingText = "";
            }

            else if (rblDoReports.SelectedValue == "DoStatement")
            {
                plOption1.Visible = false;
                plOption1.GroupingText = "";
                plOption2.Visible = false;
                plOption2.GroupingText = "";
            }
           

        }
        public void ReportOption(string Option)
        {
           
            ReportDocument cryRpt;
            
            string strReportName;
            string strPath;
            string fromValue = Convert.ToDateTime(txtFromDate.Text).Date.ToString("yyyy,MM,dd");
            string ToValue = Convert.ToDateTime(txtToDate.Text).Date.ToString("yyyy,MM,dd"); ;
            //cryRpt.Close();
            string SelectionFormula;
            try
            {
                switch (Option)
                {
                    case "DoStatmentDealerwise":

                       
                        strReportName = "~//report//DOStateDealer.rpt";
                        strPath = Server.MapPath(strReportName);
                        
                      
                        if (String.IsNullOrEmpty(lblCode.Text))
                        {

                            SelectionFormula = "{Sales.InvDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";


                        }
                        else
                        {

                            SelectionFormula = "{Sales.InvDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {Sales.CustId} = '" + lblCode.Text + "'";

                        }
                                         

                        ////cryRpt.Close();
                        break;

                    case "DoStatmentGhatwise":


                       
                        strReportName = "~//report//DOStateGhat.rpt";
                        strPath = Server.MapPath(strReportName);
                        
                        if (String.IsNullOrEmpty(lblCode2.Text))
                        {

                            SelectionFormula = "{Sales.InvDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";


                        }
                        else
                        {

                            SelectionFormula = "{Sales.InvDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {Sales.StoreCode} = " + lblCode2.Text + "";

                        }

                       
                        //cryRpt.Close();
                        break;

                    case "DoStatmentProductwise":
                        
                       
                        strReportName = "~//report//DOStateProd.rpt";
                        strPath = Server.MapPath(strReportName);
                        
                       
                            SelectionFormula = "{Sales.InvDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";
                                       
                      
                        //cryRpt.Close();
                        break;

                    case "DoStatement":
                        
                       
                        strReportName = "~//report//DOStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        

                        SelectionFormula = "{Sales.InvDate} in Date(" +
                                           fromValue + ")   to Date (" +
                                           ToValue + ")";


                        break;

                    
                    default:
                        throw new ArgumentException
                        (
                        "GetDataReader was given an incorrect Request for data"
                        );
                }
                Session["strPath"] = strPath;
                Session["SelectionFormula"] = SelectionFormula;
                Session["fromDate"] = fromValue;
                Session["ToDate"] = ToValue;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion Private Method
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                }
                //lblMessage.Text = string.Empty;
                if ((!IsPostBack) && (!IsCallback))
                {
                    txtFromDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                    txtToDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                    hfDoTC.Value = "1";
                  
                    ReportsPanelOption();

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

        protected void btnSearchInfo_Click(object sender, ImageClickEventArgs e)
        {
            string reportname;
           
                reportname = rblDoReports.SelectedValue;
            
            DataTable dt = new DataTable();
            dt = loadAllDealerInfo(txtSearch.Text);
            hfShowList.Value = "1";
            gvlistofBasicData.SelectedIndex = -1;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSearch2_Click(object sender, ImageClickEventArgs e)
        {
            string reportname;
           
                reportname = rblDoReports.SelectedValue;
            
            DataTable dt = new DataTable();
            dt = GetGhat(txtSearch.Text);
            hfShowList.Value = "2";
            gvlistofBasicData.SelectedIndex = -1;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvlistofBasicData.SelectedRow;
            if (hfShowList.Value == "1")
            {
                lblCode.Text = row.Cells[1].Text;
                lblName.Text = row.Cells[2].Text;
                lblCode2.Text = String.Empty;
                lblName2.Text = String.Empty;

            }
            else if (hfShowList.Value == "2")
            {
                lblCode.Text = string.Empty;
                lblName.Text = String.Empty;
                lblCode2.Text = row.Cells[1].Text;
                lblName2.Text = row.Cells[2].Text;
            }
            gvlistofBasicData.SelectedIndex = -1;
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string reportname;
           
                reportname = rblDoReports.SelectedValue;
            

            DataTable dt = new DataTable();
            if (hfShowList.Value == "1")
            {
                dt = loadAllDealerInfo(txtSearch.Text);
            }
            else if (hfShowList.Value == "2")
            {
                dt = GetGhat(txtSearch.Text);
            }
            else
            {
                dt = null;
            }

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void rblDoReports_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            hfDoTC.Value = "1";
            ReportsPanelOption();
            
        }

       

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            string reportname;
           
                reportname = rblDoReports.SelectedValue;
            
            
            ReportOption(reportname);
            Session["AllStatement"] = "1";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "~/UI/ReportForStatement.aspx" + "','_blank')", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
        }
        protected void btnShowStatment_Click(object sender, EventArgs e)
        {
            string option = rblDoReports.SelectedValue;
            ReportOption(option);
            Session["AllStatement"] = "2";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "~/UI/ReportForStatement.aspx" + "','_blank')", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string reportname;
           
                reportname = rblDoReports.SelectedValue;
            
          

            DataTable dt = new DataTable();
            if (hfShowList.Value == "1")
            {
                dt = loadAllDealerInfo(txtSearch.Text);
            }
            else if (hfShowList.Value == "2")
            {
                dt = GetGhat(txtSearch.Text);
            }
            else
            {
                dt = null;
            }

         
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }
    }
}