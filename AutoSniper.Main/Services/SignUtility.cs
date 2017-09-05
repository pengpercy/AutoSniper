using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

namespace AutoSniper.Main
{
    class SignUtility
    {
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="secretkey"></param>
        /// <returns></returns>

        public static string MakeSign(string queryString, string secretkey)
        {
            var encryptedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("SHA")).ComputeHash(Encoding.UTF8.GetBytes(secretkey.Trim()));
            var stringBuilder = new StringBuilder();
            Array.ForEach(encryptedBytes, s => { stringBuilder.AppendFormat("{0:x2}", s); });
            secretkey = stringBuilder.ToString();

            byte[] k_ipad = new byte[64];
            byte[] k_opad = new byte[64];

            var keyb = Encoding.UTF8.GetBytes(secretkey);

            for (int i = keyb.Length; i < 64; i++)
            {
                k_ipad[i] = 54;
                k_opad[i] = 92;
            }

            for (int i = 0; i < keyb.Length; i++)
            {
                k_ipad[i] = (byte)(keyb[i] ^ 0x36);
                k_opad[i] = (byte)(keyb[i] ^ 0x5c);
            }

            byte[] sMd5_1 = new MD5CryptoServiceProvider().ComputeHash(k_ipad.Concat(Encoding.UTF8.GetBytes(queryString)).ToArray());
            encryptedBytes = new MD5CryptoServiceProvider().ComputeHash(k_opad.Concat(sMd5_1).ToArray());
            stringBuilder = new StringBuilder();
            Array.ForEach(encryptedBytes, s => { stringBuilder.AppendFormat("{0:x2}", s); });
            return stringBuilder.ToString();
        }

    }
}
