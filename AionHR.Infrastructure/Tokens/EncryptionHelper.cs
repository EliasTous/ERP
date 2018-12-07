using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure
{
     class SHA
    {
        public SHA()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static byte[] HashSHA1(string plainText)
        {

            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();

            return
                SHA1.ComputeHash(
                Encoding.ASCII.GetBytes(plainText));

        }
    }

    public class EncryptionHelper
    {
        private const string basicAPIKey = "gpf485dksj2udskj39fj394kfj34kf2nmnvc921030cksfjfi29492kadhk";

        private const string initVector = "tu89geji340t89u2";
        private const int keysize = 256;
        public static string encrypt(string _text)
        {
            return EncryptionHelper.ByteArrToHex(SHA.HashSHA1(_text));
        }

        public static string encrypt(string _text, string _key)
        {
            if (_key == null)
                _key = basicAPIKey;
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(_text);
            PasswordDeriveBytes password = new PasswordDeriveBytes(_key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] Encrypted = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(Encrypted);
        }

        public static string decrypt(string encryptedText, string Key)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] DeEncryptedText = Convert.FromBase64String(encryptedText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[DeEncryptedText.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        public static string ByteArrToHex(byte[] bytearr)
        {
            string hexadecimal = "";
            foreach (byte b in bytearr)
            {
                hexadecimal += IntToHex(Convert.ToInt32(b), 2);
            }

            string res = "";


            for (int counter = 0; counter < hexadecimal.Length; counter += 8)
            {
                string s = hexadecimal.Substring(Convert.ToInt32(counter), 8);
                res += Convert.ToString(s[6]) + Convert.ToString(s[7]);
                res += Convert.ToString(s[4]) + Convert.ToString(s[5]);
                res += Convert.ToString(s[2]) + Convert.ToString(s[3]);
                res += Convert.ToString(s[0]) + Convert.ToString(s[1]);
            }

            return res;//hexadecimal;
        }

        public static string StringToHex(string hexstring)
        {
            string hexadecimal = "";
            foreach (char chr in hexstring)
            {
                //MessageBox.Show(chr.ToString() + "  " + IntToHex(Convert.ToInt32(chr)));
                hexadecimal += IntToHex(Convert.ToInt32(chr), 2);
            }

            return hexadecimal;
        }

        public static String IntToHex(int hexint)
        //  This method converts a integer into a hexadecimal string representing the
        //  int value. The returned string will look like this: 55FF. Note that there is
        //  no leading '#' in the returned string! 
        {
            int counter, reminder;
            String hexstr;

            counter = 1;
            hexstr = "";
            while (hexint + 15 > Math.Pow(16, counter - 1))
            {
                reminder = (int)(hexint % Math.Pow(16, counter));
                reminder = (int)(reminder / Math.Pow(16, counter - 1));
                if (reminder <= 9)
                {
                    hexstr = hexstr + (char)(reminder + 48);
                }
                else
                {
                    hexstr = hexstr + (char)(reminder + 55);
                }
                hexint -= reminder;
                counter++;
            }
            return ReverseString(hexstr);
        }

        public static String IntToHex(int hexint, int length)
        //  This version of the IntToHex method returns a hexadecimal string representing the
        //  int value in the given minimum length. If the hexadecimal string is = SqlReader.Short(er then the
        //  length parameter the missing characters will be filled up with leading zeroes.
        //  Note that the returend string though is not truncated if the value exeeds the length!
        {
            String hexstr, ret;
            int counter;
            hexstr = IntToHex(hexint);
            ret = "";
            if (hexstr.Length < length)
            {
                for (counter = 0; counter < (length - hexstr.Length); counter++)
                {
                    ret = ret + "0";
                }
            }
            return ret + hexstr;
        }

        public static String ReverseString(String inStr)
        //  Helper Method that reverses a String.
        {
            String outStr;
            int counter;
            outStr = "";
            for (counter = inStr.Length - 1; counter >= 0; counter--)
            {
                outStr = outStr + inStr[counter];
            }
            return outStr;
        }
    }
}
