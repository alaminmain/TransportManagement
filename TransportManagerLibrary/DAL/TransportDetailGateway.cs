using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;


namespace TransportManagerLibrary.DAL
{
    public class TransportDetailGateway:IDisposable
    {
        /*commandObj.CommandText = "SELECT     TransportDetails.ProductCode, Product.ProductName, TransportDetails.OrderQty, TransportDetails.UnitPrice"
                                        +" FROM         TransportDetails INNER JOIN Product ON TransportDetails.ProductCode = Product.ProductCode"
+ " WHERE     (MovementNo = @MovementNo)";
            commandObj.Parameters.AddWithValue("@MovementNo", MovementNo);*/
        public DataTable get_Transport_Detail(string MovementNo)
        {
            string selectSql = "SELECT     TransportDetails.ProductCode, Product.ProductName, TransportDetails.OrderQty, TransportDetails.UnitPrice as Rent, TransportDetails.OrderQty*TransportDetails.UnitPrice as TotalPrice "
                                        + " FROM         TransportDetails INNER JOIN Product ON TransportDetails.ProductCode = Product.ProductCode"
+ " WHERE     (MovementNo = @MovementNo)";
            SqlParameter parameter = new SqlParameter("@MovementNo", MovementNo);
            try
            {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TripNo"></param>
        /// <returns></returns>
        public DataTable GetMovmentDetailForVoucherIncome(string TripNo)
        {
            string selectSql = @"SELECT  Transport.TransportDate as Date,Product.ProductName as Product, StoreLocation.StoreName as TripFrom,STUFF(COALESCE(',' + NULLIF(Customer.Add1, ''), '') "
                               + " + COALESCE(',' + NULLIF(Customer.Add2, ''), '') + COALESCE(',' + NULLIF(Customer.Add3, ''), ''), 1, 1, '')  AS TripTo, "
                              + "  (TransportDetails.OrderQty * TransportDetails.UnitPrice) as Rent FROM            TransportDetails INNER JOIN "
                            +" Transport ON TransportDetails.MovementNo = Transport.MovementNo INNER JOIN  Product ON TransportDetails.ProductCode = Product.ProductCode INNER JOIN "
                         +" Customer ON Transport.CustId = Customer.CustId INNER JOIN StoreLocation ON Transport.StoreCode = StoreLocation.StoreCode "
                        +" WHERE(Transport.TripNo = @TripNO)";
            SqlParameter parameter = new SqlParameter("@TripNo", TripNo);
            try
            {
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



        public int InsertUpdateTransProductDetail(int ComCode_1,string MovementNo_2,int TransportDSLNO, string ProductCode, decimal OrderQty,decimal UnitPrice)
        {
            //       @ComCode_1 	[smallint],
            //@TransportSLNo_2 	[varchar](50),
            //@ProductCode varchar (20),
            //@OrderQty 	[numeric](14,2),
            //@UnitPrice [money])
            using (var conn = new SqlConnection())
            {

                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Transport_Details";

                commandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt).Value = ComCode_1;
                commandObj.Parameters.Add("@MovementNo_2", SqlDbType.VarChar, 50).Value = MovementNo_2;
                commandObj.Parameters.Add("@TransportDSLNO", SqlDbType.Int).Value = TransportDSLNO;
                commandObj.Parameters.Add("@ProductCode", SqlDbType.VarChar, 10).Value = ProductCode;
                commandObj.Parameters.Add("@OrderQty", SqlDbType.Decimal).Value = OrderQty;
                commandObj.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = UnitPrice;

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

        public int deleteTransDetail(int ComCode_1, string MovementNo_2)
        {
            using (var conn = new SqlConnection())
            {
                SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "usp_Delete_Transport_Details";

            commandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt).Value = ComCode_1;
            commandObj.Parameters.Add("@MovementNo_2", SqlDbType.VarChar, 20).Value = MovementNo_2;


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

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
