using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;

namespace TransportManagerUI.UI
{
    public partial class VehicleMovmentEntry : System.Web.UI.Page
    {
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

            foreach (Control control in Panel4.Controls)
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
            dvVehicle.DataSource = null;
            dvVehicle.DataBind();
            gvVehicleStatus.DataSource = GetCurrentVehicleStatus();
            gvVehicleStatus.DataBind();
            hfShowTrip.Value = String.Empty;
        }
        private DataTable GetVehicleMovement(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {
                    dt2 = gatwayObj.GetVehicleMovmentAll(1);
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[VehicleStatus]='2'");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();


                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("VehicleID").Contains(searchKey) || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()));
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
        private DataTable GetDriver(string searchKey)
        {
            try
            {
                DataTable dt;


                using (PersonalGateway gatwayObj = new PersonalGateway())
                {


                    dt = gatwayObj.GetAllDriver();
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
        private DataTable GetVehicle(string searchKey)
        {
            try
            {
                DataTable dt = null;
                DataTable dt2;


                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {
                    dt2 = gatwayObj.GetAllVehicle(1);
                    DataRow[] foundRows;

                    // Use the Select method to find all rows matching the filter.
                    foundRows = dt2.Select("[VehicleStatus]<> '2'");
                    if (foundRows.Length > 0)
                        dt = foundRows.CopyToDataTable();


                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("VehicleID").Contains(searchKey) || r.Field<String>("VehicleNo").ToUpper().Contains(searchKey.ToUpper()));
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
        private DataTable GetCurrentVehicleStatus()
        {
            try
            {
                DataTable dt;


                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {


                    dt = gatwayObj.GetVehicleCurrentStatus(1);
                    
                    return dt;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }

        private bool isMandatoryFieldValidate()
        {
            if (String.IsNullOrEmpty(lblDriverCode.Text)&& String.IsNullOrEmpty(hfShowList.Value))
                return false;
         
            else
                return true;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                }
                isAuthorizeToPage();
               
                if (!IsPostBack && !IsCallback)
                {
                    txtStatusUpdateDate_CalendarExtender.SelectedDate = DateTime.Now.Date;
                    txtTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
                    gvVehicleStatus.DataSource = GetCurrentVehicleStatus();
                    gvVehicleStatus.DataBind();
                    hfShowList.Value = String.Empty;

                }
            }
            catch (Exception ex)
            {
                throw;
               
            }
        }

        protected void btnSearchVehicle_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtVehicleSearch.Text);

            gvVehicleList.DataSource = dt;
            gvVehicleList.DataBind();

            hfShowTrip_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (isMandatoryFieldValidate())
            {
                string moveRegNo = lblMovmentId.Text;
                string vehicleId = hfShowTrip.Value;
                string Time = DateTime.Now.ToString("hh:mm:ss tt");
                string movDate = txtStatusUpdateDate.Text + " " + Time;

                string DriverCode = lblDriverCode.Text;
                string Remarks = txtRemarks.Text;
                int vehicleStatus = Convert.ToInt16(ddlStatus.SelectedValue);
                string userCode = Session["UserName"].ToString();
                string VStatus = ddlStatus.SelectedItem.Text.Remove(1);
                if (VStatus == "W")
                    VStatus = "VWS";
                using (VehicleInfoGateway gatwayObj = new VehicleInfoGateway())
                {
                    string vno = gatwayObj.InsertUpdateVehicleMovement(1, moveRegNo, movDate, vehicleId, DriverCode, Remarks, vehicleStatus, VStatus, userCode);


                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
                    lblMovmentId.Text = vno;
                }
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Mandatory Field');", true);
            gvVehicleStatus.DataSource = GetCurrentVehicleStatus();
            gvVehicleStatus.DataBind();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Session["paramData"] = null;
            Session["reportOn"] = null;
            string vehicleRegId = lblMovmentId.Text;

            string reporton = "VehicleWorkSlip";

            Session["paramData"] = vehicleRegId;
            Session["reportOn"] = reporton;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "/UI/reportViewer.aspx" + "','_blank')", true);
        }

        protected void btnVehicleSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtVehicleSearch.Text);

            gvVehicleList.DataSource = dt;
            gvVehicleList.DataBind();

            hfShowTrip_ModalPopupExtender.Show();
        }

        protected void gvVehicleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvVehicleList.SelectedRow;

                hfShowTrip.Value = row.Cells[1].Text;
                DataTable dt = new DataTable();
                VehicleInfoGateway objGateway = new VehicleInfoGateway();

                //DataTable dtVehicle = new DataTable();
                //dtVehicle = objGateway.GetVehicleMovmentByVehicle(1, row.Cells[1].Text);

                //if (dtVehicle.Rows.Count >0)
                //{
                //    lblMovmentId.Text = Convert.ToString(dtVehicle.Rows[0]["MoveRegNo"]);
                //    ddlStatus.SelectedValue = Convert.ToString(dtVehicle.Rows[0]["VehicleStatus"]);
                    
                //    txtStatusUpdateDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dtVehicle.Rows[0]["moveDate"]).Date;

                //}
                dt = objGateway.GetVehicleById(1, row.Cells[1].Text);
                dvVehicle.DataSource = dt;
                dvVehicle.DataBind();
                hfShowTrip_ModalPopupExtender.Hide();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void gvVehicleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicle(txtVehicleSearch.Text);

            gvVehicleList.PageIndex = e.NewPageIndex;
            gvVehicleList.DataSource = dt;
            gvVehicleList.DataBind();
            UpdatePanel1.Update();
            hfShowTrip_ModalPopupExtender.Show();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicleMovement(txtSearch.Text);

           
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
           
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnDriver_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearchDriver.Text);

