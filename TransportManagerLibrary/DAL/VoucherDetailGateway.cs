using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class VoucherDetailGateway:IDisposable
    {
        public DataTable get_Voucher_Detail(string VoucherNo)
        {
           
           
           
            string selectSql = "SELECT     VoucherDetails.AccountCode, ChartofAccount.AccountDesc, VoucherDetails.Amount, VoucherDetails.Comments"
                                    + " FROM         VoucherDetails INNER JOIN"
                                    + " ChartofAccount ON VoucherDetails.AccountCode = ChartofAccount.AccountCode"
                                    + " WHERE     (VoucherDetails.VoucherNo = @VoucherNo)";
            SqlParameter parameter = new SqlParameter("@VoucherNo", VoucherNo);
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

        //for Income Detail
        public DataTable get_Voucher_Income_Detail(string VoucherNo)
        {

            string selectSql = "SELECT   ComCode, VoucherNo, VoucherSLNo, Date, Product, TripTo, TripFrom, Rent"
                                    + " FROM         VoucherIncomeDetail"
                                    + " WHERE     (VoucherNo = @VoucherNo)";
            SqlParameter parameter = new SqlParameter("@VoucherNo", VoucherNo);
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

        public int insertUpdateVoucherDetail(int comcode,string voucherNo,string accountCode,int voucherDetailSLNo, decimal amount,string comments)
        {
            using (var conn = new SqlConnection())
            {
                SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "usp_Voucher_Detail";
 
            commandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt).Value=comcode;
            commandObj.Parameters.Add("@VoucherNo_2",SqlDbType.VarChar,20).Value=voucherNo;
            commandObj.Parameters.Add("@AccountCode_3", SqlDbType.VarChar,10).Value=accountCode;
            commandObj.Parameters.Add("@VoucherSLNo", SqlDbType.Int).Value = voucherDetailSLNo;
            commandObj.Parameters.Add("@Amount_4", SqlDbType.Money).Value=amount;
            commandObj.Parameters.Add("@Comments_5", SqlDbType.VarChar,200).Value=comments;

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

        //Insert Voucher Income Detail
        public int InsertUpdateVoucherIncomeDetail(string VoucherNo_1, string TripNo_2, string MovementNo_3, DateTime Date_4,string Product_5,
            string TripTo_6, string TripFrom_7, decimal Rent_8)
        {
  //          @ComCode_1[smallint],
	 //@VoucherNo_2[varchar](20),
	 //@VoucherSLNo_3 int,
  //   @Date_4[datetime],
  //   @Product_5[varchar](50),
	 //@TripTo_6[varchar](50),
	 //@TripFrom_7[varchar](50),
	 //@Rent_8 money
            using (var conn = new SqlConnection())
            {
                SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "usp_Voucher_Income_Detail";
            commandObj.Parameters.Add("ComCode_1", SqlDbType.VarChar, 20).Value = VoucherNo_1;
            commandObj.Parameters.Add("VoucherNo_2", SqlDbType.VarChar, 20).Value = VoucherNo_1;
            commandObj.Parameters.Add("VoucherSLNo_3", SqlDbType.VarChar, 20).Value = TripNo_2;
           
            commandObj.Parameters.Add("Date_4", SqlDbType.DateTime).Value = Date_4.Date;
            commandObj.Parameters.Add("Product_5", SqlDbType.VarChar, 50).Value = Product_5;
            commandObj.Parameters.Add("TripTo_6", SqlDbType.VarChar, 50).Value = TripTo_6;
            commandObj.Parameters.Add("TripFrom_7", SqlDbType.VarChar, 50).Value = TripFrom_7;
            commandObj.Parameters.Add("Rent_8", SqlDbType.Money).Value = Rent_8;


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

        public int deleteVoucherDetail(int ComCode_1, string VoucherNo)
        {
            using (var conn = new SqlConnection())
            {
                SqlCommand  commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "usp_Delete_Voucher_Detail";

            commandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt).Value = ComCode_1;
            commandObj.Parameters.Add("@VoucherNo", SqlDbType.VarChar, 20).Value = VoucherNo;

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

        //Income Detail Delete
        public int deleteVoucherIncomeDetail(string TripNo, string VoucherNo)
        {
            using (var conn = new SqlConnection())
            {
                SqlCommand commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "usp_Delete_Voucher_Income_Detail";

            commandObj.Parameters.Add("@TripNo", SqlDbType.VarChar,20).Value = TripNo;
            commandObj.Parameters.Add("@VoucherNo", SqlDbType.VarChar, 20).Value = VoucherNo;

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

        public DataTable Load_Income(string TripNo)
        {
            

            string selectSql = "SELECT       Transport.MovementNo, Transport.TransportDate, 'x' AS TripFrom, TransportDetails.OrderQty * TransportDetails.UnitPrice AS Rent, Product.ProductName, Customer.Location " +
                                     "FROM         Transport INNER JOIN " +
                                     "TransportDetails ON Transport.MovementNo = TransportDetails.MovementNo INNER JOIN " +
                                     "Product ON TransportDetails.ProductCode = Product.ProductCode INNER JOIN Customer ON Transport.CustId = Customer.CustId" +
                                     " WHERE     (Transport.TripNo = @TripNo) and TranStatus<>2";
            SqlParameter parameter = new SqlParameter("@TripNo", TripNo);
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

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
