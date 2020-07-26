using System;
using System.Collections.Generic;

namespace TransportManagerLibrary.DAL
{
    class DbConnectionInfo:Gateway
    {
        
        private static string m_userName = String.Empty;
        private static string m_password = String.Empty;
        private static string m_serverName = String.Empty;
        private static string m_initialCatalog = String.Empty;
        private static bool m_useIntegratedSecurity = false;
        public static void SetConnectionString(string connstr)
        {
            Dictionary<string, string> connStringKeys = new Dictionary<string, string>();
            string[] keysBySemicolon = connstr.Split(';');
            string[] keysByEquals;
            foreach (string keySemicolon in keysBySemicolon)
            {
                keysByEquals = keySemicolon.Split('=');
                if (keysByEquals.Length == 0)
                {
                    // do nothing
                }
                else if (keysByEquals.Length == 1)
                {
                    // assume key name but no value
                    connStringKeys.Add(keysByEquals[0].ToUpper(), "");
                }
                else
                {
                    connStringKeys.Add(keysByEquals[0].ToUpper(), keysByEquals[1]);
                }
            }
            if (connStringKeys.ContainsKey("DATA SOURCE"))
            {
                m_serverName = connStringKeys["DATA SOURCE"];
            }
            if (connStringKeys.ContainsKey("DATABASE"))
            {
                m_initialCatalog = connStringKeys["DATABASE"];
            }
            if (connStringKeys.ContainsKey("INITIAL CATALOG"))
            {
                m_initialCatalog = connStringKeys["INITIAL CATALOG"];
            }
            if (connStringKeys.ContainsKey("USER ID"))
            {
                m_userName = connStringKeys["USER ID"];
            }
            if (connStringKeys.ContainsKey("PASSWORD"))
            {
                m_password = connStringKeys["PASSWORD"];
            }
            if (connStringKeys.ContainsKey("INTEGRATED SECURITY"))
            {
                m_useIntegratedSecurity = false;
            }
        }
        public static string UserName
        {
            get { return m_userName; }
        }
        public static string Password
        {
            get { return m_password; }
        }
        public static string ServerName
        {
            get { return m_serverName; }
        }
        public static string InitialCatalog
        {
            get { return m_initialCatalog; }
        }
        public static bool UseIntegratedSecurity
        {
            get { return m_useIntegratedSecurity; }
        }
    }
    
}
