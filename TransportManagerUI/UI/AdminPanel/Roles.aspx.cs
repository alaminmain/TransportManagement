using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using TransportManagerLibrary.UtilityClass;
using System.IO;

namespace TransportManagerUI.UI
{
    public partial class Roles : System.Web.UI.Page
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
           
        }
        
        private DataTable LoadChartofAccounts(string searchKey)
        {
            try
            {
                DataTable dt;


                using (ChartOfAccountGateway gatwayObj = new ChartOfAccountGateway())
                {
                    
                    dt = gatwayObj.Get_All_ChartOfACC();
                   
                    if (String.IsNullOrEmpty(searchKey))
                    {
                        
                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("AccountCode").ToUpper().Contains(searchKey.ToUpper())
           ||   r.Field<String>("AccountDesc").ToUpper().Contains(searchKey.ToUpper()));

                        dt= filtered.CopyToDataTable();
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
            dt= LoadChartofAccounts(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = LoadChartofAccounts(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int comCode1 = 1;
            string accountCode2 = lblAccountCode.Text;
            string accountDesc3 = txtAccountDesc.Text;
            string remarks = txtRemarks.Text;
           
            string userId = Session["UserName"].ToString();

            using (ChartOfAccountGateway gatwayObj = new ChartOfAccountGateway())
            {
                string accountCode = gatwayObj.InsertUpdateChartOfAccount(comCode1, accountCode2, accountDesc3, remarks,  userId);
                lblAccountCode.Text = accountCode;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('record added');", true);
            }
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearAll();
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string code = rows.Cells[1].Text;
                DataTable dt = new DataTable();

                using (ChartOfAccountGateway gatwayObj = new ChartOfAccountGateway())
                {
                    dt = gatwayObj.Get_ChartOfACC(1, code);
                }
                //ComCode, TCNo, TCDate, StoreCode, DealerId, CustId, Paymode, Remarks, TCStatus
                lblAccountCode.Text = dt.Rows[0]["AccountCode"].ToString();
                txtAccountDesc.Text =dt.Rows[0]["AccountDesc"].ToString();
                txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
               
               
                hfShowList_ModalPopupExtender.Hide();
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
            dt = LoadChartofAccounts(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }
    }
}