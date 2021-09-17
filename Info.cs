using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hcapby
{
    public static class Info
    {
        public static Browser browser = new Browser();
        public static string sitekey = "f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34";
        public static string host = "discord.com";
        public static string useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36";
        public static string version = "a786a0d";
        public static Random r = new Random();
        public static CaptchaSolver captchasolver = new CaptchaSolver();
    }
}
