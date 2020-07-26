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
    public partial class ReportForStatement : System.Web.UI.Page
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {

            try {

                if (Session["UserName"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    MemoryStream oStream = new MemoryStream();

                    CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                    CrystalReportViewer1.DisplayToolbar = false;
                    CrystalReportViewer1.SeparatePages = false;
                    CrystalReportViewer1.Visible = false;

                    ReportDocument nreport;


                    string reportCase = String.Empty;

                    if (String.IsNullOrEmpty(Session["nreport"].ToString()))
                    {

                    }
                    else
                    {
                        nreport = (ReportDocument)Session["nreport"];



                        CrystalReportViewer1.ReportSource = nreport;

                        oStream = (MemoryStream)
                        nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/pdf";
                        Response.BinaryWrite(oStream.ToArray());
                        Response.End();
                        nreport.Close();
                        nreport.Dispose();
                        Session.Clear();
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

    }
}
