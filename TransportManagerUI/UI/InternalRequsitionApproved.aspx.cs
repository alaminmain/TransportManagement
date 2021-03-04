using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI.Workshop
{
    public partial class InternalRequsitionApproved : System.Web.UI.Page
    {
        #region Private Method
        private void MakeAccessible()
        {
            gvInternalREQ.UseAccessibleHeader = true;
            gvInternalREQ.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void searchByDeptHead()
        {
            string sql = @"select SI.InternalReqNo,SI.IssVouchNo,CONVERT(varchar(10),SI.IssDate,103)AS IssDate,SI.VehicleID,VE.VehicleNo,SI.ProdSubCatCode,ITS.ProdSubCatName,SI.Remarks  
                        from StockIssue SI inner join VehicleInfo VE on SI.VehicleID=ve.VehicleID 
                             inner join ItemSubCategory ITS on SI.CateCode=ITS.CateCode and SI.ProdSubCatCode=ITS.ProdSubCatCode
                             where SI.IssuStatus='1' and SI.ServiceStatus='0'
union all
select SI.InternalReqNo,SI.IssVouchNo,CONVERT(varchar(10),SI.IssDate,103)AS IssDate,SI.VehicleID,case when SI.VehicleID IS NULL then ''
                        end VehicleNo,SI.ProdSubCatCode,ITS.ProdSubCatName,SI.Remarks                          
                        from StockIssue SI inner join ItemSubCategory ITS on SI.CateCode=ITS.CateCode and SI.ProdSubCatCode=ITS.ProdSubCatCode
                             where SI.IssuStatus='1' and SI.ServiceStatus='0'and  VehicleID is null order by SI.InternalReqNo asc";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvInternalREQ.Caption = "Pending Internal Requision By Depertment Head";
                gvInternalREQ.DataSource = dt;
                gvInternalREQ.DataBind();
                MakeAccessible();
                hfPendingBy.Value = "0";
            }
            else
            {
                gvInternalREQ.EmptyDataText = "Data not Found.";
            }
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>loadDatatable()</script>", false);
        }

        private void SearchByFactoryHead()
        {

            string sql = @"select SI.InternalReqNo,SI.IssVouchNo,CONVERT(varchar(10), SI.IssDate, 103)AS IssDate, SI.VehicleID,VE.VehicleNo,SI.ProdSubCatCode,ITS.ProdSubCatName,SI.Remarks
                           from StockIssue SI inner join VehicleInfo VE on SI.VehicleID = ve.VehicleID
                             inner join ItemSubCategory ITS on SI.CateCode = ITS.CateCode and SI.ProdSubCatCode = ITS.ProdSubCatCode
                             where SI.IssuStatus = '2' and SI.ServiceStatus = '0'
union all
select SI.InternalReqNo,SI.IssVouchNo,CONVERT(varchar(10), SI.IssDate, 103)AS IssDate, SI.VehicleID,case when SI.VehicleID IS NULL then ''
                        end VehicleNo, SI.ProdSubCatCode,ITS.ProdSubCatName,SI.Remarks
                         from StockIssue SI inner join ItemSubCategory ITS on SI.CateCode = ITS.CateCode and SI.ProdSubCatCode = ITS.ProdSubCatCode
                             where SI.IssuStatus = '2' and SI.ServiceStatus = '0'and VehicleID is null order by SI.InternalReqNo asc";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvInternalREQ.Caption = "Pending Internal Requision By Factory Head";
                gvInternalREQ.DataSource = dt;
                gvInternalREQ.DataBind();
                MakeAccessible();
                hfPendingBy.Value = "1";
            }
            else
            {
                gvInternalREQ.EmptyDataText = "Data not Found.";
            }
            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>loadDatatable()</script>", false);
        }

        #endregion Private Method

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                gvInternalREQ.DataSource = null;
                gvInternalREQ.DataBind();
            }
        }

       

        protected void btnPendingByDH_Click(object sender, EventArgs e)
        {
            gvInternalREQ.DataSource = null;
            gvInternalREQ.DataBind();
            searchByDeptHead();
        }

        protected void btnFactoryHead_Click(object sender, EventArgs e)
        {
            gvInternalREQ.DataSource = null;
            gvInternalREQ.DataBind();
            SearchByFactoryHead();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Empty;
                int count = 0;
                    for (int i = 0; i < gvInternalREQ.Rows.Count; i++)
                    {
                        CheckBox isChecked = ((CheckBox)gvInternalREQ.Rows[i].FindControl("ckVMFApproved"));

                        if (isChecked.Checked)
                        {
                        if (hfPendingBy.Value == "0")
                        {
                            sql = "UPDATE StockIssue SET IssuStatus=2 where InternalReqNo=@InternalReqNo and ComCode='1'";
                        }
                        else if (hfPendingBy.Value == "1")
                            sql = "UPDATE StockIssue SET IssuStatus=3 where InternalReqNo=@InternalReqNo and ComCode='1'";
                        SqlParameter[] parameter ={
                                              new SqlParameter("@InternalReqNo", gvInternalREQ.Rows[i].Cells[1].Text.ToString().Trim() ),
                                            };
                            SqlHelper.ExecuteNonQuery(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter);
                        count++;
                            //dc.UpdateDataBase(sql);
                        }
                        

                        
                    }
                lblMessage.Text = count + " Data Successfully Updated";
                if (hfPendingBy.Value == "0")
                    searchByDeptHead();
                else
                    SearchByFactoryHead();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ckALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)gvInternalREQ.HeaderRow.FindControl("ckALL");

            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvInternalREQ.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvInternalREQ.Rows[i].FindControl("ckVMFApproved");
                        chkboxSelect.Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < gvInternalREQ.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvInternalREQ.Rows[i].FindControl("ckVMFApproved");
                        chkboxUnselect.Checked = false;
                    }
                }
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}