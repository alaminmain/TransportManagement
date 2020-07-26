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
    public partial class ProductInfo : System.Web.UI.Page
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
        private DataTable loadAllProductInfo(string searchKey)
        {
            try
            {
                DataTable dt;


                using (ProductGateway gatwayObj = new ProductGateway())
                {

                    dt = gatwayObj.GetAllProduct();

                    if (String.IsNullOrEmpty(searchKey))
                    {


                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("ProductCode").ToUpper().Contains(searchKey.ToUpper())
           || r.Field<String>("ProductName").ToUpper().Contains(searchKey.ToUpper()));
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

        private void GetCategory()
        {
            try
            {
                DataTable dt;


                using (ProductGateway gatwayObj = new ProductGateway())
                {


                    dt = gatwayObj.GetAllProductCategory();
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataValueField = "CateCode";
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataBind();

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);

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
            if (String.IsNullOrEmpty(txtProductName.Text))
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
                //lblMessage.Text = string.Empty;
                if (!IsPostBack && !IsCallback)
                {
                    GetCategory();
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

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnShowList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllProductInfo(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isMandatoryFieldValidate())
                {
                    string CateCode = ddlCategory.SelectedValue;
                    string ProductCode = lblProductCode.Text;
                    string ProductName = txtProductName.Text;
                    string ProductName1 = txtShortName.Text;
                    string ProductDescription = txtProductDescription.Text;
                    int Reorder = Convert.ToInt32(txtReorder.Text);
                    string UnitType = ddlUnitType.SelectedItem.Text;
                    decimal DistPrice = Convert.ToDecimal(txtDistributorRate.Text);
                    decimal DealPrice = Convert.ToDecimal(txtDealPrice.Text);
                    decimal SalePrice = Convert.ToDecimal(txtSalesRate.Text);
                    
                    bool Discontinued = false;
                    if (chkDiscontinued.Checked)
                        Discontinued = true;
                    string userCode= Session["UserName"].ToString();
                    

                    using (ProductGateway gatwayObj = new ProductGateway())
                    {
                        string Productcode = gatwayObj.InsertUpdateProduct(CateCode,ProductCode,ProductName,ProductName1,ProductDescription
                            ,Reorder,UnitType,DistPrice,DealPrice,SalePrice,Discontinued,userCode);
                        lblProductCode.Text = Productcode;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Record Saved');", true);
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Required Mandatory Field');", true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllProductInfo(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllProductInfo(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearAll();
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string code = rows.Cells[1].Text;
                DataTable dt = new DataTable();

                using (ProductGateway gatwayObj = new ProductGateway())
                {
                    dt = gatwayObj.GetProductById(code);
                }
                //CateCode, ProdSubCatCode, ProductCode, ProductName, ProductName1, ProductDescription, Reorder, 
                //UnitType, DistPrice, DealPrice, SalePrice, LandingCost, PriceInDollar, Discontinued FROM Product
                lblProductCode.Text = dt.Rows[0]["ProductCode"].ToString();
                txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                txtShortName.Text = dt.Rows[0]["ProductName1"].ToString();
                txtProductDescription.Text = dt.Rows[0]["ProductDescription"].ToString();
                txtReorder.Text = dt.Rows[0]["Reorder"].ToString();
                txtDistributorRate.Text = dt.Rows[0]["DistPrice"].ToString();
                txtDealPrice.Text = dt.Rows[0]["DealPrice"].ToString();
                txtSalesRate.Text = dt.Rows[0]["SalePrice"].ToString();
              

                ddlCategory.SelectedValue = dt.Rows[0]["CateCode"].ToString();
                ddlUnitType.SelectedItem.Text= dt.Rows[0]["UnitType"].ToString();

                if (dt.Rows[0]["Discontinued"].ToString() == "True")
                    chkDiscontinued.Checked = true;
                else
                    chkDiscontinued.Checked = false;

              

                hfShowList_ModalPopupExtender.Hide();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
    }
}