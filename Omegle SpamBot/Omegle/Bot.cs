using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Omegle_SpamBot.Omegle
{
    internal class Bot : Configuration
    {
        private string _baseUrl = "https://www.omegle.com";

        private IWebDriver _driver;

        public Bot()
        {
            ReadConfiguration();

            _driver = new ChromeDriver(GetChromeOptions());
        }

        public void StartSpam()
        {
            _driver.Url = _baseUrl;

            // button textmode
            WaitUntilElementExists(By.Id("textbtn")).Click();

            // checkbox
            WaitUntilElementExists(By.XPath("/html/body/div[7]/div/p[1]/label/input")).Click();
            WaitUntilElementExists(By.XPath("/html/body/div[7]/div/p[2]/label/input")).Click();

            // confirm button
            WaitUntilElementExists(By.XPath("/html/body/div[7]/div/p[3]/input")).Click();

            // running js script
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript(GetJSCommand());
        }

        /// <summary> this will search for the element until a timeout is reached </summary>
        private IWebElement WaitUntilElementExists(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(driver => driver.FindElement(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Element with locator: '{elementLocator}' was not found in current context page.");
                throw;
            }
        }

    }
}
