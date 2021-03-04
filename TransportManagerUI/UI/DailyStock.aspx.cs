using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using TransportManagerLibrary.UtilityClass;
using MadinaWorkshop.DLL.Gateway;
using System.IO;
namespace TransportManagerUI.UI.Workshop
{
    public partial class DailyStock : System.Web.UI.Page
    {
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
            try
            {
                //if (Session["UserName"] == null)
                //{
                //    Session.Abandon();
                //    Response.Redirect("~/login.aspx");
                //}
                //isAuthorizeToPage();
                //lblMessage.Text = string.Empty;
                if (!IsPostBack && !IsCallback)
                {
                   
                        txtClosingDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                        rbtnForTheDate.Checked = true;
                    
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

        

        static int ProcessType { get; set; }
        protected void rbtnForTheDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnForTheDate.Checked)
            {
                rbtnMonthToDate.Checked = false;
            }
        }

        protected void rbtnMonthToDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMonthToDate.Checked)
            {
                rbtnForTheDate.Checked = false;
            }

        }
        #region method

        private void Process()
        {
            //processDate = Convert.ToDateTime(txtClosingDate.Text);
            //month = processDate.ToString("MM"); 
            //year =processDate.ToString("yyyy");
            try
            {
                if (rbtnForTheDate.Checked)
                {
                    ProcessType = 1;
                    int ComCode = 1;
                    string ClosingDate1 = txtClosingDate.Text;

                    //string StartDate1 = txtClosingDate.Text;

                    int rowEffected = new DailyStockGateway().DailyStock(ComCode, ClosingDate1, ProcessType);
                    if (rowEffected != 0)
                    {
                        lblMsg.Text = "Process Successfull";
                    }
                }
                else
                {
                    ProcessType = 2;
                    int ComCode = 1;
                    string ClosingDate1 = txtClosingDate.Text;

                    //string StartDate1= "01-" + month + "-" + year;

                    int rowEffected = new DailyStockGateway().DailyStock(ComCode, ClosingDate1, ProcessType);
                    if (rowEffected != 0)
                    {
                        lblMsg.Text = "Process Successfull";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string RepotrName = "DailyStock";
            Response.Redirect("reportViewerWorkshop.aspx?ClosingDate=" + txtClosingDate.Text.Trim() + "&RN=" + RepotrName);
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            this.Process();
        }
    }
}