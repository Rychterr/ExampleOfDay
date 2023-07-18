using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qatest
{
    class Functions
    {
        IWebDriver _driver;
        private ReadOnlyCollection<IWebElement> _elements;

        public Functions(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        private static Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public void FailureInformation(string _information)
        {
            Console.WriteLine("_______Failure_______");
            for (int _i = 0; _i < 2; _i++)
            {
                Console.WriteLine(_information);
            }
            Console.WriteLine("___End of failure___");
        }
        public void SuccessInformation(string _information)
        {
            Console.WriteLine("_______Success_______");
                Console.WriteLine(_information);
            Console.WriteLine("___End of success___");
        }
        public void WaitForElementToClickableClick(IWebElement _webElement)
        {
            try
            { 
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(_webElement)).Click();
            }
            catch
            {
                //debug cant click
            }
        }
        public void WaitForElementToClickableClick(IWebElement _webElement, bool _clickTrueFalse)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                if (_clickTrueFalse)
                    wait.Until(ExpectedConditions.ElementToBeClickable(_webElement)).Click();
                else
                    wait.Until(ExpectedConditions.ElementToBeClickable(_webElement));
            }
            catch
            {
                //debug cant click
            }
        }   
        public void WaitForElementIsVisible(string _xpath)
        {
            if (DoesElementExist(_xpath))
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xpath)));
            }
            else
            FailureInformation(string.Format("Element: {0} \ndoes not exist", _xpath));
        }

        public bool IsElementVisible(IWebElement _element)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return _element.Displayed && _element.Enabled;
        } 
        public bool DoesElementExist(string _xpath)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _elements = _driver.FindElements(By.XPath(_xpath));

            if (_elements.Count == 0)
                return false;
            else
                return true;
        }

        public void ClearSendKeys(IWebElement _element, string _keys)
        {
            _element.Clear();
            _element.SendKeys(_keys);
        }
    }
}
