using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;

namespace TransportManagerLibrary.DAL
{
    public class VoucherGateway:IDisposable
    {
        public DataTable GetVoucher(int comCode)
        {

            string selectSql = "SELECT    ComCode, VoucherNo, VoucherDate, TripNo, Income, Advance, TotExpense, Narration, AdditionalKM, ReturnDate, VouchStatus FROM         Voucher" +
" WHERE     ComCode=@ComCode Order By VoucherDate DESC";
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
        public DataTable GetVoucherByVoucherNo(int comCode,string VoucherNo)
        {

            string selectSql = "SELECT     ComCode, VoucherNo, VoucherDate, TripNo, Income, Advance, TotExpense, Narration, AdditionalKM, ReturnDate, VouchStatus FROM         Voucher" +
" WHERE    ComCode=@ComCode and VoucherNo=@VoucherNo Order By VoucherNo DESC";
            SqlParameter[] parameter ={
                                              new SqlParameter("@ComCode", comCode),
                                              new SqlParameter("@VoucherNo", VoucherNo)
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


        public  string InsertUpdateVoucher(int comcode_1, string VoucherNo_2, string VoucherDate_3,
            string TripNo_4, decimal Income_5, decimal Advance_6, decimal TotExpense_7, string Narration_8, 
            string ReturnDate_12,  int VoucherStatus_10, string User_Code_11, decimal? additionalKm, DataTable income,DataTable expenses)
        {

            
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                string voucherNo;
                CommonGateway cg = new CommonGateway();

                #region Insert
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.StoredProcedure;
                commandObj.CommandText = "usp_Voucher";
                commandObj.Connection = conn;

                SqlCommand incomeCommand = new SqlCommand();
                incomeCommand.CommandType = CommandType.StoredProcedure;
                incomeCommand.CommandText = "usp_Voucher_Income_Detail";
                incomeCommand.Connection = conn;

                SqlCommand ExpensesCommand = new SqlCommand();
                ExpensesCommand.CommandType = CommandType.StoredProcedure;
                ExpensesCommand.CommandText = "usp_Voucher_Detail";
                ExpensesCommand.Connection = conn;

                //SqlCommand updateTrip = new SqlCommand();
                //updateTrip.CommandType = CommandType.Text;
                //updateTrip.CommandText = "update tripinfo set Additionalkm=@AdditionalKm, TripStatus=1 where TripNo=@tripno";
                //updateTrip.Connection = conn;
                #endregion


                SqlCommand IncomeDeletecommandObj = new SqlCommand();
                IncomeDeletecommandObj.CommandType = CommandType.StoredProcedure;
                IncomeDeletecommandObj.CommandText = "usp_Delete_Voucher_Income_Detail";
                IncomeDeletecommandObj.Connection = conn;

                SqlCommand ExpenseDeletecommandObj = new SqlCommand();
                ExpenseDeletecommandObj.CommandType = CommandType.StoredProcedure;
                ExpenseDeletecommandObj.CommandText = "usp_Delete_Voucher_Detail";
                ExpenseDeletecommandObj.Connection = conn;

                conn.Open();
                var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                commandObj.Transaction = transaction;
                incomeCommand.Transaction = transaction;
                ExpensesCommand.Transaction = transaction;
                IncomeDeletecommandObj.Transaction = transaction;
                ExpenseDeletecommandObj.Transaction = transaction;
                //updateTrip.Transaction = transaction;
                try
                {
                    if (String.IsNullOrEmpty(VoucherNo_2))
                        voucherNo = NewVoucherNo();
                    else
                        voucherNo = VoucherNo_2;

                    commandObj.Parameters.Add("@comcode_1", SqlDbType.SmallInt).Value = comcode_1;
                    commandObj.Parameters.Add("@VoucherNo_2", SqlDbType.VarChar, 20).Value = voucherNo;
                    commandObj.Parameters.Add("@VoucherDate_3", SqlDbType.DateTime).Value = VoucherDate_3;
                    commandObj.Parameters.Add("@TripNo_4", SqlDbType.VarChar, 20).Value = TripNo_4;
                    commandObj.Parameters.Add("@Income_5", SqlDbType.Decimal).Value = Income_5;
                    commandObj.Parameters.Add("@Advance_6", SqlDbType.Decimal).Value = Advance_6;
                    commandObj.Parameters.Add("@TotExpense_7", SqlDbType.Decimal).Value = TotExpense_7;
                    commandObj.Parameters.Add("@Narration_8", SqlDbType.VarChar, 100).Value = Narration_8;
                    commandObj.Parameters.Add("@ReturnDate_9", SqlDbType.DateTime).Value = ReturnDate_12;
                    commandObj.Parameters.Add("@VoucherStatus_10", SqlDbType.SmallInt).Value = VoucherStatus_10;
                    commandObj.Parameters.Add("@User_Code_11", SqlDbType.VarChar, 20).Value = User_Code_11;
                    commandObj.Parameters.Add("@AdditionalKM_12", SqlDbType.Decimal).Value = additionalKm;
                    int rowEffected = commandObj.ExecuteNonQuery();

                    //update TripInfo
                    //updateTrip.Parameters.Add("@AdditionalKm", SqlDbType.SmallInt).Value = additionalKm;
                    //updateTrip.Parameters.Add("@tripno", SqlDbType.VarChar, 20).Value = TripNo_4;
                    //updateTrip.ExecuteNonQuery();

                    //Delete Income
                    IncomeDeletecommandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt).Value = comcode_1;
                    IncomeDeletecommandObj.Parameters.Add("@VoucherNo_2", SqlDbType.VarChar, 20).Value = voucherNo;
                    IncomeDeletecommandObj.ExecuteNonQuery();

                    //Delete Expenses
                    ExpenseDeletecommandObj.Parameters.Add("@ComCode_1", SqlDbType.SmallInt).Value = comcode_1;
                    ExpenseDeletecommandObj.Parameters.Add("@VoucherNo_2", SqlDbType.VarChar, 20).Value = voucherNo;
                    ExpenseDeletecommandObj.ExecuteNonQuery();


                    //Income
                    incomeCommand.Parameters.Add(new SqlParameter("@ComCode_1", SqlDbType.Int));
                    incomeCommand.Parameters.Add(new SqlParameter("@VoucherNo_2", SqlDbType.VarChar, 20));
                    incomeCommand.Parameters.Add(new SqlParameter("@VoucherSLNo_3", SqlDbType.Int, 10));
                    incomeCommand.Parameters.Add(new SqlParameter("@Date_4", SqlDbType.DateTime));
                    incomeCommand.Parameters.Add(new SqlParameter("@Product_5", SqlDbType.VarChar, 50));
                    incomeCommand.Parameters.Add(new SqlParameter("@TripTo_6", SqlDbType.VarChar, 50));
                    incomeCommand.Parameters.Add(new SqlParameter("@TripFrom_7", SqlDbType.VarChar, 50));
                    incomeCommand.Parameters.Add(new SqlParameter("@Rent_8", SqlDbType.Decimal, 10));

                    //expenses
                    ExpensesCommand.Parameters.Add(new SqlParameter("@ComCode_1", SqlDbType.Int));
                    ExpensesCommand.Parameters.Add(new SqlParameter("@VoucherNo_2", SqlDbType.VarChar, 20));
                    ExpensesCommand.Parameters.Add(new SqlParameter("@AccountCode_3", SqlDbType.VarChar, 10));
                    ExpensesCommand.Parameters.Add(new SqlParameter("@VoucherSLNo", SqlDbType.Int));
                    ExpensesCommand.Parameters.Add(new SqlParameter("@Amount_4", SqlDbType.Decimal, 10));
                    ExpensesCommand.Parameters.Add(new SqlParameter("@Comments_5", SqlDbType.VarChar, 50));
                   
                    int voucherSlNo = 1;
             
                    foreach (DataRow dr in income.Rows)
                        {


                        //commandDetailObj.Parameters.Clear();
                        incomeCommand.Parameters[0].Value = comcode_1;
                        incomeCommand.Parameters[1].Value = voucherNo;
                        incomeCommand.Parameters[2].Value = voucherSlNo;
                        incomeCommand.Parameters[3].Value = dr["Date"].ToString(); ;
                        incomeCommand.Parameters[4].Value = dr["Product"].ToString();
                        incomeCommand.Parameters[5].Value = dr["TripTo"].ToString();
                        incomeCommand.Parameters[6].Value = dr["TripFrom"].ToString();
                        incomeCommand.Parameters[7].Value = Convert.ToDecimal(dr["Rent"]);
                        
                        int incomeRowEffected = incomeCommand.ExecuteNonQuery();
                        voucherSlNo++;
                        }
                    voucherSlNo = 1;
                    foreach (DataRow dr in expenses.Rows)
                    {


                        //commandDetailObj.Parameters.Clear();
                        ExpensesCommand.Parameters[0].Value = comcode_1;
                        ExpensesCommand.Parameters[1].Value = voucherNo;
                        ExpensesCommand.Parameters[2].Value = dr["AccountCode"];
                        ExpensesCommand.Parameters[3].Value = voucherSlNo;
                        ExpensesCommand.Parameters[4].Value = Convert.ToDecimal(dr["Amount"]);
                        ExpensesCommand.Parameters[5].Value = dr["Comments"].ToString();
                                           

                        int RowEffected = ExpensesCommand.ExecuteNonQuery();
                        voucherSlNo++;
                    }

                    transaction.Commit();
                    conn.Close();

                    return voucherNo;
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

        private string NewVoucherNo()
        {
            CommonGateway CG = new CommonGateway();
            int maxId = CG.GetMaxNo("Voucher", "Convert(Integer, Right(VoucherNo,6))", "VoucherDate", DateTime.Now.Date) + 1;
            string VoucherNo = String.Format("IEB{0}-{1}", DateTime.Now.Date.ToString("yyMM"), maxId.ToString("000000"));

            return VoucherNo;
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
