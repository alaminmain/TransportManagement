using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;

namespace TransportManagerLibrary.UtilityClass
{
    public static class StringUtility
    {
        private static string encryptedConnectionString = "";
        private static string connectionString = "";
        private static string connectionString2 = "";
        private static string connectionString3 = "";
        private static string connectionString4 = "";

        static StringUtility()
        {
        }

        public static string GetAppConnectionString()
        {
            /*
            string cryptedString = !(ConfigurationManager.AppSettings["ActiveDB"].ToUpper() == "DC") ? ConfigurationManager.ConnectionStrings["AppDR"].ConnectionString : ConfigurationManager.ConnectionStrings["AppDC"].ConnectionString;
            if (!StringUtility.encryptedConnectionString.Equals(cryptedString))
            {
                StringUtility.encryptedConnectionString = cryptedString;
                StringUtility.connectionString = CryptoHelper.Decrypt(cryptedString);
               
            }
            */

            StringUtility.connectionString = ConfigurationManager.ConnectionStrings["TransportDBConnectionString"].ToString();
            return StringUtility.connectionString;
        }

        public static string RemoveNonNumeric(string acctnum)
        {
            char[] array = new char[10]
      {
        '0',
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9'
      };
            string str = "";
            foreach (char ch in acctnum)
            {
                if (Array.IndexOf<char>(array, ch) > -1)
                    str = str + ch.ToString();
            }
            return str;
        }

        public static string RemoveNonAlphaNumeric(string input)
        {
            string str = "";
            foreach (char ch in input)
            {
                if ((int)ch >= 65 && (int)ch <= 90 || (int)ch >= 48 && (int)ch <= 57 || (int)ch >= 97 && (int)ch <= 122 || (int)ch == 32)
                    str = str + ch.ToString();
            }
            return str;
        }

        public static string FormatDFIAccountNumber(string input)
        {
            string str = "";
            foreach (char ch in input)
            {
                if ((int)ch >= 65 && (int)ch <= 90 || (int)ch >= 48 && (int)ch <= 57 || ((int)ch >= 97 && (int)ch <= 122 || (int)ch == 32) || (int)ch == 45)
                    str = str + ch.ToString();
            }
            return str;
        }

        public static string GetCheckDigit(string rtWithoutCheck)
        {
            int[] numArray = new int[7]
      {
        5,
        7,
        1,
        5,
        7,
        1,
        5
      };
            int num = 0;
            for (int index = 0; index < numArray.Length; ++index)
                num += Convert.ToInt32(rtWithoutCheck[index].ToString()) * numArray[index];
            return (num + (num % 10 == 0 ? 0 : 10) - num % 10 - num).ToString();
        }

        public static string GetArchiveConnectionString()
        {
            return CryptoHelper.Decrypt(ConfigurationManager.ConnectionStrings["Archive"].ConnectionString);
        }

        public static int? GetWebServiceTimeOutInMiliSeconds()
        {
            int result = -10;
            int.TryParse(ConfigurationManager.AppSettings["ServiceTimeOutInSeconds"], out result);
            if (result < -1)
                return new int?();
            if (result == -1)
                return new int?(-1);
            else
                return new int?(result * 1000);
        }

