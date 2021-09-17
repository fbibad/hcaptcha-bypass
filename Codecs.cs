using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hcapby
{
    public class Codecs
    {
        public static string Base64Decode(string value)
        {
            try
            {
                var valueBytes = System.Convert.FromBase64String(value);
                return System.Text.Encoding.UTF8.GetString(valueBytes);
            }
            catch { return Base64Decode(value + "="); }
        }
        public static string RandomString(int length)
        {
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopgrstuvwxyz";
            string res = "";
            for (int i = 0; i < length; i++) res += chars[Info.r.Next(0, chars.Length)];
            return res;
        }
    }
}
