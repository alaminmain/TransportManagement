using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class TransContactGateway : IDisposable
    {
        public DataTable GetAllTransContact()
        {
            string selectSql = "SELECT      ComCode, TCNo, TCDate, StoreCode, DealerId, CustId, Paymode, Remarks,TotalQty, TCStatus FROM            TransContact";
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

        public DataTable GetAllTransContactForGridView(int comCode)
        {
            string selectSql = "SELECT        a.TCNo, a.TCDate, a.StoreCode, a.DealerId, b.CustName, a.CustId, c.CustName AS CustName1, a.Remarks, a.TCStatus,a.TotalQty, b.Mobile,C.Mobile " +
" FROM TransContact AS a INNER JOIN  Customer AS b ON a.DealerId = b.CustId INNER JOIN  Customer AS c ON a.CustId = c.CustId where a.ComCode=@ComCode Order By a.TCDate DESC";
            try
            {
                SqlParameter parameter = new SqlParameter("@ComCode", comCode);
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
        public DataTable GetAllTransContactWithBalance(int comCode)
        {

            string selectSql = " SELECT        TransContact.TCNo, TransContact.TCDate, TransContact.DealerId, Customer.CustName AS DealerName, TransContact.CustId, Customer_1.CustName,TransContact.TCStatus, CASE WHEN Paymode = 1 THEN 'CNF' ELSE 'FOB' END AS paymentMode,"
                         + " STUFF(COALESCE(', ' + NULLIF(Customer_1.Add1, ' '), ' ') +  COALESCE(', ' + NULLIF(Customer_1.Add2, ' '), ' ') +COALESCE(', ' + NULLIF(Customer_1.Add3, ' '), ' ') ,1, 1, ' ')  AS RetailerAddress, Customer_1.Mobile, Product.ProductName, TransContactDetails.OrderQty, TransContactDetails.InvNo "
                         + " FROM  TransContact INNER JOIN Customer ON TransContact.DealerId = Customer.CustId INNER JOIN "
                         + " Customer AS Customer_1 ON TransContact.CustId = Customer_1.CustId INNER JOIN "
                         + " StoreLocation ON TransContact.StoreCode = StoreLocation.StoreCode INNER JOIN "
                         + " TransContactDetails ON TransContact.TCNo = TransContactDetails.TCNo INNER JOIN "
                         + " Product ON TransContactDetails.ProductCode = Product.ProductCode WHERE(TransContact.ComCode = @ComCode) ORDER BY TransContact.TCNo DESC";

            try
            {
                SqlParameter parameter = new SqlParameter("@ComCode", comCode);
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
        public DataTable GetAllTransContactForGridView(int comCode,string DealerId)
        {
            string selectSql = "SELECT        a.TCNo, a.TCDate, a.StoreCode, a.DealerId, b.CustName, a.CustId, c.CustName AS custName1, a.Remarks, a.TCStatus,a.TotalQty, b.Mobile as DealerMobile,C.Mobile as CustomerMobile " +
" FROM TransContact AS a INNER JOIN  Customer AS b ON a.DealerId = b.CustId INNER JOIN  Customer AS c ON a.CustId = c.CustId where a.ComCode=@ComCode and a.Dealerid=@DealerId";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@DealerId", DealerId)
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

        public DataTable GetAllTransContactByTripNo(int comCode, string tripNo)
        {
            string selectSql = "SELECT        a.TCNo, a.TCDate, a.StoreCode, a.DealerId, b.CustName, a.CustId, c.CustName AS custName1, a.Remarks, a.TCStatus,a.TotalQty, b.Mobile as DealerMobile,C.Mobile as CustomerMobile " +
" FROM TransContact AS a INNER JOIN  Customer AS b ON a.DealerId = b.CustId INNER JOIN  Customer AS c ON a.CustId = c.CustId where a.ComCode=@ComCode  and a.TCNo in (select TCNo from TripDetail where TripNo=@TripNo)";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@TripNo", tripNo)
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



        public DataTable GetAllTransContact(int comcode,string TransContactId)
        {
            string selectSql = "SELECT      TransContact.ComCode, TransContact.TCNo, TransContact.TCDate, TransContact.StoreCode, TransContact.DealerId, TransContact.CustId, "
                      + " TransContact.Paymode, TransContact.Remarks, TransContact.TCStatus, Customer.CustName AS DealerName, Customer_1.CustName AS CustomerName,Customer_1.Mobile"
                       +" FROM         TransContact INNER JOIN Customer ON TransContact.DealerId = Customer.CustId INNER JOIN"
                      + " Customer AS Customer_1 ON TransContact.CustId = Customer_1.CustId where TransContact.ComCode=@ComCode and  TransContact.tcno=@TcNO Order By TransContact.TCDate ";
            try 
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@TcNO", TransContactId)
                                          };
                //SqlParameter parameter = new SqlParameter("@TcNO", TransContactId);
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

        public DataTable GetAllTransContactDetail(int comcode, string TransContactId)
        {
            string selectSql = "SELECT        TransContactDetails.ComCode, TransContactDetails.TCNo, TransContactDetails.TCDSLNo, TransContactDetails.InvNo, TransContactDetails.ProductCode, Product.ProductName, TransContactDetails.OrderQty, "
                         + " TransContactDetails.UnitPrice as Rent,TransContactDetails.OrderQty*TransContactDetails.UnitPrice as TotalAmount, TransContact.DealerId, Customer.CustName AS dealerName, TransContact.CustId, Customer_1.CustName"
                         + " FROM  TransContactDetails INNER JOIN  TransContact ON TransContactDetails.TCNo = TransContact.TCNo INNER JOIN"
                         + " Product ON TransContactDetails.ProductCode = Product.ProductCode INNER JOIN Customer ON TransContact.DealerId = Customer.CustId INNER JOIN"
                         + " Customer AS Customer_1 ON TransContact.CustId = Customer_1.CustId where TransContact.ComCode=@ComCode and  TransContact.tcno=@TcNO ";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@TcNO", TransContactId)
                                          };
                //SqlParameter parameter = new SqlParameter("@TcNO", TransContactId);
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


        public int InsertUpdateTransContact(int comCode_1, string tC_No_2, string tcDate_3, int storeCode_4, string dealerId_5,
            string custId_6, int payMode_7, string Remarks_8, int tcStatus_9, string user_code_10, decimal totalQty_11
          )
        {
            using (var conn = new SqlConnection())
            {

              SqlCommand commandObj = new SqlCommand();

                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_TransContact";
                //Add Parameter
                
                commandObj.Parameters.Add("@ComCode_1", SqlDbType.Int).Value = comCode_1;
                commandObj.Parameters.Add("@TCNo_2", SqlDbType.VarChar).Value = tC_No_2;
                commandObj.Parameters.Add("@TCDate_3", SqlDbType.DateTime).Value = Convert.ToDateTime(tcDate_3);
                commandObj.Parameters.Add("@StoreCode_4", SqlDbType.Int).Value = storeCode_4;
                commandObj.Parameters.Add("@DealerId_5", SqlDbType.VarChar).Value = dealerId_5;
                commandObj.Parameters.Add("@CustId_6", SqlDbType.VarChar).Value = custId_6;

                commandObj.Parameters.Add("@Paymode_7", SqlDbType.Int).Value = payMode_7;
                commandObj.Parameters.Add("@Remarks_8", SqlDbType.VarChar).Value = Remarks_8;

                commandObj.Parameters.Add("@TCStatus_9", SqlDbType.Int).Value = tcStatus_9;

                commandObj.Parameters.Add("@User_Code_10", SqlDbType.VarChar).Value = user_code_10;
                commandObj.Parameters.Add("@TotalQty_11", SqlDbType.Decimal).Value = totalQty_11;
                
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

        public string InsertUpdateTransContact(int comCode_1, string tC_No_2, string tcDate_3, int storeCode_4, string dealerId_5,
            string custId_6, int payMode_7, string Remarks_8, int tcStatus_9, string user_code_10,decimal totalQty_11, DataTable transContactDetail)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                string TcNo;
                if (String.IsNullOrEmpty(tC_No_2))
                    TcNo = NewMaxNo();
                else
                    TcNo = tC_No_2;
                CommonGateway gt = new CommonGateway();
                SqlCommand commandObj = new SqlCommand();

                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_TransContact";
                commandObj.Connection = conn;
                //Add Parameter

                commandObj.Parameters.Add("@ComCode_1", SqlDbType.Int).Value = comCode_1;
                commandObj.Parameters.Add("@TCNo_2", SqlDbType.VarChar).Value = TcNo;
                commandObj.Parameters.Add("@TCDate_3", SqlDbType.DateTime).Value = Convert.ToDateTime(tcDate_3);
                commandObj.Parameters.Add("@StoreCode_4", SqlDbType.Int).Value = storeCode_4;
                commandObj.Parameters.Add("@DealerId_5", SqlDbType.VarChar).Value = dealerId_5;
                commandObj.Parameters.Add("@CustId_6", SqlDbType.VarChar).Value = custId_6;

                commandObj.Parameters.Add("@Paymode_7", SqlDbType.Int).Value = payMode_7;
                commandObj.Parameters.Add("@Remarks_8", SqlDbType.VarChar).Value = Remarks_8;

                commandObj.Parameters.Add("@TCStatus_9", SqlDbType.Int).Value = tcStatus_9;

                commandObj.Parameters.Add("@User_Code_10", SqlDbType.VarChar).Value = user_code_10;
                commandObj.Parameters.Add("@TotalQty_11", SqlDbType.Decimal).Value = totalQty_11;

                //Delete Detail
                SqlCommand DeleteDetailcmd = new SqlCommand();

                DeleteDetailcmd.CommandType = CommandType.Text;
                DeleteDetailcmd.CommandText = "Delete TransContactDetails where comCode=@ComCode and TCNo=@TcNo";
                DeleteDetailcmd.Connection = conn;

                //Detail Table
                SqlCommand commandDetailObj = new SqlCommand();
                commandDetailObj.CommandType = CommandType.StoredProcedure;
                commandDetailObj.CommandText = "usp_TransContactDetails";
                //Add Parameter

                commandDetailObj.Connection = conn;

              
                //Detail Object

                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                commandDetailObj.Transaction = transaction;
                DeleteDetailcmd.Transaction = transaction;
                try
                {
                    int rowEffected = commandObj.ExecuteNonQuery();

                    //Delete Parameter
                    DeleteDetailcmd.Parameters.Add("@ComCode", SqlDbType.Int).Value = comCode_1;
                    DeleteDetailcmd.Parameters.Add("@TcNo", SqlDbType.VarChar).Value = TcNo;
                    DeleteDetailcmd.ExecuteNonQuery();

                    int maxNo = 1;// gt.GetMaxNo("TransContactDetails", "TCDSLNo") +1;

                    commandDetailObj.Parameters.Add(new SqlParameter("@ComCode", SqlDbType.Int));
                    commandDetailObj.Parameters.Add(new SqlParameter("@TCNo_1", SqlDbType.VarChar));
                    commandDetailObj.Parameters.Add(new SqlParameter("@InvNo_2", SqlDbType.VarChar));
                    commandDetailObj.Parameters.Add(new SqlParameter("@TCDSLNO_3", SqlDbType.Int));
                    commandDetailObj.Parameters.Add(new SqlParameter("@ProductCode_4", SqlDbType.VarChar));
                    commandDetailObj.Parameters.Add(new SqlParameter("@OrderQty_5", SqlDbType.Decimal));
                    commandDetailObj.Parameters.Add(new SqlParameter("@UnitPrice_6", SqlDbType.Decimal));


                    foreach (DataRow dr in transContactDetail.Rows)
                    {
                                             
                        //commandDetailObj.Parameters.Clear();
                        commandDetailObj.Parameters[0].Value = comCode_1;
                        commandDetailObj.Parameters[1].Value = TcNo;
                        commandDetailObj.Parameters[2].Value = dr["DoNo"].ToString();
                        commandDetailObj.Parameters[3].Value = maxNo;
                        commandDetailObj.Parameters[4].Value = dr["ProductCode"].ToString();
                        commandDetailObj.Parameters[5].Value = Convert.ToDecimal(dr["OrderQty"]);
                        commandDetailObj.Parameters[6].Value = Convert.ToDecimal(dr["Rent"]);
                        
                        int detailRowEffected = commandDetailObj.ExecuteNonQuery();
                        maxNo++;
                    }
                    transaction.Commit();
                    conn.Close();

                    return TcNo;
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

        private string NewMaxNo()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("TransContact", "Convert(Integer, Right(TCNo,6))", "TCDate", DateTime.Now) + 1;
            string DoNo = String.Format("TC{0}-{1}", DateTime.Now.ToString("yyMM"), maxId.ToString("000000"));

            return DoNo;
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
