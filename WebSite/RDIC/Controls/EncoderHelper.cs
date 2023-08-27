using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RDIC.Controls
{
    public static class EncoderHelper
    {
        private static string mByteArray = "%$#>#%232s+as#l)URa0$!@";
        private static byte[] mInitializationVector = { 0x01, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xf7, 0xEF };
        public static string Encod(string inString)
        {
            return EncryptWithByteArray(inString, mByteArray);
        }
        public static string Decod(string inString)
        {
            return DecryptWithByteArray(inString, mByteArray);
        }
        private static string EncryptWithByteArray(string inString, string inByteArray)
        {
            try
            {
                byte[] tmpKey = new byte[20];
                tmpKey = System.Text.Encoding.UTF8.GetBytes(inByteArray.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(inString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(tmpKey, mInitializationVector), CryptoStreamMode.Write);
                cs.Write(inputArray, 0, inputArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string DecryptWithByteArray(string inString, string inByteArray)
        {
            try
            {
                byte[] tmpKey = new byte[20];
                tmpKey = System.Text.Encoding.UTF8.GetBytes(inByteArray.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(inString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(tmpKey, mInitializationVector), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}