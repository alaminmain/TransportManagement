using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class OilReqGateway:IDisposable
    {
        //CommandText = "SELECT  OilSlipSLNo, Date, MovementNo, SupplierName, OilQty, Rate, OilType "+ "FROM         OilRequsition";
        public DataTable get_Oid_Req_Detail()
        {
            string selectSql = "SELECT  OilSlipSLNo, Date, MovementNo, SupplierName, OilQty, Rate, OilType "+ "FROM OilRequsition";
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


        public int InsertUpdateTransOil(int ComCode_1, string FuelSlipSLNo_2, DateTime Date_3, string TripNo_4, string SupplierName_5, 
            decimal FuelQty_6, decimal Rate_7,string FuelCode_8,string Remarks_10,decimal Km, bool Additional, byte Status, string userId)
        {

            using (var conn = new SqlConnection())
            {
                SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "usp_Oil_Req_Details";
   //          @ComCode_1,
   // @FuelSlipSLNo_2,
   // @Date_3,
   // @TripNo_4 ,
   // @SupplierName_5 ,
   // @FuelQty_6,
   //@Rate_7,
   // @FuelCode_8,
   // GETDATE(),
   //@UserCode_9 
 //           @Additional bit,
 //@Status bit
            commandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt).Value = ComCode_1;
            commandObj.Parameters.Add("@FuelSlipSLNo_2", SqlDbType.VarChar, 20).Value = FuelSlipSLNo_2;
            commandObj.Parameters.Add("@Date_3", SqlDbType.DateTime).Value = Date_3.Date;
            commandObj.Parameters.Add("@TripNo_4", SqlDbType.VarChar, 50).Value = TripNo_4;
            commandObj.Parameters.Add("@SupplierName_5", SqlDbType.VarChar, 50).Value = SupplierName_5;
           
            commandObj.Parameters.Add("@FuelQty_6", SqlDbType.Decimal).Value = FuelQty_6;
            commandObj.Parameters.Add("@Rate_7", SqlDbType.Decimal).Value = Rate_7;
            commandObj.Parameters.Add("@FuelCode_8", SqlDbType.VarChar, 20).Value = FuelCode_8;
            commandObj.Parameters.Add("@UserCode_9", SqlDbType.VarChar, 20).Value = CommonGateway.UserCode;
            commandObj.Parameters.Add("@Remarks_10", SqlDbType.VarChar, 100).Value = Remarks_10;
            commandObj.Parameters.Add("@km", SqlDbType.Decimal).Value = Km;
            commandObj.Parameters.Add("@Additional", SqlDbType.Bit).Value = Additional;
            commandObj.Parameters.Add("@Status", SqlDbType.SmallInt).Value = Status;
            commandObj.Parameters.Add("@UserId", SqlDbType.SmallInt).Value = userId;

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
