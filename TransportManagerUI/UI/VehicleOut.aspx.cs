using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI.Workshop
{
    public partial class VehicleOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                gvVMFOut.DataSource = null;
                gvVMFOut.DataBind();
                gvVehicleOUT.DataSource = null;
                gvVehicleOUT.DataBind();

                txtOutDate_CalendarExtender.SelectedDate= DateTime.Now.Date;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            this.updateStatus();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnSVehicle_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            gvVehicleOUT.DataSource = null;
            gvVehicleOUT.DataBind();
            gvVMFOut.DataSource = null;
            gvVMFOut.DataBind();
            approvedKey = "01";
            this.searchFromVMF();
        }

        protected void btnSServiceProduct_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            gvVehicleOUT.DataSource = null;
            gvVehicleOUT.DataBind();
            gvVMFOut.DataSource = null;
            gvVMFOut.DataBind();
            approvedKey = "02";
            this.SearchByFromInternalREQ();
        }

        private void searchFromVMF()
        {
            string sql = @"select VI.IssVouchNo,CONVERT(varchar(10),VI.IssDate,103)AS IssDate,VI.VehicleID,VE.VehicleNo,VI.ProdSubCatCode,ITS.ProdSubCatName,VI.Remarks
                        from VMFIssue VI inner join VehicleInfo VE on VI.VehicleID=ve.VehicleID
                             inner join ItemSubCategory ITS on VI.CateCode=ITS.CateCode and VI.ProdSubCatCode=ITS.ProdSubCatCode
                             where vi.IssuStatus='3' and VI.ServiceStatus='1' order by(VI.IssVouchNo)desc";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvVMFOut.DataSource = dt;
                gvVMFOut.DataBind();
            }
            else
            {
                lblMessage.Text = "Data not Found.";
            }
        }

        private void SearchByFromInternalREQ()
        {
            string sql = @"select SI.InternalReqNo,SI.IssVouchNo,CONVERT(varchar(10),SI.IssDate,103)AS IssDate,SI.VehicleID,VE.VehicleNo,SI.ProdSubCatCode,ITS.ProdSubCatName,SI.Remarks
                        from StockIssue SI inner join VehicleInfo VE on SI.VehicleID=ve.VehicleID
                             inner join ItemSubCategory ITS on SI.CateCode=ITS.CateCode and SI.ProdSubCatCode=ITS.ProdSubCatCode
                             where SI.IssuStatus='3' and SI.ServiceStatus='0' order by(SI.InternalReqNo)desc";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvVehicleOUT.DataSource = dt;
                gvVehicleOUT.DataBind();
            }
            else
            {
                lblMessage.Text = "Data not Found.";
            }
        }

        private void updateStatus()
        {
            try
            {
                int countRowUpdate = 0;
                if (approvedKey == "01")
                {
                    for (int i = 0; i < gvVMFOut.Rows.Count; i++)
                    {
                        CheckBox isChecked = ((CheckBox)gvVMFOut.Rows[i].FindControl("ckVMFApproved0"));

                        if (isChecked.Checked)
                        {
                            string sql = "UPDATE VMFIssue SET IssuStatus='5',OutDate=CONVERT(smalldatetime,@OutDate,103) where IssVouchNo=@IssVouchNo and ComCode='1'";
                            SqlParameter[] parameter ={
                                                new SqlParameter("@OutDate", txtOutDate.Text ),
                                              new SqlParameter("@IssVouchNo", gvVMFOut.Rows[i].Cells[1].Text.ToString().Trim() )
                                            };
                            countRowUpdate = countRowUpdate + SqlHelper.ExecuteNonQuery(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter);
                            
                            //dc.UpdateDataBase(sql);
                        }
                        lblMessage.Text = countRowUpdate+" Data Successfully Updated";
                        SearchByFromInternalREQ();
                    }
                }
                else if (approvedKey == "02")
                {
                    for (int i = 0; i < gvVehicleOUT.Rows.Count; i++)
                    {
                        CheckBox isChecked = ((CheckBox)gvVehicleOUT.Rows[i].FindControl("ckVMFApproved"));

                        if (isChecked.Checked)
                        {
                            string sql = "UPDATE StockIssue SET IssuStatus='5',OutDate=CONVERT(smalldatetime,'',103) where InternalReqNo=@InternalReqNo and ComCode='1'";
                            SqlParameter[] parameter ={
                                              new SqlParameter("@InternalReqNo", gvVMFOut.Rows[i].Cells[1].Text.ToString().Trim() )
                                            };
                            countRowUpdate = countRowUpdate + SqlHelper.ExecuteNonQuery(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter);
                          
                            //dc.UpdateDataBase(sql);
                        }
                        lblMessage.Text = countRowUpdate+" Data Successfully Updated";
                        searchFromVMF();
                    }
                }
                else
                {
                    lblMessage.Text = "please search VMF";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ckALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)gvVehicleOUT.HeaderRow.FindControl("ckALL");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvVehicleOUT.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvVehicleOUT.Rows[i].FindControl("ckVMFApproved");
                        chkboxSelect.Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < gvVehicleOUT.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvVehicleOUT.Rows[i].FindControl("ckVMFApproved");
                        chkboxUnselect.Checked = false;
                    }
                }
            }
        }

        public static string approvedKey;

        protected void ckALL0_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)gvVMFOut.HeaderRow.FindControl("ckALL0");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvVMFOut.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvVMFOut.Rows[i].FindControl("ckVMFApproved0");
                        chkboxSelect.Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < gvVMFOut.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvVMFOut.Rows[i].FindControl("ckVMFApproved0");
                        chkboxUnselect.Checked = false;
                    }
                }
            }
        }
    }
}