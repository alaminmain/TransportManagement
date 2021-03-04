using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI
{
    public partial class VoucherReports : System.Web.UI.Page
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
        private DataTable GetDriver(string searchKey)
        {
            try
            {
                DataTable dt;
                DataTable dt2 = null;
                DataView view = new DataView();

                using (PersonalGateway gatwayObj = new PersonalGateway())
                {


                    dt = gatwayObj.GetAllDriver();
                    view = new DataView(dt);
                    dt2 = view.ToTable(false, "EmpCode", "EmpName");
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt2.AsEnumerable()
    .Where(r => r.Field<String>("EmpCode").Contains(searchKey) || r.Field<String>("EmpName").ToUpper().Contains(searchKey.ToUpper()));

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
        private DataTable LoadChartofAccounts(string searchKey)
        {
            try
            {
                DataTable dt;
                DataTable dt2 = null;
                DataView view = new DataView();

                using (ChartOfAccountGateway gatwayObj = new ChartOfAccountGateway())
                {

                    dt = gatwayObj.Get_All_ChartOfACC();
                    view = new DataView(dt);
                    dt2 = view.ToTable(false, "AccountCode", "AccountDesc");
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt2.AsEnumerable()
    .Where(r => r.Field<String>("AccountCode").ToUpper().Contains(searchKey.ToUpper())
           || r.Field<String>("AccountDesc").ToUpper().Contains(searchKey.ToUpper()));

                        dt2 = filtered.CopyToDataTable();
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
        private string Formula()
        {
            string fromValue = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy,MM,dd");
            string ToValue = Convert.ToDateTime(txtToDate.Text).ToString("yyyy,MM,dd");
            //            //{Voucher.VoucherDate} in DateTime (2012, 10, 01, 00, 00, 00) to Date (2012, 10, 03, 00, 00, 00) and
            //{VehicleInfo.VehicleNo} = "DM OU - 11-7148" and
            //{ChartofAccount.AccountCode} = "1001"
            string selectionFormula = "";
            if (String.IsNullOrEmpty(lblCode.Text)==true && String.IsNullOrEmpty(lblCode2.Text)==true)
            {
                selectionFormula = "{Voucher.VoucherDate} in Date(" +
                                            fromValue +
                                            ")   to Date (" +
                                            ToValue + ")";
            }
            else if (String.IsNullOrEmpty(lblCode.Text))
            {
                selectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue +
                      ")   to Date (" + ToValue + ") And {ChartofAccount.AccountCode}='" +
                  lblCode2.Text + "'";
            }
            else if (String.IsNullOrEmpty(lblCode2.Text))
            {
                selectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue +
                    ")   to Date (" + ToValue + ") And {VehicleInfo.VehicleNo}='" +
                lblName.Text + "'";
            }
            else
            {
                selectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue +
                     ")   to Date (" + ToValue + ") And {VehicleInfo.VehicleNo}='" +
                 lblName.Text + "' and{ChartofAccount.AccountCode}='" +
                 lblCode2.Text + "'";
            }
            return selectionFormula;
        }
        public void ReportOption(string Option)
        {
            //ReportDocument //cryRpt;
            
            string strReportName;
            string strPath;
            string fromValue = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy,MM,dd");
            string ToValue = Convert.ToDateTime(txtToDate.Text).ToString("yyyy,MM,dd");
            ////cryRpt.Close();
            string SelectionFormula;
            try
            {
                switch (Option)
                {
                    case "VoucherStatement": //Voucher Statement

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptVoucherStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);
                        SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                          fromValue + ")   to Date (" +
                                           ToValue + ")";
                   


                         //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = SelectionFormula;

                       
                        //////cryRpt.Close();
                        break;

                    case "VoucherStateDealerwise": //VoucherStateDealerwise


                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptVoucherStateDealerwise.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);

                        if (String.IsNullOrEmpty(lblCode.Text))
                        {

                            SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";


                        }
                        else
                        {

                            SelectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {TripInfo.DealerId} = '" + lblCode.Text + "'";

                        }

                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        //cryRpt.RecordSelectionFormula = SelectionFormula;



                        ////cryRpt.Close();
                        break;
                   
                    case "VoucherStateTruckwise": //Voucher Statement (Vehiclewise)

                        if (String.IsNullOrEmpty(lblCode.Text))
                        {

                            SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";
                        }
                        else
                        {
                            SelectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleNo} = '" + lblName.Text + "'";
                        }
                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptVoucherStatementtruckwise.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);


                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        //cryRpt.RecordSelectionFormula = SelectionFormula;



                        ////cryRpt.Close();
                        break;
                    case "VoucherStatementDriverwise": //VoucherStatementDriverwise

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptVoucherStatementDriverwise.rpt";
                        strPath = Server.MapPath(strReportName);

                        //cryRpt.Load(strPath);

                        if (String.IsNullOrEmpty(lblCode.Text))
                        {
                            btnSearch.Enabled = false;
                            SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";


                        }
                        else
                        {
                            btnSearch.Enabled = true;
                            SelectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {TripInfo.EmpCode} = '" + lblCode.Text + "'";

                        }


                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = SelectionFormula;


                        ////cryRpt.Close();
                        break;


                    case "VoucherStatementDetail": //Voucher Statement(Dealerwise)


                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptVoucherStatementDetail.rpt";
                    strPath = Server.MapPath(strReportName);
                    //cryRpt.Load(strPath);
                    SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                           fromValue + ")   to Date (" +
                                           ToValue + ")";

                     //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                     //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                    //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                    //cryRpt.RecordSelectionFormula = SelectionFormula;

                        break;

                    

                   

                    case "VoucherStateTruckwiseDetail": //Voucher Statement Detail(Vehiclewise)

                        if (String.IsNullOrEmpty(lblCode.Text))
                    {
                        btnSearch.Enabled = false;
                        SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                           fromValue + ")   to Date (" +
                                           ToValue + ")";


                    }
                    else
                    {
                        btnSearch.Enabled = true;
                        SelectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue + ")   to Date (" +
                                                        ToValue + ") and" +
                                                                   " {VehicleInfo.VehicleNo} = '" + lblName.Text + "'";

                    }
                    //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    strReportName = "~//report//rptVoucherStatementDetailTW.rpt";
                    strPath = Server.MapPath(strReportName);
                    //cryRpt.Load(strPath);

                         //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        //cryRpt.RecordSelectionFormula = SelectionFormula;

                      
                        ////cryRpt.Close();
                        break;

                    case "VoucherStateTruckwiseMonthly"://VoucherStateTruckwiseMonthly

                        if (String.IsNullOrEmpty(lblCode.Text))
                    {
                        
                        SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                           fromValue + ")   to Date (" +
                                           ToValue + ")";


                    }
                    else
                    {
                        
                        SelectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue + ")   to Date (" +
                                                        ToValue + ") and" +
                                                                   " {VehicleInfo.VehicleNo} = '" + lblName.Text + "'";

                    }
                    //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    strReportName = "~//report//rptVoucherStatementDetailwithTruck.rpt";
                    strPath = Server.MapPath(strReportName);
                    //cryRpt.Load(strPath);

                         //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        ////cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";


                        //cryRpt.RecordSelectionFormula = SelectionFormula;


                       
                        break;
                    case "ExpenseStateTruckwise":


                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                        strReportName = "~//report//rptExpenseStatementTruck.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);


                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        SelectionFormula = Formula();
                        //cryRpt.RecordSelectionFormula = SelectionFormula;



                        ////cryRpt.Close();
                        break;

                    case "VoucherStatementSummeryTruck": //VoucherStatementSummeryTruck

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//rptVoucherSummeryTruckwise.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);
                        if (String.IsNullOrEmpty(lblCode.Text))
                        {

                            SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";

                        }
                        else
                        {
                            SelectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleNo} = '" + lblName.Text + "'";
                        }

                         //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";
                        //cryRpt.RecordSelectionFormula = SelectionFormula;
                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";


                     
                        ////cryRpt.Close();
                        break;



                    case "ExpenseStateTruckwiseDetail": //ExpenseStateTruckwiseDetail


                            //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                         strReportName = "~//report//rptExpenseStatementTruckex.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);


                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date("+ fromValue+")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date(" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";
                        SelectionFormula = Formula();
                        //cryRpt.RecordSelectionFormula = SelectionFormula;

                       
                        ////cryRpt.Close();
                        break;
                    case "ExpenseStatementSummery": //ExpenseStatementSummery


                        if (String.IsNullOrEmpty(lblCode.Text))
                    {
                        btnSearch.Enabled = false;
                        SelectionFormula = "{Voucher.VoucherDate} in Date(" +
                                           fromValue + ")   to Date (" +
                                           ToValue + ")";


                    }
                    else
                    {
                        btnSearch.Enabled = true;
                        SelectionFormula = "{Voucher.VoucherDate} in Date(" + fromValue + ")   to Date (" +
                                                        ToValue + ") and" +
                                                                   " {VehicleInfo.VehicleNo} = '" + lblName.Text + "'";

                    }
                    //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    strReportName = "~//report//rptVoucherExpSummeryTruck.rpt";
                    strPath = Server.MapPath(strReportName);
                    //cryRpt.Load(strPath);


                         //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = SelectionFormula;

                       

                        ////cryRpt.Close();
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
                POtherOptions.Visible = false;
                pExpenses.Visible = false;
            }
        }

        protected void rblReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCode.Text = String.Empty;
            lblCode2.Text = String.Empty;
            lblName.Text = String.Empty;
            lblName2.Text = String.Empty;
            if (rblReports.SelectedValue == "VoucherStatement")
            {
                POtherOptions.Visible = false;
                pExpenses.Visible = false;
            }
            else if (rblReports.SelectedValue == "VoucherStatementDetail")
            {
                POtherOptions.Visible = false;
                POtherOptions.GroupingText = "Search Vehicle";
                pExpenses.Visible = false;
                pExpenses.GroupingText = "";
            }
            else if (rblReports.SelectedValue == "VoucherStateTruckwise")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Vehicle";
                pExpenses.Visible = false;
                pExpenses.GroupingText = "";
            }
            else if (rblReports.SelectedValue == "VoucherStateTruckwiseDetail")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Vehicle";
                pExpenses.Visible = false;
                pExpenses.GroupingText = "";
            }
            else if (rblReports.SelectedValue == "VoucherStateTruckwiseMonthly")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Vehicle";
                pExpenses.Visible = false;
                pExpenses.GroupingText = "";
            }
            else if (rblReports.SelectedValue == "VoucherStatementSummeryTruck")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Vehicle";
                pExpenses.Visible = false;
                pExpenses.GroupingText = "";
            }
            else if (rblReports.SelectedValue == "VoucherStateDealerwise")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Dealer";
                pExpenses.Visible = false;
                pExpenses.GroupingText = "";
            }
            else if (rblReports.SelectedValue == "VoucherStatementDriverwise")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Driver";
                pExpenses.Visible = false;
                pExpenses.GroupingText = "";
            }
            else if (rblReports.SelectedValue == "ExpenseStatementSummery")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Vehicle";
                pExpenses.Visible = true;
                pExpenses.GroupingText = "Select Expense";
            }
            else if (rblReports.SelectedValue == "ExpenseStateTruckwise")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Vehicle";
                pExpenses.Visible = true;
                pExpenses.GroupingText = "Select Expense";
            }
            else if (rblReports.SelectedValue == "ExpenseStateTruckwiseDetail")
            {
                POtherOptions.Visible = true;
                POtherOptions.GroupingText = "Search Vehicle";
                pExpenses.Visible = true;
                pExpenses.GroupingText = "Select Expense";
            }
           
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            string option = rblReports.SelectedValue;
            ReportOption(option);
            Session["AllStatement"] = "1";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/ReportForStatement.aspx" + "','_blank')", true);
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
            if (rblReports.SelectedValue == "VoucherStateDealerwise")
            {
                dt = loadAllDealerInfo(txtSearch.Text);
            }
            else if (rblReports.SelectedValue == "VoucherStatementDriverwise")
            {
                dt = GetDriver(txtSearch.Text);
            }
            else
                dt = GetVehicle(txtSearch.Text);
            
            hfShowList.Value = "1";
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

            }
            else if (hfShowList.Value == "2")
            {
                lblCode2.Text = row.Cells[1].Text;
                lblName2.Text = row.Cells[2].Text;
            }
            gvlistofBasicData.SelectedIndex = -1; 
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string reportname = rblReports.SelectedValue;
            DataTable dt = new DataTable();
            if (hfShowList.Value=="1")
            {
                if (rblReports.SelectedValue == "VoucherStateDealerwise")
                {
                    dt = loadAllDealerInfo(txtSearch.Text);
                }
                else if (rblReports.SelectedValue == "VoucherStatementDriverwise")
                {
                    dt = GetDriver(txtSearch.Text);
                }
                else
                    dt = GetVehicle(txtSearch.Text);

            }
            else if (hfShowList.Value == "2")
            {
                dt = LoadChartofAccounts(txtSearch.Text);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string reportname = rblReports.SelectedValue;
            DataTable dt = new DataTable();
            if (hfShowList.Value == "1")
            {
                if (rblReports.SelectedValue == "VoucherStateDealerwise")
                {
                    dt = loadAllDealerInfo(txtSearch.Text);
                }
                else if (rblReports.SelectedValue == "VoucherStatementDriverwise")
                {
                    dt = GetDriver(txtSearch.Text);
                }
                else
                    dt = GetVehicle(txtSearch.Text);

            }
            else if (hfShowList.Value == "2")
            {
                dt = LoadChartofAccounts(txtSearch.Text);
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

        protected void btnSearch2_Click(object sender, ImageClickEventArgs e)
        {
            string reportname = rblReports.SelectedValue;
            DataTable dt = new DataTable();
            dt = LoadChartofAccounts(txtSearch.Text);

            hfShowList.Value = "2";
            gvlistofBasicData.SelectedIndex = -1;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            
            hfShowList_ModalPopupExtender.Show();
        }
    }
}