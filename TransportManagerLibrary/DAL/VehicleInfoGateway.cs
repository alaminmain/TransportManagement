using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class VehicleInfoGateway:IDisposable
    {

        public DataTable GetAllVehicle(int comcode)
        {
            string selectSql = "SELECT  ComCode, StoreCode, VehicleID, VehicleNo, EngineNo, ChesisNo, ModelNo, EngineVolume, PurchaseDate, VehicleDesc, MobileNo, Capacity, CapacityUnit, KmPerLiter, FuelCode, IsHired, Remarks, VehicleStatus FROM VehicleInfo where comCode=@ComCode";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode)
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
        /// <summary>
        /// Get Vehicle Id
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public DataTable GetVehicleById(int comcode,string vehicleId)
        {
            string selectSql = "SELECT    ComCode, StoreCode, VehicleID, VehicleNo, EngineNo, ChesisNo, ModelNo, EngineVolume, PurchaseDate, VehicleDesc, MobileNo, Capacity, CapacityUnit, KmPerLiter, FuelCode, IsHired, Remarks, VehicleStatus FROM            VehicleInfo where VehicleID=@VehicleId and comCode=@ComCode";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@VehicleId", vehicleId)
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

        public DataTable GetVehicleMovmentByVehicle(int comcode, string moveRegNO)
        {
            string selectSql = "SELECT     VehicleMovement.ComCode, VehicleMovement.VehicleStatus, VehicleMovement.MoveRegNo, VehicleMovement.MoveDate, VehicleMovement.VehicleID, "
                      + " VehicleMovement.EmpCode, VehicleMovement.Remarks, VehicleInfo.VehicleNo, Personal.EmpName "
                       + " FROM         VehicleMovement INNER JOIN VehicleInfo ON VehicleMovement.VehicleID = VehicleInfo.VehicleID INNER JOIN "
                      + " Personal ON VehicleMovement.EmpCode = Personal.EmpCode where VehicleMovement.ComCode=@ComCode and VehicleMovement.MoveRegNo=@MoveRegNo ";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@MoveRegNo", moveRegNO)
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
        public DataTable GetVehicleMovmentAll(int comcode)
        {
            string selectSql = "SELECT     VehicleMovement.ComCode, VehicleMovement.VehicleStatus, VehicleMovement.MoveRegNo, VehicleMovement.MoveDate, VehicleMovement.VehicleID, "
                      +" VehicleMovement.EmpCode, VehicleMovement.Remarks, VehicleInfo.VehicleNo, Personal.EmpName "
                       + " FROM         VehicleMovement INNER JOIN VehicleInfo ON VehicleMovement.VehicleID = VehicleInfo.VehicleID INNER JOIN "
                      +" Personal ON VehicleMovement.EmpCode = Personal.EmpCode where VehicleMovement.ComCode=@ComCode";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              
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

        public DataTable GetVehicleCurrentStatus(int comcode)
        {
            string selectSql = " SELECT     VehicleStatus, CASE WHEN VehicleStatus = 0 THEN 'PULL' WHEN VehicleStatus = 1 THEN 'On Trip' WHEN VehicleStatus = 2 THEN 'Workshop' WHEN VehicleStatus = 3 THEN 'Not In Service'"
                       +" END AS Status, COUNT(VehicleNo) AS NoofVehicles FROM         VehicleInfo where comCode=@ComCode GROUP BY VehicleStatus";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              
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
        public DataTable GetVehicleCurrentStatusGhatwise(int comcode, int ghatCode)
        {
            string selectSql = " SELECT     VehicleStatus, CASE WHEN VehicleStatus = 0 THEN 'PULL' WHEN VehicleStatus = 1 THEN 'On Trip' WHEN VehicleStatus = 2 THEN 'Workshop' WHEN VehicleStatus = 3 THEN 'Not In Service'"
                       + " END AS Status, COUNT(VehicleNo) AS NoofVehicles FROM         VehicleInfo where comCode=@ComCode and storeCode=@ghatCode GROUP BY VehicleStatus";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comcode),
                                              new SqlParameter("@ghatCode", ghatCode)
                                              
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

        public string InsertUpdateVehicle(int comCode,string VehicleID_1, string VehicleNo_2,string ChesisNo_3,string ModelNo_4, string EngineNo_5,
            string VehicleDesc_6, string Mobile_7, string Capacity_8,
             decimal KmPerLiter_9, int FuelCode_10, int StoreCode_11,int IsHired_12, int VehicleStatus_13, string userId,string capacityUnit_15,string EngineVolume_16,
         string PurchaseDate_17,string Remarks_18 )
        {
            
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Vehicle_Info";
                commandObj.Connection = conn;
                string vehicleCode;
                //Check Variable is null
                if (String.IsNullOrEmpty(VehicleID_1))
                {
                    vehicleCode = NewVehicleCode();
                }
                else
                    vehicleCode = VehicleID_1;
                commandObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = comCode;
                commandObj.Parameters.Add("@VehicleID_1", SqlDbType.VarChar, 50).Value = vehicleCode;
                commandObj.Parameters.Add("@VehicleNo_2", SqlDbType.VarChar, 50).Value = VehicleNo_2;
                commandObj.Parameters.Add("@ChesisNo_3", SqlDbType.VarChar, 50).Value = ChesisNo_3;
                commandObj.Parameters.Add("@ModelNo_4", SqlDbType.VarChar, 50).Value = ModelNo_4;
                
                commandObj.Parameters.Add("@EngineNo_5", SqlDbType.VarChar, 50).Value = EngineNo_5;
                commandObj.Parameters.Add("@VehicleDesc_6", SqlDbType.VarChar, 50).Value = VehicleDesc_6;
                commandObj.Parameters.Add("@Mobile_7", SqlDbType.VarChar, 20).Value = Mobile_7;
                commandObj.Parameters.Add("@Capacity_8", SqlDbType.VarChar, 20).Value = Capacity_8;
                commandObj.Parameters.Add("@KmPerLiter_9", SqlDbType.Decimal).Value = KmPerLiter_9;
                commandObj.Parameters.Add("@FuelCode_10", SqlDbType.Decimal).Value = FuelCode_10;
                commandObj.Parameters.Add("@StoreCode_11", SqlDbType.Decimal).Value = StoreCode_11;
                commandObj.Parameters.Add("@IsHired_12", SqlDbType.Decimal).Value = IsHired_12;
                
                commandObj.Parameters.Add("@VehicleStatus_13", SqlDbType.Int).Value = VehicleStatus_13;
                commandObj.Parameters.Add("@User_Code_14", SqlDbType.VarChar, 20).Value = userId;
                commandObj.Parameters.Add("@CapacityUnit_15", SqlDbType.VarChar, 20).Value = capacityUnit_15;
                commandObj.Parameters.Add("@EngineVolume_16", SqlDbType.VarChar).Value = EngineNo_5;
                commandObj.Parameters.Add("@PurchaseDate_17", SqlDbType.VarChar).Value = PurchaseDate_17;
                commandObj.Parameters.Add("@Remarks_18", SqlDbType.VarChar, 200).Value = Remarks_18;
                conn.Open();

                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;

                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return vehicleCode;
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

        public string InsertUpdateVehicleMovement(int comCode,string MoveRegNo_1, string MoveDate_2, string VehicleID_3,  
           string EmpCode_4,string Remarks_5, int VehicleStatus_6, string VStatus, string User_Code_7)
        {

            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
 ////               @ComCode 	[int],
 //   @MoveRegNo_1	varchar(20),
 //   @MoveDate_2		Datetime,
 //   @VehicleID_3	varchar(20),
 //   @EmpCode_4	varchar(10),
 //   @Remarks_5	varchar(200),
 //   @VehicleStatus_6 int,
 //   @User_Code_7 	[varchar](10)

                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "[usp_VehicleMovement]";
                commandObj.Connection = conn;
                string MoveRegNo;
                //Check Variable is null
                if (String.IsNullOrEmpty(MoveRegNo_1))
                {
                    MoveRegNo = NewVehicleMovmentCode(VStatus);
                }
                else
                    MoveRegNo =MoveRegNo_1;



                commandObj.Parameters.Add("@ComCode", SqlDbType.SmallInt).Value = comCode;
                commandObj.Parameters.Add("@MoveRegNo_1", SqlDbType.VarChar, 50).Value = MoveRegNo;
                commandObj.Parameters.Add("@MoveDate_2", SqlDbType.DateTime).Value = MoveDate_2;
                commandObj.Parameters.Add("@VehicleID_3", SqlDbType.VarChar, 50).Value = VehicleID_3;
                commandObj.Parameters.Add("@EmpCode_4", SqlDbType.VarChar, 20).Value = EmpCode_4;
                commandObj.Parameters.Add("@Remarks_5", SqlDbType.NVarChar, 200).Value = Remarks_5;
                commandObj.Parameters.Add("@VehicleStatus_6", SqlDbType.Int).Value = VehicleStatus_6;

                commandObj.Parameters.Add("@User_Code_7", SqlDbType.VarChar, 50).Value = User_Code_7;
               
                conn.Open();

                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;

                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return MoveRegNo.ToString();
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


        private string NewVehicleCode()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("VehicleInfo", "Convert(Integer, Right(VehicleID,6))") + 1;
            string CustId = String.Format("VI-{0}", maxId.ToString("000000"));

            return CustId;
        }
        private string NewVehicleMovmentCode(string status)
        {
            CommonGateway CG = new CommonGateway();

            int maxId = CG.GetMaxNo("VehicleMovement", "Convert(Integer, Right(MoveRegNo,6))", "MoveDate", DateTime.Now) + 1;
            string MovmentCode = String.Format("{0}{1}{2}",status, DateTime.Now.ToString("yyMM"), maxId.ToString("000000"));


            return MovmentCode;
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
