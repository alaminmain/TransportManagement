using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TransportManagerLibrary.UtilityClass
{
    public static class CryptoHelper
    {
        private static byte[] bytes = Encoding.ASCII.GetBytes("ZeroCool");

        static CryptoHelper()
        {
        }

        public static string Encrypt(string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                throw new ArgumentNullException("The string which needs to be encrypted can not be null or empty.");
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(CryptoHelper.bytes, CryptoHelper.bytes), CryptoStreamMode.Write);
            StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream);
            streamWriter.Write(originalString);
            ((TextWriter)streamWriter).Flush();
            cryptoStream.FlushFinalBlock();
            ((TextWriter)streamWriter).Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(string cryptedString)
        {
            if (string.IsNullOrEmpty(cryptedString))
                throw new ArgumentNullException("The string which needs to be decrypted can not be null or empty.");
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            return new StreamReader((Stream)new CryptoStream((Stream)new MemoryStream(Convert.FromBase64String(cryptedString)), cryptoServiceProvider.CreateDecryptor(CryptoHelper.bytes, CryptoHelper.bytes), CryptoStreamMode.Read)).ReadToEnd();
        }

        public static string DESEncrypt(string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                throw new ArgumentNullException("The string which needs to be encrypted can not be null or empty.");
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(CryptoHelper.bytes, CryptoHelper.bytes), CryptoStreamMode.Write);
            StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream);
            streamWriter.Write(originalString);
            ((TextWriter)streamWriter).Flush();
            cryptoStream.FlushFinalBlock();
            ((TextWriter)streamWriter).Flush();
            return Encoding.GetEncoding("iso-8859-1").GetString(memoryStream.ToArray());
        }

        public static string DESDecrypt(string cryptedString)
        {
            if (string.IsNullOrEmpty(cryptedString))
                throw new ArgumentNullException("The string which needs to be decrypted can not be null or empty.");
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            return new StreamReader((Stream)new CryptoStream((Stream)new MemoryStream(Encoding.GetEncoding("iso-8859-1").GetBytes(cryptedString)), cryptoServiceProvider.CreateDecryptor(CryptoHelper.bytes, CryptoHelper.bytes), CryptoStreamMode.Read)).ReadToEnd();
        }

        public static string ComputeHashSHA256(string fileContents)
        {
            if (string.IsNullOrEmpty(fileContents))
                throw new ArgumentNullException("The file contents to be hashed is null or empty.");
            SHA256 shA256 = SHA256.Create();
            shA256.Initialize();
            byte[] hash = shA256.ComputeHash((Stream)new MemoryStream(Encoding.GetEncoding("iso-8859-1").GetBytes(fileContents)));
            shA256.Clear();
            if (hash == null && hash.Length < 1)
                return (string)null;
            else
                return new StreamReader((Stream)new MemoryStream(hash)).ReadToEnd();
        }


        /// <summary>
        /// Get random salt for password hash.
        /// </summary>
        /// <returns>return salt as string </returns>
        public static string GetSalt()
        {
            var saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);

        }
        /// <summary>
        /// Computes a salted hash of the password and salt provided and returns as a base64 encoded string.
        /// </summary>
        /// <param name="salt">The salt to use in the hash.</param>
        /// <param name="password">The password to hash.</param>
        /// <returns></returns>
        public static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(32));
        }
    }
}
