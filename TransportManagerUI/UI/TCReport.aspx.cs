using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI
{
    public partial class TCReport : System.Web.UI.Page
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
             if (rblReports.SelectedValue == "TCStatmentDealerwise")
            {
                plOption1.Visible = true;
                plOption1.GroupingText = "Search Dealer";
                plOption2.Visible = false;
                plOption2.GroupingText = "";
            }
            else if (rblReports.SelectedValue == "TCStatmentGhatwise")
            {
                plOption1.Visible = false;
                plOption1.GroupingText = "";
                plOption2.Visible = true;
                plOption2.GroupingText = "Search Ghat";
            }
            else if (rblReports.SelectedValue == "TCStatmentProductWise")
            {
                plOption1.Visible = false;
                plOption1.GroupingText = "";
                plOption2.Visible = false;
                plOption2.GroupingText = "";
            }

            else if (rblReports.SelectedValue == "TCStatement")
            {
                plOption1.Visible = false;
                plOption1.GroupingText = "";
                plOption2.Visible = false;
                plOption2.GroupingText = "";
            }

        }
        public void ReportOption(string Option)
        {
            TransportManagerLibrary.DAL.CommonGateway cm = new CommonGateway();
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
                    

                    case "TCStatmentDealerwise":


                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TCStateDealer.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);
                        if (String.IsNullOrEmpty(lblCode.Text))
                        {

                            SelectionFormula = "{TransContact.TCDate} in Date(" +
                                               fromValue + ")   to Date(" +
                                               ToValue + ")";


                        }
                        else
                        {

                            SelectionFormula = "{TransContact.TCDate} in Date(" + fromValue + ")   to Date(" +
                                                            ToValue + ") and" +
                                                                       " {TransContact.DealerId} = '" + lblCode.Text + "'";

                        }

                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        cryRpt.RecordSelectionFormula = SelectionFormula;



                        Session["nreport"] = cryRpt;
                        //cryRpt.Close();
                        break;

                    case "TCStatmentGhatwise":


                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TCStateGhat.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);
                        if (String.IsNullOrEmpty(lblCode2.Text))
                        {

                            SelectionFormula = "{TransContact.TCDate} in Date(" +
                                               fromValue + ")   to Date(" +
                                               ToValue + ")";


                        }
                        else
                        {

                            SelectionFormula = "{TransContact.TCDate} in Date(" + fromValue + ")   to Date(" +
                                                            ToValue + ") and" +
                                                                       " {TransContact.StoreCode} = '" + Convert.ToInt32(lblCode2.Text) + "'";

                        }
                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        cryRpt.RecordSelectionFormula = SelectionFormula;


                        Session["nreport"] = cryRpt;
                        //cryRpt.Close();
                        break;

                    case "TCStatmentProductWise":


                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TCStateProd.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);

                        SelectionFormula = "{TransContact.TCDate} in Date(" +
                                           fromValue + ")   to Date(" +
                                           ToValue + ")";



                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        cryRpt.RecordSelectionFormula = SelectionFormula;



                        Session["nreport"] = cryRpt;
                        //cryRpt.Close();
                        break;

                    case "TCStatement":


                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TCStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);

                        SelectionFormula = "{TransContact.TCDate} in Date(" +
                                           fromValue + ")   to Date(" +
                                           ToValue + ")";



                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        cryRpt.RecordSelectionFormula = SelectionFormula;



                        Session["nreport"] = cryRpt;
                        //cryRpt.Close();
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
            //string reportname;
            
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

        

        protected void rblReports_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            ReportsPanelOption();
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            
            
            ReportOption(rblReports.SelectedValue);
            Session["AllStatement"] = "1";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "~/UI/ReportForStatement.aspx" + "','_blank')", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
        }
        protected void btnShowStatment_Click(object sender, EventArgs e)
        {
            string option = rblReports.SelectedValue;
            ReportOption(option);
            Session["AllStatement"] = "2";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "~/UI/ReportForStatement.aspx" + "','_blank')", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
           

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