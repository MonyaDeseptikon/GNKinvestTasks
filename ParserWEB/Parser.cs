using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static System.Net.WebRequestMethods;




namespace ParserWEB
{
    internal class Parser
    {

        internal List<string> spiderMain(string userQuery, string urlForSearch)
        {
            List<string> pictures = new List<string>();

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
            pictures.Add(ScreenshotMake(spider));

            string urlForLoad = GetURLFirstPicture(spider);
            pictures.Add(LoadPicture(urlForLoad));

            spider.Quit();

            /* DEBUG
            Console.WriteLine(pictures[0]);
            Console.WriteLine(pictures[1]);    
            */

            return pictures;
        }

        private IWebDriver CreateSpider()
        {
            string usedUA = "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 148.0.0.0 YaBrowser / 26.6.0.0 Safari / 537.36";

            string wellMaintainedUserProfile = Path.Combine(Directory.GetCurrentDirectory(), "WellMaintainedUserProfile");
            ChromeOptions spiderOptions = new ChromeOptions();
            //Создание нагулянного профиля пользователя
            spiderOptions.AddArgument($"user-data-dir={wellMaintainedUserProfile}");
            //Невидимый режим
            spiderOptions.AddArgument("--headless=new");
            //Отключение флага автоматизации
            spiderOptions.AddArgument("--disable-blink-features=AutomationControlled");
            //Отключение аппаратного ускорения графики
            spiderOptions.AddArgument("--disable-gpu");
            //Отключение систем безопасности
            //spiderOptions.AddArgument("--no-sandbox");
            //Интерфейс пользователя
            spiderOptions.AddArgument($"--user-agent={usedUA}");
            //Размер окна
            spiderOptions.AddArgument("--window-size=1920,1080");
            //Отключение ошибки отсутствия сетфиката
            spiderOptions.AddArgument("--ignore-certificate-errors");
            //Разрешить всплывающие окна
            spiderOptions.AddArgument("--disable-popup-blocking");
            //Рекомендация ИИ - современные антибот системы начали проверять этот флаг
            spiderOptions.AddArgument("--enable-automation");

            IWebDriver spider = new ChromeDriver(spiderOptions);

            return spider;
        }


        private string LoadPicture(string urlForLoad)
        {            
            string tempPath = Path.GetTempFileName();
            tempPath = Path.ChangeExtension(tempPath, ".png");
            byte[] imageBytes = new HttpClient().GetByteArrayAsync(urlForLoad).Result;
            System.IO.File.WriteAllBytes(tempPath, imageBytes);

            return tempPath;
        }
        private void inputQuery(string query, IWebDriver driver)
        {
            IWebElement boxForInput = driver.FindElement(By.CssSelector("textarea.HeaderForm-Input"));
            boxForInput.SendKeys(query);
            boxForInput.SendKeys(Keys.Enter);
        }

        private string GetURLFirstPicture(IWebDriver driver)
        {
            IWebElement? picture = null;
            try
            {
                picture = driver.FindElement(By.CssSelector(".SerpList-Content img"));
            } catch (WebDriverException)
            {
                Console.WriteLine($"!!!!!!!!!!!!!Картинка не доступна Парсеру");
                throw;
            }
            return picture.GetAttribute("src");
        }

        private string ScreenshotMake(IWebDriver driver)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string tempPath = Path.GetTempFileName();
            tempPath = Path.ChangeExtension(tempPath, ".png");
            ss.SaveAsFile(tempPath);

            return tempPath;
        }

        private void WaitForLoadTextBox(IWebDriver driver, int timeToSleep)
        {
            string waitingElement = "textarea.HeaderForm-Input";

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToSleep));
                wait.Until(d => d.FindElements(By.CssSelector(waitingElement)).Count > 0);
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