            gvDriverList.DataSource = dt;
            gvDriverList.DataBind();
            hfDriverSearch_ModalPopupExtender.Show();
        }

        protected void btnSearchDriver_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearchDriver.Text);

            gvDriverList.DataSource = dt;
            gvDriverList.DataBind();
            hfDriverSearch_ModalPopupExtender.Show();
        }

        protected void gvDriverList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvDriverList.SelectedRow;
            lblDriverCode.Text = row.Cells[1].Text;
            lblDriverName.Text = row.Cells[2].Text;
        }

        protected void gvDriverList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetDriver(txtSearchDriver.Text);

            gvDriverList.PageIndex = e.NewPageIndex;
            gvDriverList.DataSource = dt;
            gvDriverList.DataBind();
            UpdatePanel2.Update();
            hfDriverSearch_ModalPopupExtender.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicleMovement(txtSearch.Text);

            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvlistofBasicData.SelectedRow;

                
                DataTable dt = new DataTable();
                using (VehicleInfoGateway objGateway = new VehicleInfoGateway())
                {
                    dt = objGateway.GetVehicleMovmentByVehicle(1, row.Cells[1].Text);
                    hfShowTrip.Value = dt.Rows[0]["VehicleID"].ToString();
                    lblMovmentId.Text = row.Cells[1].Text;
                    lblDriverCode.Text = dt.Rows[0]["EmpCode"].ToString();
                    lblDriverName.Text = dt.Rows[0]["EmpName"].ToString();

                    txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                    if (String.IsNullOrEmpty(dt.Rows[0]["VehicleStatus"].ToString()))
                        ddlStatus.SelectedValue = "0";
                    else
                        ddlStatus.SelectedValue = dt.Rows[0]["VehicleStatus"].ToString();
                    txtStatusUpdateDate_CalendarExtender.SelectedDate = Convert.ToDateTime(dt.Rows[0]["MoveDate"]).Date;
                    txtTime.Text = Convert.ToDateTime(dt.Rows[0]["MoveDate"]).ToString("hh:mm:ss tt");
                    dvVehicle.DataSource = objGateway.GetVehicleById(1, dt.Rows[0]["VehicleID"].ToString()); ;
                    dvVehicle.DataBind();
                }
                txtSearch.Text = String.Empty;
               
                hfShowTrip_ModalPopupExtender.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetVehicleMovement(txtSearch.Text);
            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}