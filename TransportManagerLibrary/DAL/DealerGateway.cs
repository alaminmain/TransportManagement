using System.Data;
using System.Data.SqlClient;
using System;
using Microsoft.ApplicationBlocks.Data;
using TransportManagerLibrary.UtilityClass;
using TransportManagerLibrary.DAO;

namespace TransportManagerLibrary.DAL
{
    public class DealerGateway:IDisposable
    {
        /// <summary>
        /// Get All Customer 
        /// </summary>
        /// <returns></returns>
        public  DataTable Get_All_Customer()
        {
            string selectSql = "SELECT        CustId, CustName, CustNameBangla, CustAddressBang, Mobile, LocDistance"
+" FROM Customer WHERE(DealerId IS not NULL) AND(ComCode = 1)";
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

        public DataTable GetCustomerById(int comcode,string custId)
        {
            string selectSql = "SELECT        Customer.ComCode, Customer.CustId, Customer.CustName, Customer.CustNameBangla, Customer.CustAddressBang, Customer.DealerId, Customer_1.CustName AS DealerName, Customer.ContactPer, "
                         +" Customer.Designation, Customer.CustType, Customer.AgreeDate, Customer.Add1, Customer.Add2, Customer.Add3, Customer.Phone, Customer.Mobile, Customer.Email, Customer.FAX, Customer.Location, "
                         + " Customer.LocDistance,Customer.Status FROM    Customer LEFT OUTER JOIN   Customer AS Customer_1 ON Customer.DealerId = Customer_1.CustId "
                         + " WHERE(Customer.ComCode = @ComCode) AND(Customer.CustId = @CustId)";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@CustId", custId)
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

        public DataTable Get_All_Dealer()
        {
            string selectSql = "SELECT        CustId, CustName, CustNameBangla, CustAddressBang, Mobile, LocDistance"
+ " FROM Customer WHERE(DealerId IS NULL) AND(ComCode = 1)";
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

        public DataTable Get_All_CustomerByDealerId(string DealerId)
        {
            string selectSql = "SELECT        CustId, CustName, CustNameBangla, CustAddressBang, Mobile, LocDistance"
+ " FROM Customer WHERE DealerId=@DealerId AND(ComCode = 1)";
            try
            {
                SqlParameter parameter = new SqlParameter("@DealerId", DealerId);
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

        

        public string InsertUpdateCustomer(int ComCode, string CustId, string CustName, string CustNameBangla, string CustAddressBang, string DealerId, string CustType, string ContactPer, string Add1, string Add2, string Add3,
        string Phone, string Mobile, string Location, int locDistance, int status, string userId)
        {
           
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                string custId;
                if(String.IsNullOrEmpty(DealerId))
                {
                    if (String.IsNullOrEmpty(CustId))
                    {
                        custId = GetDealerId();
                    }
                    else
                        custId = CustId;
                }
                else
                {
                    if (String.IsNullOrEmpty(CustId))
                    {
                        custId = GetCustId();
                    }
                    else
                        custId = CustId;
                }

                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Customer";
                commandObj.Connection = conn;

                commandObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = ComCode;
                commandObj.Parameters.Add("@CustId", SqlDbType.VarChar, 20).Value = custId;
                commandObj.Parameters.Add("@CustName", SqlDbType.VarChar, 50).Value = CustName;

                commandObj.Parameters.Add("@CustNameBangla", SqlDbType.NChar).Value = CustNameBangla;
                commandObj.Parameters.Add("@CustAddressBang", SqlDbType.NChar).Value = CustAddressBang;
                if (string.IsNullOrEmpty(DealerId))
                {
                    commandObj.Parameters.Add("@DealerId", SqlDbType.VarChar, 100).Value = DBNull.Value;
                }
                else
                {
                    commandObj.Parameters.Add("@DealerId", SqlDbType.VarChar, 100).Value = DealerId;
                }
                commandObj.Parameters.Add("@CustType", SqlDbType.VarChar, 20).Value = CustType;
                commandObj.Parameters.Add("@ContactPer", SqlDbType.VarChar, 50).Value = ContactPer;
                commandObj.Parameters.Add("@Add1", SqlDbType.VarChar, 50).Value = Add1;
                commandObj.Parameters.Add("@Add2", SqlDbType.VarChar, 50).Value = Add2;
                commandObj.Parameters.Add("@Add3", SqlDbType.VarChar, 50).Value = Add3;
                commandObj.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = Phone;
                commandObj.Parameters.Add("@Mobile", SqlDbType.VarChar, 20).Value = Mobile;
                commandObj.Parameters.Add("@Location", SqlDbType.VarChar, 50).Value = Location;
                commandObj.Parameters.Add("@LocDistance", SqlDbType.Int).Value = locDistance;
                commandObj.Parameters.Add("@status", SqlDbType.SmallInt).Value = status;
                commandObj.Parameters.Add("@User_Code", SqlDbType.VarChar, 20).Value = userId;
                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return custId;
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

        private string GetCustId()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("Customer", "Convert(Integer, Right(CustId,6))") + 1;
            string CustId = String.Format("CI{0}", maxId.ToString("000000"));
            return CustId;
        }

        private string GetDealerId()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("Customer", "Convert(Integer, Right(CustId,6))") + 1;
            string CustId = String.Format("DI{0}", maxId.ToString("000000"));

            return CustId;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
