using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace NjhLib.Utils
{
    public class Security
    {
        #region jiami jiemi

        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str2 = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str2 = str2 + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str2;
        }
        /// MD5 16位加密 加密后密码为小写
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string MD516(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }

        /// <summary>
        /// 把字符串转换为base64编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string s)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s));
        }
        public static string Base64Decrypt(string s)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }

        /// <summary>
        /// SHA1加密，不可逆转
        /// </summary>
        /// <param name="str">string str:被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string SHA1(string str)
        {
            System.Security.Cryptography.SHA1 s1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] byte1;
            byte1 = s1.ComputeHash(Encoding.Default.GetBytes(str));
            s1.Clear();
            return Convert.ToBase64String(byte1);
        }

        #region 进制转换

        /// <summary>
        /// 把十进制转换为toBase指定的进制数
        /// </summary>
        /// <param name="tenvalue"></param>
        /// <param name="toBase"></param>
        /// <returns></returns>
        public static string ConvertFromTen(Int64 tenvalue, int toBase)
        {
            string rs = string.Empty;
            Int64 result = tenvalue;
            while (result > 0)
            {
                Int64 val = result % toBase;
                rs = Convert1((int)val).ToString() + rs;
                result = (Int64)(result / toBase);
            }
            return rs;
        }

        /// <summary>
        /// 把toBase表示的进制数转换为十进制
        /// </summary>
        /// <param name="str"></param>
        /// <param name="toBase"></param>
        /// <returns></returns>
        public static Int64 ConvertToTen(string str, int toBase)
        {
            int length = str.Length;
            Int64 result = 0;
            for (int i = 0; i < length; i++)
            {
                Int64 val = (Int64)Math.Pow(toBase, (length - i - 1));
                char c = str[i];
                Int64 tmp = Convert1(c);
                result += tmp * val;
            }
            return result;
        }

        /// <summary>
        /// 10进制转换为62进制
        /// </summary>
        /// <param name="tenvalue"></param>
        /// <returns></returns>
        public static string ConvertTenToSixtyTwo(Int64 tenvalue)
        {
            string rs = string.Empty;
            Int64 result = tenvalue;
            while (result > 0)
            {
                Int64 val = result % 62;
                rs = Convert1((int)val).ToString() + rs;
                result = (Int64)(result / 62);
            }
            return rs;
        }

        /// <summary>
        /// 62进制转换为10进制
        /// </summary>
        /// <param name="sixtytwo"></param>
        /// <returns></returns>
        public static Int64 ConvertSixtytwoToTen(string sixtytwo)
        {
            int length = sixtytwo.Length;
            Int64 result = 0;
            for (int i = 0; i < length; i++)
            {
                Int64 val = (Int64)Math.Pow(62, (length - i - 1));
                char c = sixtytwo[i];
                Int64 tmp = Convert1(c);
                result += tmp * val;
            }
            return result;
        }

        private static int Convert1(char c)
        {
            switch (c)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;

                case 'a':
                    return 10;
                case 'b':
                    return 11;
                case 'c':
                    return 12;
                case 'd':
                    return 13;
                case 'e':
                    return 14;
                case 'f':
                    return 15;
                case 'g':
                    return 16;
                case 'h':
                    return 17;
                case 'i':
                    return 18;
                case 'j':
                    return 19;
                case 'k':
                    return 20;
                case 'l':
                    return 21;
                case 'm':
                    return 22;
                case 'n':
                    return 23;
                case 'o':
                    return 24;
                case 'p':
                    return 25;
                case 'q':
                    return 26;
                case 'r':
                    return 27;
                case 's':
                    return 28;
                case 't':
                    return 29;
                case 'u':
                    return 30;
                case 'v':
                    return 31;
                case 'w':
                    return 32;
                case 'x':
                    return 33;
                case 'y':
                    return 34;
                case 'z':
                    return 35;

                case 'A':
                    return 36;
                case 'B':
                    return 37;
                case 'C':
                    return 38;
                case 'D':
                    return 39;
                case 'E':
                    return 40;
                case 'F':
                    return 41;
                case 'G':
                    return 42;
                case 'H':
                    return 43;
                case 'I':
                    return 44;
                case 'J':
                    return 45;
                case 'K':
                    return 46;
                case 'L':
                    return 47;
                case 'M':
                    return 48;
                case 'N':
                    return 49;
                case 'O':
                    return 50;
                case 'P':
                    return 51;
                case 'Q':
                    return 52;
                case 'R':
                    return 53;
                case 'S':
                    return 54;
                case 'T':
                    return 55;
                case 'U':
                    return 56;
                case 'V':
                    return 57;
                case 'W':
                    return 58;
                case 'X':
                    return 59;
                case 'Y':
                    return 60;
                case 'Z':
                    return 61;
                default:
                    return 0;
            }


        }

        private static char Convert1(int val)
        {
            switch (val)
            {
                case 0:
                    return '0';
                case 1:
                    return '1';
                case 2:
                    return '2';
                case 3:
                    return '3';
                case 4:
                    return '4';
                case 5:
                    return '5';
                case 6:
                    return '6';
                case 7:
                    return '7';
                case 8:
                    return '8';
                case 9:
                    return '9';

                case 10:
                    return 'a';
                case 11:
                    return 'b';
                case 12:
                    return 'c';
                case 13:
                    return 'd';
                case 14:
                    return 'e';
                case 15:
                    return 'f';
                case 16:
                    return 'g';
                case 17:
                    return 'h';
                case 18:
                    return 'i';
                case 19:
                    return 'j';
                case 20:
                    return 'k';
                case 21:
                    return 'l';
                case 22:
                    return 'm';
                case 23:
                    return 'n';
                case 24:
                    return 'o';
                case 25:
                    return 'p';
                case 26:
                    return 'q';
                case 27:
                    return 'r';
                case 28:
                    return 's';
                case 29:
                    return 't';
                case 30:
                    return 'u';
                case 31:
                    return 'v';
                case 32:
                    return 'w';
                case 33:
                    return 'x';
                case 34:
                    return 'y';
                case 35:
                    return 'z';

                case 36:
                    return 'A';
                case 37:
                    return 'B';
                case 38:
                    return 'C';
                case 39:
                    return 'D';
                case 40:
                    return 'E';
                case 41:
                    return 'F';
                case 42:
                    return 'G';
                case 43:
                    return 'H';
                case 44:
                    return 'I';
                case 45:
                    return 'J';
                case 46:
                    return 'K';
                case 47:
                    return 'L';
                case 48:
                    return 'M';
                case 49:
                    return 'N';
                case 50:
                    return 'O';
                case 51:
                    return 'P';
                case 52:
                    return 'Q';
                case 53:
                    return 'R';
                case 54:
                    return 'S';
                case 55:
                    return 'T';
                case 56:
                    return 'U';
                case 57:
                    return 'V';
                case 58:
                    return 'W';
                case 59:
                    return 'X';
                case 60:
                    return 'Y';
                case 61:
                    return 'Z';
            }
            return '0';
        }
        #endregion




        #region aes加密 解密
        /// <summary>
        ///256位 aes加密方法
        /// </summary>
        /// <param name="toEncrypt">要加密的字符串</param>
        /// <param name="key">32 密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string toEncrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 256位 aes算法解密
        /// </summary>
        /// <param name="toDecrypt">要解密的字符串</param>
        /// <param name="key">密钥 32</param>
        /// <returns></returns>
        public static string AESDecrypt(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        private static string checkKeyLength(string key)
        {
            string origal = "!zIpuTdR@sEf=as6FygUoW8kQmGhlXcV";
            int length = key.Length;
            if (length < 32)
            {
                key += origal.Substring(0, 32 - length);
            }
            else if (length > 32)
            {
                key = MD5(key);
            }
            return key;
        }


        /// <summary>
        /// aes  2.0 返回62 进制
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEncrypt62(string toEncrypt, string key)
        {
            key = checkKeyLength(key);
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in resultArray)
            {
                //sb.AppendFormat("{0:X2}", b);
                sb.Append(ConvertTenToSixtyTwo(b).PadLeft(2, '0'));
            }
            // return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return sb.ToString();
        }
        /// <summary>
        /// aes解密 2.0 由62进制解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecrypt62(string toDecrypt, string key)
        {
            key = checkKeyLength(key);
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            //  byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            int halfInputLength = toDecrypt.Length / 2;
            byte[] toDecryptArray = new byte[halfInputLength];
            for (int x = 0; x < halfInputLength; x++)
            {
                Int64 i = ConvertSixtytwoToTen(toDecrypt.Substring(x * 2, 2));
                toDecryptArray[x] = (byte)i;
            }

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        /// <summary>
        /// aes  2.0 返回16 进制
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEncrypt2(string toEncrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in resultArray)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            // return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return sb.ToString();
        }

        /// <summary>
        /// aes解密 2.0  由16进制解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecrypt2(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            //  byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            int halfInputLength = toDecrypt.Length / 2;
            byte[] toEncryptArray = new byte[halfInputLength];
            for (int x = 0; x < halfInputLength; x++)
            {
                int i = (Convert.ToInt32(toDecrypt.Substring(x * 2, 2), 16));
                toEncryptArray[x] = (byte)i;
            }

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        /// <summary>
        /// aes 加密 4.0 
        /// </summary>
        /// <param name="toEncrypt">加密前字符串</param>
        /// <param name="key">32位的key</param>
        /// <returns>加密后字符串</returns>
        public static string AESEncryptHEX(string toEncrypt, string key)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(toEncrypt);

            aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
            aes.IV = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 16));

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        /// aes 解密 4.0
        /// </summary>
        /// <param name="toDecrypt">aes加密的字符串</param>
        /// <param name="key">32位解密密钥</param>
        /// <returns>加密前字符串</returns>
        public static string AESDecryptHEX(string toDecrypt, string key)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
            aes.IV = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 16));

            int halfInputLength = toDecrypt.Length / 2;
            byte[] inputByteArray = new byte[halfInputLength];
            for (int x = 0; x < halfInputLength; x++)
            {
                int i = (Convert.ToInt32(toDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }

        #endregion

        #region des加密 解密

        ///DES加密,可用
        public static string DESEncryptHEX(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            // ret.ToString();
            return ret.ToString();

        }
        ///DES解密 可
        public static string DESDecryptHEX(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// des 加密2
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string DESEncrypt2(string encryptString, string encryptKey)
        {
            encryptKey = GetSubString(encryptKey, 8, "");
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] keys = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            byte[] buffer = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, keys), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());

        }
        /// <summary>
        /// des解密2
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string DesDecrypt2(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = GetSubString(decryptKey, 8, "");
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
                byte[] keys = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
                byte[] buffer = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch
            {
                return "";
            }

        }
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string str = p_SrcString;
            if (p_Length < 0)
            {
                return str;
            }
            byte[] bytes = Encoding.Default.GetBytes(p_SrcString);
            if (bytes.Length <= p_StartIndex)
            {
                return str;
            }
            int length = bytes.Length;
            if (bytes.Length > (p_StartIndex + p_Length))
            {
                length = p_Length + p_StartIndex;
            }
            else
            {
                p_Length = bytes.Length - p_StartIndex;
                p_TailString = "";
            }
            int num2 = p_Length;
            int[] numArray = new int[p_Length];
            byte[] destinationArray = null;
            int num3 = 0;
            for (int i = p_StartIndex; i < length; i++)
            {
                if (bytes[i] > 0x7f)
                {
                    num3++;
                    if (num3 == 3)
                    {
                        num3 = 1;
                    }
                }
                else
                {
                    num3 = 0;
                }
                numArray[i] = num3;
            }
            if ((bytes[length - 1] > 0x7f) && (numArray[p_Length - 1] == 1))
            {
                num2 = p_Length + 1;
            }
            destinationArray = new byte[num2];
            Array.Copy(bytes, p_StartIndex, destinationArray, 0, num2);
            return (Encoding.Default.GetString(destinationArray) + p_TailString);
        }


        /// <summary>
        /// des 加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESEncode(string encryptString, string key)
        {
            //加密加密字符串是否为空
            if (string.IsNullOrEmpty(encryptString))
            {
                throw new ArgumentNullException("encryptString", "不能为空");
            }
            //加查密钥是否为空
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "不能为空");
            }
            //将密钥转换成字节数组
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            //设置初始化向量
            byte[] keyIV = keyBytes;
            //将加密字符串转换成UTF8编码的字节数组
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            //调用EncryptBytes方法加密
            byte[] resultByteArray = EncryptBytes(inputByteArray, keyBytes, keyIV);
            //将字节数组转换成字符串并返回
            return Convert.ToBase64String(resultByteArray);
        }
        public static string DESDecode(string decryptString, string key)
        {
            if (string.IsNullOrEmpty(decryptString))
            {
                throw new ArgumentNullException("decryptString", "不能为空");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "不能为空");
            }
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyIV = keyBytes;
            //将解密字符串转换成Base64编码字节数组
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            //调用DecryptBytes方法解密
            byte[] resultByteArray = DecryptBytes(inputByteArray, keyBytes, keyIV);
            //将字节数组转换成UTF8编码的字符串
            return Encoding.UTF8.GetString(resultByteArray);
        }
        /// <summary>
        /// 采用DES算法对字节数组解密
        /// </summary>
        /// <param name="sourceBytes">要加密的字节数组</param>
        /// <param name="keyBytes">算法的密钥，长度为8的倍数，最大长度64</param>
        /// <param name="keyIV">算法的初始化向量，长度为8的倍数，最大长度64</param>
        /// <returns></returns>
        public static byte[] DecryptBytes(byte[] soureBytes, byte[] keyBytes, byte[] keyIV)
        {
            if (soureBytes == null || keyBytes == null || keyIV == null)
            {
                throw new ArgumentNullException("soureBytes和keyBytes及keyIV", "不能为空。");
            }
            else
            {
                //检查密钥数组长度是否是8的倍数并且长度是否小于64
                keyBytes = CheckByteArrayLength(keyBytes);
                //检查初始化向量数组长度是否是8的倍数并且长度是否小于64
                keyIV = CheckByteArrayLength(keyIV);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(soureBytes, 0, soureBytes.Length);
                cStream.FlushFinalBlock();
                //将内存流转换成字节数组
                byte[] buffer = mStream.ToArray();
                mStream.Close();//关闭流
                cStream.Close();//关闭流
                return buffer;
            }
        }

        /// <summary>
        /// 采用DES算法对字节数组加密
        /// </summary>
        /// <param name="sourceBytes">要加密的字节数组</param>
        /// <param name="keyBytes">算法的密钥，长度为8的倍数，最大长度64</param>
        /// <param name="keyIV">算法的初始化向量，长度为8的倍数，最大长度64</param>
        /// <returns></returns>
        public static byte[] EncryptBytes(byte[] sourceBytes, byte[] keyBytes, byte[] keyIV)
        {
            if (sourceBytes == null || keyBytes == null || keyIV == null)
            {
                throw new ArgumentNullException("sourceBytes和keyBytes", "不能为空。");
            }
            else
            {
                //检查密钥数组长度是否是8的倍数并且长度是否小于64
                keyBytes = CheckByteArrayLength(keyBytes);
                //检查初始化向量数组长度是否是8的倍数并且长度是否小于64
                keyIV = CheckByteArrayLength(keyIV);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                //实例化内存流MemoryStream
                MemoryStream mStream = new MemoryStream();
                //实例化CryptoStream
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(sourceBytes, 0, sourceBytes.Length);
                cStream.FlushFinalBlock();
                //将内存流转换成字节数组
                byte[] buffer = mStream.ToArray();
                mStream.Close();//关闭流
                cStream.Close();//关闭流
                return buffer;
            }
        }
        /// <summary>
        /// 检查密钥或初始化向量的长度，如果不是8的倍数或长度大于64则截取前8个元素
        /// </summary>
        /// <param name="byteArray">要检查的数组</param>
        /// <returns></returns>
        private static byte[] CheckByteArrayLength(byte[] byteArray)
        {
            byte[] resultBytes = new byte[8];
            //如果数组长度小于8
            if (byteArray.Length < 8)
            {
                return Encoding.UTF8.GetBytes("12345678");
            }
            //如果数组长度不是8的倍数
            else if (byteArray.Length % 8 != 0 || byteArray.Length > 64)
            {
                Array.Copy(byteArray, 0, resultBytes, 0, 8);
                return resultBytes;
            }
            else
            {
                return byteArray;
            }
        }


        #region des3

        ///   <summary> 
        ///   3des加密字符串 
        ///   </summary> 
        ///   <param   name= "a_strString "> 要加密的字符串 </param> 
        ///   <param   name= "a_strKey "> 密钥 </param> 
        ///   <returns> 加密后并经base64编码的字符串 </returns> 
        ///   <remarks> 静态方法，采用默认ascii编码 </remarks> 
        public static string EncryptDES(string a_strString, string a_strKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();

            //TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            //MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            byte[] b = strToToHexByte(a_strKey);

            DES.Key = b;
            // DES.IV = b;
            //  DES.Key = b;//ASCIIEncoding.ASCII.GetBytes(a_strKey);//hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(a_strKey));
            DES.Mode = CipherMode.ECB;
            DES.Padding = PaddingMode.PKCS7;



            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = ASCIIEncoding.UTF8.GetBytes(a_strString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }//end   method 


        private static byte[] strToToHexByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        ///   <summary> 
        ///   3des解密字符串 
        ///   </summary> 
        ///   <param   name= "a_strString "> 要解密的字符串 </param> 
        ///   <param   name= "a_strKey "> 密钥 </param> 
        ///   <returns> 解密后的字符串 </returns> 
        ///   <exception   cref= " "> 密钥错误 </exception> 
        ///   <remarks> 静态方法，采用默认ascii编码 </remarks> 
        public static string DecryptDES(string a_strString, string a_strKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();


            DES.Key = strToToHexByte(a_strKey);
            DES.Mode = CipherMode.ECB;
            DES.Padding = PaddingMode.PKCS7;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            string result = " ";
            try
            {
                byte[] Buffer = Convert.FromBase64String(a_strString);
                result = ASCIIEncoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {

                throw (new Exception("Invalid   Key   or   input   string   is   not   a   valid   base64   string ", e));
            }

            return result;
        }//end   method 




        #endregion




        #endregion


        #endregion
    }
}
