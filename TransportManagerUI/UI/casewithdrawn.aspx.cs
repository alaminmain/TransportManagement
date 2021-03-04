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
    public partial class casewithdrawn : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {

                txtFromDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                txtToDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                txtIssDate0_CalendarExtender.SelectedDate = DateTime.Now.Date;
                LoadData();

            }
        }
        #region method
        private void LoadData()
        {
            string sql1 = @"Select AW.AdministrativNo,convert(varchar (10),AW.IssDate,103)as IssDate,AW.VehicleID,VI.VehicleNo,AW.ModelNo,AW.CateID,ACI.CateName,AW.EmpCode,P.EmpName,cast((VAW.TotAmount)as decimal(18,2))AS TotAmount  
                          from AdministrativWork AW inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join AdminItemCate ACI on AW.CateID=ACI.CateID
                           inner join Personal P on AW.EmpCode=P.EmpCode and AW.ComCode=P.ComCode
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo  
                            where AW.status=1 and AW.CateID='01'and AW.IssDate between CONVERT(smalldatetime,@fromDate,103)and CONVERT(smalldatetime,@toDate,103)order by (AW.AdministrativNo)asc";
            DataTable dt1 = new DataTable();
            SqlParameter[] parameter ={
                                              new SqlParameter("@fromDate",txtFromDate.Text ),
                                              new SqlParameter("@toDate",txtToDate.Text )
                                            };
            dt1 = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql1, parameter).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                gvCaseWidraw.DataSource = null;
                gvCaseWidraw.DataBind();
                gvCaseWidraw.DataSource = dt1;
                gvCaseWidraw.DataBind();
            }
            else
            {
                gvCaseWidraw.DataSource = null;
                gvCaseWidraw.DataBind();
                lblMsg.Text = "Data not found.";
            }
        }

        private void updateStatus()
        {
            try
            {
                int rowupdate = 0;
                for (int i = 0; i < gvCaseWidraw.Rows.Count; i++)
                {
                    CheckBox isChecked = ((CheckBox)gvCaseWidraw.Rows[i].FindControl("ckAdminApproved"));

                    if (isChecked.Checked)
                    {
                        string sql = "UPDATE AdministrativWork SET Status='4',AttendDate=convert(smalldatetime,@AttendDate,103),Discount=@Discount where AdministrativNo=@AdministrativNo and ComCode='1' ";
                        SqlParameter[] parameter ={
                                              new SqlParameter("@AttendDate",txtWidrawDate.Text ),
                                              new SqlParameter("@Discount",Convert.ToDecimal(((TextBox)(gvCaseWidraw.Rows[i].FindControl("txtDiscount"))).Text) ),
                                              new SqlParameter("@AdministrativNo",gvCaseWidraw.Rows[i].Cells[1].Text.ToString().Trim() )
                                            };
                        rowupdate = rowupdate+SqlHelper.ExecuteNonQuery(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter);
                        //dc.UpdateDataBase(sql);
                        
                    }
                    

                }
                lblMsg.Text = rowupdate+" Data Successfully Updated";
            }
            catch (Exception ex)
            {
                lblMsg1.Text = ex.Message;
            }
        }

        #endregion


        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg1.Text = "";
            this.updateStatus();

        }

        protected void btnApproved1_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            this.LoadData();
        }

        protected void ckALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)gvCaseWidraw.HeaderRow.FindControl("ckALL");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvCaseWidraw.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvCaseWidraw.Rows[i].FindControl("ckAdminApproved");
                        chkboxSelect.Checked = true;

                    }
                }
                else
                {
                    for (int i = 0; i < gvCaseWidraw.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvCaseWidraw.Rows[i].FindControl("ckAdminApproved");
                        chkboxUnselect.Checked = false;

                    }
                }
            }
        }
    }
}