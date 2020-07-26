using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class SalesGateway : IDisposable
    {
        public DataTable GetSales()
        {
            string selectSql = "SELECT ComCode, StoreCode, InvNo, InvDate, ChalanNo, ChalanDate, CustId, Paymode, InvAmount,TotalQty,TotalBalQty, Discount, Remarks, InvStatus, EmpCode FROM Sales";
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

        public DataTable GetAllSales()
        {
//            DO NO   SAP DO NO Date    Delivery from   "Party
//Code"	"DEALER
//NAME"	Address	Material Code	"Material
//TYPE"	"ORDER
//QUANTITY"	"SO
//QUANTITY"	"Pending
//QUANTITY"	"Free /
//No free"	DO Price

            string selectSql = "SELECT     a.ComCode, a.StoreCode, a.InvNo,a.DONo, a.InvDate, a.ChalanNo, a.ChalanDate, a.CustId, b.CustName, "
            + " CASE WHEN Paymode = 1 THEN 'CNF' ELSE 'FOB' END AS paymentMode, a.Paymode, a.InvAmount,a.TotalQty,a.TotalBalQty,(a.TotalQty-a.TotalBalQty) as BalQty, a.Discount, a.Remarks, a.InvStatus, a.EmpCode "
             + " FROM         Sales AS a INNER JOIN   Customer AS b ON a.CustId = b.CustId order by a.InvDate desc";
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

        public DataTable GetAllSalesWithAddress()
        {
           

            string selectSql = "SELECT        a.ComCode, a.StoreCode, a.InvNo, a.DONo, a.InvDate, a.ChalanNo, a.ChalanDate, a.CustId, b.CustName, CASE WHEN Paymode = 1 THEN 'CNF' ELSE 'FOB' END AS paymentMode, a.Paymode, a.InvStatus, " 
                         +" SalesProducts.ProductCode, SalesProducts.OrderQty, ABS(SalesProducts.OrderBalQty) AS SOQty, SalesProducts.OrderQty - SalesProducts.OrderBalQty AS PendingQty, SalesProducts.UnitPrice, Product.ProductName, "
                         + " StoreLocation.StoreName,STUFF(COALESCE(', ' + NULLIF(b.Add1, ' '), ' ') +  COALESCE(', ' + NULLIF(b.Add2, ' '), ' ') +COALESCE(', ' + NULLIF(b.Add3, ' '), ' ') ,1, 1, ' ') AS address"
                         +" FROM SalesProducts INNER JOIN"
                         +" Product ON SalesProducts.ProductCode = Product.ProductCode INNER JOIN"
                         +" Sales AS a INNER JOIN Customer AS b ON a.CustId = b.CustId ON SalesProducts.InvNo = a.InvNo INNER JOIN"
                         + " StoreLocation ON a.StoreCode = StoreLocation.StoreCode Where (SalesProducts.OrderQty - SalesProducts.OrderBalQty)>0  ORDER BY a.InvNo DESC";
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

        public DataTable GetAllSalesByDealer(int comcode,string custId)
        {
            string selectSql = "SELECT     a.ComCode, a.StoreCode, a.InvNo, a.InvDate, a.ChalanNo, a.ChalanDate, a.CustId, b.CustName, "
            + " CASE WHEN Paymode = 1 THEN 'CNF' ELSE 'FOB' END AS paymentMode, a.Paymode, a.InvAmount,a.TotalQty,a.TotalBalQty,(a.TotalQty-a.TotalBalQty) as BalQty, a.Discount, a.Remarks, a.InvStatus, a.EmpCode,a.DONo "
             + " FROM         Sales AS a INNER JOIN   Customer AS b ON a.CustId = b.CustId where a.ComCode=@ComCode and a.CustId=@CustId and (a.TotalQty-a.TotalBalQty)>0  order by a.InvDate desc";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@CustId",custId)
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

        public DataTable GetSalesById(int ComCode,string InvNo)
        {
            string selectSql = "SELECT     Sales.ComCode, Sales.StoreCode, Sales.InvNo, Sales.InvDate, Sales.ChalanNo, Sales.ChalanDate, Sales.CustId, Sales.Paymode, Sales.InvAmount, Sales.Discount, "
                      + " Sales.Remarks, Sales.InvStatus, Sales.EmpCode,Sales.DONo, Customer.CustName,Sales.TotalQty,Sales.TotalBalQty,(Sales.TotalQty-Sales.TotalBalQty) as BalQty FROM         Sales INNER JOIN"
                      + " Customer ON Sales.CustId = Customer.CustId  WHERE     (Sales.ComCode = @ComCode) AND (Sales.InvNo = @InvNo)  ";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", ComCode),
                                              new SqlParameter("@InvNo",InvNo)
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
        public int InsertUpdateSales(int dmlType,int ComCode_1, int storeCode,int invNo_2,string invDate_3,
        string chalanNo_4,string chalanDate_5,string custId_6,decimal invAmount_7,decimal discount_8,int payMode_9,
        string remarks_10,int invStatus_11, string userCode_12,string DONo_13)            
           
        {
            using (var conn = new SqlConnection())
            {
                              
                SqlCommand commandObj = new SqlCommand();

                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Sales";
                //Add Parameter
                commandObj.Parameters.Add("@DMLType", SqlDbType.Int, 20).Value = dmlType;
                commandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt, 20).Value = ComCode_1;
                commandObj.Parameters.Add("@StoreCode", SqlDbType.Int, 100).Value = storeCode;

                commandObj.Parameters.Add("@InvNo_2", SqlDbType.VarChar).Value = invNo_2;

                commandObj.Parameters.Add("@InvDate_3", SqlDbType.SmallDateTime, 50).Value = Convert.ToDateTime(invDate_3);
                commandObj.Parameters.Add("@ChalanNo_4", SqlDbType.VarChar, 20).Value = chalanNo_4;

                commandObj.Parameters.Add("@ChalanDate_5", SqlDbType.SmallDateTime, 20).Value = Convert.ToDateTime(chalanDate_5);

                commandObj.Parameters.Add("@InvAmount_7", SqlDbType.Decimal, 10).Value = invAmount_7;
                commandObj.Parameters.Add("@Discount_8", SqlDbType.Decimal, 10).Value = discount_8;
                commandObj.Parameters.Add("@PayMode_9", SqlDbType.Int, 10).Value = payMode_9;

                commandObj.Parameters.Add("@Remarks_10", SqlDbType.VarChar, 20).Value = remarks_10;

                commandObj.Parameters.Add("@InvStatus_11", SqlDbType.TinyInt).Value = invStatus_11;
                
                commandObj.Parameters.Add("@User_Code_12", SqlDbType.VarChar, 20).Value = userCode_12;
                commandObj.Parameters.Add("@DONo_13", SqlDbType.VarChar, 20).Value = DONo_13;

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


      public string InsertUpdateSales(int dmlType, int ComCode_1, int storeCode, string invNo_2, string invDate_3,
      string chalanNo_4, string chalanDate_5, string custId_6, decimal invAmount_7, decimal discount_8, int payMode_9,
      string remarks_10, int invStatus_11, string userCode_12,string DONo_13,decimal totalQty_14, DataTable salesDetail)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                string doNo;
                if (String.IsNullOrEmpty(invNo_2))
                    doNo = NewDONo();
                else
                    doNo = invNo_2;
                    
                SqlCommand commandObj = new SqlCommand();
                CommonGateway gt = new CommonGateway();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Sales";
                commandObj.Connection = conn;

                SqlCommand commandDetailObj = new SqlCommand();
                commandDetailObj.CommandType = CommandType.StoredProcedure;
                commandDetailObj.CommandText = "usp_SalesProducts";

                SqlCommand comDeleteDetailObj = new SqlCommand();
                comDeleteDetailObj.CommandType = CommandType.Text;
                comDeleteDetailObj.CommandText = "Delete SalesProducts where comcode=@comcode and InvNo=@InvNo_2 ";

                comDeleteDetailObj.Connection = conn;
                //Add Parameter

                commandDetailObj.Connection = conn;
       
                //Add Parameter
                commandObj.Parameters.Add("@DMLType", SqlDbType.Int, 20).Value = dmlType;
                commandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt, 20).Value = ComCode_1;
                commandObj.Parameters.Add("@StoreCode", SqlDbType.Int, 100).Value = storeCode;
                commandObj.Parameters.Add("@InvNo_2", SqlDbType.VarChar).Value = doNo;
                commandObj.Parameters.Add("@InvDate_3", SqlDbType.SmallDateTime, 50).Value = Convert.ToDateTime(invDate_3).ToShortDateString();
                commandObj.Parameters.Add("@ChalanNo_4", SqlDbType.VarChar, 20).Value = chalanNo_4;
                commandObj.Parameters.Add("@ChalanDate_5", SqlDbType.SmallDateTime, 20).Value = Convert.ToDateTime(chalanDate_5);
                commandObj.Parameters.Add("@CustId_6", SqlDbType.VarChar, 20).Value = custId_6;
                commandObj.Parameters.Add("@InvAmount_7", SqlDbType.Decimal, 10).Value = invAmount_7;
                commandObj.Parameters.Add("@Discount_8", SqlDbType.Decimal, 10).Value = discount_8;
                commandObj.Parameters.Add("@PayMode_9", SqlDbType.Int, 10).Value = payMode_9;

                commandObj.Parameters.Add("@Remarks_10", SqlDbType.VarChar, 20).Value = remarks_10;

                commandObj.Parameters.Add("@InvStatus_11", SqlDbType.TinyInt).Value = invStatus_11;

                commandObj.Parameters.Add("@User_Code_12", SqlDbType.VarChar, 20).Value = userCode_12;
                commandObj.Parameters.Add("@DONo_13", SqlDbType.VarChar, 20).Value = DONo_13;
                commandObj.Parameters.Add("@TotalQty_14", SqlDbType.Decimal, 10).Value = totalQty_14;
                
                //Detail Object
              
                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                commandDetailObj.Transaction = transaction;
                comDeleteDetailObj.Transaction = transaction;
                try
                {
                    int rowEffected = commandObj.ExecuteNonQuery();
                    int maxNo = 1;
                    comDeleteDetailObj.Parameters.Add("@comcode", SqlDbType.SmallInt, 20).Value = ComCode_1;
                    comDeleteDetailObj.Parameters.Add("@InvNo_2", SqlDbType.VarChar).Value = doNo;

                    comDeleteDetailObj.ExecuteNonQuery();

                    commandDetailObj.Parameters.Add(new SqlParameter("@DMLType", SqlDbType.Int));
                    commandDetailObj.Parameters.Add(new SqlParameter("@ComCode", SqlDbType.Int, 20));
                    commandDetailObj.Parameters.Add(new SqlParameter("@StoreCode", SqlDbType.Int, 10));
                    commandDetailObj.Parameters.Add(new SqlParameter("@InvNo_1", SqlDbType.VarChar));
                    commandDetailObj.Parameters.Add(new SqlParameter("@slNo_2", SqlDbType.Int));
                    commandDetailObj.Parameters.Add(new SqlParameter("@ProductCode_3", SqlDbType.VarChar, 20));
                    commandDetailObj.Parameters.Add(new SqlParameter("@UnitPrice_4", SqlDbType.Money, 10));
                    commandDetailObj.Parameters.Add(new SqlParameter("@OrderQty_5", SqlDbType.Decimal, 10));
                    commandDetailObj.Parameters.Add(new SqlParameter("@Discount", SqlDbType.Decimal, 10));

                    
                    foreach (DataRow dr in salesDetail.Rows)
                    {
                        string discount = "0";
                        
                        if (String.IsNullOrEmpty(dr["Discount"].ToString()))
                        {
                            discount = "0";
                        }
                        else
                        {
                            discount = dr["Discount"].ToString();
                        }
                                               
                        //commandDetailObj.Parameters.Clear();
                        commandDetailObj.Parameters[0].Value = 1;
                        commandDetailObj.Parameters[1].Value = ComCode_1;
                        commandDetailObj.Parameters[2].Value = storeCode;
                        commandDetailObj.Parameters[3].Value = doNo;
                        commandDetailObj.Parameters[4].Value = maxNo;
                        commandDetailObj.Parameters[5].Value = dr["ProductCode"].ToString();
                        commandDetailObj.Parameters[6].Value = Convert.ToDecimal(dr["UnitPrice"]);
                        commandDetailObj.Parameters[7].Value = Convert.ToDecimal(dr["Quantity"]);
                        commandDetailObj.Parameters[8].Value = Convert.ToDecimal(discount);
                        int detailRowEffected = commandDetailObj.ExecuteNonQuery();
                        maxNo++;
                    }
                    transaction.Commit();
                    conn.Close();

                    return doNo;
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

      private string NewDONo()
      {
          CommonGateway CG = new CommonGateway();
          int maxId = CG.GetMaxNo("Sales", "Convert(Integer, Right(InvNo,6))", "InvDate", DateTime.Now) + 1;
          string DoNo = String.Format("DO{0}-{1}", DateTime.Now.ToString("yyMM"), maxId.ToString("000000"));

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
