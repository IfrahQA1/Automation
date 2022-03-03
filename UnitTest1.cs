using Automation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using Assert = NUnit.Framework.Assert;

namespace Automation
{
    public class references_paths
    {
        public string Url_vFlux { get; set; }
        public string Url_Mailinator { get; set; }
        public string Submit_xPath { get; set; }
        public string txtMailnatorUserName_css_selector { get; set; }
        public string verification_mail_xpath { get; set; }
        public string verification_code_xpath { get; set; }
        public string OTP_code_xpath { get; set; }
        public string OTP_code_sub_xpath { get; set; }


        public references_paths()
        {
            this.Url_vFlux = "http://34.75.248.77:9080/#/";
            this.Url_Mailinator = "https://www.mailinator.com/";
            this.Submit_xPath = "//*[@id='root']/div[2]/div[1]/div/div/div/div/div/form/div[7]/button";
            this.txtMailnatorUserName_css_selector = "#site-header > div.g5core-top-bar.g5core-top-bar-desktop > div > div > div.g5core-top-bar-left > div > button";
            this.verification_mail_xpath = "/html/body/div/main/div[2]/div[3]/div/div[4]/div/div/table/tbody/tr[1]/td[3]";
            this.verification_code_xpath = "/html/body/table/tbody/tr/td/div[1]/div/div/div/div/div/div/div[2]/p[3]";
            this.OTP_code_xpath = "//*[@id='root']/div[2]/div[1]/div/div/div/div/div/form/div[1]/div[1]/div[1]/input";
            this.OTP_code_sub_xpath = "/ html / body / div[2] / div[2] / div[1] / div / div / div / div / div / form / div[2] / button[2]";

        }
    } 
}
    

    public class Tests
    {
        //Browser Driver
        IWebDriver webDriver = new ChromeDriver();
        references_paths rp = new references_paths();
        


        [SetUp]
        public void Setup()
        {
            //Maximize chrome window
            webDriver.Manage().Window.Maximize();

        }

        [Test]
        public void Login()
        {
           
            //implicit wait
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Navigate to URL
            webDriver.Navigate().GoToUrl(rp.Url_vFlux);

            //Login_username
            var txtUserName = webDriver.FindElement(By.Name("Username"));

            txtUserName.SendKeys("ifrahuser");

            //Login_Password
            webDriver.FindElement(By.Name("Password")).SendKeys("Test@123");

            //Submit
            webDriver.FindElement(By.XPath(rp.Submit_xPath)).Submit();

        //Switch to Mailinatorverification_mail
        webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.SwitchTo().NewWindow(WindowType.Tab);
            webDriver.Navigate().GoToUrl(rp.Url_Mailinator);
            
            
            var txtMailnatorUserName = webDriver.FindElement(By.Id("search"));
            Assert.That(txtMailnatorUserName.Displayed, Is.True);
            txtMailnatorUserName.SendKeys("ifrahuser");
            
            webDriver.FindElement(By.CssSelector(rp.txtMailnatorUserName_css_selector)).Click();
            
           //Go for verification mail
            webDriver.FindElement(By.XPath(rp.verification_mail_xpath)).Click();
            
             //Get Verification_Code
            webDriver.SwitchTo().Frame("html_msg_body");
            webDriver.FindElement(By.ClassName("bg"));

            IWebElement vcode = webDriver.FindElement(By.XPath(rp.verification_code_xpath));

            string s = vcode.Text; //gets code
            Console.WriteLine(" " + s);
            
            webDriver.SwitchTo().Window(webDriver.WindowHandles.First());

            //OTP code input
            var OTP = webDriver.FindElement(By.XPath(rp.OTP_code_xpath));

            OTP.SendKeys(s);

            //OTP code submission
            webDriver.FindElement(By.XPath(rp.OTP_code_sub_xpath)).Click();

            var result = webDriver.FindElement(By.XPath("//*[@id='dd']/div/div[1]/div/div[2]"));

            bool flag = false;
            if (result.Text.Equals("ifrahuser"))
            {
                flag = true;
                // This method will return True when the page title matches with specified string
                Console.WriteLine("Successful Login, User Matched");
            }
            Assert.IsTrue(flag, "Page title is not matching with expected");


        }
        [TearDown]
        public void close_Browser()
        {
            //webDriver.Quit();
        }


}
