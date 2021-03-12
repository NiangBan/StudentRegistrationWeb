using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace StudentRegistrationWeb.Helper
{
    public class RijndaelCrypt
    {
        #region Private/Protected Member Variables

        /// <summary>
        /// Decryptor
        /// 
        private readonly ICryptoTransform _decryptor;

        /// <summary>
        /// Encryptor
        /// 
        private readonly ICryptoTransform _encryptor;

        /// <summary>
        /// 16-byte Private Key
        /// 
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("ThisIsUrPassword");
        private readonly byte[] _IV;

        /// <summary>
        /// Public Key
        /// 
        private readonly byte[] _password;

        /// <summary>
        /// Rijndael cipher algorithm
        /// 
        private readonly RijndaelManaged _cipher;

        #endregion

        #region Private/Protected Properties

        private ICryptoTransform Decryptor { get { return _decryptor; } }
        private ICryptoTransform Encryptor { get { return _encryptor; } }

        #endregion

        #region Private/Protected Methods
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// 
        /// <param name="password">Public key
        public RijndaelCrypt(string password)
        {
            //Encode digest
            var md5 = new MD5CryptoServiceProvider();
            _password = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            //Initialize objects
            _cipher = new RijndaelManaged();
            _decryptor = _cipher.CreateDecryptor(_password, IV);
            _encryptor = _cipher.CreateEncryptor(_password, IV);

        }

        /// <summary>
        /// Constructor
        /// 
        /// <param name="password">Public key
        public RijndaelCrypt(string password, string IV)
        {

            _IV = Encoding.ASCII.GetBytes(IV);
            //Encode digest
            var md5 = new MD5CryptoServiceProvider();
            _password = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            //Initialize objects
            _cipher = new RijndaelManaged();
            _decryptor = _cipher.CreateDecryptor(_password, _IV);
            _encryptor = _cipher.CreateEncryptor(_password, _IV);

        }

        #endregion

        #region Public Properties
        #endregion

        #region Public Methods

        public string Decrypt(string text)
        {
            try
            {
                byte[] input = Convert.FromBase64String(text);

                var newClearData = Decryptor.TransformFinalBlock(input, 0, input.Length);
                // return Encoding.ASCII.GetString(newClearData);
                return Encoding.UTF8.GetString(newClearData);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                return null;
            }
            catch (ObjectDisposedException oe)
            {
                Console.WriteLine("The object has already been disposed." + oe);
                return null;
            }


        }

        public string Encrypt(string text)
        {
            try
            {
                var buffer = Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(Encryptor.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                return null;
            }
            catch (ObjectDisposedException oe)
            {
                Console.WriteLine("The object has already been disposed." + oe);
                return null;
            }

        }


        public static string EncryptAES(string text, string inputKey, string IV)
        {
            var md5 = new MD5CryptoServiceProvider();
            var password = md5.ComputeHash(Encoding.ASCII.GetBytes(inputKey));
            var IVBytes = Encoding.UTF8.GetBytes(IV);

            //Initialize objects
            var cipher = new RijndaelManaged();
            var encryptor = cipher.CreateEncryptor(password, IVBytes);

            try
            {
                //var buffer = Encoding.ASCII.GetBytes(text);
                var buffer = Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(encryptor.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                return null;
            }
            catch (ObjectDisposedException oe)
            {
                Console.WriteLine("The object has already been disposed." + oe);
                return null;
            }
        }

        public static string DecryptAES(string text, string inputKey, string IV)
        {
            var md5 = new MD5CryptoServiceProvider();
            var password = md5.ComputeHash(Encoding.ASCII.GetBytes(inputKey));
            var IVBytes = Encoding.UTF8.GetBytes(IV);

            //Initialize objects
            var cipher = new RijndaelManaged();
            var decryptor = cipher.CreateDecryptor(password, IVBytes);

            try
            {
                byte[] input = Convert.FromBase64String(text);

                var newClearData = decryptor.TransformFinalBlock(input, 0, input.Length);
                //return Encoding.ASCII.GetString(newClearData);
                return Encoding.UTF8.GetString(newClearData);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                return null;
            }
            catch (ObjectDisposedException oe)
            {
                Console.WriteLine("The object has already been disposed." + oe);
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetUniqueKey(int size)
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        #endregion
    }
}