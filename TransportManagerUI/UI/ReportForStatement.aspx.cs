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
        ReportDocument nreport = new ReportDocument();
        private void LoadStreamCR()
        {
            System.IO.Stream stream = nreport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);


            nreport.Close();
            nreport.Dispose();
            //Send pdf to client

            byte[] PDFByteArray = new Byte[stream.Length];

            stream.Position = 0;

            stream.Read(PDFByteArray, 0, Convert.ToInt32(stream.Length));

            Context.Response.ClearContent();

            Context.Response.ClearHeaders();

            Context.Response.AddHeader("content-disposition", "filename=Report.pdf");

            Context.Response.ContentType = "application/pdf";

            Context.Response.AddHeader("content-length", PDFByteArray.Length.ToString());

            Context.Response.BinaryWrite(PDFByteArray);

            Context.Response.Flush();
            Context.Response.Close();
            Context.Response.End();

            stream.Flush();
            stream.Close();
            stream.Dispose();

            GC.Collect();
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    CommonGateway cm = new CommonGateway();


                    if (Session["strPath"] != null)
                    {
                        nreport.Load(Session["strPath"].ToString());

                        if (Session["SelectionFormula"] != null)
                            nreport.RecordSelectionFormula = Session["SelectionFormula"].ToString();

                        if (Session["fromDate"] != null && Session["ToDate"] != null)
                        {
                            nreport.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" +
                                                                                    Session["fromDate"].ToString() + ")";
                            nreport.DataDefinition.FormulaFields["DateTo"].Text = "Date (" +
                                                                                  Session["ToDate"].ToString() + ")";
                        }

                        nreport = cm.ConnectionInfo(nreport);

                        if (Session["AllStatement"] != null)
                        {
                            if (Session["AllStatement"].ToString() == "2")
                            {
                                CrystalReportViewer1.Visible = true;

                                //nreport.Refresh();

                                CrystalReportViewer1.ReportSource = nreport;

                                CrystalReportViewer1.DataBind();
                                //CrystalReportViewer1.RefreshReport();
                                CrystalReportViewer1.Zoom(90);
                                Session["ReportDocument"] = nreport;

                            }
                            else
                            {
                                LoadStreamCR();
                            }


                        }
                        //// do all your reporting stuff here, then add it to session like so
                        //ReportDocument crystalReportDocument = new ReportDocumment();
                        //crystalReportDocument.SetDataSource(DataTableHere);
                        ////_reportViewer is the crystalviewer which you have on ur aspx form
                        //_reportViewer.ReportSource = crystalReportDocument;
                        //Session["ReportDocument"] = crystalReportDocument;
                    }
                    else
                    {
                        ReportDocument doc = (ReportDocument) Session["ReportDocument"];
                        CrystalReportViewer1.ReportSource = doc;
                    }
                }
            }
        }
            protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument doc = (ReportDocument)Session["ReportDocument"];
            CrystalReportViewer1.ReportSource = doc;
            
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            //Session["strPath"] = null;
            //Session["SelectionFormula"] = null;
            //Session["fromDate"] = null;
            //Session["ToDate"] = null;
            //Session["AllStatement"] = null;
            //nreport.Close();
            //nreport.Dispose();
        }

      

        private void loadCRViwer()
        {
           
        }

    }
}
