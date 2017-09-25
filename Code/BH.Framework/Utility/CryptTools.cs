using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BH.Framework.Utility
{

    /// <summary>
    /// 加密/解密辅助类
    /// </summary>
    public static class CryptTools
    {
         

        /// <summary>
        /// TripleDES加密
        /// </summary>
        /// <param name="content">需要加密的明文内容</param>
        /// <param name="secret">加密密钥</param>
        /// <returns>返回加密后密文字符串</returns>
        public static string Encrypt(string content, string secret) 
        {
            if ((content == null) || (secret == null) || (content.Length == 0) || (secret.Length == 0))
                throw new ArgumentNullException("Invalid Argument");

            byte[] key = GetKey(secret);
            byte[] contentByte = Encoding.Unicode.GetBytes(content);
            var msTicket = new MemoryStream();

            msTicket.Write(contentByte, 0, contentByte.Length);

            byte[] contentCryptByte = Crypt(msTicket.ToArray(), key);

            string contentCryptStr = Encoding.ASCII.GetString(Base64Encode(contentCryptByte));

            return contentCryptStr;
        }

        /// <summary>
        /// TripleDES解密 
        /// </summary>
        /// <param name="content">需要解密的密文内容</param>
        /// <param name="secret">解密密钥</param>
        /// <returns>返回解密后明文字符串</returns>
        public static string Decrypt(string content, string secret)
        {
            if ((content == null) || (secret == null) || (content.Length == 0) || (secret.Length == 0))
                throw new ArgumentNullException("Invalid Argument");

            byte[] key = GetKey(secret);

            byte[] cryByte = Base64Decode(Encoding.ASCII.GetBytes(content));
            byte[] decByte = Decrypt(cryByte, key);

            byte[] realDecByte = decByte;
            byte[] prefix = new byte[Constants.Operation.UnicodeReversePrefix.Length];
            Array.Copy(realDecByte, prefix, 2);

            if (CompareByteArrays(Constants.Operation.UnicodeReversePrefix, prefix))
            {
                for (int i = 0; i < realDecByte.Length - 1; i = i + 2)
                {
                    byte switchTemp = realDecByte[i];
                    realDecByte[i] = realDecByte[i + 1];
                    realDecByte[i + 1] = switchTemp;
                }
            }
            return Encoding.Unicode.GetString(realDecByte);
        }

        /// <summary>
        /// TripleDES加密
        /// </summary>
        /// <param name="source">需要加密的密文内容</param>
        /// <param name="key">加密密钥</param>
        /// <returns>返回加密后密文字节</returns>
        public static byte[] Crypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = new TripleDESCryptoServiceProvider();
            dsp.Mode = CipherMode.ECB;

            ICryptoTransform des = dsp.CreateEncryptor(key, null);

            return des.TransformFinalBlock(source, 0, source.Length);
        }


        /// <summary>
        /// TripleDES解密
        /// </summary>
        /// <param name="source">需要解密的密文内容</param>
        /// <param name="key">解密密钥</param>
        /// <returns>返回解密后密文字节</returns>
        public static byte[] Decrypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentNullException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = new TripleDESCryptoServiceProvider();
            dsp.Mode = CipherMode.ECB;

            ICryptoTransform des = dsp.CreateDecryptor(key, null);

            byte[] ret = new byte[source.Length + 8];

            int num = des.TransformBlock(source, 0, source.Length, ret, 0);

            ret = des.TransformFinalBlock(source, 0, source.Length);
            ret = des.TransformFinalBlock(source, 0, source.Length);
            num = ret.Length;

            byte[] realByte = new byte[num];
            Array.Copy(ret, realByte, num);
            ret = realByte;
            return ret;
        }

        //原始base64编码
        public static byte[] Base64Encode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            ToBase64Transform tb64 = new ToBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            while (pos + 3 < source.Length)
            {
                buff = tb64.TransformFinalBlock(source, pos, 3);
                stm.Write(buff, 0, buff.Length);
                pos += 3;
            }

            buff = tb64.TransformFinalBlock(source, pos, source.Length - pos);
            stm.Write(buff, 0, buff.Length);

            return stm.ToArray();

        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static  string  Md5(string  source)
        { 
            // ReSharper disable PossibleNullReferenceException
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5").ToLower();
            // ReSharper restore PossibleNullReferenceException
        }

        //原始base64解码
        public static byte[] Base64Decode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            FromBase64Transform fb64 = new FromBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            while (pos + 4 < source.Length)
            {
                buff = fb64.TransformFinalBlock(source, pos, 4);
                stm.Write(buff, 0, buff.Length);
                pos += 4;
            }

            buff = fb64.TransformFinalBlock(source, pos, source.Length - pos);
            stm.Write(buff, 0, buff.Length);
            return stm.ToArray();

        }


        /// <summary>
        /// 类型转换（String -> byte[]）
        /// </summary>
        /// <param name="secret">字符串</param>
        /// <returns>返回byte[]类型</returns>
        public static byte[] GetKey(string secret)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentException("Secret is not valid");

            ASCIIEncoding ae = new ASCIIEncoding();
            byte[] temp = Hash(ae.GetBytes(secret));

            byte[] ret = new byte[Constants.Operation.KeySize];

            if (temp.Length < Constants.Operation.KeySize)
            {
                System.Array.Copy(temp, 0, ret, 0, temp.Length);
                int i;
                for (i = temp.Length; i < Constants.Operation.KeySize; i++)
                {
                    ret[i] = 0;
                }
            }
            else
                System.Array.Copy(temp, 0, ret, 0, Constants.Operation.KeySize);

            return ret;
        }

        /// <summary>
        /// 比较两个byte数组是否相同
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool CompareByteArrays(byte[] source, byte[] dest)
        {
            if ((source == null) || (dest == null))
                throw new ArgumentException("source or dest is not valid");

            if (source.Length != dest.Length)
                return false;
            else
                if (source.Length == 0)
                    return true;

            return !source.Where((t, i) => t != dest[i]).Any();
        }

        /// <summary>
        /// 使用md5计算散列
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] Hash(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            MD5 m = MD5.Create();
            return m.ComputeHash(source);
        }

        /// <summary>
        /// 对传入的明文密码进行Hash加密,密码不能为中文
        /// </summary>
        /// <param name="oriPassword">需要加密的明文密码</param>
        /// <returns>经过Hash加密的密码</returns>
        public static string HashPassword(string oriPassword)
        {
            if (string.IsNullOrEmpty(oriPassword))
                throw new ArgumentException("oriPassword is valid");

            ASCIIEncoding acii = new ASCIIEncoding();
            byte[] hashedBytes = Hash(acii.GetBytes(oriPassword));

            StringBuilder sb = new StringBuilder(30);
            foreach (byte b in hashedBytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString().ToLower();
        }
    }

    /// <summary>
    /// 类名称   ：Constants
    /// 类说明   ：加解密算法常量.
    /// 作者     ：
    /// 完成日期 ：
    /// </summary>
    public class Constants
    {
        public struct Operation
        {
            public static readonly int KeySize = 24;
            public static readonly byte[] UnicodeOrderPrefix = new byte[2] { 0xFF, 0xFE };
            public static readonly byte[] UnicodeReversePrefix = new byte[2] { 0xFE, 0xFF };
        }
    }
}