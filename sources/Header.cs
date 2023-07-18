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

namespace qatest
{
    public class Header
    {
        //search
        [FindsBy(How = How.Id, Using = "search-field")]
        private IWebElement _searchTxtBox; 
        
        [FindsBy(How = How.XPath, Using = "(//*[@aria-label='Search results']//a[@href!='/'])[1]")]
        private IWebElement _searchTxtBoxFirstItem;
        string _searchTxtBoxFirstItemXPATH = "(//*[@aria-label='Search results']//a[@href!='/'])[1]";
        
        [FindsBy(How = How.XPath, Using = "//*[@aria-label='Search results']/p[contains(text(), 'No results found!')]")]
        private IWebElement _searchTxtBoxNoResults;
        string _searchTxtBoxNoResultsXPATH = "//*[@aria-label='Search results']/p[contains(text(), 'No results found!')]";

        string _searchTxtStartTypingXPATH = "//*[@aria-label='Search results']/p[contains(text(), 'Start typing to see search results!')]";

        //sign in
        [FindsBy(How = How.XPath, Using = "//div[@aria-label='My account']")]
        private IWebElement _signInAfterAccCreationCreateAccButton;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Create an account')]")]
        private IWebElement _createAnAccount; 

        [FindsBy(How = How.Id, Using = "firstname")]
        private IWebElement _firstname;
        
        [FindsBy(How = How.Id, Using = "lastname")]
        private IWebElement _lastname;
        
        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement _email; 
        
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement _password; 
        
        [FindsBy(How = How.Id, Using = "confirm_password")]
        private IWebElement _confirmPassword;
        
