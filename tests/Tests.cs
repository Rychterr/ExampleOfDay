//Inside SeleniumTest.cs

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
using System.Threading.Tasks;

namespace qatest.tests

{
    [TestFixture]
    public class Tests

    {
        IWebDriver _driver;
        [SetUp]
        public void Setup()
        {
            
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _driver = new ChromeDriver(path + @"\drivers\");
            _driver.Navigate().GoToUrl("https://qatest-dev.indvp.com/");
        }

        [TearDown]
        public void Cleanup()
        {
            //_driver.Close();
            //_driver.Quit();
        }

        [Test]
        public void SearchClickAddToBasket()
        {
            string _searchItem = "White Porcelain Shell Plates - Set of Four";
            Header hr = new Header(_driver);

            if (hr.SearchAndClickFirstResult(_searchItem))
            {
                Products pr = new Products(_driver);
                pr.AddToBasket();
                Assert.IsTrue(hr.WasNewItemAddedToCart()); // if added, before product availability appears, it doesnt check correctly is an item in stock or not
            }
            else
                Assert.Fail();
        }
        [Test]
        public void SearchNoResults()
        {
            string _searchItem = "t";
            Header hr = new Header(_driver);
            hr.Search(_searchItem);
            Assert.IsTrue(hr.IsThereNoResultsFound());

        }
        [Test]
        public void SearchStartTyping()
        {
            Header hr = new Header(_driver);
            hr.Search("");
            Assert.IsTrue(hr.IsThereStartTyping());

        }

        //Account creation
        [Test]
        public void CreateAccountSignIn()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.CreateAccountClick();
            hr.FillAccountDetails(true, "Micha³","Rychter","random", "Password1!");
            hr.SignInClickAfterAccCreationClick();

            SignedIn si = new SignedIn(_driver);
            Assert.IsTrue(si.IsLoggedIn());
        }

        [Test]
        public void CreateAccountSignInEmailTaken()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.CreateAccountClick();
            hr.FillAccountDetails(true, "Micha³", "Rychter", "testeremail@mail.com", "Password1!");

            Assert.IsTrue(hr.SignInClickAfterAccCreationCheckIfEmailTakenWarning());
        }
        [Test]
        public void CreateAccountSignInDifferentPasswords() //_______________________________________doesnt detect it!!!
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.CreateAccountClick();
            hr.FillAccountDetails(true, "Micha³", "Rychter", "random", "Password1!", "Password");
            hr.SignInClickAfterAccCreationClick();

            Assert.IsTrue(hr.SignInClickAfterAccCheckIfIncorrectDataWarning());
        }
        [Test]
        public void CreateAccountSignInTooShortPassword() //_______________________________________Inncorect data warning doesnt have a background!!!
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.CreateAccountClick();
            hr.FillAccountDetails(true, "Micha³", "Rychter", "random", "P");
            hr.SignInClickAfterAccCreationClick();

            Assert.IsTrue(hr.SignInClickAfterAccCreationCheckIfPasswordTooShort());
        } 
        [Test]
        public void CreateAccountSignInTooWeakPassword()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.CreateAccountClick();
            hr.FillAccountDetails(true, "Micha³", "Rychter", "random", "Password");
            hr.SignInClickAfterAccCreationClick();

            Assert.IsTrue(hr.SignInClickAfterAccCreationCheckIfPasswordTooWeak());
        }

        [Test]
        public void CreateAccountSignInRequiredFields()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.CreateAccountClick();
            hr.FillAccountDetails(true, "Micha³", "Rychter", "random", "Password");
            hr.SignInClickAfterAccCreationClick();

            Assert.IsTrue(hr.SignInClickAfterAccCreationCheckIfPasswordTooWeak());
        }

        [Test]
        public void CreateAccountEmptyFieldsWarrnings()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.CreateAccountClick();
            Assert.IsTrue(hr.FillAccountDetailsCheckRequiredField(true, "Micha³", "Rychter", "random", "Password1!"));
        }
        // Account SignIn
        [Test]
        public void SignIn()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.SignInFillAccountDetails("testeremail@mail.com", "Password1!");
            hr.SignInClick();

            SignedIn si = new SignedIn(_driver);
            Assert.IsTrue(si.IsLoggedIn());
        }

        [Test]
        public void SignInWrongPassword()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.SignInFillAccountDetails("testeremail@mail.com", "Password2!");

            Assert.IsTrue(hr.SignInClickCheckIfDetectWrongPassword());
        }
        [Test]
        public void SignInTooShortPassword()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.SignInFillAccountDetails("testeremail@mail.com", "Pas");

            Assert.IsTrue(hr.SignInClickCheckIfPasswordTooShort());
        }
        [Test]
        public void SignInInvalidEmail()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();

            hr.SignInFillAccountDetails("testeremail", "Password1!");
            if (hr.SignInClickCheckIfEmailIsValid())
            {
                hr.SignInFillAccountDetails("testeremail@mail", "Password1!");
                if (hr.SignInClickCheckIfEmailIsValid())
                {
                    hr.SignInFillAccountDetails("@mail", "Password1!");
                    Assert.IsTrue(hr.SignInClickCheckIfEmailIsValid());
                }
                else
                Assert.Fail();
            }
            else
            Assert.Fail();
        }
        [Test]
        public void SignInEmptyEmailAndPassword()
        {
            Header hr = new Header(_driver);
            hr.SignInCreateAccClick();
            Assert.IsTrue(hr.SignInClickCheckIfEmailPasswordIsEmpty());
        }
        
    }
}