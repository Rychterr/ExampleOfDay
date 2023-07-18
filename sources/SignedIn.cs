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
    class SignedIn
    {

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Edit details')]")]
        private IWebElement _editDetails;
        


        IWebDriver _driver;
        Functions _fun;
        public SignedIn(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
            _fun = new Functions(_driver);
        }

        public bool IsLoggedIn()
        {
            return _fun.IsElementVisible(_editDetails);
        }

    }
}
