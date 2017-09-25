using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DapperInfrastructure.DapperWrapper.Encrypt
{

    /// <summary>
    /// 数据库链接加密
    /// </summary>
    public static class DesCode
    {
        // KEY 8位
        private const string CodeKey = "lovelove";
        private static readonly byte[] Keys = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };

        public static string EncryptDes(string encryptString)
        {
            return EncryptDes(encryptString, CodeKey);
        }

        public static string DecryptDes(string decryptString)
        {
            return DecryptDes(decryptString, CodeKey);
        }

        private static string EncryptDes(string encryptString, string codeKey)
        {
            MemoryStream mStream = null;
            CryptoStream cStream = null;
            try
            {
                var rgbKey = Encoding.UTF8.GetBytes(codeKey);
                var rgbIv = Keys;
                var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var dcsp = new DESCryptoServiceProvider();
                mStream = new MemoryStream();
                cStream = new CryptoStream(mStream, dcsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                var str = Convert.ToBase64String(mStream.ToArray());
                return str;
            }
            catch (Exception)
            {
                return encryptString;
            }
            finally
            {
                if (mStream != null)
                {
                    mStream.Close();
                    mStream.Dispose();
                }
                if (cStream != null)
                {
                    cStream.Close();
                    cStream.Dispose();
                }
            }
        }
        private static string DecryptDes(string decryptString, string codeKey)
        {
            MemoryStream mStream = null;
            CryptoStream cStream = null;
            try
            {
                var rgbKey = Encoding.UTF8.GetBytes(codeKey);
                var rgbIv = Keys;
                var inputByteArray = Convert.FromBase64String(decryptString);
                var dcsp = new DESCryptoServiceProvider();
                mStream = new MemoryStream();
                cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                var str = Encoding.UTF8.GetString(mStream.ToArray());
                return str;
            }
            catch (Exception)
            {
                return decryptString;
            }
            finally
            {
                if (mStream != null)
                {
                    mStream.Close();
                    mStream.Dispose();
                }
                if (cStream != null)
                {
                    cStream.Close();
                    cStream.Dispose();
                }
            }
        }
    }
}