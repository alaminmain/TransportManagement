using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class TransContactDetailGateway : IDisposable
    {
        public DataTable GetAllTransContactDetail()
        {
            string selectSql = "SELECT        ComCode, TCNo, TCDSLNo, InvNo, ProductCode, OrderQty, UnitPrice FROM   TransContactDetails";
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

        public DataTable GetAllTransContactDetail(string TransContactId)
        {
            string selectSql = "SELECT  ComCode, TCNo, TCDSLNo, InvNo, ProductCode, OrderQty, UnitPrice as Rent FROM   TransContactDetails where tcno=@TcNO";
            try
            {
                SqlParameter parameter = new SqlParameter("@TcNO", TransContactId);
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

        public DataTable GetTransContactDetail(int comcode,string TransContactId)
        {
            string selectSql = "SELECT        a.ComCode, a.TCNo, a.TCDSLNo, a.InvNo, a.ProductCode, b.ProductName1 as ProductName, a.OrderQty, a.UnitPrice as Rent, a.OrderQty*a.UnitPrice as TotalPrice "
                             + " FROM TransContactDetails AS a INNER JOIN Product AS b ON a.ProductCode = b.ProductCode WHERE ComCode=@ComCode and a.TCNo = @TcNO";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@TcNO", TransContactId)
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


        public int InsertUpdateTransContactDetail(
            int comCode, string tcNo_1, string invNo_2, int TcdSlno_3, string productCode_4, decimal orderQty_5, decimal unitPrice_6) 
           
        {
            using (var conn = new SqlConnection())
            {

                /*
                @ComCode 	[smallint],
	 @TCNo_1 	[varchar](20),
	 @InvNo_2 	[varchar](20),
	 @TCDSLNO_3 int,
	 @ProductCode_4 varchar (10),
	 @OrderQty_5 	[numeric](14,2),
	 @UnitPrice_6 [money])
                 */
                SqlCommand commandObj = new SqlCommand();

                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_TransContactDetails";
                //Add Parameter

                commandObj.Parameters.Add("@ComCode", SqlDbType.Int).Value = comCode;
                commandObj.Parameters.Add("@TCNo_2", SqlDbType.VarChar).Value = tcNo_1;
                commandObj.Parameters.Add("@InvNo_2", SqlDbType.VarChar).Value = invNo_2;
                commandObj.Parameters.Add("@TCDSLNO_3", SqlDbType.Int).Value = TcdSlno_3;
                commandObj.Parameters.Add("@ProductCode_4", SqlDbType.VarChar).Value = productCode_4;
                commandObj.Parameters.Add("@OrderQty_5", SqlDbType.Decimal).Value = orderQty_5;

                commandObj.Parameters.Add("@UnitPrice_6", SqlDbType.Decimal).Value = unitPrice_6;
              
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
