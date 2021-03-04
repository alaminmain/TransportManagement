using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using TransportManagerLibrary.DAL;
using System.Data;
using TransportManagerLibrary.UtilityClass;
using System.IO;

namespace TransportManagerUI.UI
{
    public partial class MovementReports : System.Web.UI.Page
    {
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
                txtFromDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                txtToDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
            }
        }

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
        public void ReportOption(string Option)
        {
            //ReportDocument //cryRpt;
            //
            string strReportName;
            string strPath;
            string fromValue = Convert.ToDateTime(txtFromDate.Text).Date.ToString("yyyy,MM,dd");
            string ToValue = Convert.ToDateTime(txtToDate.Text).Date.ToString("yyyy,MM,dd"); ;
            ////cryRpt.Close();
            string SelectionFormula = String.Empty;
            try
            {
                switch (Option)
                {
                    case "MovementStatementRpt": //Movement Statement

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMovementStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);
                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date(" + ToValue + ")";

                        ////cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = "{Transport.TransportDate} in Date(" + fromValue + ")   to Date (" +
                                                            //ToValue + ")";

                        //////cryRpt.Close();
                        break;

                    case "MoveStatementDetail": //Statement Details



                        SelectionFormula = "{Transport.TransportDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";
                        
                       
                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailState.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);
                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date(" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = SelectionFormula;


                        //nreport = ConnectionInfo(//cryRpt);
                        //Session["nreport"] = //cryRpt;
                        ////cryRpt.Close();
                        break;

                    case "MoveStatementVehicle": //MoveStatementVehicle


                        if (String.IsNullOrEmpty(lblName.Text))
                        {
                            btnSearchInfo.Enabled = false;
                            SelectionFormula = "{Transport.TransportDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")and {Transport.TranStatus} <> 2";


                        }
                        else

                        {
                            btnSearchInfo.Enabled = true;
                            SelectionFormula = "{Transport.TransportDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleNo} = '" + lblName.Text + "' and {Transport.TranStatus} <> 2";

                        }

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailStateTruckwise.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);


                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = SelectionFormula;

                        //nreport = ConnectionInfo(//cryRpt);

                        ////cryRpt.Close();
                        break;

                    case "MoveDetailDealer"://MoveDetailDealer

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailStateDealer.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);


                        if (String.IsNullOrEmpty(lblCode.Text))
                        {
                            btnSearchInfo.Enabled = false;
                            SelectionFormula = "{Transport.TransportDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")and {Transport.TranStatus} <> 2";


                        }
                        else

                        {
                            btnSearchInfo.Enabled = true;
                            SelectionFormula = "{Transport.TransportDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {Transport.DealerId} = '" + lblCode.Text + "' and {Transport.TranStatus} <> 2";

                        }

                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        //cryRpt.RecordSelectionFormula = SelectionFormula;

                        //nreport = ConnectionInfo(//cryRpt);
                        //Session["nreport"] = //cryRpt;
                        ////cryRpt.Close();
                        break;



                    case "MoveProductWise": //MoveProductWise

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptProductWiseStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);
                        SelectionFormula = "{Transport.TransportDate} in Date(" +
                                              fromValue + ")   to Date (" +
                                              ToValue + ")and {Transport.TranStatus} <> 2";

                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        ////cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";


                        //cryRpt.RecordSelectionFormula = SelectionFormula;


                        //nreport = ConnectionInfo(//cryRpt);
                        //Session["nreport"] = //cryRpt;
                        ////cryRpt.Close();
                        break;



                    case "MoveDealerwiseSummery": //MoveDealerwiseSummery
                        
                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailSummeryDealer.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);
                        if (String.IsNullOrEmpty(lblCode.Text))
                        {
                            btnSearchInfo.Enabled = false;
                            SelectionFormula = "{Transport.TransportDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")and {Transport.TranStatus} <> 2";


                        }
                        else
                        {
                            btnSearchInfo.Enabled = true;
                            SelectionFormula = "{Transport.TransportDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {Transport.DealerId} = '" + lblCode.Text + "' and {Transport.TranStatus} <> 2";

                        }


                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = SelectionFormula;

                        //nreport = ConnectionInfo(//cryRpt);
                        //Session["nreport"] = //cryRpt;

                        ////cryRpt.Close();
                        break;
                    //---------------
                    

                    default:
                        throw new ArgumentException
                        (
                        "GetDataReader was given an incorrect Request for data"
                        );
                }

                // nreport = ConnectionInfo(//cryRpt);
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

        #endregion



        protected void rblReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCode.Text = String.Empty;
            lblName.Text = String.Empty;
            if (rblReports.SelectedValue == "MovementStatementRpt")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "MoveStatementDetail")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "MoveStatementVehicle")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "MoveDetailDealer")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "MoveDetailVehiclewise")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "MoveProductWise")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "MoveTruckwiseSummery")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "MoveDealerwiseSummery")
            {
                POtherOptions.Visible = true;
            }
            

        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvlistofBasicData.SelectedRow;
            lblCode.Text = row.Cells[1].Text;
            lblName.Text = row.Cells[2].Text;
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string reportname = rblReports.SelectedValue;
            DataTable dt = new DataTable();
            if (reportname == "MoveDetailDealer" || reportname == "MoveDealerwiseSummery")
            {
                dt = loadAllDealerInfo(txtSearch.Text);

            }
            else if (reportname == "MoveStatementVehicle" || reportname == "MoveDetailVehiclewise" || reportname == "MoveTruckwiseSummery" )
            {
                dt = GetVehicle(txtSearch.Text);
            }

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string reportname = rblReports.SelectedValue;
            DataTable dt = new DataTable();
            if (reportname == "MoveDetailDealer" || reportname == "MoveDealerwiseSummery")
            {
                dt = loadAllDealerInfo(txtSearch.Text);

            }
            else if (reportname == "MoveStatementVehicle" || reportname == "MoveDetailVehiclewise" || reportname == "MoveTruckwiseSummery")
            {
                dt = GetVehicle(txtSearch.Text);
            }


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            string option = rblReports.SelectedValue;
            ReportOption(option);
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
        protected void btnSearchInfo_Click(object sender, ImageClickEventArgs e)
        {
            string reportname = rblReports.SelectedValue;
            DataTable dt = new DataTable();
            if (reportname == "MoveDetailDealer" || reportname == "MoveDealerwiseSummery")
            {
                dt = loadAllDealerInfo(txtSearch.Text);

            }
            else if (reportname == "MoveStatementVehicle" || reportname == "MoveDetailVehiclewise" || reportname == "MoveTruckwiseSummery")
            {
                dt = GetVehicle(txtSearch.Text);
            }
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();

        }
    }
}