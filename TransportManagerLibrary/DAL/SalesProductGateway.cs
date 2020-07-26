using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class SalesProductGateway : IDisposable
    {
        public DataTable GetAllSalesProduct()
        {
            string selectSql = "SELECT       ComCode, StoreCode, InvNo, SLNo, ProductCode, OrderQty,OrderBalQty, CostPrice, DistPrice, DealPrice, SalePrice, UnitPrice, VAT, Discount FROM SalesProducts";
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

        public DataTable GetAllSalesProductBySales(int ComCode, string InvNo)
        {
            string selectSql = "SELECT       ComCode, StoreCode, InvNo, SLNo, ProductCode, OrderQty,OrderQty Quantity,OrderBalQty, (OrderQty-OrderBalQty) as PendingQty CostPrice, DistPrice, DealPrice, SalePrice, UnitPrice, VAT, Discount FROM SalesProducts where ComCode=@ComCode and Invno=@InvNo";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", ComCode),
                                              new SqlParameter("@InvNo",InvNo)
                                          };
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

        public DataTable GetSalesProductBySales(int ComCode, string InvNo)
        {
            string selectSql = "SELECT        a.ComCode, a.StoreCode, a.InvNo, a.SLNo, a.ProductCode, b.ProductName ProductName, a.OrderQty Quantity,a.OrderBalQty, (a.OrderQty-a.OrderBalQty) as PendingQty, a.UnitPrice UnitPrice, (a.OrderQty*a.UnitPrice) TotalPrice "
               + "FROM SalesProducts AS a INNER JOIN Product AS b ON a.ProductCode = b.ProductCode WHERE(a.InvNo = @InvNo) and a.ComCode=@ComCode";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", ComCode),
                                              new SqlParameter("@InvNo",InvNo)
                                          };
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

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


        public int InsertUpdateSalesProduct(
        int dmlType,int comCode, int storeCode,string invNo_1,int slno_2,string productCode_3,decimal unitPrice_4,decimal orderQty_5,
         decimal discount)
        {
            using (var conn = new SqlConnection())
            {

                SqlCommand commandObj = new SqlCommand();

                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Personal";
                //Add Parameter
                commandObj.Parameters.Add("@DMLType", SqlDbType.Int, 20).Value = dmlType;
                commandObj.Parameters.Add("@ComCode", SqlDbType.Int, 20).Value = comCode;
                commandObj.Parameters.Add("@StoreCode", SqlDbType.Int, 100).Value = storeCode;
                commandObj.Parameters.Add("@InvNo_1", SqlDbType.VarChar).Value = invNo_1;
                commandObj.Parameters.Add("@slNo_2", SqlDbType.Int, 50).Value = slno_2;
                commandObj.Parameters.Add("@ProductCode_3", SqlDbType.VarChar, 20).Value = productCode_3;
                commandObj.Parameters.Add("@UnitPrice_4", SqlDbType.Money, 10).Value = unitPrice_4;
                commandObj.Parameters.Add("@OrderQty_5", SqlDbType.Decimal, 10).Value = orderQty_5;
                commandObj.Parameters.Add("@Discount", SqlDbType.Decimal, 10).Value = discount;
                
                commandObj.Connection = conn;
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                conn.Open();
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    conn.Close();

                    return rowEffected;
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logger.LogError(ex.ToString());
                    //throw new ApplicationException("Cannot Inserted!!!", ex);
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
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
