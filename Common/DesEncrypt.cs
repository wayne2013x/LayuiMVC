using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Common
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class DesEncrypt
    {
        #region 加密和解密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string str)
        {
            //定義Md5密碼服務類 
            MD5CryptoServiceProvider mdcpValu = new MD5CryptoServiceProvider();
            //將傳入的值轉換成UTF8格式。便於加密時的格式統一
            byte[] bDestination = Encoding.UTF8.GetBytes(str);
            //加密
            byte[] bDestinationMd5 = mdcpValu.ComputeHash(bDestination);
            //將加密后的值賦給字符串
            string asDestination = "";
            foreach (byte bVal in bDestinationMd5)
                asDestination += bVal.ToString();
            return asDestination;
        }

        /// <summary>
        /// DES加密方法
        /// </summary>
        /// <param name="strPlain">明文</param>
        /// <param name="strDESKey">密钥</param>
        /// <param name="strDESIV">向量</param>
        /// <returns>密文</returns>
        public static string Encode(string source, string _DESKey = "LayuiMVC")
        {
            StringBuilder sb = new StringBuilder();
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] key = ASCIIEncoding.ASCII.GetBytes(_DESKey);
                byte[] iv = ASCIIEncoding.ASCII.GetBytes(_DESKey);
                byte[] dataByteArray = Encoding.UTF8.GetBytes(source);
                des.Mode = System.Security.Cryptography.CipherMode.CBC;
                des.Key = key;
                des.IV = iv;
                string encrypt = "";
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
                return encrypt;
            }
        }

        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的base64串</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string Decode(string source, string sKey = "LayuiMVC")
        {
            try
            {
                byte[] inputByteArray = System.Convert.FromBase64String(source);//Encoding.UTF8.GetBytes(source);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    string str = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                    return str;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion 加密和解密
    }
}
