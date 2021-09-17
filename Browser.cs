using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hcapby
{
    public class Browser
    {
        string chromedriverPATH = ""; //YOUR CHROMEDRIVER PATH
        public IWebDriver Driver { get; set; }
        public void Run(ChromeOptions browserOptions = null, bool headless = true)
        {
            if (browserOptions == null) browserOptions = new ChromeOptions();
            if (headless) browserOptions.AddArgument("-headless");
            Driver = new ChromeDriver(chromedriverPATH, browserOptions);
        }
        public object ExecuteJS(string script)
        {
            return ((IJavaScriptExecutor)Driver).ExecuteScript(script);
        }
        public void Close()
        {
            Driver.Close();
        }
    }
}
