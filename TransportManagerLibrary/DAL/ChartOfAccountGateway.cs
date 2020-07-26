// ReSharper disable RedundantUsingDirective
// ReSharper restore RedundantUsingDirective
using System.Data;
using System.Data.SqlClient;
using System;
using Microsoft.ApplicationBlocks.Data;
using TransportManagerLibrary.UtilityClass;


namespace TransportManagerLibrary.DAL
{
    public class ChartOfAccountGateway:IDisposable
    {
        public DataTable Get_All_ChartOfACC()
        {
            string selectSql = "SELECT [ComCode]  ,[AccountCode],[AccountDesc],[Remarks],[AccCategory] FROM [TransportDB].[dbo].[ChartofAccount]";
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
        public DataTable Get_ChartOfACC(int comCode, string accCode)
        {
            string selectSql = "SELECT [ComCode]  ,[AccountCode],[AccountDesc],[Remarks],[AccCategory] FROM [TransportDB].[dbo].[ChartofAccount] where comcode=@ComCode and AccountCode=@accCode ";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@accCode",accCode)
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
        #region InsertUpdateDelete

        public string InsertUpdateChartOfAccount(int comCode1,string accountCode2, string accountDesc3, string remarks4, string accCategory5 ,string userCode)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                string accCode;
                if (String.IsNullOrEmpty(accountCode2))
                    accCode = GetAccountsCode();
                else
                    accCode = accountCode2;
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Chart_of_Account";
                commandObj.Connection = conn;
                commandObj.Parameters.Add("@comcode_1", SqlDbType.SmallInt).Value = comCode1;
                commandObj.Parameters.Add("@AccountCode_2", SqlDbType.VarChar, 20).Value = accCode;
                commandObj.Parameters.Add("@AccountDesc_3", SqlDbType.VarChar, 100).Value = accountDesc3;
                commandObj.Parameters.Add("@Remarks_4", SqlDbType.VarChar, 100).Value = remarks4;
                commandObj.Parameters.Add("@AccCategory_5", SqlDbType.VarChar, 20).Value = accCategory5;
                commandObj.Parameters.Add("@User_Code_6", SqlDbType.VarChar, 20).Value = userCode;

                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return accCode;
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

        public string GetAccountsCode()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("ChartofAccount", "AccountCode") + 1;
            string Code = String.Format(maxId.ToString("0000"));

            return Code;
        }


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
