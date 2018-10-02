using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsSecurity.Encryption
{
    public static class SyahDesignCryptograhy
    {
        private static String strKey = "#eMedicV3.0@DSBYTES2007?";
        private static SyahDesign.Security.Cryptography.Algorithm algEncryption = SyahDesign.Security.Cryptography.Algorithm.Rijndael;
        private static SyahDesign.Security.Cryptography.EncodingType encEncodingType = SyahDesign.Security.Cryptography.EncodingType.BASE_64;

        public static string Encrypt(string StrToEncrypt)
        {
            SyahDesign.Security.Cryptography.Key = strKey;
            SyahDesign.Security.Cryptography.EncryptionAlgorithm = algEncryption;
            SyahDesign.Security.Cryptography.Encoding = encEncodingType;
            if (SyahDesign.Security.Cryptography.EncryptString(StrToEncrypt))
            {
                return SyahDesign.Security.Cryptography.Content;
            }
            else
            {
                return "";
            }
        }
        public static String Decrypt(String StrToDecrypt)
        {
            SyahDesign.Security.Cryptography.Key = strKey;
            SyahDesign.Security.Cryptography.EncryptionAlgorithm = algEncryption;
            SyahDesign.Security.Cryptography.Encoding = encEncodingType;
            SyahDesign.Security.Cryptography.Content = StrToDecrypt;
            if (SyahDesign.Security.Cryptography.DecryptString())
            {
                return SyahDesign.Security.Cryptography.Content;
            }
            else
            {
                return "";
            }
        }
    }
}
