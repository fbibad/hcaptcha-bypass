using Newtonsoft.Json;
using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Security.Cryptography;

namespace hcapby
{
    public class CaptchaSolver
    {
        public CaptchaSolver()
        {
            Info.browser.Run();
        }
        dynamic getN()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://hcaptcha.com/checksiteconfig?host=" + Info.host + "&sitekey=" + Info.sitekey + "&sc=1&swa=1");
            httpWebRequest.Headers.GetType().InvokeMember("ChangeInternal",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                Type.DefaultBinder, httpWebRequest.Headers, new object[] { "Connection", "Keep-Alive"}
            );
            httpWebRequest.Method = "GET";
            httpWebRequest.Host = "hcaptcha.com";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.UserAgent = Info.useragent;
            httpWebRequest.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Headers.Add("Cache-Control", "no-cache");
            httpWebRequest.Referer = "https://newassets.hcaptcha.com/";
            httpWebRequest.Headers.Add("Origin", "https://newassets.hcaptcha.com");
            httpWebRequest.Headers.Add("Sec-Fetch-Dest", "empty");
            httpWebRequest.Headers.Add("Sec-Fetch-Mode", "cors");
            httpWebRequest.Headers.Add("Sec-Fetch-Site", "same-site");
            httpWebRequest.Headers.Add("Sec-GPC", "1");
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return JsonConvert.DeserializeObject(streamReader.ReadToEnd());
            }
        }
        public string SolveCaptcha(string sitekey = null, string host = null, string invitelink = null)
        {
            if (sitekey != null) Info.sitekey = sitekey;
            if (host != null) Info.host = host;
            if (invitelink != null) Info.invitelink = invitelink;
            dynamic N = getN();
            dynamic c = N.c;
            string actualN = getNreal(c);
            string body = $"v={Info.version}&sitekey={Info.sitekey}&host={Info.host}&hl=en&motionData={fuckingMotionData()}&n={actualN.Replace("\"", "")}&c={Uri.EscapeDataString(JsonConvert.SerializeObject(c))}";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://hcaptcha.com/getcaptcha?s=" + Info.sitekey);
            httpWebRequest.Headers.GetType().InvokeMember("ChangeInternal",
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
    Type.DefaultBinder, httpWebRequest.Headers, new object[] { "Connection", "Keep-Alive" }
);
            httpWebRequest.Method = "POST";
            httpWebRequest.Host = "hcaptcha.com";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Accept = "application/json";
            httpWebRequest.UserAgent = Info.useragent;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Headers.Add("Origin", "https://newassets.hcaptcha.com");
            httpWebRequest.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            httpWebRequest.Referer = "https://newassets.hcaptcha.com/";
            httpWebRequest.Headers.Add("Cookie", "hc_accessibility="+Info.accessabilityToken);
            httpWebRequest.Headers.Add("Sec-Fetch-Dest", "empty");
            httpWebRequest.Headers.Add("Sec-Fetch-Mode", "cors");
            httpWebRequest.Headers.Add("Sec-Fetch-Site", "same-site");
            httpWebRequest.Headers.Add("Sec-GPC", "1");
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                dynamic res = JsonConvert.DeserializeObject(streamReader.ReadToEnd());
                string _res = (string)res.generated_pass_UUID;
                if (string.IsNullOrWhiteSpace(_res)) return "CAPTCHA SOLVE FAILED";
                else return _res;
            }
        }
        string getNreal(dynamic c)
        {
            string requ = c.req;
            dynamic rq = JsonConvert.DeserializeObject(Codecs.Base64Decode(requ.Split('.')[1]));
            string rt = rq.l + "/" + c.type + ".js";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(rt);
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string script = streamReader.ReadToEnd();
                return JsonConvert.SerializeObject((dynamic)Info.browser.ExecuteJS(script + "return " + c.type + "(\"" + c.req + "\")"));
            }
        }
        string fuckingMotionData()
        {
            long l = getUnixTimestamp();
            string widget = Codecs.RandomString(12);
            string mot = $@"{{""st"":{l+806},""mm"":[[24,8,{l+ 1382}],[26,24,{l+1415}],[27,28,{l+ 1431}],[28,31,{l+ 1448}],[29,32,{l+ 1464}],[31,31,{l+ 1580}]],""mm-mp"":29.571428571428573,""md"":[[29,32,{l+ 1519}]],""md-mp"":0,""mu"":[[32,29,{l+ 1590}]],""mu-mp"":0,""v"":1,""topLevel"":{{""st"":{l},""sc"":{{""availWidth"":1920,""availHeight"":1040,""width"":1920,""height"":1080,""colorDepth"":24,""pixelDepth"":24,""availLeft"":0,""availTop"":0}},""nv"":{{""vendorSub"":"""",""productSub"":""20030107"",""vendor"":""Google Inc."",""maxTouchPoints"":0,""userActivation"":{{}},""brave"":{{}},""globalPrivacyControl"":true,""doNotTrack"":null,""geolocation"":{{}},""connection"":{{}},""webkitTemporaryStorage"":{{}},""webkitPersistentStorage"":{{}},""hardwareConcurrency"":4,""cookieEnabled"":true,""appCodeName"":""Mozilla"",""appName"":""Netscape"",""appVersion"":""5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36"",""platform"":""Win32"",""product"":""Gecko"",""userAgent"":""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36"",""language"":""en-US"",""languages"":[""en-US""],""onLine"":true,""webdriver"":false,""scheduling"":{{}},""bluetooth"":{{}},""clipboard"":{{}},""credentials"":{{}},""keyboard"":{{}},""managed"":{{}},""mediaDevices"":{{}},""storage"":{{}},""serviceWorker"":{{}},""wakeLock"":{{}},""deviceMemory"":1,""hid"":{{}},""locks"":{{}},""mediaCapabilities"":{{}},""mediaSession"":{{}},""permissions"":{{}},""presentation"":{{}},""usb"":{{}},""xr"":{{}},""userAgentData"":{{""brands"":[{{""brand"":""Chromium"",""version"":""93""}},{{""brand"":"" Not;A Brand"",""version"":""99""}}],""mobile"":false}},""plugins"":{{}}}},""dr"":"""",""inv"":false,""exec"":false,""wn"":[[1548,950,1,{l+2}]],""wn-mp"":0,""xy"":[[0,0,1,{l+3}]],""xy-mp"":0,""mm"":[[647,545,{l+1373}]],""mm-mp"":0}},""session"":[],""widgetList"":[""{widget}""],""widgetId"":""{widget}"",""href"":""https://discord.com/register"",""prev"":{{""escaped"":false,""passed"":false,""expiredChallenge"":false,""expiredResponse"":false}}}}";
            return Uri.EscapeDataString(mot);
        }
        int rnd(int min, int max) => Info.r.Next(min, max);
        string s(long l) => l.ToString();
        long getUnixTimestamp()
        {
            return ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
        }
    }
}
