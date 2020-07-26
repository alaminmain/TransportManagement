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
using System.IO;

namespace TransportManagerUI.UI
{
    public partial class TripReports : System.Web.UI.Page
    {
        
        public ReportDocument ConnectionInfo(ReportDocument rpt)
        {
            ReportDocument crSubreportDocument;
            string[] strConnection = ConfigurationManager.ConnectionStrings[("TransportDBConnectionString")].ConnectionString.Split(new char[] { ';' });

            Database oCRDb = rpt.Database;

            Tables oCRTables = oCRDb.Tables;

            CrystalDecisions.CrystalReports.Engine.Table oCRTable = default(CrystalDecisions.CrystalReports.Engine.Table);

            TableLogOnInfo oCRTableLogonInfo = default(CrystalDecisions.Shared.TableLogOnInfo);

            ConnectionInfo oCRConnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();

            oCRConnectionInfo.ServerName = strConnection[0].Split(new char[] { '=' }).GetValue(1).ToString();
            oCRConnectionInfo.Password = strConnection[2].Split(new char[] { '=' }).GetValue(1).ToString();
            oCRConnectionInfo.UserID = strConnection[1].Split(new char[] { '=' }).GetValue(1).ToString();

            for (int i = 0; i < oCRTables.Count; i++)
            {
                oCRTable = oCRTables[i];
                oCRTableLogonInfo = oCRTable.LogOnInfo;
                oCRTableLogonInfo.ConnectionInfo = oCRConnectionInfo;
                oCRTable.ApplyLogOnInfo(oCRTableLogonInfo);
                if (oCRTable.TestConnectivity())
                    //' If there is a "." in the location then remove the
                    // ' beginning of the fully qualified location.
                    //' Example "dbo.northwind.customers" would become
                    //' "customers".
                    oCRTable.Location = oCRTable.Location.Substring(oCRTable.Location.LastIndexOf(".") + 1);


            }

            for (int i = 0; i < rpt.Subreports.Count; i++)
            {

                {
                    //  crSubreportObject = (SubreportObject);
                    crSubreportDocument = rpt.OpenSubreport(rpt.Subreports[i].Name);
                    oCRDb = crSubreportDocument.Database;
                    oCRTables = oCRDb.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in oCRTables)
                    {
                        oCRTableLogonInfo = aTable.LogOnInfo;
                        oCRTableLogonInfo.ConnectionInfo = oCRConnectionInfo;
                        aTable.ApplyLogOnInfo(oCRTableLogonInfo);
                        if (aTable.TestConnectivity())
                            //' If there is a "." in the location then remove the
                            // ' beginning of the fully qualified location.
                            //' Example "dbo.northwind.customers" would become
                            //' "customers".
                            aTable.Location = aTable.Location.Substring(aTable.Location.LastIndexOf(".") + 1);

                    }
                }
            }
            //  }

            rpt.Refresh();
            return rpt;
        }

        public void loginCrystalreport(ReportDocument Report)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;

            SqlConnectionStringBuilder SConn =
              new SqlConnectionStringBuilder(connectionString);

            Report.DataSourceConnections[0].SetConnection(
              SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }
            isAuthorizeToPage();
            if ((!IsPostBack)&&(!IsCallback))
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
                DataTable dt2=null;
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
                DataTable dt2=null;
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
            ReportDocument cryRpt;
            ReportDocument nreport;
            string strReportName;
            string strPath;
            string fromValue = Convert.ToDateTime(txtFromDate.Text).Date.ToString("yyyy,MM,dd,00,00,00");
            string ToValue = Convert.ToDateTime(txtToDate.Text).Date.ToString("yyyy,MM,dd,00,00,00"); ;
            //cryRpt.Close();
            string SelectionFormula;
            try
            {
                switch (Option)
                {
                    case "TripDetailRpt":

                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailState.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);



                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        cryRpt.RecordSelectionFormula = "{Transport.TransportDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ")";

                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        ////cryRpt.Close();
                        break;

                    case "TripDetailDealerRpt":


                        if (String.IsNullOrEmpty(lblName.Text))
                        {

                            SelectionFormula = "{Transport.TransportDate} in DateTime(" +
                                               fromValue + ")   to DateTime (" +
                                               ToValue + ")";
                        }
                        else

