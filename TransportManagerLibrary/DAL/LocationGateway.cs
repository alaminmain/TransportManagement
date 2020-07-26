using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class LocationGateway:IDisposable
    {
        /// <summary>
        /// Get All Location
        /// </summary>
        /// <returns>A Data Table</returns>
        public DataTable Get_All_Location()
        {
            string selectSql = "";
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


        public int InsertUpdateLocation(string LocationNo, string LocName,decimal LocDistance,string userId)
        {
            //   @LocSLNo int,
            //@LocName varchar(20),
            //@LocDistance decimal(18,0)
            using (var conn = new SqlConnection())
            {
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Location_Info";


                commandObj.Parameters.Add("@LocSLNo", SqlDbType.Int).Value = LocationNo;
                commandObj.Parameters.Add("@LocName", SqlDbType.VarChar, 50).Value = LocName;
                commandObj.Parameters.Add("@LocDistance", SqlDbType.Decimal).Value = LocDistance;
                commandObj.Parameters.Add("@userId", SqlDbType.Decimal).Value = userId;
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
