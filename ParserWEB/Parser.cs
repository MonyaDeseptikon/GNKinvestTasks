using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace ParserWEB
{
    public class Parser
    {

        public void spiderMain(string urlForSearch, string userQuery)
        {
            IWebDriver spider = CreateSpider();
            spider.Navigate().GoToUrl(urlForSearch);
            WaitForLoadTextBox(spider, 10);

            inputQuery(userQuery, spider);
            WaitForLoadFirstClass(spider, 10);


        }

            private IWebDriver CreateSpider() {
            
            ChromeOptions spiderOptions = new ChromeOptions();
            spiderOptions.AddArgument("--headless=new");
            spiderOptions.AddArgument("--disable-blink-features=AutomationControlled");

            string usedUA = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 148.0.0.0 YaBrowser / 26.6.0.0 Safari / 537.36";
            spiderOptions.AddArgument($"--user-agent={usedUA}");

            spiderOptions.AddArgument("--window-size=1920,1080");
          
            IWebDriver spider = new ChromeDriver(spiderOptions);

            return spider;
        }

        private void WaitForLoadTextBox(IWebDriver driver, int timeToSleep)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToSleep));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("textarea.mini-suggest__input")));  
        }

        private void WaitForLoadFirstClass(IWebDriver driver, int timeToSleep)
        {
           var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToSleep));
           wait.Until(d => d.FindElements(By.CssSelector(".serp-item")).Count > 0);
        }

        private void inputQuery(string query, IWebDriver driver)
        {
            IWebElement boxForInput = driver.FindElement(By.CssSelector("textarea.mini-suggest__input"));
            boxForInput.Click();
            boxForInput.SendKeys(query);
            boxForInput.SendKeys(Keys.Enter);
        }


    }
}
