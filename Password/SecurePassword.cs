﻿using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Password {
    class SecurePassword {
        public static class Global {
            public const String strPermutation = "ouiveyxaqtd";
            public const Int32 bytePermutation1 = 0x19;
            public const Int32 bytePermutation2 = 0x59;
            public const Int32 bytePermutation3 = 0x17;
            public const Int32 bytePermutation4 = 0x41;
        }

        public static string Encrypt(string strData) {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));
        }

        private static byte[] Encrypt(byte[] strData) {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Global.strPermutation,
            new byte[] { Global.bytePermutation1,
                             Global.bytePermutation2,
                             Global.bytePermutation3,
                             Global.bytePermutation4});

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        private static byte[] Decrypt(byte[] strData) {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Global.strPermutation,
            new byte[] { Global.bytePermutation1,
                             Global.bytePermutation2,
                             Global.bytePermutation3,
                             Global.bytePermutation4});

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}
