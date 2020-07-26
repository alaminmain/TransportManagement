using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;
namespace TransportManagerLibrary.DAL
{
    public class TripInfoGateway : IDisposable
    {

        public DataTable GetAllTripInfo()
        {
            string selectSql = "SELECT ComCode, StoreCode, TripNo, TripDate, TransportBy, AgentID, VehicleID, EmpCode, Capacity, KmPerLiter, FuelSlipNo, SupplierName, FuelCode, FuelRate, FuelQty, AdjFuelQty,Remarks, Totalkm, Additionalkm, CapacityBal,  TripStatus, FROM  TripInfo order by TripInfo.TripDate DESC";
            //SqlParameter parameter = new SqlParameter("@trip_no", tripNo);
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

        public DataTable GetAllTripInfoForGridView()
        {
            string selectSql = "SELECT      TripInfo.ComCode, TripInfo.StoreCode, TripInfo.TripNo, TripInfo.TripDate, TripInfo.TransportBy, TripInfo.DealerId, TripInfo.AgentID, TripInfo.VehicleID, "
                      + " TripInfo.EmpCode, TripInfo.Capacity, TripInfo.KmPerLiter, TripInfo.FuelSlipNo, TripInfo.SupplierName, TripInfo.FuelCode, TripInfo.FuelRate, TripInfo.FuelQty,TripInfo.AdjFuelQty, "
                      + " TripInfo.Remarks, TripInfo.Totalkm, TripInfo.Additionalkm, TripInfo.CapacityBal, TripInfo.ReturnDate, TripInfo.TripStatus,  TripInfo.Entry_Date, "
                      + " TripInfo.Update_Date, TripInfo.UserCode, Customer.CustName,Customer.Phone, Customer.Mobile, Personal.EmpName, VehicleInfo.VehicleNo, VehicleAgent.AgentName "
                      +" FROM TripInfo INNER JOIN VehicleInfo ON TripInfo.VehicleID = VehicleInfo.VehicleID INNER JOIN  Customer ON TripInfo.DealerId = Customer.CustId INNER JOIN"
                      + " Personal ON TripInfo.EmpCode = Personal.EmpCode INNER JOIN   VehicleAgent ON TripInfo.AgentID = VehicleAgent.AgentID order by TripInfo.TripDate DESC ";
            //SqlParameter parameter = new SqlParameter("@trip_no", tripNo);
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
        public DataTable GetAllPendingTripInfoForGridView()
        {
            string selectSql = "SELECT      TripInfo.ComCode, TripInfo.StoreCode, TripInfo.TripNo, TripInfo.TripDate, TripInfo.TransportBy, TripInfo.DealerId, TripInfo.AgentID, TripInfo.VehicleID, "
                      + " TripInfo.EmpCode, TripInfo.Capacity, TripInfo.KmPerLiter, TripInfo.FuelSlipNo, TripInfo.SupplierName, TripInfo.FuelCode, TripInfo.FuelRate, TripInfo.FuelQty, TripInfo.AdjFuelQty, "
                      + " TripInfo.Remarks, TripInfo.Totalkm, TripInfo.Additionalkm, TripInfo.CapacityBal, TripInfo.ReturnDate, TripInfo.TripStatus,  TripInfo.Entry_Date, "
                      + " TripInfo.Update_Date, TripInfo.UserCode, Customer.CustName,Customer.Phone, Customer.Mobile, Personal.EmpName, VehicleInfo.VehicleNo, VehicleAgent.AgentName "
                      + " FROM TripInfo INNER JOIN VehicleInfo ON TripInfo.VehicleID = VehicleInfo.VehicleID INNER JOIN  Customer ON TripInfo.DealerId = Customer.CustId INNER JOIN"
                      + " Personal ON TripInfo.EmpCode = Personal.EmpCode INNER JOIN   VehicleAgent ON TripInfo.AgentID = VehicleAgent.AgentID where tripinfo.Tripno not in (select distinct tripno from voucher) order by TripInfo.TripDate DESC ";
            //SqlParameter parameter = new SqlParameter("@trip_no", tripNo);
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

        public DataTable GetAllNotBilledTripDriverwise()
        {
            string selectSql = @"SELECT        TripInfo.EmpCode as Code, Personal.EmpName as Driver, COUNT(TripInfo.TripNo) AS TotalTrip " +
                " FROM TripInfo INNER JOIN Personal ON TripInfo.EmpCode = Personal.EmpCode  WHERE        (TripInfo.TripStatus = '1') " +
                " GROUP BY TripInfo.EmpCode, Personal.EmpName ORDER BY TotalTrip DESC";
            //SqlParameter parameter = new SqlParameter("@trip_no", tripNo);
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
        public DataTable GetAllNotBilledTripVehiclewise()
        {
            string selectSql = @"SELECT        TripInfo.VehicleID, VehicleInfo.VehicleNo, COUNT(TripInfo.TripNo) AS TotalTrip " +
                "FROM TripInfo INNER JOIN VehicleInfo ON TripInfo.VehicleID = VehicleInfo.VehicleID  WHERE        (TripInfo.TripStatus = '1') GROUP BY TripInfo.VehicleID, " +
                "VehicleInfo.VehicleNo ORDER BY TotalTrip DESC";
            //SqlParameter parameter = new SqlParameter("@trip_no", tripNo);
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
        private string NewTripNo()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("TripInfo", "Convert(Integer, Right(TripNo,6))", "TripDate", DateTime.Now) + 1;
            string TripNo = String.Format("TP{0}-{1}", DateTime.Now.ToString("yyMM"), maxId.ToString("000000"));

            return TripNo;
        }
        public DataTable GetTripInfo(string tripNo)
        {
            string selectSql = "SELECT     TripInfo.ComCode, TripInfo.StoreCode, TripInfo.TripNo, TripInfo.TripDate, TripInfo.TransportBy, TripInfo.AgentID, TripInfo.VehicleID, TripInfo.EmpCode, "
                      + " TripInfo.Capacity, TripInfo.KmPerLiter, TripInfo.FuelSlipNo, TripInfo.SupplierName, TripInfo.FuelCode, TripInfo.FuelRate, TripInfo.FuelQty,TripInfo.AdjFuelQty, TripInfo.Remarks, "
                      + " TripInfo.Totalkm, TripInfo.Additionalkm, TripInfo.CapacityBal, TripInfo.TripStatus, VehicleInfo.VehicleNo, Personal.EmpName,Personal.Mobile, TripInfo.DealerId, "
                      + " Customer.CustName,Customer.Mobile as DealerMobile,Customer.Phone FROM         TripInfo INNER JOIN VehicleInfo ON TripInfo.VehicleID = VehicleInfo.VehicleID INNER JOIN"
                      + " Personal ON TripInfo.EmpCode = Personal.EmpCode INNER JOIN  Customer ON TripInfo.DealerId = Customer.CustId WHERE     (TripInfo.TripNo = @trip_no) order by TripInfo.TripDate DESC";
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

 //       @ComCode_1[smallint],
	// @TripNo_2[varchar] (20) ,
	// @FuelSlipNo_3 varchar(20) ,
	// @SupplierName_4 varchar(50) ,
	// @FuelQty_5 decimal(18,2),
	// @Totalkm_6 decimal(18,2),
	// @Additionalkm_7 decimal(18,2),
	// @TripStatus_8 smallint,
 //    @UserCode_9    varchar(20),
	//@AdjFuelQty_10 decimal(18, 2)
        public string InsertUpdateFuelInfo(int comcode_1, string tripNo_2, string fuelSlipNo_3,
           string supplierName_4, decimal? fuelQty_5, decimal? AdjFuelQty_6,int? tripStatus_7, string userCode_8)

        {
         
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {

                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_TripFuelSlip";
                commandObj.Connection = conn;
                if (String.IsNullOrEmpty(fuelSlipNo_3))
                {
                    fuelSlipNo_3 = GetFuelSlipNo();
                }

                //           SqlParameter FualRate_14 =
                //new SqlParameter("@FualRate_14", ValueOrDBNullIfZero(fuelRate_14));
                commandObj.Parameters.Add("@comcode_1", SqlDbType.SmallInt).Value = comcode_1;
                
                commandObj.Parameters.Add("@TripNo_2", SqlDbType.VarChar).Value = tripNo_2;
                
                commandObj.Parameters.Add("@FuelSlipNo_3", SqlDbType.VarChar, 20).Value = fuelSlipNo_3;
                commandObj.Parameters.Add("@SupplierName_4", SqlDbType.VarChar).Value = supplierName_4;
              
                commandObj.Parameters.Add(new SqlParameter { SqlValue = fuelQty_5 ?? (object)DBNull.Value, ParameterName = "FuelQty_5" });

                commandObj.Parameters.Add("@AdjFuelQty_6", SqlDbType.Decimal).Value = AdjFuelQty_6;
                commandObj.Parameters.Add("@TripStatus_7", SqlDbType.VarChar, 20).Value = tripStatus_7;
                commandObj.Parameters.Add("@UserCode_8", SqlDbType.VarChar, 20).Value = userCode_8;
                
                

                conn.Open();

                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;

                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return fuelSlipNo_3;
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
                    conn.Dispose();
                }
            }

        }


  //      @ComCode_1[smallint],
	 //@StoreCode_2[int],
	 //@TripNo_3[varchar] (20) ,
	 //@TripDate_4[datetime],
	 //@TransportBy_5[int],
	 //@AgentID_6[varchar](20),
	 //@VehicleID_7[varchar](20),
	 //@EmpCode_8[varchar](10),
	 //@Capacity_9 varchar(50) ,
	 //@KmPerLiter_10 decimal(18,2),
	 //@FuelCode_11 varchar(20),
	 //@FualRate_12 decimal(18,2),
	 //@Remarks_13 varchar(100),
	 //@TripStatus_14 smallint,
  //   @UserCode_15   varchar(20),
	 //@DealerId_16 varchar(20),
	 //@CapacityBal_17 int
        public string InsertUpdateTripInfo(int comcode_1, int storeCode_2, string tripNo_3, string tripDate_4, int transportBy_5,
            string agentId_6, string vehicleId_7, string empCode_8, string capacity_9, decimal kmPerLiter_10,  string fuelCode_11, decimal? fuelRate_12,
            string Remarks_13, int? tripStatus_14, string userCode_15, string dealerId_16, int CapacityBal_17, DataTable detail)

        {
            
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {

                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_TripInfo";
                commandObj.Connection = conn;
                //Check Variable is null
                if (String.IsNullOrEmpty(tripNo_3))
                {
                    tripNo_3 = NewTripNo();
                }

                //           SqlParameter FualRate_14 =
                //new SqlParameter("@FualRate_14", ValueOrDBNullIfZero(fuelRate_14));
                commandObj.Parameters.Add("@comcode_1", SqlDbType.SmallInt).Value = comcode_1;
                commandObj.Parameters.Add("@StoreCode_2", SqlDbType.Int).Value = storeCode_2;
                commandObj.Parameters.Add("@TripNo_3", SqlDbType.VarChar).Value = tripNo_3;
                commandObj.Parameters.Add("@TripDate_4", SqlDbType.DateTime).Value = tripDate_4;
                commandObj.Parameters.Add("@TransportBy_5", SqlDbType.Int).Value = transportBy_5;
                commandObj.Parameters.Add("@AgentID_6", SqlDbType.VarChar, 20).Value = agentId_6;
                commandObj.Parameters.Add("@VehicleID_7", SqlDbType.VarChar, 20).Value = vehicleId_7;

                commandObj.Parameters.Add("@EmpCode_8", SqlDbType.VarChar, 20).Value = empCode_8;
                commandObj.Parameters.Add("@Capacity_9", SqlDbType.VarChar, 50).Value = capacity_9;
                commandObj.Parameters.Add("@KmPerLiter_10", SqlDbType.Decimal).Value = kmPerLiter_10;
              
                commandObj.Parameters.Add("@FuelCode_11", SqlDbType.VarChar).Value = fuelCode_11;

                commandObj.Parameters.Add(new SqlParameter { SqlValue = fuelRate_12 ?? (object)DBNull.Value, ParameterName = "FualRate_12" });
                
                commandObj.Parameters.Add("@Remarks_13", SqlDbType.VarChar).Value = Remarks_13;
               
                commandObj.Parameters.Add("@TripStatus_14", SqlDbType.VarChar, 20).Value = tripStatus_14;
                commandObj.Parameters.Add("@UserCode_15", SqlDbType.VarChar, 20).Value = userCode_15;
                commandObj.Parameters.Add("@DealerId_16", SqlDbType.VarChar, 20).Value = dealerId_16;
                commandObj.Parameters.Add("@CapacityBal_17", SqlDbType.Int).Value = CapacityBal_17;
              

                //Detail Table
                SqlCommand commandDetailObj = new SqlCommand();
                commandDetailObj.CommandType = CommandType.StoredProcedure;
                commandDetailObj.CommandText = "usp_TripDetail";
                //Add Parameter

                commandDetailObj.Connection = conn;
                //delete detail
                SqlCommand comDeleteDetailObj = new SqlCommand();
                comDeleteDetailObj.CommandType = CommandType.Text;
                comDeleteDetailObj.CommandText = "DELETE FROM [Transportdb].[dbo].[TripDetail] WHERE TripDetail.ComCode=@comcode and TripDetail.tripNo=@tripNo ";

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

                    //detail Delete Insert
                    CommonGateway gt = new CommonGateway();
                    int maxNo = 1;// gt.GetMaxNo("TripDetail", "tripdetailSL") + 1;
                    //Delete Detail
                    comDeleteDetailObj.Parameters.Add("@comcode", SqlDbType.SmallInt, 20).Value = comcode_1;
                    comDeleteDetailObj.Parameters.Add("@tripNo", SqlDbType.VarChar).Value = tripNo_3;

                    comDeleteDetailObj.ExecuteNonQuery();


                    //                  @ComCode_1 	[smallint],
  //                  @Tripno_2[varchar](20),
	 //@TripdetailSL_3 int,
  //   @TCNo_4[varchar](20),
	 //@ProductCode_6 varchar (10),
  //   @OrderQty_7[numeric](14, 2),
  //   @UnitPrice_8[money])

                    commandDetailObj.Parameters.Add(new SqlParameter("@ComCode_1", SqlDbType.SmallInt));
                    commandDetailObj.Parameters.Add(new SqlParameter("@Tripno_2", SqlDbType.VarChar));
                    commandDetailObj.Parameters.Add(new SqlParameter("@TripdetailSL_3", SqlDbType.Int));
                    commandDetailObj.Parameters.Add(new SqlParameter("@TCNo_4", SqlDbType.VarChar));
                    
                    commandDetailObj.Parameters.Add(new SqlParameter("@ProductCode_6", SqlDbType.VarChar));
                    commandDetailObj.Parameters.Add(new SqlParameter("@OrderQty_7", SqlDbType.Decimal));
                    commandDetailObj.Parameters.Add(new SqlParameter("@UnitPrice_8", SqlDbType.Decimal));


                    foreach (DataRow dr in detail.Rows)
                    {

                        //commandDetailObj.Parameters.Clear();
                        commandDetailObj.Parameters[0].Value = comcode_1;
                        commandDetailObj.Parameters[1].Value = tripNo_3;
                        commandDetailObj.Parameters[2].Value = maxNo;
                        commandDetailObj.Parameters[3].Value = dr["tcno"].ToString();
                      
                        commandDetailObj.Parameters[4].Value = dr["ProductCode"].ToString();

                        commandDetailObj.Parameters[5].Value = Convert.ToDecimal(dr["OrderQty"]);
                        commandDetailObj.Parameters[6].Value = Convert.ToDecimal(dr["Rent"]);

                        int detailRowEffected = commandDetailObj.ExecuteNonQuery();
                        maxNo++;
                    }


                    transaction.Commit();
                    conn.Close();

                    return tripNo_3;
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

        public string UpdateCapacityBalance(string tripNo,decimal totalkm)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {

                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.Text;
                commandObj.CommandText = "Update TripInfo set totalkm=@totalkm where tripno=@tripNo";
                commandObj.Connection = conn;



                
                commandObj.Parameters.Add("@totalkm", SqlDbType.Decimal).Value = totalkm;

                commandObj.Parameters.Add("@tripNo", SqlDbType.VarChar).Value = tripNo;

                conn.Open();

                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;

                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return tripNo;
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
        private object ValueOrDBNullIfZero(object val)
        {
            try
            {
                if ((decimal)val == 0 || String.IsNullOrEmpty(val.ToString()))
                    return DBNull.Value;
                return val;
            }
            catch (NullReferenceException ex)
            {
                return DBNull.Value;
            }
        }
        public string GetFuelSlipNo()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("TripInfo", "Convert(Integer, Right(FuelSlipNo,6))", "TripDate", DateTime.Now) + 1;
            string fuelSlip = String.Format("FS{0}-{1}", DateTime.Now.ToString("yyMM"), maxId.ToString("000000"));

            return fuelSlip;
        }

    

    #region IDisposable Members

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

        #endregion

    }
}
