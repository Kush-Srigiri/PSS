using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.IO;
using System.Security.Cryptography;
using System;
using CredentialManagement;

namespace Project.scripts
{

    public class Crypt
    {
        private const string KeyName = "Project"; 
        private const int SaltSize = 16;
        private const int KeySize = 16;
        private readonly byte[] _key;
        private readonly string _keyIdentifier;


        public Crypt(string password, string keyIdentifier = null)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));

            _keyIdentifier = keyIdentifier ?? Guid.NewGuid().ToString();
            _key = RetrieveOrGenerateKey(password);

        }

        private byte[] RetrieveOrGenerateKey(string password)
        {
            // Try to retrieve the key from Windows Credential Store
            byte[] storedKey = RetrieveKeyFromCredentialManager();
            if (storedKey != null)
                return storedKey;

            // If no key is found, generate a new one using PBKDF2
            byte[] newKey = GenerateKey(password);
            StoreKeyInCredentialManager(newKey);
            return newKey;
        }

        private byte[] GenerateKey(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(KeySize);
            }
        }


        private void StoreKeyInCredentialManager(byte[] key)
        {
            using (var cred = new Credential())
            {
                cred.Username = "Project";
                cred.Password = Convert.ToBase64String(key);
                cred.Target = KeyName;
                cred.Type = CredentialType.Generic;
                cred.PersistanceType = PersistanceType.LocalComputer;
                cred.Save();
            }
        }

        private byte[] RetrieveKeyFromCredentialManager()
        {
            using (var cred = new Credential())
            {
                cred.Target = KeyName;
                if (cred.Load())
                {
                    return Convert.FromBase64String(cred.Password);
                }
            }
            return null; 
        }

        public byte[] Encrypt(string plainText)
        {
            if (plainText == null)
                throw new ArgumentNullException(nameof(plainText));

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return ms.ToArray();
                }
            }
        }

        public string Decrypt(byte[] cipherText)
        {
            if (cipherText == null || cipherText.Length < 16)
                throw new ArgumentException("Invalid cipher text", nameof(cipherText));

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;

                byte[] iv = new byte[16];
                Array.Copy(cipherText, 0, iv, 0, iv.Length);
                aes.IV = iv;

                using (MemoryStream ms = new MemoryStream(cipherText, 16, cipherText.Length - 16))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }


}
