using System;
using System.Security.Cryptography;
using System.Text;

namespace StudentRegistrationWeb.Extension
{
    public class CommonUtils
    {
        public static string UserType = "Corporate";
        public static string ClientVersion = "1.0";
        public static string FirebaseToken = "123";
        public static string DeviceModel = "123";
        public static string DeviceID = "123";
        public static string DeviceOS = "Browser";
        public static string Secure_Url_Prefix = "secure";
        public static string Root_Url_Prefix = "/business"; // Default Value [/business]
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }

        public static string SHA256HexHashString(string stringIn)
        {

            string saltedcode = EncodedbySalted(System.Web.HttpContext.Current.Session["UserNameForSalted"].ToString());//salted user name
            string hashString;
            using (var sha256 = SHA256Managed.Create())
            {
                var hash = sha256.ComputeHash(Encoding.Default.GetBytes(stringIn + saltedcode));
                hashString = ToHex(hash, false);
            }

            return hashString;
        }

        public static string EncodedbySalted(string decodestring)
        {

            decodestring = decodestring.ToLower().Replace("a", "@").Replace("i", "!").Replace("l", "1").Replace("e", "3").Replace("o", "0").Replace("s", "$").Replace("n", "&");
            return decodestring;


        }

        public static string AESKeyForTicket()
        {
            return "560A10CD-6346-4CF0-A2E8-671F9B6B9EA0";
        }

        public static string AESIVForTicket()
        {
            return "yiUl6VJyegpXqtz3";
        }

        public static string HardCodeKeyForAES()
        {
            return "560A18CD-6346-4CF0-A2E8-671F9B6B9EA9";
        }

        public static string HardCodeIVForAES()
        {
            return "CTfKxBSt6tkBv3E5";
        }

        //convert from decimal format to (string)       
        public static string ConvertDecimalToStr(Decimal DecValue)
        {
            return string.Format("{0:N2}", DecValue);
        }
    }
}
