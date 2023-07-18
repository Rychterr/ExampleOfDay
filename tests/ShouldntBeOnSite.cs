using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using qatest;
using qatest.sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qatest.tests
{
    class ShouldntBeOnSite
    {
        IWebDriver _driver;
        [SetUp]
        public void Setup()
        {

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _driver = new ChromeDriver(path + @"\drivers\");

            //var DeviceDriver = ChromeDriverService.CreateDefaultService();
            //DeviceDriver.HideCommandPromptWindow = true;
            //ChromeOptions options = new ChromeOptions();
            //options.AddArguments("--disable-infobars");
            //driver = new ChromeDriver(DeviceDriver, options);
            //driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://qatest-dev.indvp.com/");
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void Cleanup()
        {
            //_driver.Close();
            // _driver.Quit();
        }
        [Test]
        public void SearchTestProduct()
        {
           //product should not exist
            string _searchItem = "test";
            Header hr = new Header(_driver);
            if (hr.SearchAndClickFirstResult(_searchItem))
            {
                Products pr = new Products(_driver);
                pr.AddToBasket();
                Assert.IsTrue(!pr.CheckIfTestProductExist());
            }
            else
                Assert.Fail();
        }
        [Test]
        public void SearchInfiniteLoad()
        {
            //for ever loading if after searched word we delete one letter
            string _searchItem = "White Porcelain Shell Plates - Set of Four";
            Header hr = new Header(_driver);
            hr.SearchAndDelete(_searchItem,1);
            Assert.IsTrue(hr.CheckIfSearchResultIsValid());
        }
        [Test]
        public void CreateAccountCannotReopen()
        {
            //after closing sign in & create an account by clicking on it, during account creation, user is not able to reopen it
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();
            hr.CreateAccountClick();
            hr.FillAccountDetails(true, "Michał", "Rychter", "testeremail@mail.com", "Password1!");
            hr.SignInCreateAccClick();
            Thread.Sleep(3000);
            hr.SignInCreateAccClick();
            Assert.IsTrue(hr.SignInAfterAccCreationIsClickable());
        }
        [Test]
        public void CreateAccountAfterReopen()
        {
            //after reopening creating account, all input is lost
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();
            hr.CreateAccountClick();
            hr.FillAccountDetails(true, "Michał", "Rychter", "testeremail@mail.com", "Password1!");
            hr.SignInCreateAccClick();
            Thread.Sleep(3000);
            hr.SignInCreateAccClick();
            Assert.IsTrue(hr.SignInAfterAccCreationIsClickable());
        }
        [Test]
        public void SignInCannotClose()
        {
            //cant close by clicking sign in & create an account
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.SignInCreateAccClick();
            Thread.Sleep(3000);
            hr.SignInCreateAccClick();
            Assert.IsTrue(!hr.SignInIsClickable());
        }
    }
}