        [FindsBy(How = How.XPath, Using = "//div[@class='MyAccountOverlay-Buttons']/button[@type='submit']")]
        private IWebElement _signInAfterAccCreation; 
        
        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Sign in')]")]
        private IWebElement _signIn;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'A customer with the same email address already exists in an associated website.')]")]
        private IWebElement _emailTaken; 
        
        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox']")]
        private IWebElement _newsLetter;

        string _emailTakenXPATH = "//p[contains(text(),'A customer with the same email address already exists in an associated website.')]";
        
        string _incorrectDataXPATH = "//p[contains(text(),'Incorrect data!')]";

        string _passwordTooWeakXPATH = "//p[contains(text(),'Minimum of different classes of characters in password is 3')]";

        string _passwordIsTooShortXPATH = "(//p[contains(text(), 'Password should be at least 8 characters long')])[1]";
        string _repeatPasswordIsTooShortXPATH = "(//p[contains(text(), 'Password should be at least 8 characters long')])[2]";

        string _wrongPasswordXPATH = "//p[contains(text(),'The account sign-in was incorrect')]";

        string _invalidEmailXPATH = "//p[contains(text(),'Email is invalid.')]";

        string _requiredFieldXPATH = "//label[@for='{0}']/following-sibling::p[contains(text(),'This field is required!')]";
        //other
        IWebDriver _driver;
        Functions _fun;
        public int _numberOfAddedItem = 0;

        public Header(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
            _fun = new Functions(_driver);
        }

        //_____SEARCH_____
        public void Search(string _searchText)
        {
            
            _fun.WaitForElementToClickableClick(_searchTxtBox);
            _searchTxtBox.SendKeys(_searchText);
        }
        public void SearchAndDelete(string _searchText, int _deletedCharacters)
        {
            _fun.WaitForElementToClickableClick(_searchTxtBox);
            _searchTxtBox.SendKeys(_searchText);
            if (CheckIfSearchResultIsValid())
            for (int _i=0; _i<_deletedCharacters; _i++)
            _searchTxtBox.SendKeys(Keys.Backspace);
        }
        public bool CheckIfSearchResultIsValid()
        {
            _fun.WaitForElementToClickableClick(_searchTxtBox);
            if (_fun.DoesElementExist(_searchTxtBoxFirstItemXPATH))
            {
                _fun.SuccessInformation("Have found results!");
                return true;
            }
            else if (_fun.DoesElementExist(_searchTxtBoxNoResultsXPATH))
            {
                _fun.SuccessInformation("Haven't found any results!");
                return true;
            }
            else
            {
                _fun.FailureInformation("Haven't found either element!");
                return false;
            }
        }
        public bool SearchAndClickFirstResult(string _searchText)
        {
            _fun.WaitForElementToClickableClick(_searchTxtBox);
            _searchTxtBox.SendKeys(_searchText);

            if (_fun.DoesElementExist(_searchTxtBoxFirstItemXPATH))
            {
                _fun.WaitForElementToClickableClick(_searchTxtBoxFirstItem);
                return true;
            }
            else if (_fun.DoesElementExist(_searchTxtBoxNoResultsXPATH))
            {
                _fun.FailureInformation("Haven't found any results!");
                return false;
            } 
            else if (_fun.DoesElementExist(_searchTxtStartTypingXPATH))
            {
                _fun.FailureInformation("Haven't gotten product to search for!");
                return false;
            }
            else
            {
                _fun.FailureInformation("Haven't found either element!");
                return false;
            }
        }
        public bool IsThereNoResultsFound()
        {
            return _fun.DoesElementExist(_searchTxtBoxNoResultsXPATH);
        }   
        public bool IsThereStartTyping()
        {
            return _fun.DoesElementExist(_searchTxtStartTypingXPATH);
        }

        public bool WasNewItemAddedToCart()
        {
            _numberOfAddedItem++;
            string _amountOfItemsInCart = string.Format("//*[@aria-label='Items in cart'][contains(text(),'{0}')]", _numberOfAddedItem);
            if (_fun.DoesElementExist(_amountOfItemsInCart))
            return true;
            else
            {
                _fun.FailureInformation("Haven't added an item to the cart!");
                _numberOfAddedItem--;
                return false;
            }
        }
        //_____SEARCH END_____

        //_____SIGN IN_____
        public void SignInCreateAccClick()
        {
            _fun.WaitForElementToClickableClick(_signInAfterAccCreationCreateAccButton);
        }
        public void CreateAccountClick()
        {
            _fun.WaitForElementToClickableClick(_createAnAccount);
        }
        // create acc
        public bool requiredField(string _requiredFieldFor)
        {
            string _xpath = string.Format(_requiredFieldXPATH, _requiredFieldFor);
            if (_fun.DoesElementExist(_xpath))
                return true;
            else
            {
                _fun.FailureInformation("Required field: '"+ _xpath + "' warrning was not found");
                return false;
            }
        }

        public bool AllRequiredFields()
        {
            requiredField("email");
            requiredField("password");
            return true;
        }

        public bool FillAccountDetailsCheckRequiredField(bool _subscribeToNewsLetter, string _stringName, string _stringLastname, string _stringEmail, string _stringPassword) // set _stringEmail to 'random', to generate random email on each test
        {
            _fun.WaitForElementToClickableClick(_firstname);

            if (_subscribeToNewsLetter)
                _newsLetter.Click();

            _signInAfterAccCreation.Click();
            if (requiredField("firstname") && requiredField("lastname") && requiredField("email") && requiredField("password") && requiredField("confirm_password"))
            {
                _firstname.SendKeys(_stringName);
                _signInAfterAccCreation.Click();
                if (requiredField("lastname") && requiredField("email") && requiredField("password") && requiredField("confirm_password"))
                {
                    _lastname.SendKeys(_stringLastname);
                    _signInAfterAccCreation.Click();
                    if (requiredField("email") && requiredField("password") && requiredField("confirm_password"))
                    {
                        if (_stringEmail.Equals("random"))
                        {
                            _stringEmail = _fun.RandomString(10);
                            _stringEmail += "@mail.com";
                        }
                        _email.SendKeys(_stringEmail);
                        _signInAfterAccCreation.Click();
                        if (requiredField("password") && requiredField("confirm_password"))
                        {
                            _password.SendKeys(_stringPassword);
                            _signInAfterAccCreation.Click();
                            if (requiredField("confirm_password"))
                            {
                                _confirmPassword.SendKeys(_stringPassword);
                                _signInAfterAccCreation.Click();
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else 
                return false;
        }

        public void FillAccountDetails(bool _subscribeToNewsLetter ,string _stringName, string _stringLastname, string _stringEmail, string _stringPassword) // set _stringEmail to 'random', to generate random email on each test
        {
            _fun.WaitForElementToClickableClick(_firstname);
            _firstname.SendKeys(_stringName);
            _lastname.SendKeys(_stringLastname);
            if (_stringEmail.Equals("random"))
            {
                _stringEmail = _fun.RandomString(10);
                _stringEmail += "@mail.com";
            }
            _email.SendKeys(_stringEmail);
            _password.SendKeys(_stringPassword);
            _confirmPassword.SendKeys(_stringPassword);
            if (_subscribeToNewsLetter)
                _newsLetter.Click();
        }
        public void FillAccountDetails(bool _subscribeToNewsLetter, string _stringName, string _stringLastname, string _stringEmail, string _stringPassword, string _stringConfirmPassword) // set _stringEmail to 'random', to generate random email on each test
        {
            _fun.WaitForElementToClickableClick(_firstname);
            _firstname.SendKeys(_stringName);
            _lastname.SendKeys(_stringLastname);
            if (_stringEmail.Equals("random"))
            {
                _stringEmail = _fun.RandomString(10);
                _stringEmail += "@mail.com";
            }
            _email.SendKeys(_stringEmail);
            _password.SendKeys(_stringPassword);
            _confirmPassword.SendKeys(_stringConfirmPassword);
            if (_subscribeToNewsLetter)
                _newsLetter.Click();
        }
        // sign in
        public void SignInClickAfterAccCreationClick()
        {
            _signInAfterAccCreation.Click();
        }

        public bool SignInAfterAccCreationIsClickable()
        {
            return (_fun.IsElementVisible(_signInAfterAccCreation));
        }

        public bool SignInClickAfterAccCreationCheckIfEmailTakenWarning()
        {
            _signInAfterAccCreation.Click();
            return _fun.DoesElementExist(_emailTakenXPATH);
        }
        public bool SignInClickAfterAccCheckIfIncorrectDataWarning()
        {
            _signInAfterAccCreation.Click();
            return _fun.DoesElementExist(_incorrectDataXPATH);
        } 
        public bool SignInClickAfterAccCreationCheckIfPasswordTooWeak()
        {
            _signInAfterAccCreation.Click();
            return _fun.DoesElementExist(_passwordTooWeakXPATH);
        } 
        public bool SignInClickAfterAccCreationCheckIfPasswordTooShort()
        {
            _signInAfterAccCreation.Click();
            if (_fun.DoesElementExist(_passwordTooWeakXPATH))
                if (_fun.DoesElementExist(_passwordIsTooShortXPATH))
                    return _fun.DoesElementExist(_repeatPasswordIsTooShortXPATH);
                else
                return false;
            else
            return false;
        }

        public void SignInFillAccountDetails(string _stringEmail, string _stringPassword)
        {
            _fun.WaitForElementToClickableClick(_email);
            _fun.ClearSendKeys(_email, _stringEmail);
            _fun.ClearSendKeys(_password, _stringPassword);
        }

        public void SignInClick()
        {
            _signIn.Click();
        }
        public bool SignInIsClickable()
        {
            return (_fun.IsElementVisible(_signIn));
        }
        public bool SignInClickCheckIfDetectWrongPassword()
        {
            _signIn.Click();
            return _fun.DoesElementExist(_wrongPasswordXPATH);
        } 
        public bool SignInClickCheckIfPasswordTooShort()
        {
            _signIn.Click();
            return _fun.DoesElementExist(_passwordIsTooShortXPATH);
        } 
        public bool SignInClickCheckIfEmailIsValid()
        {
            _signIn.Click();
            return _fun.DoesElementExist(_invalidEmailXPATH);
        }
        
        public bool SignInClickCheckIfEmailPasswordIsEmpty()
        {
            _signIn.Click();
            return requiredField("email") && requiredField("password");
        } 
        

    }
}
