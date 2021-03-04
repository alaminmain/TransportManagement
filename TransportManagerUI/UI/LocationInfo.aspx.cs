using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI
{
    public partial class LocationInfo : System.Web.UI.Page
    {
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
                //lblMessage.Text = string.Empty;
                if (!IsPostBack && !IsCallback)
                {
                    //ClearSession();
                    //FillChalanTypDDL();
                    //LoadChartofAccounts("");
                    //if (ddlDestination.Items.Count > 0)//&& ddlOrigin.Items.Count > 0 && ddlChalanTyp.Items.Count > 0 && 
                    //{
                    //    ClearAllCtl();
                    //}
                    //txtChalanSearch.Focus();
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
            txtDistance.Text = "0.00";
            txtRent.Text = "0.00";


        }
        private bool isMandatoryFieldValidate()
        {
            if (String.IsNullOrEmpty(txtLocationName.Text))
                return false;
            else
                return true;
        }

        private DataTable LoadLocation(string searchKey)
        {
            try
            {
                DataTable dt;


                using (LocationGateway gatwayObj = new LocationGateway())
                {

                    dt = gatwayObj.GetAllLocation();

                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<Int32>("LocSLNO").Equals(searchKey)
           || r.Field<String>("LocName").ToUpper().Contains(searchKey.ToUpper()));

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



        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadLocation(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadLocation(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (isMandatoryFieldValidate())
            {

                
                string locSLNo = lblLocSlNo.Text;
                string locName  = txtLocationName.Text;
                string locDistict = txtDistict.Text;
                decimal locDistance = 0;
                if (String.IsNullOrEmpty(txtDistance.Text))
                    locDistance = 0;
                else

                locDistance = Convert.ToDecimal(txtDistance.Text.Trim());

                decimal locRent = 0;
                if (String.IsNullOrEmpty(txtRent.Text))
                    locRent = 0;
                else
                    locRent =Convert.ToDecimal(txtRent.Text.Trim());
               

                using (LocationGateway gatwayObj = new LocationGateway())
                {
                    string locSLNO = gatwayObj.InsertUpdateLocation(locSLNo,locName,locDistict,locDistance,locRent);
                    if (String.IsNullOrEmpty(locSLNO) == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('record Saved');", true);
                        lblLocSlNo.Text = locSLNO;
                    }
                    else
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('record not saved');", true);

                }
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Mandatory Field');", true);
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearAll();
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string code = rows.Cells[1].Text;
                DataTable dt = new DataTable();

                using (LocationGateway gatwayObj = new LocationGateway())
                {
                    dt = gatwayObj.GetLocationByLocId(1, code);
                }
                //LocSLNO, LocName, District, LocDistance, LocRent
                lblLocSlNo.Text = dt.Rows[0]["LocSLNO"].ToString();
                txtLocationName.Text = dt.Rows[0]["LocName"].ToString();
                txtDistict.Text = dt.Rows[0]["District"].ToString();
                txtDistance.Text = dt.Rows[0]["LocDistance"].ToString();
                txtRent.Text = dt.Rows[0]["LocRent"].ToString();
               

                hfShowList_ModalPopupExtender.Hide();
                txtSearch.Text = String.Empty;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadLocation(txtSearch.Text);

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