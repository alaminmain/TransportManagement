using MadinaWorkshop.DLL.Gateway;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerUI.UI.Workshop
{
    public partial class WorkshopProduct : System.Web.UI.Page
    {

        #region Private Method
        private DataTable GetAll(string searchKey)
        {
            try
            {
                DataTable dt = null;
                //DataTable dt2;
                string sql = @"select IT.ProductCode,IT.StoreCode,IT.ProductName,IT.ProductBName,IT.ProductDiscription from Item IT ";

                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];

                if (String.IsNullOrEmpty(searchKey))
                {

                }
                else
                {
                    var filtered = dt.AsEnumerable()
.Where(r => r.Field<String>("ProductCode").Contains(searchKey) || r.Field<String>("ProductName").ToUpper().Contains(searchKey.ToUpper()));
                    dt = filtered.CopyToDataTable();

                }
                return dt;


            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }
        private void saveProduct()
        {
            try
            {

                string CateCode = ddlProductCategoryID.SelectedValue;
                string ProdSubCatCode = ddlProductSubCategoryID.SelectedValue;
                string ProductCode;

                if (string.IsNullOrEmpty(lblProductCode.Text.Trim()))
                {
                    
                    ProductCode = Product_maxID();
                }
                else
                {
                    ProductCode = lblProductCode.Text.Trim();
                }
                string StoreCode = "15" + '-' + CateCode + '-' + ProdSubCatCode + '-' + ProductCode;
                string ProductName = txtProductName.Text.Trim();
                string ProductBName = txtProductBName.Text.Trim();
                string ProductDiscription;
                if (string.IsNullOrEmpty(txtProductDiscription.Text))
                {
                    ProductDiscription = "";
                }
                else
                {
                    ProductDiscription = txtProductDiscription.Text.Trim();
                }
                int Reorder;
                if (string.IsNullOrEmpty(txtReorder.Text.Trim()))
                {
                    Reorder = 0;
                }
                else
                {
                    Reorder = Convert.ToInt32(txtReorder.Text.Trim());
                }
                string UnitType = ddlUniteType.Text.Trim();
                decimal DistPrice;
                if (string.IsNullOrEmpty(txtDistPrice.Text.Trim()))
                {
                    DistPrice = 0;

                }
                else
                {
                    DistPrice = Convert.ToDecimal(txtDistPrice.Text.Trim());
                }
                decimal DealPrice;
                if (string.IsNullOrEmpty(txtDealerPrice.Text.Trim()))
                {
                    DealPrice = 0;
                }
                else
                {
                    DealPrice = Convert.ToDecimal(txtDealerPrice.Text.Trim());
                }
                decimal SalePrice;
                if (string.IsNullOrEmpty(txtSalePrice.Text.Trim()))
                {
                    SalePrice = 0;
                }
                else
                {
                    SalePrice = Convert.ToDecimal(txtSalePrice.Text.Trim());
                }
                string userId = "";
                int rowEffected = new ProductGateway().InsertProduct(CateCode, ProdSubCatCode, ProductCode, StoreCode, ProductName, ProductBName, ProductDiscription, Reorder, UnitType, DistPrice, DealPrice, SalePrice,userId);
                if (rowEffected != 0)
                {
                    lblProductCode.Text = ProductCode;
                    txtStoreCode.Text = StoreCode;
                    lblMsg.Text = "Saved";
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        private void SaveItemStock()
        {
            int ComCode = 1;
            //string StoreCode = txtConsultantCode.Text + '-' + txtCategoryCode.Text + '-' + txtSubCategoryCode.Text + '-' + txtSTProductCode.Text;
            string StoreCode = txtStoreCode.Text;
            string ProductCode = lblProductCode.Text;
            decimal PhyStock = Convert.ToDecimal(txtPhyStock.Text.Trim());
            decimal ReqQTY = 0;
            int rowEffected = new ItemStockGateway().InsertItemStock(ComCode, StoreCode, ProductCode, PhyStock, ReqQTY);

            if (rowEffected != 0)
            {
                lblMsg.Text = "Saved";
            }
        }
        private void Load_ddlCategory()
        {
            string sql = @"select CateCode,CateName from Item_Category;";
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlProductCategoryID.DataSource = dt;
                ddlProductCategoryID.DataTextField = "CateName";
                ddlProductCategoryID.DataValueField = "CateCode";
                ddlProductCategoryID.DataBind();
            }

        }
        private void Load_ddlProdSubCategory()
        {
            string sql = @"select CateCode,ProdSubCatCode,ProdSubCatName from ItemSubCategory where CateCode=@CateCode";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={
                                              new SqlParameter("@CateCode", ddlProductCategoryID.SelectedValue),

                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

            if (dt.Rows.Count > 0)
            {
                ddlProductSubCategoryID.DataSource = dt;
                ddlProductSubCategoryID.DataTextField = "ProdSubCatName";
                ddlProductSubCategoryID.DataValueField = "ProdSubCatCode";
                ddlProductSubCategoryID.DataBind();
            }

        }
        private string Product_maxID()
        {
            String sql = @"select ISNULL(Max(Convert(integer,Right(ProductCode,5))), 0) + 1 from Item where CateCode=@CateCode and ProdSubCatCode=@ProdSubCatCode";
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={
                                              new SqlParameter("@CateCode", ddlProductCategoryID.SelectedValue),
                                             new SqlParameter("@ProdSubCatCode", ddlProductSubCategoryID.SelectedValue)

                                            };
            dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

            
            int count = int.Parse(dt.Rows[0][0].ToString());
            string NewZoonID = count.ToString().PadLeft(5, '0');

            return NewZoonID;

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
                    Load_ddlCategory();
                    Load_ddlProdSubCategory();
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
            dt = GetAll(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveProduct();
            if (chkAddStoreInfo.Checked)
            {
                SaveItemStock();
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetAll(txtSearch.Text);
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
            hfShowList_ModalPopupExtender.Show();
        }

        protected void gvlistofBasicData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                string sql = @"select IT.CateCode,IT.ProdSubCatCode,IT.ProductCode,IT.StoreCode,IT.ProductName,IT.ProductBName,IT.ProductDiscription,IT.Reorder,IT.UnitType,IT.DistPrice,IT.DealPrice, IT.SalePrice,ITS.PhyStock from Item IT right outer join ItemStock ITS on IT.ProductCode=ITS.ProductCode and IT.StoreCode=ITS.StoreCode where IT.StoreCode=@StoreCode";
                GridViewRow rows = gvlistofBasicData.SelectedRow;
                string code = rows.Cells[2].Text;

                DataTable dt = new DataTable();

                SqlParameter[] parameter ={
                                              new SqlParameter("@StoreCode", code),

                                            };
                dt = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, sql, parameter).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ddlProductCategoryID.SelectedValue = dt.Rows[0]["CateCode"].ToString();
                    ddlProductSubCategoryID.SelectedValue = dt.Rows[0]["ProdSubCatCode"].ToString();
                    lblProductCode.Text = dt.Rows[0]["ProductCode"].ToString();
                    txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                    txtProductBName.Text = dt.Rows[0]["ProductBName"].ToString();
                    txtProductDiscription.Text = dt.Rows[0]["ProductDiscription"].ToString();
                    txtReorder.Text = dt.Rows[0]["Reorder"].ToString();
                    txtDistPrice.Text = dt.Rows[0]["DistPrice"].ToString();
                    txtDealerPrice.Text = dt.Rows[0]["DealPrice"].ToString();
                    txtSalePrice.Text = dt.Rows[0]["SalePrice"].ToString();
                    ddlUniteType.SelectedValue = dt.Rows[0]["UnitType"].ToString();
                    txtStoreCode.Text = dt.Rows[0]["StoreCode"].ToString();

                    txtPhyStock.Text = dt.Rows[0]["PhyStock"].ToString();
                }
                else
                    clearAll();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    

    protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt = new DataTable();
        dt = GetAll(txtSearch.Text);

        gvlistofBasicData.PageIndex = e.NewPageIndex;
        gvlistofBasicData.DataSource = dt;
        gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        hfShowList_ModalPopupExtender.Show();
    }

        protected void ddlProductCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_ddlProdSubCategory();
        }
    }
}