                        {

                            SelectionFormula = "{Transport.TransportDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ") and" +
                                                                       " {Customer.CustName} = '" + lblName.Text + "'";

                        }
                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailStateDealer.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);
                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        cryRpt.RecordSelectionFormula = SelectionFormula;


                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        //cryRpt.Close();
                        break;

                    case "TripDetailTruckWiseRpt":


                        if (String.IsNullOrEmpty(lblName.Text))
                        {
                            btnSearchInfo.Enabled = false;
                            SelectionFormula = "{Transport.TransportDate} in DateTime(" +
                                               fromValue + ")   to DateTime (" +
                                               ToValue + ")and {Transport.TranStatus} <> 2";


                        }
                        else

                        {
                            btnSearchInfo.Enabled = true;
                            SelectionFormula = "{Transport.TransportDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleNo} = '" + lblName.Text + "' and {Transport.TranStatus} <> 2";

                        }

                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailStateTruckwise.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);


                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        cryRpt.RecordSelectionFormula = SelectionFormula;

                        nreport = ConnectionInfo(cryRpt);
                        //cryRpt.Close();
                        break;

                    case "TripProductWiseRpt":

                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptProductWiseStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);


                        SelectionFormula = "{Transport.TransportDate} in DateTime(" +
                                                fromValue + ")   to DateTime (" +
                                                ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        cryRpt.RecordSelectionFormula = SelectionFormula;

                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        //cryRpt.Close();
                        break;

                    case "TripStateRpt":

                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMovementStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);
                        SelectionFormula = "{Transport.TransportDate} in DateTime(" +
                                                fromValue + ")   to DateTime (" +
                                                ToValue + ")and {Transport.TranStatus} <> 2";

                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";


                        cryRpt.RecordSelectionFormula = SelectionFormula;


                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        //cryRpt.Close();
                        break;

                    case "TruckvsDate":

                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTruckvsDate.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);


                        //SelectionFormula = "{Transport.TransportDate} in DateTime(" +
                        //                           fromValue + ")   to DateTime (" +
                        //                           ToValue + ")and {Transport.TranStatus} <> 2";
                        if (String.IsNullOrEmpty(lblName.Text))
                        {
                            btnSearchInfo.Enabled = false;
                            SelectionFormula = "{Transport.TransportDate} in DateTime(" +
                                               fromValue + ")   to DateTime (" +
                                               ToValue + ")and {Transport.TranStatus} <> 2";


                        }
                        else
                        {
                            btnSearchInfo.Enabled = true;
                            SelectionFormula = "{Transport.TransportDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleNo} = '" + lblName.Text + "' and {Transport.TranStatus} <> 2";

                        }

                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";
                        cryRpt.RecordSelectionFormula = SelectionFormula;
                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";


                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        //cryRpt.Close();
                        break;

                    case "CancelTripStatement":



                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailState.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);



                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        cryRpt.RecordSelectionFormula = "{Transport.TransportDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ")and {Transport.TranStatus}=2";

                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        //cryRpt.Close();
                        break;

                    case "TruckwiseSummery":


                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rtpTruckwiseDelivery.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);



                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        cryRpt.RecordSelectionFormula = "{Transport.TransportDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ")and {Transport.TranStatus}<>2";

                        nreport = ConnectionInfo(cryRpt);

                        //cryRpt.Close();
                        break;
                    case "DealerwiseSummery":


                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptTripMoveDetailSummeryDealer.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);



                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        cryRpt.RecordSelectionFormula = "{Transport.TransportDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ")and {Transport.TranStatus}<>2";

                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;

                        //cryRpt.Close();
                        break;
                    //---------------
                    case "FuelTripStatement":


                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptFuelTripStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);


                        SelectionFormula = "{TripInfo.Date} in DateTime(" +
                                                fromValue + ")   to DateTime (" +
                                                ToValue + ") AND {TripInfo.Status}<>2";

                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        cryRpt.RecordSelectionFormula = SelectionFormula;


                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        //cryRpt.Close();
                        break;

                    case "FuelTruckwiseStatement":


                        if (String.IsNullOrEmpty(lblName.Text))
                        {

                            SelectionFormula = "{TripInfo.Date} in DateTime(" +
                                               fromValue + ")   to DateTime (" +
                                               ToValue + ") AND {TripInfo.TripStatus}<>2";


                        }
                        else

                        {

                            SelectionFormula = "{TripInfo.Date} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleNo} = '" + lblName.Text + "' AND {TripInfo.TripStatus}<>2";

                        }

                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptFuelTruckwiseStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);

                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        cryRpt.RecordSelectionFormula = SelectionFormula;


                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        //cryRpt.Close();
                        break;

                    case "FuelSummeryStatement":


                        if (String.IsNullOrEmpty(lblName.Text))
                        {

                            SelectionFormula = "{TripInfo.TripDate} in DateTime(" +
                                               fromValue + ")   to DateTime (" +
                                               ToValue + ") AND {TripInfo.Status}<>2";


                        }
                        else
                        {

                            SelectionFormula = "{TripInfo.TripDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleNo} = '" + lblName.Text + "' AND {TripInfo.TripStatus}<>2";

                        }

                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptFuelSummery.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);

                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        cryRpt.RecordSelectionFormula = SelectionFormula;


                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;
                        //cryRpt.Close();
                        break;
                  //-------Vehicle Status Related Report

                    case "VehicleMovement":
                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//VehicleMovement.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);



                        cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        if (String.IsNullOrEmpty(lblCode.Text))
                        {

                            SelectionFormula = "{VehicleMovement.MoveDate} in DateTime(" +
                                               fromValue + ")   to DateTime (" +
                                               ToValue + ") ";


                        }
                        else
                        {

                            SelectionFormula = "{VehicleMovement.MoveDate} in DateTime(" + fromValue + ")   to DateTime (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleID} = '" + lblCode.Text + "'";

                        }

                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;

                        //cryRpt.Close();
                        break;

                    case "VehicleStatus":
                        cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//VehicleStatus.rpt";
                        strPath = Server.MapPath(strReportName);
                        cryRpt.Load(strPath);



                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "DateTime(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "DateTime (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = "{Transport.TransportDate} in DateTime(" + fromValue + ")   to DateTime (" +
                        //                                    ToValue + ")and {Transport.TranStatus}<>2";

                        nreport = ConnectionInfo(cryRpt);
                        Session["nreport"] = nreport;

                        //cryRpt.Close();
                        break;


                    default:
                        throw new ArgumentException
                        (
                        "GetDataReader was given an incorrect Request for data"
                        );
                }
               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }
       
        #endregion

        

        protected void rblReports_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            if(rblReports.SelectedValue== "TripStateRpt")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "TripDetailRpt")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "TripDetailDealerRpt")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "TripDetailTruckWiseRpt")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "TripProductWiseRpt")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "TruckvsDate")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "CancelTripStatement")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "TruckwiseSummery")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "DealerwiseSummery")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "FuelTripStatement")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "FuelTruckwiseStatement")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "FuelSummeryStatement")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "VehicleMovement")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "VehicleStatus")
            {
                POtherOptions.Visible = false;
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
            if (reportname == "TripDetailDealerRpt" || reportname == "DealerwiseSummery")
            {
                dt = loadAllDealerInfo(txtSearch.Text);

            }
            else if (reportname == "TripDetailTruckWiseRpt" || reportname == "TruckvsDate" || reportname == "FuelTruckwiseStatement" || reportname == "VehicleMovement")
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
            if (reportname == "TripDetailDealerRpt" || reportname == "DealerwiseSummery")
            {
                dt = loadAllDealerInfo(txtSearch.Text);

            }
            else if (reportname == "TripDetailTruckWiseRpt" || reportname == "TruckvsDate" || reportname == "FuelTruckwiseStatement" || reportname == "VehicleMovement")
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

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "~/UI/ReportForStatement.aspx" + "','_blank')", true);
            Response.Redirect("~/UI/ReportForStatement.aspx");
            
        }

        protected void btnSearchInfo_Click(object sender, ImageClickEventArgs e)
        {
            string reportname = rblReports.SelectedValue;
            DataTable dt = new DataTable();
            if (reportname== "TripDetailDealerRpt" || reportname == "DealerwiseSummery")
            {
                dt = loadAllDealerInfo(txtSearch.Text);
                
            }
            else if (reportname == "TripDetailTruckWiseRpt" || reportname == "TruckvsDate" || reportname == "FuelTruckwiseStatement" || reportname == "VehicleMovement")
            {
                dt = GetVehicle(txtSearch.Text);
            }
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();

        }
    }
}