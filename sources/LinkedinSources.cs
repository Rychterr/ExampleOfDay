using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qatest.sources
{
    class LinkedinSources
    {

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement _username;
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement _password;
        [FindsBy(How = How.XPath, Using = "//*[@type='submit']")]
        private IWebElement _login;
        [FindsBy(How = How.XPath, Using = "//*[@action-type='ACCEPT']")]
        private IWebElement _cookie;   

        [FindsBy(How = How.XPath, Using = "//span[text()= 'Dodaj notatkę']")]
        private IWebElement _note; 
        [FindsBy(How = How.XPath, Using = "//*[@name='message']")]
        private IWebElement _message; 
        [FindsBy(How = How.XPath, Using = "//span[text()= 'Wyślij']")]
        private IWebElement _send;    
        [FindsBy(How = How.XPath, Using = "//span[text()= 'Dalej']")]
        private IWebElement _nextPage; 
        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Odrzuć']")]
        private IWebElement _cancel;

        string _addContactXPATH = "(//span[text()= 'Nawiąż kontakt'])[{0}]";
        IWebDriver _driver;
        Functions _fun;
        public LinkedinSources(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
            _fun = new Functions(_driver);
        }

        public void LogIn()
        {
            _fun.WaitForElementToClickableClick(_cookie);
            _fun.WaitForElementToClickableClick(_username);
            _username.SendKeys("michalrychterr@gmail.com");
            _fun.WaitForElementToClickableClick(_password);
            _password.SendKeys("");
            _login.Click();
        }

        public void SendMsg()
        {
            for (int _i = 1; _i < 11; _i++)
            {
                _fun.WaitForElementToClickableClick(_nextPage, false);
                string _xpath = string.Format(_addContactXPATH, 1);
                    if (_fun.DoesElementExist(_xpath))
                    {
                    IWebElement _addConact = _driver.FindElement(By.XPath(_xpath));
                    _fun.WaitForElementToClickableClick(_addConact);
                    _fun.WaitForElementToClickableClick(_note);
                    _fun.WaitForElementToClickableClick(_message);
                    _message.SendKeys("Hello! \nI'm looking for new job opportunities, could you help me find some? :D");

                    if (_fun.IsElementVisible(_send))
                        _send.Click();
                    else
                        _cancel.Click();
                    }
                    else
                    {
                    _nextPage.Click();
                    _i = 0;
                    }

            }            
        }
    }
}
