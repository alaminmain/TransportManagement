using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

using CrystalDecisions.Shared;
using TransportManagerLibrary.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using TransportManagerLibrary.UtilityClass;
using System.IO;

namespace TransportManagerUI.UI
{
    public partial class VehicleStatusReports : System.Web.UI.Page
    {
        
      
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
                POtherOptions.Visible = false;
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

        private DataTable LoadAllDriver(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;
                DataView view = new DataView();

                using (PersonalGateway gatwayObj = new PersonalGateway())
                {
                    dt2 = gatwayObj.GetAllDriver();
                    

                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select();
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();

                    view = new DataView(dt);
                    dt = view.ToTable(false, "AgentCode", "AgentName");
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("EmpCode").Contains(searchKey) || r.Field<String>("EmpName").ToUpper().Contains(searchKey.ToUpper()));
                        dt = filtered.CopyToDataTable();

                    }
                    return dt;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }

        private DataTable loadAllAgent(string searchKey)
        {
            try
            {
                DataTable dt;
                DataView view = new DataView();

                using (VehicleAgentGateway gatwayObj = new VehicleAgentGateway())
                {

                    dt = gatwayObj.GetAllAgent();
                    view = new DataView(dt);
                    dt = view.ToTable(false, "AgentID", "AgentName");
                    if (String.IsNullOrEmpty(searchKey))
                    {


                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("AgentID").ToUpper().Contains(searchKey.ToUpper())
           || r.Field<String>("AgentName").ToUpper().Contains(searchKey.ToUpper()));
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

        private DataTable loadGhatList(string searchKey)
        {
            try
            {
                DataTable dt;
                DataView view = new DataView();

                using (StoreLocation gatwayObj = new StoreLocation())
                {

                    dt = gatwayObj.GetAllStoreLocation(1);
                    view = new DataView(dt);
                    dt = view.ToTable(false, "StoreCode", "StoreName");
                    if (String.IsNullOrEmpty(searchKey))
                    {


                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("StoreCode").ToUpper().Contains(searchKey.ToUpper())
           || r.Field<String>("StoreName").ToUpper().Contains(searchKey.ToUpper()));
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
            //ReportDocument //cryRpt;
            //
            string strReportName;
            string strPath;
            string fromValue = Convert.ToDateTime(txtFromDate.Text).Date.ToString("yyyy,MM,dd");
            string ToValue = Convert.ToDateTime(txtToDate.Text).Date.ToString("yyyy,MM,dd"); ;
            ////cryRpt.Close();
            string SelectionFormula=String.Empty;
            try
            {
                switch (Option)
                {

                    case "TripStatementRpt": //Trip Statement

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TripStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);
                        SelectionFormula = "{TripInfo.TripDate} in Date(" +
                                                fromValue + ")   to Date(" +
                                                ToValue + ") And {TripInfo.TripStatus}<>2 ";

                      
                     
                        break;

                  
                    case "TripAgentwise": //Agentwise


                        if (String.IsNullOrEmpty(lblName.Text))
                        {

                            SelectionFormula = "{TripInfo.TripDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")";
                        }
                        else

                        {

                            SelectionFormula = "{TripInfo.TripDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {TripInfo.AgentID} = '" + lblCode.Text + "'";

                        }
                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TripStateAgent.rpt";
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

                    case "TripGhatwise": //Ghatwise


                        if (String.IsNullOrEmpty(lblName.Text))
                        {
                            btnSearchInfo.Enabled = false;
                            SelectionFormula = "{TripInfo.TripDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")and {TripInfo.TripStatus} <> 2";


                        }
                        else

                        {
                            btnSearchInfo.Enabled = true;
                            SelectionFormula = "{TripInfo.TripDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {TripInfo.StoreCode} = '" + lblName.Text + "' and {TripInfo.TripStatus} <> 2";

                        }

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TripStateGhat.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);


                         //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        //cryRpt.RecordSelectionFormula = SelectionFormula;

                        //nreport = ConnectionInfo(//cryRpt);

                        ////cryRpt.Close();
                        break;


                    case "NotBilledTrip": //PendingTrip (Not Billed)

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TripStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);
                        SelectionFormula = "{TripInfo.TripDate} in Date(" +
                                                fromValue + ")   to Date (" +
                                                ToValue + ") and {TripInfo.TripStatus} = 1";

                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        ////cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";


                        //cryRpt.RecordSelectionFormula = SelectionFormula;


                        //nreport = ConnectionInfo(//cryRpt);
                        //Session["nreport"] = //cryRpt;
                        ////cryRpt.Close();
                        break;



                    case "PendingVehiclewise": //Pending Trip(VehicleWise)

                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//TripStatement.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);


                        //SelectionFormula = "{Transport.TransportDate} in Date(" +
                        //                           fromValue + ")   to Date (" +
                        //                           ToValue + ")and {Transport.TranStatus} <> 2";
                        if (String.IsNullOrEmpty(lblName.Text))
                        {
                            btnSearchInfo.Enabled = false;
                            SelectionFormula = "{TripInfo.TripDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ")and {TripInfo.TripStatus} = 1";


                        }
                        else
                        {
                            btnSearchInfo.Enabled = true;
                            SelectionFormula = "{TripInfo.TripDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {TripInfo.VehicleID} = '" + lblCode.Text + "' and {TripDate.TripStatus} = 1";

                        }

                         //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";
                        //cryRpt.RecordSelectionFormula = SelectionFormula;
                        //cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";


                        //nreport = ConnectionInfo(//cryRpt);
                        //Session["nreport"] = //cryRpt;
                        ////cryRpt.Close();
                        break;
                                                        
                   

                    case "VehicleWorkshopStatement": //Workshop Statement
                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//VehicleMoveState.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);



                        //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                        //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        ////cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                      

                            SelectionFormula = "{VehicleMovement.MoveDate} in Date(" +
                                               fromValue + ")   to Date(" +
                                               ToValue + ") and {VehicleMovement.VehicleStatus}=2";
                       
                        break;

                    case "VehicleMovement": //Vehicle Movement Register
                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//VehicleMovement.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);



                         //cryRpt.DataDefinition.FormulaFields["DateFrom"].Text = "Date(" + fromValue + ")";
                         //cryRpt.DataDefinition.FormulaFields["DateTo"].Text = "Date (" + ToValue + ")";

                        ////cryRpt.DataDefinition.FormulaFields["FTrace"].Text = "0";

                        if (String.IsNullOrEmpty(lblCode.Text))
                        {

                            SelectionFormula = "{VehicleMovement.MoveDate} in Date(" +
                                               fromValue + ")   to Date (" +
                                               ToValue + ") ";


                        }
                        else
                        {

                            SelectionFormula = "{VehicleMovement.MoveDate} in Date(" + fromValue + ")   to Date (" +
                                                            ToValue + ") and" +
                                                                       " {VehicleInfo.VehicleID} = '" + lblCode.Text + "'";

                        }

                        //nreport = ConnectionInfo(//cryRpt);
                        //Session["nreport"] = //cryRpt;

                        ////cryRpt.Close();
                        break;

                    case "VehicleStatus": //Vehicle Current Status
                        //cryRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        strReportName = "~//report//VehicleStatus.rpt";
                        strPath = Server.MapPath(strReportName);
                        //cryRpt.Load(strPath);

                        
                        break;


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
            if(rblReports.SelectedValue== "TripStatementRpt")
            {
                POtherOptions.Visible = false;
            }
           
            else if (rblReports.SelectedValue == "TripAgentwise")
            {
                POtherOptions.Visible = true;
            }
            else if (rblReports.SelectedValue == "TripGhatwise")
            {
                POtherOptions.Visible = true;
            }
         
            else if (rblReports.SelectedValue == "NotBilledTrip")
            {
                POtherOptions.Visible = false;
            }
            else if (rblReports.SelectedValue == "PendingVehiclewise")
            {
                POtherOptions.Visible = true;
            }
           
            else if (rblReports.SelectedValue == "VehicleWorkshopStatement")
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
            if (reportname == "TripGhatwise")
            {
                dt = loadGhatList(txtSearch.Text);

            }
            else if (reportname == "TripAgentwise")
            {
                dt = loadAllAgent(txtSearch.Text);
            }
            else if (reportname == "PendingDriverwise")
            {
                dt = LoadAllDriver(txtSearch.Text);
            }

            else if ( reportname == "PendingVehiclewise" || reportname == "VehicleMovement")
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
            if (reportname == "TripGhatwise")
            {
                dt = loadGhatList(txtSearch.Text);

            }
            else if (reportname == "TripAgentwise")
            {
                dt = loadAllAgent(txtSearch.Text);
            }
            else if (reportname == "PendingDriverwise")
            {
                dt = LoadAllDriver(txtSearch.Text);
            }

            else if (reportname == "PendingVehiclewise" || reportname == "VehicleMovement")
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
            if (reportname == "TripGhatwise")
            {
                dt = loadGhatList(txtSearch.Text);

            }
            else if (reportname == "TripAgentwise")
            {
                dt = loadAllAgent(txtSearch.Text);
            }
            else if (reportname == "PendingDriverwise")
            {
                dt = LoadAllDriver(txtSearch.Text);
            }

            else if (reportname == "PendingVehiclewise" || reportname == "VehicleMovement")
            {
                dt = GetVehicle(txtSearch.Text);
            }
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();

        }
    }
}