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
    public partial class AdministrativWorkApproved : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {

                AdminWorkType();
                

            }
        }
        #region method

        private void updateStatus()
        {
            try
            {
                int rowupdate = 0;
                for (int i = 0; i < gvAdminApproved.Rows.Count; i++)
                {
                    CheckBox isChecked = ((CheckBox)gvAdminApproved.Rows[i].FindControl("ckAdminApproved"));

                    if (isChecked.Checked)
                    {
                        string sql = "UPDATE AdministrativWork SET Status='1' where AdministrativNo=@AdministrativNo and ComCode='1' ";
                        SqlParameter[] parameter ={

                                              new SqlParameter("@AdministrativNo",gvAdminApproved.Rows[i].Cells[3].Text.ToString().Trim() )
                                            };
                        rowupdate = rowupdate + SqlHelper.ExecuteNonQuery(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter);
                        //dc.UpdateDataBase(sql);
                    }
                    lblMsg1.Text = rowupdate + " Data Successfully Updated";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void AdminWorkType()
        {
            string sql1 = @"Select AW.AdministrativNo,convert(varchar (10),AW.IssDate,103)as IssDate,AW.VehicleID,VI.VehicleNo,AW.ModelNo,AW.CateID,ACI.CateName,AW.EmpCode,P.EmpName,cast((VAW.TotAmount)as decimal(18,2))AS TotAmount  
                          from AdministrativWork AW inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join AdminItemCate ACI on AW.CateID=ACI.CateID
                           inner join Personal P on AW.EmpCode=P.EmpCode and AW.ComCode=P.ComCode
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo  
                           where AW.status=0 and AW.CateID=@CateID order by (AW.AdministrativNo)desc";
            DataTable dt1 = new DataTable();
            SqlParameter[] parameter ={

                                              new SqlParameter("@CateID",ddlWorkType.SelectedValue )
                                            };
            dt1 = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql1, parameter).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                gvAdminApproved.DataSource = null;
                gvAdminApproved.DataBind();
                gvAdminApproved.Columns[5].Visible = true;
                gvAdminApproved.DataSource = dt1;
                gvAdminApproved.DataBind();
            }
            else
            {
                gvAdminApproved.DataSource = null;
                gvAdminApproved.DataBind();
                lblMsg.Text = "Data not found.";
            }
        }

        private void LoadData()
        {
            string sql1 = @"Select AW.AdministrativNo,convert(varchar (10),AW.IssDate,103)as IssDate,AW.VehicleID,VI.VehicleNo,AW.ModelNo,AW.CateID,ACI.CateName,AW.EmpCode,P.EmpName,cast((VAW.TotAmount)as decimal(18,2))AS TotAmount  
                          from AdministrativWork AW inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join AdminItemCate ACI on AW.CateID=ACI.CateID
                           inner join Personal P on AW.EmpCode=P.EmpCode and AW.ComCode=P.ComCode
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo  
                           where AW.status=0 and AW.CateID='01'order by (AW.AdministrativNo)desc";
            DataTable dt1 = new DataTable();
            dt1 = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                gvAdminApproved.DataSource = null;
                gvAdminApproved.DataBind();
                gvAdminApproved.Columns[5].Visible = true;
                gvAdminApproved.DataSource = dt1;
                gvAdminApproved.DataBind();
            }
            else
            {
                gvAdminApproved.DataSource = null;
                gvAdminApproved.DataBind();
                lblMsg.Text = "Data not found.";
            }
        }
        private void LoadPaper()
        {
            string sql2 = @"Select AW.AdministrativNo,convert(varchar (10),AW.IssDate,103)as IssDate,AW.VehicleID,VI.VehicleNo,AW.ModelNo,AW.CateID,ACI.CateName,AW.EmpCode,cast((VAW.TotAmount)as decimal(18,2))AS TotAmount  
                          from AdministrativWork AW inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join AdminItemCate ACI on AW.CateID=ACI.CateID
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo  
                           where AW.status=0 And AW.CateID='02'order by (AW.AdministrativNo)desc";
            DataTable dt2 = new DataTable();
            dt2 = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql2).Tables[0];
            if (dt2.Rows.Count > 0)
            {
                gvAdminApproved.DataSource = null;
                gvAdminApproved.DataBind();
                gvAdminApproved.Columns[5].Visible = false;
                gvAdminApproved.DataSource = dt2;
                gvAdminApproved.DataBind();
            }
            else
            {
                gvAdminApproved.DataSource = null;
                gvAdminApproved.DataBind();
                lblMsg.Text = "Data not found.";
            }
        }


        private void LoadAccident()
        {
            string sql1 = @"Select AW.AdministrativNo,convert(varchar (10),AW.IssDate,103)as IssDate,AW.VehicleID,VI.VehicleNo,AW.ModelNo,AW.CateID,ACI.CateName,AW.EmpCode,P.EmpName,cast((VAW.TotAmount)as decimal(18,2))AS TotAmount  
                          from AdministrativWork AW inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join AdminItemCate ACI on AW.CateID=ACI.CateID
                           inner join Personal P on AW.EmpCode=P.EmpCode and AW.ComCode=P.ComCode
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo  
                           where AW.status=0 and AW.CateID='03'order by (AW.AdministrativNo)desc";
            DataTable dt1 = new DataTable();
            dt1 = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                gvAdminApproved.DataSource = null;
                gvAdminApproved.DataBind();
                gvAdminApproved.Columns[5].Visible = true;
                gvAdminApproved.DataSource = dt1;
                gvAdminApproved.DataBind();
            }
            else
            {
                gvAdminApproved.DataSource = null;
                gvAdminApproved.DataBind();
                lblMsg.Text = "Data not found.";
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
            this.updateStatus();

        }

        protected void ckALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)gvAdminApproved.HeaderRow.FindControl("ckALL");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvAdminApproved.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvAdminApproved.Rows[i].FindControl("ckAdminApproved");
                        chkboxSelect.Checked = true;

                    }
                }
                else
                {
                    for (int i = 0; i < gvAdminApproved.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvAdminApproved.Rows[i].FindControl("ckAdminApproved");
                        chkboxUnselect.Checked = false;

                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg1.Text = "";
            lblName.Text = "";

            lblName.Text = "VEHICLE " + ddlWorkType.SelectedItem.Text.ToUpper();
            AdminWorkType();

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

        }

        protected void ddlWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblName.Text = "VEHICLE " + ddlWorkType.SelectedItem.Text;
            AdminWorkType();
        }
    }
}