using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TransportManagerLibrary.DAL
{
    public class Sms:Gateway
    {
        public int InsertUpdatePortInfo(string p_strPortName, int p_uBaudRate, int p_uDataBits, int p_uReadTimeout, int p_uWriteTimeout)
        {
          
            commandObj = new SqlCommand();
            commandObj.CommandType = CommandType.StoredProcedure;
            commandObj.CommandText = "exec usp_Chart_of_Account 1,'" + p_strPortName + "'," + p_uBaudRate + "," +
                                     p_uDataBits + "," + p_uReadTimeout + "," + p_uWriteTimeout + "";
          

            commandObj.Connection = connectionObj;

            connectionObj.Open();
            int rowEffected = commandObj.ExecuteNonQuery();
            connectionObj.Close();

            return rowEffected;


        }
    }
}
