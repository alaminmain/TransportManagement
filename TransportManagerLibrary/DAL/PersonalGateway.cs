using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class PersonalGateway : IDisposable
    {
        //EmpCode, Cardno, EmpName, ComCode, ShiftId, EmpType, MGR, GradeID, DeptID, DesignationID, AppointDate, JoiningDate, ConfirmDate, PBXExt, 
        //EducDegree, BirthDate, Sex, MeritalStatus, SpouseName, FatherName, MotherName, BloodGroup, Add1, Add2, Add3, PostCode, Country, Phone, Mobile, Fax, Email, 
        //PStatus, BasicFinal, OTPerHour, AccNo, BankID, Photo, WHoliday, Userpws, DrivingLicen,

        public DataTable GetAllPersonnel()
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
        public DataTable GetAllDriver()
        {
            string selectSql = "SELECT  EmpCode, Cardno, EmpName, ComCode,StoreCode, ShiftId, EmpType, FatherName, MotherName, BloodGroup, Add1, Add2, Add3, PostCode, Country, Phone, Mobile, Fax, Email,  PStatus, BasicFinal, DrivingLicen,NID, DrivingCapacity FROM Personal where cardno=1";
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

        public DataTable GetDriverById(int comCode, string empCode)
        {
            string selectSql = "SELECT  EmpCode, Cardno, EmpName, ComCode,StoreCode, ShiftId, EmpType, FatherName, MotherName, BloodGroup, Add1, Add2, Add3, PostCode, Country, Phone, Mobile, Fax, Email,PStatus, BasicFinal, DrivingLicen,NID, DrivingCapacity "                 + " FROM Personal where cardno=1 and comcode=@ComCode and EmpCode=@EmpCode";
            try
            {
                SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@EmpCode",empCode)
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

        public string InsertUpdatePersonal(string EmpCode, string Cardno, string EmpName, int ComCode, string FatherName, 
            string MotherName, string BloodGroup, string Add1, string Add2, string Add3, string Mobile, int PStatus, 
            string DrivingLicen, string userId, int storeCode, string nid, string drivingCapacity)
        {

            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {

  
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Personal";
                commandObj.Connection = conn;
                string empcode;
                //Check Variable is null
                if (String.IsNullOrEmpty(EmpCode))
                {
                    empcode = NewEmpCode();
                }
                else
                    empcode = EmpCode;

                commandObj.Parameters.Add("@EmpCode_1", SqlDbType.VarChar, 20).Value = empcode;
                commandObj.Parameters.Add("@Cardno_2", SqlDbType.VarChar, 20).Value = Cardno;
                commandObj.Parameters.Add("@EmpName_3", SqlDbType.VarChar, 100).Value = EmpName;

                commandObj.Parameters.Add("@ComCode_4", SqlDbType.SmallInt).Value = ComCode;


                commandObj.Parameters.Add("@FatherName_20", SqlDbType.VarChar, 50).Value = FatherName;
                commandObj.Parameters.Add("@MotherName_21", SqlDbType.VarChar, 50).Value = MotherName;

                commandObj.Parameters.Add("@BloodGroup_22", SqlDbType.VarChar, 20).Value = BloodGroup;

                commandObj.Parameters.Add("@Add1_23", SqlDbType.VarChar, 50).Value = Add1;
                commandObj.Parameters.Add("@Add2_24", SqlDbType.VarChar, 50).Value = Add2;
                commandObj.Parameters.Add("@Add3_25", SqlDbType.VarChar, 50).Value = Add3;

                commandObj.Parameters.Add("@Mobile_29", SqlDbType.VarChar, 20).Value = Mobile;
                //commandObj.Parameters.Add("@IsHired_31", SqlDbType.TinyInt).Value = IsHired;
                commandObj.Parameters.Add("@PStatus_32", SqlDbType.TinyInt).Value = PStatus;
                commandObj.Parameters.Add("@DrivingLicen_33", SqlDbType.VarChar, 20).Value = DrivingLicen;
                commandObj.Parameters.Add("@User_code_34", SqlDbType.VarChar, 20).Value = userId;
                commandObj.Parameters.Add("@StoreCode_35", SqlDbType.Int).Value = storeCode;
                commandObj.Parameters.Add("@NID_36", SqlDbType.VarChar, 20).Value = nid;
                commandObj.Parameters.Add("@DrivingCapacity_37", SqlDbType.VarChar, 20).Value =drivingCapacity;
                conn.Open();

                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;

                try
                {

                    int rowEffected = commandObj.ExecuteNonQuery();
                    transaction.Commit();
                    conn.Close();

                    return empcode;
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

        private string NewEmpCode()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("Personal", "Convert(Integer, Right(EmpCode,6))", "Cardno=1") + 1;
            string CustId = String.Format("DR{0}", maxId.ToString("000000"));

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


