using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Helper
{
    /// <summary>
    /// MD5加密
    /// 2015-10-24 11:42 hemajun
    /// </summary>
    public class HelperMD5
    {
        /// <summary>
        /// 字符进行MD5加密
        /// </summary>
        public static string GetMD5(string str)
        {
            string strReturn = "";
            byte[] data = System.Text.Encoding.Unicode.GetBytes(str);
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] result = md5.ComputeHash(data);
                strReturn = BitConverter.ToString(result).Replace("-", "");
                return strReturn;
            }
        }  
    }
}
