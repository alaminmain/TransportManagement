using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;
using System.Text;
namespace TransportManagerLibrary.DAL
{
    public class TransportMasterGateway:IDisposable
    {
       
        public DataTable GetTripInfo(string tripNo)
        {
            string selectSql = "SELECT DISTINCT Transport.TripNo,CONVERT(varchar(10), Transport.TransportDate, 103)AS Date, Transport.VehicleID, VehicleInfo.VehicleNo, VehicleInfo.KmPerLiter, Personal.EmpName, OilRequsition.km, OilRequsition.FuelQty,OilRequsition.Rate,Location.LocDistance"
                             + " FROM         Transport INNER JOIN"
                      + " VehicleInfo ON Transport.VehicleID = VehicleInfo.VehicleID INNER JOIN"
                       + " Personal ON Transport.EmpCode = Personal.EmpCode INNER JOIN"
                       + " OilRequsition ON Transport.TripNo = OilRequsition.TripSLNo  INNER JOIN "
                       + " Location ON Transport.LocSLNO = Location.LocSLNO"
                     + " WHERE     (Transport.TripNo = @trip_no"; 
            SqlParameter parameter = new SqlParameter("@trip_no", tripNo);
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

        /// <summary>
        /// Get All Movement
        /// </summary>
        /// <param name="comCode"></param>
        /// <returns></returns>
        public DataTable GetAllMovement(int comCode)
        {
            string selectSql = "cSELECT        ComCode, TripNo, MovementNo, TransportDate, TCNo, DealerId, CustId, StoreCode, TransportBy, AgentID, VehicleID, EmpCode, Remarks, TranStatus FROM Transport where ComCode=@ComCode order by TransportDate DESC";
            SqlParameter parameter = new SqlParameter("@ComCode", comCode);
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

        /// <summary>
        /// Get All Movement
        /// </summary>
        /// <param name="comCode"></param>
        /// <returns></returns>
        public DataTable GetAllMovementAll(int comCode)
        {
            string selectSql = "SELECT        Transport.TripNo, Transport.MovementNo, Transport.TransportDate, StoreLocation.StoreName, Customer_1.CustName, " +
                "STUFF(COALESCE (',' + NULLIF (Customer_1.Add1, ''), '') + COALESCE (',' + NULLIF (Customer_1.Add2, ''), '') " +
                "                         + COALESCE(',' + NULLIF(Customer_1.Add3, ''), ''), 1, 1, '') AS RetailerAddress, Customer.CustName AS DealerName, Personal.EmpName, VehicleInfo.VehicleNo FROM  Transport INNER JOIN " +
                " Customer ON Transport.DealerId = Customer.CustId INNER JOIN Customer AS Customer_1 ON Transport.CustId = Customer_1.CustId INNER JOIN " +
                " StoreLocation ON Transport.StoreCode = StoreLocation.StoreCode INNER JOIN TripInfo ON Transport.TripNo = TripInfo.TripNo INNER JOIN Personal ON TripInfo.EmpCode = Personal.EmpCode INNER JOIN " +
                "                         VehicleInfo ON TripInfo.VehicleID = VehicleInfo.VehicleID Where Transport.ComCode=@ComCode and TranStatus=0 order by TransportDate DESC";
            SqlParameter parameter = new SqlParameter("@ComCode", comCode);
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

        public DataTable GetMovmentforGridview(int comCode)
        {
            string selectSql = "SELECT   a.ComCode, a.TripNo, a.MovementNo, a.TransportDate, a.TCNo, a.DealerId, b.CustName AS DealerName, a.CustId, c.CustName "+
" FROM Transport AS a INNER JOIN  Customer AS b ON a.DealerId = b.CustId INNER JOIN Customer AS c ON a.CustId = c.CustId "+
" WHERE  a.comcode=@ComCode ORDER BY TransportDate DESC";
            SqlParameter parameter = new SqlParameter("@ComCode", comCode);
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

        
        public DataTable GetAllMovement(int comCode,string movementNo)
        {
            string selectSql = "SELECT     Transport.ComCode, Transport.TripNo, Transport.MovementNo, Transport.TransportDate, Transport.TCNo, Transport.DealerId, Transport.CustId, Transport.StoreCode, Transport.Remarks, Transport.TranStatus, Customer.CustName AS DealerName, Customer_1.CustName AS CustomerName"
                                + " FROM  Transport INNER JOIN Customer ON Transport.DealerId = Customer.CustId INNER JOIN  Customer AS Customer_1 ON Transport.CustId = Customer_1.CustId where Transport.ComCode=@ComCode and Transport.MovementNo=@movementNo";
            SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@movementNo", movementNo)
                                          };
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

        public DataTable GetMovmentforReport(int comCode, string movementNo)
        {
            StringBuilder sb = new StringBuilder();
            string selectSql =sb.Append("SELECT      CompInfo.cCompname, CompInfo.cAdd1, CompInfo.cAdd2, CompInfo.cAdd3, Transport.TripNo, Transport.MovementNo, Transport.TransportDate, "
                      +"TransportDetails.ProductCode, TransportDetails.OrderQty, TransportDetails.UnitPrice, VehicleInfo.VehicleNo, Personal.EmpName, Product.ProductName, "
                      +"Transport.User_Code, Customer.CustName, Customer_1.CustName AS DealerId, Customer.Add1, Customer.Add2, Customer.Add3, Customer.Mobile, "
                     +" Customer_1.Add1 AS DAdd1, Customer_1.Add2 AS DAdd2, Customer_1.Add3 AS DAdd3, Personal.EmpCode, Product.UnitType, Customer.CustNameBangla," 
                      +" Customer.CustAddressBang, Customer.Location "
                    +" FROM          dbo.Transport AS Transport INNER JOIN "
                +"      dbo.CompInfo AS CompInfo ON Transport.ComCode = CompInfo.ComCode INNER JOIN "
                  +"    dbo.TransportDetails AS TransportDetails ON Transport.MovementNo = TransportDetails.MovementNo INNER JOIN "
                   +"   dbo.VehicleInfo AS VehicleInfo ON Transport.VehicleID = VehicleInfo.VehicleID INNER JOIN "
                    +"  dbo.Personal AS Personal ON Transport.EmpCode = Personal.EmpCode INNER JOIN "
                     +" dbo.Customer AS Customer ON Transport.CustId = Customer.CustId INNER JOIN "
                      +" dbo.Customer AS Customer_1 ON Transport.DealerId = Customer_1.CustId INNER JOIN "
                      +" dbo.Product AS Product ON TransportDetails.ProductCode = Product.ProductCode "
+"where Transport.ComCode=@ComCode and Transport.MovementNo=@movementNo").ToString();
            SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@movementNo", movementNo)
                                          };
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

        public int InsertUpdateTransport(int comcode_1, string MovementNo_2, DateTime TransportDate_3,
          string InvNo_4, string DealerId_5, string CustId_6, int LocSLNO_7, string VehicleID_8, string EmpCode_9,
            string Remarks_10, int Status_11, string TripNo)
        {
            using (var conn = new SqlConnection())
            {
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Transport";
                commandObj.Parameters.Add("@comcode_1", SqlDbType.SmallInt).Value = comcode_1;
                commandObj.Parameters.Add("@MovementNo_2", SqlDbType.VarChar, 20).Value = MovementNo_2;
                commandObj.Parameters.Add("@TransportDate_3", SqlDbType.DateTime).Value = TransportDate_3.Date;
                commandObj.Parameters.Add("@InvNo_4", SqlDbType.VarChar, 20).Value = InvNo_4;
                commandObj.Parameters.Add("@DealerId_5", SqlDbType.VarChar, 20).Value = DealerId_5;
                commandObj.Parameters.Add("@CustId_6", SqlDbType.VarChar, 20).Value = CustId_6;
                commandObj.Parameters.Add("@LocSLNO_7", SqlDbType.Int).Value = LocSLNO_7;
                commandObj.Parameters.Add("@VehicleID_8", SqlDbType.VarChar, 20).Value = VehicleID_8;
                commandObj.Parameters.Add("@EmpCode_9", SqlDbType.VarChar, 20).Value = EmpCode_9;
                commandObj.Parameters.Add("@Remarks_10", SqlDbType.VarChar, 200).Value = Remarks_10;
                commandObj.Parameters.Add("@TranStatus_11", SqlDbType.SmallInt).Value = Status_11;
                commandObj.Parameters.Add("@User_Code_12", SqlDbType.VarChar, 20).Value = CommonGateway.UserCode.ToString();
                commandObj.Parameters.Add("@TripNo", SqlDbType.VarChar, 20).Value = TripNo;

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

        public string InsertUpdateTransport(int ComCode_1, string TripNo_2, string MovementNo_3, string TransportDate_4,string TCNo_5,
            string DealerId_6, string CustId_7,
           string Remarks_8, int TranStatus_9, string User_Code_10, int StoreCode_11, DataTable MovementDetail)
        {
     //       @ComCode_1 	[smallint],
     //@TripNo_2		[varchar] (20),
     //@MovementNo_3 	[varchar](50),
     //@TransportDate_4 	[datetime],
     //@TCNo_5 	[varchar](20),
     //@DealerId_6 	[varchar](20),
     //@CustId_7 	[varchar](20),
     //@Remarks_8 	[varchar](50),
     //@TranStatus_9 	[int],
     //@User_Code_10 	[varchar](10)
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                string movementNO;
                if (String.IsNullOrEmpty(MovementNo_3))
                    movementNO = NewMaxNo();
                else
                    movementNO = MovementNo_3;
                CommonGateway gt = new CommonGateway();
                SqlCommand commandObj = new SqlCommand();

                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Transport";
                commandObj.Connection = conn;
                //Add Parameter

                commandObj.Parameters.Add("@ComCode_1", SqlDbType.Int).Value = ComCode_1;
                commandObj.Parameters.Add("@TripNo_2", SqlDbType.VarChar).Value = TripNo_2;
                commandObj.Parameters.Add("@MovementNo_3", SqlDbType.VarChar).Value = movementNO;
                commandObj.Parameters.Add("@TransportDate_4", SqlDbType.DateTime).Value = Convert.ToDateTime(TransportDate_4);
                commandObj.Parameters.Add("@TCNo_5", SqlDbType.VarChar).Value = TCNo_5;
                commandObj.Parameters.Add("@DealerId_6", SqlDbType.VarChar).Value = DealerId_6;
                commandObj.Parameters.Add("@CustId_7", SqlDbType.VarChar).Value = CustId_7;

                
                commandObj.Parameters.Add("@Remarks_8", SqlDbType.VarChar).Value = Remarks_8;

                commandObj.Parameters.Add("@TranStatus_9", SqlDbType.Int).Value = TranStatus_9;

                commandObj.Parameters.Add("@User_Code_10", SqlDbType.VarChar).Value = User_Code_10;
                commandObj.Parameters.Add("@StoreCode_11", SqlDbType.Int).Value = StoreCode_11;

                //Detail Table
                SqlCommand commandDetailObj = new SqlCommand();
                commandDetailObj.CommandType = CommandType.StoredProcedure;
                commandDetailObj.CommandText = "usp_Transport_Details";
                //Add Parameter

                commandDetailObj.Connection = conn;
                //delete detail
                SqlCommand comDeleteDetailObj = new SqlCommand();
                comDeleteDetailObj.CommandType = CommandType.Text;
                comDeleteDetailObj.CommandText = "DELETE FROM [Transportdb].[dbo].[TransportDetails] WHERE TransportDetails.ComCode=@comcode and TransportDetails.MovementNo=@movementNo ";

                comDeleteDetailObj.Connection = conn;

                //Detail Object

                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                commandDetailObj.Transaction = transaction;
                comDeleteDetailObj.Transaction = transaction;
                try
                {
                    int rowEffected = commandObj.ExecuteNonQuery();

                    int maxNo = 1;// gt.GetMaxNo("TransportDetails", "TransportDSLNo") + 1;
                    //Delete Detail
                    comDeleteDetailObj.Parameters.Add("@comcode", SqlDbType.SmallInt, 20).Value = ComCode_1;
                    comDeleteDetailObj.Parameters.Add("@movementNo", SqlDbType.VarChar).Value = movementNO;

                    comDeleteDetailObj.ExecuteNonQuery();


                    commandDetailObj.Parameters.Add(new SqlParameter("@ComCode_1", SqlDbType.SmallInt));
                    commandDetailObj.Parameters.Add(new SqlParameter("@MovementNo_2", SqlDbType.VarChar));
                    commandDetailObj.Parameters.Add(new SqlParameter("@TransportDSLNO_3", SqlDbType.Int));
                    
                    commandDetailObj.Parameters.Add(new SqlParameter("@ProductCode_4", SqlDbType.VarChar));
                    commandDetailObj.Parameters.Add(new SqlParameter("@OrderQty_5", SqlDbType.Decimal));
                    commandDetailObj.Parameters.Add(new SqlParameter("@UnitPrice_6", SqlDbType.Decimal));


                    foreach (DataRow dr in MovementDetail.Rows)
                    {

                        //commandDetailObj.Parameters.Clear();
                        commandDetailObj.Parameters[0].Value = ComCode_1;
                        commandDetailObj.Parameters[1].Value = movementNO;
                        commandDetailObj.Parameters[2].Value = maxNo;
                        commandDetailObj.Parameters[3].Value = dr["ProductCode"].ToString();
                       
                        commandDetailObj.Parameters[4].Value = Convert.ToDecimal(dr["OrderQty"]);
                        commandDetailObj.Parameters[5].Value = Convert.ToDecimal(dr["Rent"]);

                        int detailRowEffected = commandDetailObj.ExecuteNonQuery();
                        maxNo++;
                    }
                    transaction.Commit();
                    conn.Close();

                    return movementNO;
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
            int maxId = CG.GetMaxNo("Transport", "Convert(Integer, Right(MovementNo,6))", "TransportDate", DateTime.Now) + 1;
            string DoNo = String.Format("MOV{0}-{1}", DateTime.Now.ToString("yyMM"), maxId.ToString("000000"));

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