        public static void SetCurrentCulture()
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["Language"] == null)
                return;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(HttpContext.Current.Session["Language"].ToString());
        }

        public static string WordWrap(string stringToWrap, byte wrapAfterThisManyChars)
        {
            if (string.IsNullOrEmpty(stringToWrap) || stringToWrap.Trim().Length <= (int)wrapAfterThisManyChars)
                return stringToWrap;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(stringToWrap.Substring(0, (int)wrapAfterThisManyChars) + "<WBR>");
            int startIndex = (int)wrapAfterThisManyChars;
            while (startIndex < stringToWrap.Length)
            {
                if (stringToWrap.Length - startIndex > (int)wrapAfterThisManyChars)
                    stringBuilder.Append(stringToWrap.Substring(startIndex, (int)wrapAfterThisManyChars) + "<WBR>");
                else
                    stringBuilder.Append(stringToWrap.Substring(startIndex));
                startIndex += (int)wrapAfterThisManyChars;
            }
            return ((object)stringBuilder).ToString();
        }

        public static bool MatchPasswordPolicy(DataTable dt, string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < Convert.ToInt32(dt.Rows[0]["MINIMUM_PASSWORD_LENGTH"]) || password.Length > Convert.ToInt32(dt.Rows[0]["MAXIMUM_PASSWORD_LENGTH"]))
                return false;
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            foreach (char ch in password)
            {
                StringUtility.CharType charType;
                if ((int)ch >= 48 && (int)ch <= 57)
                {
                    ++num2;
                    charType = StringUtility.CharType.Digit;
                }
                else if ((int)ch >= 97 && (int)ch <= 122 || (int)ch >= 65 && (int)ch <= 90)
                {
                    ++num1;
                    charType = StringUtility.CharType.Alphabet;
                }
                else
                {
                    ++num3;
                    charType = StringUtility.CharType.SpecialCharacter;
                }
                if (num5 == (int)ch)
                    ++num4;
                else
                    num4 = 1;
                num5 = (int)ch;
                switch (charType)
                {
                    case StringUtility.CharType.Alphabet:
                        if (num4 > Convert.ToInt32(dt.Rows[0]["ALPHABET_SUCCESSIVE_COUNT"].ToString()))
                            return false;
                        else
                            break;
                    case StringUtility.CharType.Digit:
                        if (num4 > Convert.ToInt32(dt.Rows[0]["NUMBER_SUCCESSIVE_COUNT"].ToString()))
                            return false;
                        else
                            break;
                    case StringUtility.CharType.SpecialCharacter:
                        if (num4 > Convert.ToInt32(dt.Rows[0]["SC_SUCCESSIVE_COUNT"].ToString()))
                            return false;
                        else
                            break;
                }
            }
            return num1 <= Convert.ToInt32(dt.Rows[0]["ALPHABET_MAXIMUM_COUNT"].ToString()) && num1 >= Convert.ToInt32(dt.Rows[0]["ALPHABET_MINIMUM_COUNT"].ToString()) && (num2 <= Convert.ToInt32(dt.Rows[0]["NUMBER_MAXIMUM_COUNT"].ToString()) && num2 >= Convert.ToInt32(dt.Rows[0]["NUMBER_MINIMUM_COUNT"].ToString())) && (num3 <= Convert.ToInt32(dt.Rows[0]["SC_MAXIMUM_COUNT"].ToString()) && num3 >= Convert.ToInt32(dt.Rows[0]["SC_MINIMUM_COUNT"].ToString()));
        }

        public static void ErrorPageRedirect(string pageTitle, string errorMsgGeneric, string errorMsgSpecific, string returnedPageRelativePath, HttpResponse response)
        {
            try
            {
                if (HttpContext.Current.Session == null || response == null)
                    return;
                HttpContext.Current.Session["ErrorMessageType"] = (object)((object)MessageType.Error).ToString();
                HttpContext.Current.Session["ErrorMessageHeader"] = (object)pageTitle;
                HttpContext.Current.Session["GenericErrorMessage"] = (object)errorMsgGeneric;
                HttpContext.Current.Session["DetailedErrorMessage"] = (object)string.Format("An unexpected error has occurred in our application, and the details has been logged.{0}\r\n                                                                            Please report this to administrator.{0} You may click the Back button to navigate off this page.", (object)Environment.NewLine);
                HttpContext.Current.Session["ReturnedToPage"] = (object)returnedPageRelativePath;
                response.Redirect("~/UI/ErrorPage.aspx");
                response.End();
            }
            catch
            {
            }
        }

        public static void ErrorPageRedirect(MessageType type, string pageTitle, string errorMsgGeneric, string errorMsgSpecific, string returnedPageRelativePath, HttpResponse response)
        {
            try
            {
                if (HttpContext.Current.Session == null || response == null)
                    return;
                HttpContext.Current.Session["ErrorMessageType"] = (object)((object)type).ToString();
                HttpContext.Current.Session["ErrorMessageHeader"] = (object)pageTitle;
                HttpContext.Current.Session["GenericErrorMessage"] = (object)errorMsgGeneric;
                HttpContext.Current.Session["DetailedErrorMessage"] = (object)string.Format("An unexpected error has occurred in our application, and the details has been logged.{0}\r\n                                                                            Please report this to administrator.{0} You may click the Back button to navigate off this page.", (object)Environment.NewLine);
                HttpContext.Current.Session["ReturnedToPage"] = (object)returnedPageRelativePath;
                response.Redirect("~/UI/ErrorPage.aspx");
                response.End();
            }
            catch
            {
            }
        }

        private enum CharType
        {
            Alphabet,
            Digit,
            SpecialCharacter,
        }
    }
}
