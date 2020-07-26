using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class StoreLocation:IDisposable
    {
        public DataTable GetAllStoreLocation(int comcode)
        {
            string selectSql = "SELECT ComCode, StoreCode, StoreName, Add1+' ,'+Add2+' ,'+Add3 as Address, PostCode, Phone, Fax, Mobile, Email, WebAddress, StoreStatus FROM StoreLocation where ComCode=@ComCode";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                             
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

        public DataTable GetStoreLocation(int comcode,int storeCode)
        {
            string selectSql = "SELECT ComCode, StoreCode, StoreName, Add1, Add2, Add3, PostCode, Phone, Fax, Mobile, Email, WebAddress, StoreStatus FROM StoreLocation where storeCode=@StoreCode and ComCode=@ComCode ";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@StoreCode", storeCode)
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
        public string InsertUpdateStoreLocation(int ComCode,string StoreCode, string StoreName, string Add1, string Add2, string Add3,string PostCode, string Phone,
            string Fax, string Mobile, string Email, string WebAddress, int StoreStatus, string userId)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                int storeCode;

                if (String.IsNullOrEmpty(StoreCode))
                    {
                        storeCode = GetStoreCode();
                    }
                    else
                    storeCode = Convert.ToInt32(StoreCode);
              
                /*@ComCode 		[int],
	 @StoreCode 	int,
	 @StoreName 	[varchar](50),
	 @Add1 			[varchar](200),
	 @Add2 			[varchar](200),
	 @Add3 			[varchar](200),
	 @PostCode 		[varchar](30),
	 @Phone 		[varchar](30),
	 @Fax 			[varchar](30),
	 @Mobile 		[varchar](30),
	 @Email 		[varchar](40),
	 @StoreStatus	[tinyint],
	 @WebAddress 	[varchar](100),
	 @User_Code_14 	[varchar](10)*/
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_StoreLocation";
                commandObj.Connection = conn;

                commandObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = ComCode;
                commandObj.Parameters.Add("@StoreCode", SqlDbType.Int).Value = storeCode;
                commandObj.Parameters.Add("@StoreName", SqlDbType.VarChar, 50).Value = StoreName;

                commandObj.Parameters.Add("@Add1", SqlDbType.VarChar, 200).Value = Add1;
                commandObj.Parameters.Add("@Add2", SqlDbType.VarChar, 200).Value = Add2;
                commandObj.Parameters.Add("@Add3", SqlDbType.VarChar, 200).Value = Add3;
                commandObj.Parameters.Add("@PostCode", SqlDbType.VarChar, 20).Value = PostCode;
                commandObj.Parameters.Add("@Phone", SqlDbType.VarChar, 30).Value = Phone;
                commandObj.Parameters.Add("@Fax", SqlDbType.VarChar, 20).Value = Fax;
                commandObj.Parameters.Add("@Mobile", SqlDbType.VarChar, 20).Value = Mobile;
                commandObj.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = Email;
                
                commandObj.Parameters.Add("@StoreStatus", SqlDbType.SmallInt).Value = StoreStatus;
                commandObj.Parameters.Add("@WebAddress", SqlDbType.VarChar, 100).Value = WebAddress;
                commandObj.Parameters.Add("@User_Code_14", SqlDbType.VarChar, 20).Value = userId;
                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return storeCode.ToString();
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

        private int GetStoreCode()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = 
            CG.GetMaxNo("StoreLocation", "StoreCode") + 1;
            //string CustId = String.Format("{0}", maxId.ToString("000"));
            return maxId;
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
