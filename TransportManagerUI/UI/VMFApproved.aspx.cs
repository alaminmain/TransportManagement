using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI.Workshop
{
    public partial class VMFApproved : System.Web.UI.Page
    {
        #region Private Method

        private void searchVMFByDeptHead()
        {
            string sql = @"select VI.IssVouchNo,CONVERT(varchar(10),VI.IssDate,103)AS IssDate,VI.VehicleID,VE.VehicleNo,VI.ProdSubCatCode,ITS.ProdSubCatName,VI.Remarks
                        from VMFIssue VI inner join VehicleInfo VE on VI.VehicleID=ve.VehicleID
                             inner join ItemSubCategory ITS on VI.CateCode=ITS.CateCode and VI.ProdSubCatCode=ITS.ProdSubCatCode
                             where VI.IssuStatus='0'";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvVMFApproved.DataSource = dt;
                gvVMFApproved.DataBind();
                gvVMFApproved.Caption = "VMF Approve for Department Head";
                MakeAccessible();
                hfPendingBy.Value = "0";
            }
            else
            {
                gvVMFApproved.EmptyDataText = "Data not Found.";
            }
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>loadDatatable()</script>", false);
        }

        private void SearchByFactoryHead()
        {
            string sql = @"select VI.IssVouchNo,CONVERT(varchar(10),VI.IssDate,103)AS IssDate,VI.VehicleID,VE.VehicleNo,VI.ProdSubCatCode,ITS.ProdSubCatName,VI.Remarks
                        from VMFIssue VI inner join VehicleInfo VE on VI.VehicleID=ve.VehicleID
                             inner join ItemSubCategory ITS on VI.CateCode=ITS.CateCode and VI.ProdSubCatCode=ITS.ProdSubCatCode
                             where vi.IssuStatus='2'";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvVMFApproved.DataSource = dt;
                gvVMFApproved.DataBind();
                gvVMFApproved.Caption = "VMF Approve for Factory Head";
                MakeAccessible();
                hfPendingBy.Value = "1";
            }
            else
            {
                gvVMFApproved.EmptyDataText = "Data not Found.";
            }
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>loadDatatable()</script>", false);
        }

        #endregion Private Method

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                gvVMFApproved.DataSource = null;
                gvVMFApproved.DataBind();
            }
        }

        private void MakeAccessible()
        {
            gvVMFApproved.UseAccessibleHeader = true;
            gvVMFApproved.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnPendingByDH_Click(object sender, EventArgs e)
        {
            searchVMFByDeptHead();
        }

        protected void btnFactoryHead_Click(object sender, EventArgs e)
        {
            SearchByFactoryHead();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int countRowUpdate = 0;
                if (hfPendingBy.Value == "0")
                {
                    for (int i = 0; i < gvVMFApproved.Rows.Count; i++)
                    {
                        CheckBox isChecked = ((CheckBox)gvVMFApproved.Rows[i].FindControl("ckVMFApproved"));

                        if (isChecked.Checked)
                        {
                            string sql = "UPDATE VMFIssue SET IssuStatus=2 where IssVouchNo=@IssVouchNo and ComCode='1'";
                            SqlParameter[] parameter ={
                                              new SqlParameter("@IssVouchNo", gvVMFApproved.Rows[i].Cells[1].Text.ToString().Trim() ),
                                            };
                            countRowUpdate = countRowUpdate + SqlHelper.ExecuteNonQuery(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter);
                            
                            //dc.UpdateDataBase(sql);
                        }
                        lblMessage.Text = countRowUpdate + " VMF Successfully Updated";
                        searchVMFByDeptHead();
                    }
                }
                else if (hfPendingBy.Value == "1")
                {
                    for (int i = 0; i < gvVMFApproved.Rows.Count; i++)
                    {
                        CheckBox isChecked = ((CheckBox)gvVMFApproved.Rows[i].FindControl("ckVMFApproved"));

                        if (isChecked.Checked)
                        {
                            string sql = "UPDATE VMFIssue SET IssuStatus=3 where IssVouchNo=@IssVouchNo and ComCode='1'";
                            SqlParameter[] parameter ={
                                              new SqlParameter("@IssVouchNo", gvVMFApproved.Rows[i].Cells[1].Text.ToString().Trim() ),
                                            };
                            countRowUpdate = countRowUpdate + SqlHelper.ExecuteNonQuery(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter); ;
                        }
                        lblMessage.Text = countRowUpdate+" VMF Successfully Updated";
                        SearchByFactoryHead();
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
            CheckBox cb = (CheckBox)gvVMFApproved.HeaderRow.FindControl("ckALL");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvVMFApproved.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvVMFApproved.Rows[i].FindControl("ckVMFApproved");
                        chkboxSelect.Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < gvVMFApproved.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvVMFApproved.Rows[i].FindControl("ckVMFApproved");
                        chkboxUnselect.Checked = false;
                    }
                }
            }
        }
    }
}