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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace TransportManagerUI.UI
{
    public partial class ReportViewer : System.Web.UI.Page
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
            string connectionString =ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;

            SqlConnectionStringBuilder SConn =
              new SqlConnectionStringBuilder(connectionString);

            Report.DataSourceConnections[0].SetConnection(
              SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);
           
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
                isAuthorizeToPage();
            }
                
            else
            {
                //MemoryStream oStream = new MemoryStream();
                ReportDocument nreport = new ReportDocument();
                //CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                //CrystalReportViewer1.DisplayToolbar = false;
                //CrystalReportViewer1.SeparatePages = false;
                //CrystalReportViewer1.Visible = false;
                ReportDocument Report;
                
                string str;

                string reportCase = String.Empty;

                if (String.IsNullOrEmpty(Session["reportOn"].ToString()))
                {

                }
                else
                {
                    reportCase = Session["reportOn"].ToString();
                    switch (reportCase)
                    {
                        case "MovementChalan":
                            string movementNo = Session["Movement"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//TripChallan.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("TransportNo", movementNo);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        case "DO":
                            string dono = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//rptDO.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("dono", dono);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        case "TC":
                            string tcno = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//rptTC.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("tcno", tcno);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        case "TCCNF":
                            string tcno1 = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//rptTCCNF.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("tcno", tcno1);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        
                        case "FuelSlip":
                            string fuelSlipno = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//rptOilRequsition.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("FuelSlipNo", fuelSlipno);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        case "Voucher":
                            string voucherno = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//rptBills.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("VoucherNo", voucherno);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        case "TripAdvice":
                            string tripNo = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//rptTripAdvice.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("TripNo", tripNo);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        case "VehicleWorkSlip":
                            string vehicleNo = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//VehicleWorkSlip.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("moveRegNo", vehicleNo);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        case "Customer":
                            //string vehicleNo = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//CustomerList.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            //nreport.SetParameterValue("moveRegNo", vehicleNo);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        case "TripAdvanceAdvice":
                            string tripadvNo = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//TripAdvance.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("TripAdvNo", tripadvNo);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;
                        case "TripOther":
                            string TripOther = Session["paramData"].ToString();
                            Report = new ReportDocument();
                            str = Server.MapPath("~//report//TripChallanOther.rpt");
                            Report.Load(str);
                            nreport = ConnectionInfo(Report);

                            nreport.SetParameterValue("TripNo", TripOther);
                            //CrystalReportViewer1.ReportSource = nreport;

                            //oStream = (MemoryStream)
                            //nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(oStream.ToArray());
                            //Response.End();
                            break;

                        default:
                            throw new ArgumentException
                            (
                              "incorrect Request for data"
                            );
                    }

                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream = nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(byteArray);
                    Response.Flush();
                    Response.Close();

                    nreport.Close();
                    nreport.Dispose();

                    Report.Close();
                    Report.Dispose();
                    GC.Collect();

                }

            }
        }
    }
}