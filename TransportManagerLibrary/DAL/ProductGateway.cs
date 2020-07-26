using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class ProductGateway : IDisposable
    {


        public DataTable GetAllProduct()
        {
            string selectSql = "Select CateCode, ProdSubCatCode, ProductCode, ProductName, ProductName1, ProductDescription, Reorder, UnitType, DistPrice, DealPrice, SalePrice, LandingCost, PriceInDollar, Discontinued FROM Product";
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql);

                if (dataSet != null && dataSet.Tables.Count > 0)
                    return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return (DataTable)null;
        }

        public DataTable GetProductById(string productCode)
        {
            string selectSql = "SELECT CateCode, ProdSubCatCode, ProductCode, ProductName, ProductName1, ProductDescription, Reorder, UnitType, DistPrice, DealPrice, SalePrice, LandingCost, PriceInDollar, Discontinued FROM Product where ProductCode=@Productcode";
            try
            {
                SqlParameter parameter = new SqlParameter("productCode", productCode);
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql,parameter);

                if (dataSet != null && dataSet.Tables.Count > 0)
                    return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return (DataTable)null;
        }

        public DataTable GetAllProductCategory()
        {
            string selectSql = "SELECT     CateCode, CategoryName, Remarks FROM         Prod_Category";
            try
            {
               
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql);

                if (dataSet != null && dataSet.Tables.Count > 0)
                    return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                throw;
            }
            return (DataTable)null;
        }


        public string InsertUpdateProduct(string CateCode, string ProductCode, string ProductName, string ProductName1, string ProductDescription, int Reorder,
            string UnitType, decimal DistPrice, decimal DealPrice, decimal SalePrice,
                     bool Discontinued, string userCode)

        {

            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {

                
                if (String.IsNullOrEmpty(ProductCode))
                    ProductCode = GetProductCode(CateCode);

              
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Product";
                commandObj.Connection = conn;

                commandObj.Parameters.Add("@CateCode", SqlDbType.SmallInt).Value = CateCode;
                commandObj.Parameters.Add("@ProductCode", SqlDbType.VarChar, 20).Value = ProductCode;

                commandObj.Parameters.Add("@ProductName", SqlDbType.VarChar, 100).Value = ProductName;
                commandObj.Parameters.Add("@ProductName1", SqlDbType.VarChar, 100).Value = ProductName1;
                commandObj.Parameters.Add("@ProductDescription", SqlDbType.VarChar, 200).Value = ProductDescription;
                commandObj.Parameters.Add("@Reorder", SqlDbType.Int, 20).Value = Reorder;

                commandObj.Parameters.Add("@UnitType", SqlDbType.VarChar, 20).Value = UnitType;
                commandObj.Parameters.Add("@DistPrice", SqlDbType.Money).Value = DistPrice;
                commandObj.Parameters.Add("@DealPrice", SqlDbType.Money, 20).Value = DealPrice;
                commandObj.Parameters.Add("@SalePrice", SqlDbType.Money, 200).Value = SalePrice;
                

                commandObj.Parameters.Add("@Discontinued", SqlDbType.Bit, 20).Value = Discontinued;
                commandObj.Parameters.Add("@User_Code", SqlDbType.VarChar, 20).Value = userCode;

                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return ProductCode;
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError(ex.ToString());
                    throw new ApplicationException("Cannot Inserted!!!", ex);
                    throw;
                }
                finally
                {
                    conn.Close();
                    transaction.Dispose();
                    conn.Dispose();
                }

            }
        }

        private string GetProductCode(string category)
        {
            CommonGateway CG = new CommonGateway();
            string WhereClause = "CateCode="+ category ;
            int maxId = CG.GetMaxNo("Product", "Convert(Integer, Right(ProductCode,2))", WhereClause) + 1;
            string Code = String.Format("{0}{1}",category,maxId.ToString("000"));

            return Code;
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion



    }
}
