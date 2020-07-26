using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using TransportManagerLibrary.UtilityClass;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
namespace TransportManagerLibrary.DAL
{
    public class CommonGateway
    {
        /// <summary>
        /// Get Max No. Insert Table Name, FieldName and Number of Charrecter Needed
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetMaxNo(string tableName, string fieldName,string fieldDate,  DateTime year)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
               SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.Text;
                commandObj.CommandText = "SELECT Max(" + fieldName + ")"
                                         + " FROM " + tableName + " Where year(" + fieldDate + ")=('" + year.Year + "')";

                commandObj.Connection = conn;

                conn.Open();
                string maxNo = Convert.ToString(commandObj.ExecuteScalar());
                conn.Close();
                int MaxNo = 0;
                if (maxNo != String.Empty)
                {
                    MaxNo = Convert.ToInt32(maxNo);
                }
                else
                {
                    MaxNo = 0;
                }
                return MaxNo;
            }
        }

        public int GetMaxNo(string tableName, string fieldName)
        {
            
            ///Get MaxNo for INT type Code.

            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.Text;
                commandObj.CommandText = "SELECT Max("+ fieldName +") as Expr1"
                                         + " FROM " + tableName + "";

                commandObj.Connection = conn;

                conn.Open();
                string maxNo = Convert.ToString(commandObj.ExecuteScalar());
                conn.Close();
                int MaxNo = 0;
                if (String.IsNullOrEmpty(maxNo))
                {
                    MaxNo = 0;
                }
                else
                {
                    MaxNo = Convert.ToInt32(maxNo);
                }
                return MaxNo;
            }
        }

        public int GetMaxNo(string tableName, string fieldName,string whereClause)
        {

            ///Get MaxNo for INT type Code.

            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.Text;
                commandObj.CommandText = "SELECT Max(" + fieldName + ")"
                                         + " FROM " + tableName + " WHERE "+whereClause+"  ";

                commandObj.Connection = conn;

                conn.Open();
                string maxNo = Convert.ToString(commandObj.ExecuteScalar());
                conn.Close();
                int MaxNo = 0;
                if (String.IsNullOrEmpty(maxNo))
                {
                    MaxNo = 0;
                }
                else
                {
                    MaxNo = Convert.ToInt32(maxNo);
                }
                return MaxNo;
            }
        }


        public DataTable Get_Item_BySearch(string sql)
        {

            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                SqlCommand commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.Text;
                commandObj.CommandText = sql;
                commandObj.Connection = conn;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commandObj);
                DataTable ddt = new DataTable();

                da.Fill(ddt);
                conn.Close();

                return ddt;
            }
        }

        public DataTable Get_Item_BySearch(string sql,string searchKey)
        {
            string selectSql = sql; 
            
            SqlParameter parameter = new SqlParameter("@SearchKey", searchKey);
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
        public object get_Scaler_Value(string Sql)
        {
            using (var conn = new SqlConnection(StringUtility.GetAppConnectionString()))
            {
                SqlCommand commandObj = new SqlCommand();
                
                object ObjValue;
                commandObj = new SqlCommand();
                commandObj.CommandType = CommandType.Text;
                commandObj.CommandText = Sql;

                //commandObj.Parameters.AddWithValue("@MovementNo", MovementNo);
                commandObj.Connection = conn;
                conn.Open();
                ObjValue = commandObj.ExecuteScalar();
                conn.Close();
                return ObjValue;
            }
        }
        public static object UserCode { get; set; }

        public ReportDocument ConnectionInfo(ReportDocument rpt)
        {
            ReportDocument crSubreportDocument;
            string[] strConnection = ConfigurationManager.ConnectionStrings[("TransportDBConnectionString")].ConnectionString.Split(new char[] { ';' });

            Database oCRDb = rpt.Database;

            Tables oCRTables = oCRDb.Tables;

            CrystalDecisions.CrystalReports.Engine.Table oCRTable = default(CrystalDecisions.CrystalReports.Engine.Table);

            TableLogOnInfo oCRTableLogonInfo = default(CrystalDecisions.Shared.TableLogOnInfo);

            ConnectionInfo oCRConnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();

            oCRConnectionInfo.ServerName = strConnection[0].Split(new char[] { '=' }).GetValue(1).ToString();
            oCRConnectionInfo.Password = strConnection[2].Split(new char[] { '=' }).GetValue(1).ToString();
            oCRConnectionInfo.UserID = strConnection[1].Split(new char[] { '=' }).GetValue(1).ToString();

            for (int i = 0; i < oCRTables.Count; i++)
            {
                oCRTable = oCRTables[i];
                oCRTableLogonInfo = oCRTable.LogOnInfo;
                oCRTableLogonInfo.ConnectionInfo = oCRConnectionInfo;
                oCRTable.ApplyLogOnInfo(oCRTableLogonInfo);
                if (oCRTable.TestConnectivity())
                    //' If there is a "." in the location then remove the
                    // ' beginning of the fully qualified location.
                    //' Example "dbo.northwind.customers" would become
                    //' "customers".
                    oCRTable.Location = oCRTable.Location.Substring(oCRTable.Location.LastIndexOf(".") + 1);


            }

            for (int i = 0; i < rpt.Subreports.Count; i++)
            {

                {
                    //  crSubreportObject = (SubreportObject);
                    crSubreportDocument = rpt.OpenSubreport(rpt.Subreports[i].Name);
                    oCRDb = crSubreportDocument.Database;
                    oCRTables = oCRDb.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in oCRTables)
                    {
                        oCRTableLogonInfo = aTable.LogOnInfo;
                        oCRTableLogonInfo.ConnectionInfo = oCRConnectionInfo;
                        aTable.ApplyLogOnInfo(oCRTableLogonInfo);
                        if (aTable.TestConnectivity())
                            //' If there is a "." in the location then remove the
                            // ' beginning of the fully qualified location.
                            //' Example "dbo.northwind.customers" would become
                            //' "customers".
                            aTable.Location = aTable.Location.Substring(aTable.Location.LastIndexOf(".") + 1);

                    }
                }
            }
            //  }

            rpt.Refresh();
            return rpt;
        }

        public void loginCrystalreport(ReportDocument Report)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;

            SqlConnectionStringBuilder SConn =
              new SqlConnectionStringBuilder(connectionString);

            Report.DataSourceConnections[0].SetConnection(
              SConn.DataSource, SConn.InitialCatalog, SConn.UserID, SConn.Password);

        }

    }

    public enum Status
    {
        Active=0,InActive=1,Cancel=2
    }
}
