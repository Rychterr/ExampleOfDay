using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using qatest;
using qatest.sources;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qatest.tests
{
    class linkedin
    {
        IWebDriver _driver;
        Functions _fun;
        [SetUp]
        public void Setup()
        {

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _driver = new ChromeDriver(path + @"\drivers\");
            _driver.Navigate().GoToUrl("https://www.linkedin.com/uas/login?session_redirect=https%3A%2F%2Fwww%2Elinkedin%2Ecom%2Fsearch%2Fresults%2Fpeople%2F%3Fkeywords%3Dhiring%2520manager%26page%3D3%26sid%3DIzt&fromSignIn=true&trk=cold_join_sign_in");
            _fun = new Functions(_driver);
        }

        [TearDown]
        public void Cleanup()
        {
            //_driver.Close();
            //_driver.Quit();
        }


        [Test]
        public void LinkedinSendMsg()
        {
            LinkedinSources ln = new LinkedinSources(_driver);
            ln.LogIn();
            ln.SendMsg();
        }
    }
}
