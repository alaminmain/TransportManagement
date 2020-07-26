using System.Data.SqlClient;
using System.Configuration;


namespace TransportManagerLibrary.DAL
{
    public class Gateway
    {
        protected SqlConnection connectionObj;
        protected SqlCommand commandObj;
        public Gateway()
        {


            connectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["MadinaDBConnectionString"].ConnectionString);

        }
    }
}
