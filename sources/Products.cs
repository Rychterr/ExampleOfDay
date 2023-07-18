using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qatest.sources
{
    class Products
    {
        
        [FindsBy(How = How.XPath, Using = "//*[contains(text(), 'Add to basket')]/../../button")]
        private IWebElement _addToBasket;

        string __productAvailabilityXPATH = "//span[@class='ProductActions-Stock']";

        string __productTestTitleXPATH = "//p[contains(text(), 'Test')]";

        IWebDriver _driver;
        public Products(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public void AddToBasket()
        {
            Functions _fun = new Functions(_driver);
            _fun.WaitForElementIsVisible(__productAvailabilityXPATH);
            _fun.WaitForElementToClickableClick(_addToBasket);
        }
        public bool CheckIfTestProductExist()
        {
            Functions _fun = new Functions(_driver);
            if (_fun.DoesElementExist(__productTestTitleXPATH))
            {
                _fun.FailureInformation("'Test' product should have been removed");
                return true;
            }
            else
            return false;
        }
    }
}
