using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class VehicleAgentGateway:IDisposable
    {
        public DataTable GetAllAgent()
        {
            string selectSql = "SELECT     ComCode, AgentID, AgentName, ContactName, Add1, Add2, Add3, PostCode, Phone, Fax, Mobile, Email, WebAddress, Remarks, Status FROM         VehicleAgent";
            try
            {
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql);

                if (dataSet != null && dataSet.Tables.Count > 0)
                    return dataSet.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                return (DataTable)null;
            }
            
        }
        public DataTable GetAgentByAgentId(int comCode, string AgentID)
        {
            string selectSql = "SELECT     ComCode, AgentID, AgentName, ContactName, Add1, Add2, Add3, PostCode, Phone, Fax, Mobile, Email, WebAddress, Remarks, Status FROM         VehicleAgent where comcode=@ComCode and AgentID=@AgentID ";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@AgentID",AgentID)
                                          };
                DataSet dataSet = SqlHelper.ExecuteDataset(StringUtility.GetAppConnectionString(), CommandType.Text, selectSql, parameter);

                if (dataSet != null && dataSet.Tables.Count > 0)
                    return dataSet.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), new object[0]);
                return (DataTable)null;
            }
            
        }
        #region InsertUpdate

        public string InsertUpdateVehicleAgent(int ComCode, string AgentId, string AgentName,string ContactName, string Add1, string Add2, string Add3, string PostCode, string Phone,
           string Fax, string Mobile, string Email,int Status, string WebAddress, string Remarks, string userId)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                string agentid;

                if (String.IsNullOrEmpty(AgentId))
                {
                    agentid = GetAgentCode();
                }
                else
                    agentid = AgentId;

     //           @ComCode 		[int],
     //@AgentID 		[varchar](20),
     //@AgentName 	[varchar](50),
     //@ContactName 	[varchar](50),
     //@Add1 			[varchar](200),
     //@Add2 			[varchar](200),
     //@Add3 			[varchar](200),
     //@PostCode 		[varchar](30),
     //@Phone 		[varchar](30),
     //@Fax 			[varchar](30),
     //@Mobile 		[varchar](30),
     //@Email 		[varchar](40),
     //@Status		[smallint],
     //@WebAddress 	[varchar](100),
     //@Remarks 		[varchar](100),
     //@User_Code_14 	[varchar](10)
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_VehicleAgent";
                commandObj.Connection = conn;

                commandObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = ComCode;
                commandObj.Parameters.Add("@AgentID", SqlDbType.VarChar).Value = agentid;
                commandObj.Parameters.Add("@AgentName", SqlDbType.VarChar, 50).Value = AgentName;
                commandObj.Parameters.Add("@ContactName", SqlDbType.VarChar, 50).Value = ContactName;

                commandObj.Parameters.Add("@Add1", SqlDbType.VarChar, 200).Value = Add1;
                commandObj.Parameters.Add("@Add2", SqlDbType.VarChar, 200).Value = Add2;
                commandObj.Parameters.Add("@Add3", SqlDbType.VarChar, 200).Value = Add3;
                commandObj.Parameters.Add("@PostCode", SqlDbType.VarChar, 20).Value = PostCode;
                commandObj.Parameters.Add("@Phone", SqlDbType.VarChar, 30).Value = Phone;
                commandObj.Parameters.Add("@Fax", SqlDbType.VarChar, 20).Value = Fax;
                commandObj.Parameters.Add("@Mobile", SqlDbType.VarChar, 20).Value = Mobile;
                commandObj.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = Email;

                commandObj.Parameters.Add("@Status", SqlDbType.SmallInt).Value = Status;
                commandObj.Parameters.Add("@WebAddress", SqlDbType.VarChar, 100).Value = WebAddress;
                commandObj.Parameters.Add("@Remarks", SqlDbType.VarChar, 100).Value = Remarks;
                commandObj.Parameters.Add("@User_Code_14", SqlDbType.VarChar, 20).Value = userId;
                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return agentid.ToString();
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

        

        private string GetAgentCode()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("VehicleAgent", "Convert(Integer, Right(AgentID,6))") + 1;
            string agentCode = String.Format("{0}", maxId.ToString("000000"));

            return agentCode;
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
