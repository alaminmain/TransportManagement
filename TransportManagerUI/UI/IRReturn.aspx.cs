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
    public partial class IRReturn : System.Web.UI.Page
    {

        #region Private Method
                
        private void loadddlDepartment()
        {
            string sql = @"select CateCode,CateName from Item_Category";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "CateName";
                ddlDepartment.DataValueField = "CateCode";
                ddlDepartment.DataBind();
            }
        }
        private void Load_ddlProdSubCategory()
        {
            string sql = @"select CateCode,ProdSubCatCode,ProdSubCatName from ItemSubCategory where CateCode=@CateCode";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={
                                              new SqlParameter("@CateCode", ddlDepartment.SelectedValue)
                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql,parameter).Tables[0];

            if (dt.Rows.Count > 0)
            {
                ddlSubDepartment.DataSource = dt;
                ddlSubDepartment.DataTextField = "ProdSubCatName";
                ddlSubDepartment.DataValueField = "ProdSubCatCode";
                ddlSubDepartment.DataBind();
            }

        }
        private DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ProductCode"));

            dt.Columns.Add(new DataColumn("StoreCode"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("UnitPrice"));
            dt.Columns.Add(new DataColumn("Commment"));

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                if ((row.FindControl("ck") as CheckBox).Checked)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    //for (int i = 0; i < row.Cells.Count; i++)
                    //{
                    //    if (i != 0)
                    dr["ProductCode"] = row.Cells[0].Text;
                    dr["StoreCode"] = row.Cells[3].Text;
                    dr["Quantity"] = (row.FindControl("txtIssuQTY") as TextBox).Text;
                    dr["UnitPrice"] = (row.FindControl("txtPrice") as TextBox).Text;
                    dr["Commment"] = (row.FindControl("txtComment") as TextBox).Text;
                    //}
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private void saveReturnOrd()
        {

            try
            {
                int ComCode = 1;
                string ReturnNo_1;
                if (string.IsNullOrEmpty(lblReturnNo.Text))
                {
                    lblReturnNo.Text = Return_maxID();
                    ReturnNo_1 = lblReturnNo.Text.Trim();
                }
                else
                {
                    ReturnNo_1 = lblReturnNo.Text.Trim();
                }
                string RetDate_2 = txtIssDate.Text;
                string InternalReqNo_3 = lblIRNo.Text;
                string ProdSubCatCode_4 = ddlSubDepartment.SelectedValue;

                string CateCode_5 = ddlDepartment.SelectedValue;
                decimal RetAmount_6;
                if (string.IsNullOrEmpty(txtTotalAMT.Text))
                {
                    RetAmount_6 = 0;
                }
                else
                {
                    RetAmount_6 = Convert.ToDecimal(txtTotalAMT.Text);
                }


                string RetRemarks_7;
                if (string.IsNullOrEmpty(txtRemark.Text))
                {
                    RetRemarks_7 = "";
                }
                else
                {
                    RetRemarks_7 = txtRemark.Text;
                }
                string userId = "";
                DataTable detail = new DataTable();
                detail = GetDataTable(gvEditVMF);

                string returnId = (new ReturnOrdGateway().InsertUpdateReturnOrd(ComCode, ReturnNo_1, RetDate_2, InternalReqNo_3, ProdSubCatCode_4, CateCode_5, RetAmount_6, RetRemarks_7, userId,detail));
                if(String.IsNullOrEmpty(returnId))
                {
                    lblMsg.Text = "Data Saved!";
                }
            }
            catch (Exception ex)
            {
                this.lblMsg.Text = (ex.Message);
            }

        }

        private string Return_maxID()
        {

            string CurrentYear = DateTime.Today.ToString("yyyy");
            String sql = @"select ISNULL(Max(Convert(integer,Right(ReturnNo,6))), 0) + 1 from ReturnOrd where ComCode='1'and year(RetDate)=" + CurrentYear + "";

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            int count = int.Parse(dt.Rows[0][0].ToString());
            string NewIssVoucherNo = count.ToString().PadLeft(6, '0');
            string InternalReq = "RET" + "-" + CurrentYear + NewIssVoucherNo;
            return InternalReq;


        }
        private void ckSelectAllItem()
        {
            CheckBox cb = (CheckBox)gvEditVMF.HeaderRow.FindControl("ckAll");
            decimal Qty = 0;
            decimal amount = 0;
            if (cb != null)
            {
                if (cb.Checked)
                {
                    for (int i = 0; i < gvEditVMF.Rows.Count; i++)
                    {
                        CheckBox chkboxSelect = (CheckBox)gvEditVMF.Rows[i].FindControl("ck");
                        chkboxSelect.Checked = true;

                        Qty = Qty + Convert.ToDecimal(((TextBox)gvEditVMF.Rows[i].FindControl("txtIssuQTY")).Text);
                        amount = amount +
                                 Convert.ToDecimal(((TextBox)gvEditVMF.Rows[i].FindControl("txtTotalAMT")).Text);
                    }
                }
                else
                {
                    for (int i = 0; i < gvEditVMF.Rows.Count; i++)
                    {
                        CheckBox chkboxUnselect = (CheckBox)gvEditVMF.Rows[i].FindControl("ck");
                        chkboxUnselect.Checked = false;
                        Qty = 0;
                        amount = 0;

                    }
                }
                txtTotalQTY.Text = Qty.ToString("0");
                txtTotalAMT.Text = amount.ToString("0.00");
            }
        }
        private DataTable loadAllInternalReq(string searchKey)
        {
            try
            {
                string sql = @"SELECT        ComCode, InternalReqNo, IssVouchNo, CONVERT(varchar(10), IssDate, 103) AS IssDate, VehicleID
                    FROM            StockIssue                  
                    ORDER BY InternalReqNo DESC";
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
                if (String.IsNullOrEmpty(searchKey))
                {


                }
                else
                {
                    var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("InternalReqNo").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("IssVouchNo").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();
                }
                return dt;

            }

            catch (Exception ex)
            {

                return null;

            }
        }
        private void loadInternalReqByID()
        {
            try
            {
                GridViewRow rows = gvIR.SelectedRow;
                string code = rows.Cells[1].Text;
                string sql = @"SELECT        SI.InternalReqNo, SI.IssDate  AS IssDate,  SI.Remarks, SI.CateCode, SID.IssQty, 
                         SID.ProductCode, SID.StoreCode, CAST(SID.IssQty AS DECIMAL(18, 2)) AS IssQty, CAST(SID.PurPrice AS DECIMAL(18, 2)) AS PurPrice, CAST(SID.PurPrice * SID.IssQty AS DECIMAL(18, 2)) AS TotalAMT, IT.ProductName, SID.Comment,
                         IT.ProductBName, SI.ProdSubCatCode, ITS.ProdSubCatName
                         FROM            StockIssue AS SI INNER JOIN
                         StockIssueDetail AS SID ON SI.ComCode = SID.ComCode AND SI.InternalReqNo = SID.InternalReqNo INNER JOIN
                         Item AS IT ON SID.ProductCode = IT.ProductCode AND SID.StoreCode = IT.StoreCode INNER JOIN
                         ItemSubCategory AS ITS ON SI.ProdSubCatCode = ITS.ProdSubCatCode AND SI.CateCode = ITS.CateCode
                            where si.InternalReqNo=@InternalReqNo";
                         //VI.IssuStatus='0'


                DataTable dt = new DataTable();

                SqlParameter[] parameter ={
                                              new SqlParameter("@InternalReqNo", code)
                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];
                //dt = dc.SelectDs(sql).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    lblIRNo.Text = dt.Rows[0]["InternalReqNo"].ToString();
                    //txtIssVoucherNo.Text = dt.Rows[0]["IssVouchNo"].ToString().Trim();
                    //txtIssDate.Text = dt.Rows[0]["IssDate"].ToString().Trim();
                    txtIssDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["IssDate"].ToString()).Date;
                    //txtModelNo.Text = dt.Rows[0]["ModelNo"].ToString().Trim();

                    txtRemark.Text = dt.Rows[0]["Remarks"].ToString().Trim();
                    //txtFromDate.Text = dt.Rows[0]["FromDate"].ToString().Trim();
                    //txtToDate.Text = dt.Rows[0]["ToDate"].ToString().Trim();

                    ddlDepartment.SelectedValue = dt.Rows[0]["CateCode"].ToString().Trim();



                    ddlSubDepartment.SelectedValue = dt.Rows[0]["ProdSubCatCode"].ToString().Trim();

                    //loadDDLTechnician();
                    //ddlTechnicianID.SelectedValue = dt.Rows[0]["TechnitianID"].ToString().Trim();

                    gvEditVMF.DataSource = dt;
                    gvEditVMF.DataBind();

                    CheckBox cb = (CheckBox)gvEditVMF.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();

                    //if (servicestatus == "1")
                    //{
                    //    ckWithoutParts.Checked = true;
                    //    this.loadDDLServiceProduct();

                    //    txtIssuQty.Text = "0";
                    //    txtIssuQty.Enabled = false;

                    //}
                    //else
                    //{
                    //    ckWithoutParts.Checked = false;

                    //    txtIssuQty.Text = "";
                    //    txtIssuQty.Enabled = true;
                    //}

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable loadAllReturn(string searchKey)
        {
            try
            {
                string sql = @"SELECT        ReturnNo, CONVERT(varchar(10), RetDate, 103) AS RetDate, InternalReqNo, CateCode, ProdSubCatCode, RetAmount, RetRemarks FROM            ReturnOrd order by RetDate ";
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
                if (String.IsNullOrEmpty(searchKey))
                {


                }
                else
                {
                    var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("InternalReqNo").ToUpper().Contains(searchKey.ToUpper())
       || r.Field<String>("ReturnNo").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();
                }
                return dt;

            }

            catch (Exception ex)
            {

                return null;

            }
        }
     




        private void clear_Grid()
        {


            gvEditVMF.DataSource = null;
            gvEditVMF.DataBind();
        }

       

        private void Clear_Form()
        {
            lblMsg.Text = "";


            txtIssDate.Text = "";


           
            txtTotalQTY.Text = "";
            txtTotalAMT.Text = "";
          
        }
           


      
        private void loadReturnData()
        {
            try
            {
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string code = rows.Cells[1].Text;

                string sql = @"SELECT        RO.ReturnNo, RO.RetDate, RO.InternalReqNo, RO.CateCode, RO.ProdSubCatCode, ROP.ProductCode, ROP.StoreCode, CAST(ROP.RetQty AS decimal(14, 2)) AS IssQty, CAST(ROP.UnitPrice AS decimal(18, 2)) AS PurPrice, 
                         CAST(ROP.RetQty * ROP.UnitPrice AS decimal(18, 2)) AS TotalAMT, IT.ProductName, IT.ProductBName, RO.RetAmount, RO.RetRemarks, ROP.Comment, StockIssue.IssDate
FROM            ReturnOrd AS RO INNER JOIN
                         ReturnProd AS ROP ON RO.ReturnNo = ROP.ReturnNo AND RO.ComCode = ROP.ComCode INNER JOIN
                         Item AS IT ON ROP.ProductCode = IT.ProductCode AND ROP.StoreCode = IT.StoreCode INNER JOIN
                         StockIssue ON RO.InternalReqNo = StockIssue.InternalReqNo
                            where RO.ReturnNo=@ReturnNo";
                DataTable dt = new DataTable();
                SqlParameter[] parameter ={
                                               new SqlParameter("@ReturnNo", rows.Cells[1].Text),
                                            };

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    ddlDepartment.SelectedValue = dt.Rows[0]["CateCode"].ToString();

                    Load_ddlProdSubCategory();
                    ddlSubDepartment.SelectedValue = dt.Rows[0]["ProdSubCatCode"].ToString();
                    txtIssDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["IssDate"].ToString()).Date;
                    txtReturnDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["RetDate"].ToString());
                    //txtIssDate.Text = dt.Rows[0]["IssDate"].ToString();
                    lblIRNo.Text = dt.Rows[0]["InternalReqNo"].ToString();
                    txtRemark.Text = dt.Rows[0]["RetRemarks"].ToString();
                    txtTotalAMT.Text = dt.Rows[0]["RetAmount"].ToString();
                    lblReturnNo.Text = dt.Rows[0]["ReturnNo"].ToString();
                    //txtReturnDate.Text = dt.Rows[0]["RetDate"].ToString();

                    gvEditVMF.DataSource = null;
                    gvEditVMF.DataBind();

                    gvEditVMF.DataSource = dt;
                    gvEditVMF.DataBind();
                    CheckBox cb = (CheckBox)gvEditVMF.HeaderRow.FindControl("ckAll");
                    cb.Checked = true;
                    ckSelectAllItem();
                    

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

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


        private void clearAll()
        {
            foreach (Control control in Panel2.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    txt.Text = "";
                }
                if (control is Label)
                {
                    Label txt = (Label)control;
                    txt.Text = "";
                }
                if (control is DropDownList)
                {
                    DropDownList txt = (DropDownList)control;
                    txt.ClearSelection();
                }
                if (control is GridView)
                {
                    GridView txt = (GridView)control;
                    txt.DataSource = null;
                    txt.DataBind();
                }

            }

            foreach (Control control in Panel6.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    txt.Text = "";
                }
                if (control is Label)
                {
                    Label txt = (Label)control;
                    txt.Text = "";
                }
                if (control is DropDownList)
                {
                    DropDownList txt = (DropDownList)control;
                    txt.ClearSelection();
                }
                if (control is GridView)
                {
                    GridView txt = (GridView)control;
                    txt.DataSource = null;
                    txt.DataBind();
                }

            }

        }
        private bool isMandatoryFieldValidate()
        {
            //if (String.IsNullOrEmpty(.Text))
            //    return false;
            //else
            return true;
        }
        #endregion
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
                    loadddlDepartment();
                    Load_ddlProdSubCategory();
                    txtReturnDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                    txtIssDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                   

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

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllReturn(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveReturnOrd();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string RepotrName = "ReturnOrder";
            Response.Redirect("reportViewerWorkshop.aspx?RID=" +lblReturnNo.Text + "&RN=" + RepotrName);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllReturn(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadReturnData();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllReturn(txtSearch.Text);
            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void ck_CheckedChanged(object sender, EventArgs e)
        {
            decimal Qty = 0;
            decimal amount = 0;
            foreach (GridViewRow row in gvEditVMF.Rows)
            {
                if ((row.FindControl("ck") as CheckBox).Checked)
                {
                    Qty = Qty + Convert.ToDecimal(((TextBox)row.FindControl("txtIssuQTY")).Text);
                    amount = amount +
                             Convert.ToDecimal(((TextBox)row.FindControl("txtTotalAMT")).Text);
                }
            }
            txtTotalQTY.Text = Qty.ToString("0");
            txtTotalAMT.Text = amount.ToString("0.00");
        }



        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_ddlProdSubCategory();
            
        }

        protected void ddlSubDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

       


        protected void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            ckSelectAllItem();
        }

        protected void btnIR_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllInternalReq(txtSearch.Text);
            gvIR.DataSource = dt;
            gvIR.DataBind();
            upIR.Update();
            hfShowIRGrid_ModalPopupExtender.Show();
        }

        protected void btnIRSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllInternalReq(txtSearch.Text);
            gvIR.DataSource = dt;
            gvIR.DataBind();
            upIR.Update();
            hfShowIRGrid_ModalPopupExtender.Show();
        }

        protected void gvIR_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInternalReqByID();
        }

        protected void gvIR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllInternalReq(txtSearch.Text);
            gvIR.PageIndex = e.NewPageIndex;
            gvIR.DataSource = dt;
            gvIR.DataBind();
            upIR.Update();
            hfShowIRGrid_ModalPopupExtender.Show();
        }
    }
}