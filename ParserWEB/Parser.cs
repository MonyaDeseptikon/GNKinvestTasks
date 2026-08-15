using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;
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

        public void spiderMain(string userQuery, string urlForSearch)
        {
            IWebDriver spider = CreateSpider();
            try
            {
                spider.Navigate().GoToUrl(urlForSearch);
            }
            catch (WebDriverException)
            {
                Console.WriteLine("!!!!!!!!!!!!!Браузер не смог открыть предоставленный URL");                   
            }
            ClosePopUpElement(spider);
            WaitForLoadTextBox(spider, 10);                           
            
            inputQuery(userQuery, spider);
            ClosePopUpElement(spider);
            WaitForLoadFirstClass(spider, 20);
            ScreenshotMake(spider, "Открытие поисковой страницы.png");

            OpenFirstPicture(spider);
            WaitForLoadFirstClass(spider, 10);
            ScreenshotMake(spider, "Результат поиска.png");

            spider.Quit();
        }

            private IWebDriver CreateSpider() {
            string usedUA = "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 148.0.0.0 YaBrowser / 26.6.0.0 Safari / 537.36";

            string wellMaintainedUserProfile = Path.Combine(Directory.GetCurrentDirectory(), "WellMaintainedUserProfile");                  
            ChromeOptions spiderOptions = new ChromeOptions();
            //spiderOptions.AddArgument($"user-data-dir={wellMaintainedUserProfile}");
            spiderOptions.AddArgument("--headless=new");
            spiderOptions.AddArgument("--disable-blink-features=AutomationControlled");
            spiderOptions.AddArgument("--disable-dev-shm-usage");
            spiderOptions.AddArgument("--disable-gpu");
            spiderOptions.AddArgument("--no-sandbox");            
            spiderOptions.AddArgument($"--user-agent={usedUA}");
            spiderOptions.AddArgument("--window-size=1920,1080");
          
            IWebDriver spider = new ChromeDriver(spiderOptions);

            return spider;
        }

        

        private void inputQuery(string query, IWebDriver driver)
        {
            IWebElement boxForInput = driver.FindElement(By.CssSelector("textarea"));        
            boxForInput.SendKeys(query);
            boxForInput.SendKeys(Keys.Enter);
        }

        private void OpenFirstPicture(IWebDriver driver)
        {
            IWebElement picture = driver.FindElement(By.CssSelector(".SerpList-Content"));
            picture.Click();
        }

        private void ScreenshotMake(IWebDriver driver, string picName)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            //string tempPath = Path.GetTempFileName();
            string tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), picName);
            //string tempPath = Path.Combine(Path.GetTempFileName(), picName);
            ss.SaveAsFile(tempPath);
        }

        private void WaitForLoadTextBox(IWebDriver driver, int timeToSleep)
        {
            string waitingElement = "textarea";

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToSleep));
              wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(waitingElement)));
              
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                Console.WriteLine($"!!!!!!!!!!!!!Время ожидания истекло, нужный элемент {waitingElement} не доступен Парсеру");               
            }
        }

        private void WaitForLoadFirstClass(IWebDriver driver, int timeToSleep)
        {
            string waitingElement = ".SerpList-Content";

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToSleep));
                wait.Until(d => d.FindElements(By.CssSelector(waitingElement)).Count > 0);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"!!!!!!!!!!!!!Время ожидания истекло, нужный элемент {waitingElement} не доступен Парсеру");
         
            }
        }

        private void ClosePopUpElement(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            try
            {
                var continueButton = wait.Until(d => d.FindElements(By.XPath("//*[local-name()='button' or local-name()='a'][descendant-or-self::*[contains(text(), 'Продолжить')]] | " +
            "//*[local-name()='button' or local-name()='a'][descendant-or-self::*[contains(text(), 'Закрыть')]]")).FirstOrDefault());

                if (continueButton != null && continueButton.Displayed)
                {
                    continueButton.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("!!!!!!!!!!!!!ПопАп объекта нет");
            }

        }
    }
}
