using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class MobileNoGateway:IDisposable
    {

        public DataTable LoadMobileNo()
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

        public int InsertUpdateMobileNo(string name, string number,DateTime time,bool isActive)
        {
            //       @ComCode_1 	[smallint],
            //@TransportSLNo_2 	[varchar](50),
            //@ProductCode varchar (20),
            //@OrderQty 	[numeric](14,2),
            //@UnitPrice [money])

            using (var conn = new SqlConnection())
            {
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.Text;
                commandObj.Parameters.Add("@Name", SqlDbType.VarChar, 20).Value = name;
                commandObj.Parameters.Add("@Number", SqlDbType.VarChar, 20).Value = number;
                commandObj.Parameters.Add("@IsActive", SqlDbType.VarChar, 10).Value = isActive.ToString();
                commandObj.Parameters.Add("@Time", SqlDbType.DateTime).Value = time;
                commandObj.CommandText = "Insert into  MobileNo (Name, Mobile, Time, IsActive)values(@Name,@Number,@Time,@IsActive)";



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

        public int deleteMobileDetail()
        {
            using (var conn = new SqlConnection())
            {

                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.Text;
                commandObj.CommandText = "Delete from MobileNo";

                try
                {
                    int rowEffected = commandObj.ExecuteNonQuery();
                    conn.Close();

                    return rowEffected;
                }

                catch (Exception ex)
                {
                   
                    Logger.LogError(ex.ToString());
                    throw new ApplicationException("Cannot Inserted!!!", ex);
                    throw;
                }
                finally
                {
                    conn.Close();
                   
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
