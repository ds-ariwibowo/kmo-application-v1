using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsSecurity.Encryption
{
    public static class SyahDesignCryptograhy
    {
        private static String strKey = "#eMedicV3.0@DSBYTES2007?"; //Key For Encrypt
        private static SyahDesign.Security.Cryptography.Algorithm algEncryption = SyahDesign.Security.Cryptography.Algorithm.Rijndael; //Type of Algorithm for Encryption
        private static SyahDesign.Security.Cryptography.EncodingType encEncodingType = SyahDesign.Security.Cryptography.EncodingType.BASE_64; //Encoding Type for Encryption

        /// <summary>
        /// Basic Function to Encrypt text
        /// </summary>
        /// <param name="StrToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string StrToEncrypt)
        {
            SyahDesign.Security.Cryptography.Key = strKey; //Set Key for Encryption
            SyahDesign.Security.Cryptography.EncryptionAlgorithm = algEncryption; //Set Algorithm Type
            SyahDesign.Security.Cryptography.Encoding = encEncodingType; //Set Econding Type
            if (SyahDesign.Security.Cryptography.EncryptString(StrToEncrypt)) //Encrypt the data if the encryption which will return true if the Encryption Success
            {
                return SyahDesign.Security.Cryptography.Content; //Return the Encryption Result as a string
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Function for Decrypt Encrypted Data
        /// </summary>
        /// <param name="StrToDecrypt"></param>
        /// <returns></returns>
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
