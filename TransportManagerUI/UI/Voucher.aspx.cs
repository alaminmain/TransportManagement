using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;
using System.IO;

namespace TransportManagerUI.UI
{
    public partial class Voucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }
            isAuthorizeToPage();
            if (!IsPostBack && !IsCallback)
            {
                loadChartofAccounts();
                txtVoucherDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                txtReturnDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
            }
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllVoucher(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();

            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvTrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvTrip.SelectedRow;


            DataTable dt = new DataTable();
            using (TripInfoGateway gatwayObj = new TripInfoGateway())
            {
                dt = gatwayObj.GetTripInfo(row.Cells[1].Text);

            }

            dvTrip.DataSource = dt;
            dvTrip.DataBind();
            DataTable movementIncome = new DataTable();
            using (TransportDetailGateway gatwayObj = new TransportDetailGateway())
            {
                movementIncome = gatwayObj.GetMovmentDetailForVoucherIncome(row.Cells[1].Text);
            }
           decimal totalRent = movementIncome.AsEnumerable().Sum(x => Convert.ToDecimal(x["Rent"]));
            gvIncome.DataSource = movementIncome;
            gvIncome.DataBind();
            txtIncome.Text = totalRent.ToString("0.00");
            loadChartofAccounts();
            NetPayment();
           

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "calculateNetIncome()", true);
            //txtTripInfo.Text = "Trip No- " + row.Cells[1].Text + " " + row.Cells[2].Text;
        }

        protected void gvTrip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllTrip(txtSearchTrip.Text);

            gvTrip.PageIndex = e.NewPageIndex;
            gvTrip.DataSource = dt;
            gvTrip.DataBind();
            upTrip.Update();
            hfTC_ModalPopupExtender.Show();
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

        

        private void clearAll()
        {
            foreach (Control control in Panel5.Controls)
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
            foreach (Control control in Panel8.Controls)
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


            gvIncome.DataSource = null;
            gvIncome.DataBind();
            dvTrip.DataSource = null;
            dvTrip.DataBind();

            //clear Inputbox
            txtIncome.Text = "0.00";
            txtTotalIncome.Text = "0.00";
            txtNetIncome.Text = "0.00";
            txtTotalExpense.Text = "0.00";
            txtTotalAmount.Text = "0.00";
            
            loadChartofAccounts();
        }
        private DataTable LoadAllVoucher(string searchKey)
        {
            try
            {
                DataTable dt;


                using (VoucherGateway gatwayObj = new VoucherGateway())
                {


                    dt = gatwayObj.GetVoucher(1);
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("VoucherNo").Contains(searchKey));
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
        private DataTable loadAllMovement(string searchKey)
        {
            try
            {
                DataTable dt;


                using (TransportMasterGateway gatwayObj = new TransportMasterGateway())
                {


                    dt = gatwayObj.GetMovmentforGridview(1);
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("MovementNo").Contains(searchKey));
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
        private DataTable LoadAllTrip(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {


                    dt2 = gatwayObj.GetAllTripInfoForGridView();

                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[TripStatus]= '1'");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("TripNo").Contains(searchKey));
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
        DataTable GetTripDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 1; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    //if(i!=0)
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                //for (int i = 0; i < row.Cells.Count; i++)
                //{
                //    if (i != 0)
                dr["Date"] = (row.FindControl("TextBox1") as TextBox).Text; //row.Cells[1].Text;
                dr["Product"] = row.Cells[2].Text;
                dr["TripFrom"] = row.Cells[3].Text;
                dr["TripTo"] = row.Cells[4].Text;
                dr["Rent"] = row.Cells[5].Text;

                //}
                dt.Rows.Add(dr);
            }
            return dt;
        }

        DataTable GetExpensesDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    //if(i!=0)
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }


            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                //for (int i = 0; i < row.Cells.Count; i++)
                //{
                //    if (i != 0)
                //row.FindControl("")
                dr["AccountCode"] = row.Cells[0].Text;
                dr["AccountDesc"] = row.Cells[1].Text;
                dr["Amount"] = (row.FindControl("txtAmount") as TextBox).Text; ;
                dr["Comments"] = (row.FindControl("txtComments") as TextBox).Text;


                //}
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void loadChartofAccounts()
        {
            try
            {
                DataTable dt;
                decimal totalFuelAmount = 0;

                using (ChartOfAccountGateway gatwayObj = new ChartOfAccountGateway())
                {


                    dt = gatwayObj.Get_All_ChartOfACC();
                }
                dt.Columns.Add("Amount");
                dt.Columns.Add("Comments");

                if (dvTrip.DataItemCount != 0)
                {
                    DataTable dttrip = new DataTable();
                    using (TripInfoGateway gatwayObj = new TripInfoGateway())
                    {
                        string tripno = dvTrip.Rows[0].Cells[1].Text.ToString();
                        dttrip = gatwayObj.GetTripInfo(tripno);
                        totalFuelAmount = (Convert.ToDecimal(dttrip.Rows[0]["FuelQty"]) - Convert.ToDecimal(dttrip.Rows[0]["AdjFuelQty"])) * Convert.ToDecimal(dttrip.Rows[0]["FuelRate"]);
                    }

                }

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["AccountCode"].ToString() == "1001")
                    {
                        dr["Amount"] = Convert.ToDecimal(totalFuelAmount.ToString("0.00"));
                        dr["Comments"] = String.Empty;
                    }

                    else
                    {
                        dr["Amount"] = 0;
                        dr["Comments"] = String.Empty;
                    }
                }

                gvExpenses.DataSource = dt;
                gvExpenses.DataBind();
                txtTotalAmount.Text = String.Format("{0:0.00}", Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Amount"]))));
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private bool isMandatoryFieldValidate()
        {
           
            if (dvTrip.Rows.Count <= 0)
                return false;
            //else if (gvIncome.Rows.Count <= 0)
            //    return false;
            else
                return true;
        }

        #endregion

        protected void btnSearchTrip_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = LoadAllTrip(txtSearchTrip.Text);

                gvTrip.DataSource = dt;
                gvTrip.DataBind();
                hfTC_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAddIncome_Click(object sender, EventArgs e)
        {
            if (hfInsertorUpdate.Value == "Insert")
            {
                try
                {
                    DataTable dt = new DataTable();
                    //DataTable previousDt = new DataTable();
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Product");
                    dt.Columns.Add("TripFrom");
                    dt.Columns.Add("TripTo");
                    dt.Columns.Add("Rent");

                    DataRow dr = dt.NewRow();
                    dr["Date"] = DateTime.Now;
                    dr["Product"] = txtProduct.Text;
                    dr["TripFrom"] = txtTripFrom.Text;
                    dr["TripTo"] = txtTripTo.Text;
                    dr["Rent"] = txtRentAmount.Text;

                    if (gvIncome.Rows.Count <= 0)
                    {
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dt = GetTripDataTable(gvIncome);
                        DataRow dr1 = dt.NewRow();
                        dr1["Date"] = txtVoucherDate.Text;
                        dr1["Product"] = txtProduct.Text;
                        dr1["TripFrom"] = txtTripFrom.Text;
                        dr1["TripTo"] = txtTripTo.Text;
                        dr1["Rent"] = txtRentAmount.Text;
                        dt.Rows.Add(dr1);
                    }
                    gvIncome.DataSource = dt;
                    gvIncome.DataBind();
                    txtIncome.Text = Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Rent"])));

                    txtProduct.Text = String.Empty;
                    txtTripFrom.Text = String.Empty;
                    txtTripTo.Text = String.Empty;
                    txtRentAmount.Text = String.Empty;
                    NetPayment();

                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMandatoryFieldValidate())
                {
                    GridViewRow rows = gvTrip.SelectedRow;
                    NetPayment();
                    int comcode_1 = 1;
                    string VoucherNo_2 = lblVoucherNo.Text;
                    string VoucherDate_3 = txtVoucherDate.Text;

                    string TripNo_4 = dvTrip.Rows[0].Cells[1].Text.ToString();
                    decimal Income_5 = Convert.ToDecimal(txtIncome.Text);
                    decimal Advance_6 = Convert.ToDecimal(txtAdvance.Text);
                    decimal TotExpense_7 = Convert.ToDecimal(txtTotalAmount.Text);
                    string Narration_8 = txtRemarks.Text;
                    string ReturnDate_12 = txtReturnDate.Text;
                    int VoucherStatus_10 = Convert.ToInt32(ddlStatus.SelectedValue);
                    string userId = Session["UserName"].ToString();
                    decimal additionalKm = Convert.ToDecimal(txtAdditionalKM.Text);

                    DataTable income = new DataTable();
                    DataTable expenses = new DataTable();

                    income = GetTripDataTable(gvIncome);
                    expenses = GetExpensesDataTable(gvExpenses);

                    using (VoucherGateway gatewayObj = new VoucherGateway())
                    {
                        lblVoucherNo.Text = gatewayObj.InsertUpdateVoucher(comcode_1, VoucherNo_2, VoucherDate_3, TripNo_4, Income_5, Advance_6
                            , TotExpense_7, Narration_8, ReturnDate_12, VoucherStatus_10, userId, additionalKm, income, expenses);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
                    }
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Mandatory Field');", true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void loadAllValue()
        {
            DataTable dt = new DataTable();


        }

        protected void gvExpenses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetExpensesDataTable(gvExpenses);

            dt.Rows[e.RowIndex].Delete();


            //decimal total = Convert.ToDecimal(dt.Compute("Sum(TotalPrice)", ""));
            //txtTotalAmount.Text = total.ToString();


            gvExpenses.DataSource = dt;
            gvExpenses.DataBind();
            txtTotalAmount.Text = Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Amount"])));

        }

        protected void gvIncome_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetTripDataTable(gvIncome);

            dt.Rows[e.RowIndex].Delete();


            //decimal total = Convert.ToDecimal(dt.Compute("Sum(TotalPrice)", ""));
            //txtTotalAmount.Text = total.ToString();


            gvIncome.DataSource = dt;
            gvIncome.DataBind();
            txtIncome.Text = Convert.ToString(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Rent"])));

        }
        public void NetPayment()
        {
            try
            {
                decimal advance = 0;
                if (txtAdvance.Text == String.Empty)
                    advance = 0;
                else
                {
                    advance = Convert.ToDecimal(txtAdvance.Text);
                }

                decimal totalExpense = 0;
                if (txtTotalAmount.Text == String.Empty)
                {
                    totalExpense = 0;
                }
                else
                    totalExpense = Convert.ToDecimal(txtTotalAmount.Text);
                if (String.IsNullOrEmpty(txtIncome.Text))
                {
                    txtIncome.Text = "0";
                }
                totalExpense = totalExpense + advance;
                txtTotalIncome.Text = txtIncome.Text;
                txtTotalExpense.Text = txtTotalAmount.Text;
                txtTotalAdvance.Text = txtAdvance.Text;

                decimal netPayment = Convert.ToDecimal(txtIncome.Text) - totalExpense;
                txtNetIncome.Text = netPayment.ToString("0.00");
                updnetIncome.Update();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblVoucherNo.Text) == false)
            {
                Session["paramData"] = null;
                Session["reportOn"] = null;
                string voucherno = lblVoucherNo.Text;

                string reporton = "Voucher";

                Session["paramData"] = voucherno;
                Session["reportOn"] = reporton;
                //btnReport.PostBackUrl = "~/UI/reportViewer.aspx";

                //Response.Write("<script>window.open('~/UI/reportViewer.aspx','_blank');</script>");
                Response.Redirect("~/UI/reportViewer.aspx");
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Please Select Voucher No');", true);
        }

        protected void btnTripOk_Click(object sender, EventArgs e)
        {

        }

        protected void txtAdditionalKM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal ExtraFuel = Convert.ToDecimal(txtAdditionalKM.Text) / Convert.ToDecimal(dvTrip.Rows[7].Cells[1].Text);
                decimal Amount = ExtraFuel * Convert.ToDecimal(dvTrip.Rows[8].Cells[1].Text);
                gvExpenses.Rows[7].Cells[3].Text = Amount.ToString("0.00");
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void txtIncome_TextChanged(object sender, EventArgs e)
        {
            NetPayment();
        }

        protected void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            NetPayment();
        }

        protected void txtAdvance_TextChanged(object sender, EventArgs e)
        {
            NetPayment();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadAllVoucher(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadVoucherUI();
        }

        private void loadVoucherUI()
        {
            try
            {
                GridViewRow row = gvlistofBasicData.SelectedRow;
                //Load Related Info
                DataTable dt = new DataTable();
                using (VoucherGateway gatwayObj = new VoucherGateway())
                {
                    dt = gatwayObj.GetVoucherByVoucherNo(1, row.Cells[1].Text);
                }
                //ComCode, VoucherNo, VoucherDate, TripNo, Income, Advance, TotExpense, Narration, Km,Fuel, KmPerLitre, FuelRate,AdditionalKM, ReturnDate, VouchStatus
               

                lblVoucherNo.Text = dt.Rows[0]["VoucherNo"].ToString();
                txtVoucherDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["VoucherDate"]).Date;
                txtReturnDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["ReturnDate"]).Date;

                txtAdvance.Text = Convert.ToDecimal(dt.Rows[0]["Advance"]).ToString("0.00");
                txtRemarks.Text = dt.Rows[0]["Narration"].ToString();
                ddlStatus.SelectedValue = dt.Rows[0]["VouchStatus"].ToString();

                DataTable loadTrip = new DataTable();
                using (TripInfoGateway gatwayObj = new TripInfoGateway())
                {
                    loadTrip = gatwayObj.GetTripInfo(dt.Rows[0]["TripNo"].ToString());
                    txtAdditionalKM.Text = Convert.ToInt32(loadTrip.Rows[0]["Additionalkm"]).ToString("0");
                }
              
                dvTrip.DataSource = loadTrip;
                dvTrip.DataBind();
                
                //Load Income
                DataTable getIncome = new DataTable();
                DataTable getExpense = new DataTable();
                using (VoucherDetailGateway gt = new VoucherDetailGateway())
                {
                    getIncome = gt.get_Voucher_Income_Detail(lblVoucherNo.Text);
                    getExpense = gt.get_Voucher_Detail(lblVoucherNo.Text);
                }

                gvIncome.DataSource = getIncome;
                gvIncome.DataBind();
                gvExpenses.DataSource = getExpense;
                gvExpenses.DataBind();

                txtIncome.Text = txtIncome.Text = Convert.ToDecimal(getIncome.AsEnumerable().Sum(x => Convert.ToDecimal(x["Rent"]))).ToString("0.00");
                txtTotalAmount.Text = Convert.ToDecimal(getExpense.AsEnumerable().Sum(x => Convert.ToDecimal(x["Amount"]))).ToString("0.00");

                NetPayment();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}