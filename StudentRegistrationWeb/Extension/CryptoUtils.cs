using System;
using System.Security.Cryptography;
using System.Text;

namespace StudentRegistrationWeb.Extension
{
    public class CryptoUtils
    {
        public static string EncryptionKey = "560A18CD-6346-4CF0-A2E8-671F9B6B9EA9";
        public static string EncryptionIV = "CTfKxBSt6tkBv3E5";
        public static string TicketKey = "560A10CD-6346-4CF0-A2E8-671F9B6B9EA0";
        public static string TicketIV = "yiUl6VJyegpXqtz3";

        public string Encrypt(string plainText)
        {
            return Base64Encode(plainText);
        }

        public string Encrypt(string text, string key, string iv)
        {
            var md5 = new MD5CryptoServiceProvider();
            var password = md5.ComputeHash(Encoding.ASCII.GetBytes(key));
            var IVBytes = Encoding.UTF8.GetBytes(iv);

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
        public string Decrypt(string text, string key, string iv)
        {
            var md5 = new MD5CryptoServiceProvider();
            var password = md5.ComputeHash(Encoding.ASCII.GetBytes(key));
            var IVBytes = Encoding.UTF8.GetBytes(iv);

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
        public string GetUniqueKey(int size)
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

        public string EncryptForExtension(string plainText)
        {
            return Base64Encode(plainText);
            
        }
        public string DecryptForExtension(string encryptedText)
        {
            
            return Base64Decode(encryptedText);
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}