﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using SeleniumAdvance_Group2.PageObject.Login;
using System.Xml;
using System.Diagnostics;

using System.Threading;

namespace SeleniumAdvance_Group2.Common
{
    public class CommonActions
    {
        public static void OpenBrowser(string browser)

        {
            switch (browser.ToLower())
            {

                case "chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-extensions");
                    Constant.WebDriver = new ChromeDriver(options);
                    Constant.WebDriver.Manage().Window.Maximize();
                    break;
                case "ie":
                    InternetExplorerOptions optionIE = new InternetExplorerOptions();
                    optionIE.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    Constant.WebDriver = new InternetExplorerDriver(optionIE);
                    Constant.WebDriver.Manage().Window.Maximize();
                    break;
                case "firefox":
                    Constant.WebDriver = new FirefoxDriver();
                    Constant.WebDriver.Manage().Window.Maximize();
                    break;
                case "edgewin":
                    Constant.WebDriver = new EdgeDriver();
                    Constant.WebDriver.Manage().Window.Maximize();
                    break;
                default:
                    Console.WriteLine(String.Format("Browser '{0}' not recognized. Spawning default Firefox browser.", browser));
                    Constant.WebDriver = new FirefoxDriver();
                    Constant.WebDriver.Manage().Window.Maximize();
                    break;
            }
        }
        public LoginPage OpenURL(string url)
        {
            Constant.WebDriver.Navigate().GoToUrl(url);
            return new LoginPage();
        }
        private Dictionary<string, string>[] ReadXMlFile(string filename)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(filename);

            Dictionary<string, string> typeDictionary = new Dictionary<string, string>();
            Dictionary<string, string> valueDictionary = new Dictionary<string, string>();
            foreach (XmlNode node in xd.DocumentElement.ChildNodes)
            {
                typeDictionary.Add(node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText);
                valueDictionary.Add(node.ChildNodes[0].InnerText, node.ChildNodes[2].InnerText);
            }
            Dictionary<string, string>[] iDictionary = new Dictionary<string, string>[2];
            iDictionary[0] = typeDictionary;
            iDictionary[1] = valueDictionary;
            return iDictionary;
        }
        public Dictionary<string, string>[] ReadXML()
        {
            string page = GetClassCaller(2);
            if (page == "EditPage")
                page = "NewPage";
            if (page == "NewPanelForPage")
                page = "NewPanelPage";
            string filename = Constant.XMLPath + page + ".xml";
            return ReadXMlFile(filename);
        }
        private static string GetClassCaller(int level)
        {
            var m = new StackTrace().GetFrame(level).GetMethod();
            return m.DeclaringType.Name;
        }
        public IWebElement FindElement(By control)
        {
            WaitForControl(control, Constant.Timeout);
            return Constant.WebDriver.FindElement(control);
        }
        public IWebElement FindElement(string locator)
        {
            return FindElement(FindElementBy(locator));
        }

        //this method is just used for test case 49
        public IWebElement FindElementFor49(string locator)
        {
            return FindElement(locator);
        }
        private By FindElementBy(string locator)
        {
            string page = GetClassCaller(4);
            Dictionary<string, string>[] iDictionary = new Dictionary<string, string>[2];
            switch (page)
            {
                case "LoginPage":
                    iDictionary = Constant.LoginDictionary;
                    break;
                case "NewPanelPage":
                case "NewPanelForPage":
                    iDictionary = Constant.NewPanelDictionary;
                    break;
                case "PanelManagerPage":
                    iDictionary = Constant.PanelManagerDictionary;
                    break;
                case "EditPage":
                case "NewPage":
                    iDictionary = Constant.NewPageDictionary;
                    break;
                case "DataProfileManagerPage":
                    iDictionary = Constant.DataProfileDictionary;
                    break;
                case "NewDataProfilePage":
                    iDictionary = Constant.NewDataProfileDictionary;
                    break;
                case "EditDataProfilePage":
                    iDictionary = Constant.EditDataProfileDictionary;
                    break;
                case "GeneralPage":
                    iDictionary = Constant.GeneralDictionary;
                    break;
                case "ChoosePanelPage":
                    iDictionary = Constant.ChoosePanelDictionary;
                    break;
                case "PanelConfigurationPage":
                    iDictionary = Constant.PanelConfigurationDictionary;
                    break;
                case "SelectFolderPage":
                    iDictionary = Constant.SelectFolderPageDictionary;
                    break;

            }
            return FindElementFromXML(locator, iDictionary);
        }
        private By FindElementFromXML(string key, Dictionary<string, string>[] iDictionary)
        {
            return FindElementByType(iDictionary[0][key], iDictionary[1][key]);
        }
        private By FindElementByType(string type, string value)
        {
            By element = null;
            switch (type.ToLower())
            {
                case "id":
                    element = By.Id(value);
                    break;
                case "xpath":
                    element = By.XPath(value);
                    break;
                case "class":
                    element = By.ClassName(value);
                    break;
                case "name":
                    element = By.Name(value);
                    break;
            }

            return element;
        }
        public IWebElement FindElementFromPage(string locator)
        {
            return FindElement(locator);
        }

        public void ClickControl(By control)
        {
            FindElement(control).Click();
        }
        public void ClickControlByJS(string locator)
        {
            IWebElement webElement = FindElement(locator);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Constant.WebDriver;
            executor.ExecuteScript("arguments[0].click();", webElement);
        }

        public void ClickControl(string locator)
        {
            FindElement(locator).Click();
        }

        public void TypeValue(string locator, string value)
        {
            IWebElement element = FindElement(locator);
            element.Clear();
            element.SendKeys(value);
        }

        public void SelectItemByDropdownList(string locator, string value)
        {
            SelectElement selectElementByControl = new SelectElement(FindElement(locator));
            selectElementByControl.SelectByText(value);
        }

        public string GetText(By control)
        {
            return FindElement(control).Text;
        }

        public string GetText(IWebElement control)
        {
            return control.Text;
        }
        public bool DoesControlExist(By control)
        {
            try
            {
                Constant.WebDriver.FindElement(control);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

       
        public void WaitForControl(By control, int timesecond)
        {
            //WebDriverWait wait = new WebDriverWait(Constant.WebDriver, new TimeSpan(timesecond));
            //IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(control));
            //bool control = false;
            IWebElement element = null;

            for (int i = 0; i < timesecond; i++)
            {
                try
                {
                    element = Constant.WebDriver.FindElement(control);
                    Console.WriteLine(i.ToString() + control.ToString());
                    if (element.Displayed)
                        //add this condition since the test case 30, add panel,
                        //then open new panel again, txtdisplayname is not visible at the first time
                        return;
                }

                catch
                {
                    Thread.Sleep(1000);
                }

            }
        }

        public void WaitForPageLoad()
        {

            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(30.00));
                wait.Until(driver1 => ((IJavaScriptExecutor)Constant.WebDriver).ExecuteScript("return document.readyState").Equals("complete"));
                Thread.Sleep(1000);
            }
            catch (WebDriverException e)
            {

                Console.WriteLine(e.Message);
            }

        }

        public void WaitForControl(string locator, int timesecond)
        {
            IWebElement control = FindElement(locator);
            Constant.WebElement = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(timesecond)).Until(ExpectedConditions.ElementToBeClickable(control));
        }

        public int CountItems(By control)
        {
            WaitForControl(control, Constant.Timeout);
            return Constant.WebDriver.FindElements(control).Count;
        }

        public int CountItems(string locator)
        {
            return FindElements(locator).Count;
        }

        public IList<IWebElement> FindElements(string locator)
        {
            By element = FindElementBy(locator);
            WaitForControl(element, Constant.Timeout);
            return Constant.WebDriver.FindElements(element);
        }





        #region verify
        public void VerifyText(string expectedText, By element)
        {
            string actualText = GetText(element);
            Assert.AreEqual(expectedText, actualText);
        }
        public void VerifyTextFromControl(string expectedText, string locator)
        {
            IWebElement element = FindElement(locator);
            string actualText = GetText(element);
            Assert.AreEqual(expectedText, actualText);
        }
        public void VerifyText(string expectedText, string actualText)
        {
            Assert.AreEqual(expectedText, actualText, "Text does not match with expectation.");
        }

        public void VerifyTextFromAlertAndAccept(string expectedText)
        {
            string alertText = GetTextFromAlertPopup();
            AcceptAlert();
            VerifyText(expectedText, alertText);
        }

        #endregion

        #region alert
        public void DismissAlert()
        {
            WaitForAlertPresent(Constant.Timeout);
            IAlert alert = Constant.WebDriver.SwitchTo().Alert();
            alert.Dismiss();
            Constant.WebDriver.SwitchTo().DefaultContent();
        }

        public void AcceptAlert()
        {
            WaitForAlertPresent(Constant.Timeout);
            IAlert alert = Constant.WebDriver.SwitchTo().Alert();
            alert.Accept();
           Constant.WebDriver.SwitchTo().DefaultContent();
        }

        public void WaitForAlertPresent(int timeout)
        {
            new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(timeout)).Until(ExpectedConditions.AlertIsPresent());
        }

        public string GetTextFromAlertPopup()
        {
            WaitForAlertPresent(Constant.Timeout);
            IAlert alert = Constant.WebDriver.SwitchTo().Alert();
            return alert.Text;
        }
        #endregion

        #region 0 reference
        public void VerifyControlNotExist(By control)
        {
            Assert.IsFalse(DoesControlExist(control));
        }
        public void VerifyDoesControlExist(By control)
        {
            Assert.IsTrue(DoesControlExist(control));
        }
        public void WaitForControlNotVisible(By control, int timesecond)
        {
            Constant.WebElement = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(timesecond)).Until(ExpectedConditions.ElementIsVisible(control));
        }
        public void ClickControl(IWebElement control)
        {
            control.Click();
        }

        public void ClickControlByJS(By control)
        {
            IWebElement webElement = FindElement(control);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Constant.WebDriver;
            executor.ExecuteScript("arguments[0].click();", webElement);
        }

        public void ClickControlByJS(IWebElement control)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Constant.WebDriver;
            executor.ExecuteScript("arguments[0].click();", control);
        }

        public void TypeValue(By control, string value)
        {
            FindElement(control).Clear();
            FindElement(control).SendKeys(value);
        }

        public void TypeValue(IWebElement control, string value)
        {
            control.Clear();
            control.SendKeys(value);
        }

        public void SelectItemByDropdownList(By control, string value)
        {
            SelectElement SelectElementByXpath = new SelectElement(FindElement(control));
            SelectElementByXpath.SelectByText(value);
        }

        public void SelectItemByDropdownList(IWebElement control, string value)
        {
            SelectElement SelectElementByXpath = new SelectElement(control);
            SelectElementByXpath.SelectByText(value);
        }

        //public By BYFindElement(string locator)
        //{
        //    string page = GetClassCaller(3);
        //    //  page = page.Substring(0, page.Length - 7);
        //    string filename = Constant.XMLPath + page + ".xml";
        //    Dictionary<string, string>[] iDictionary = new Dictionary<string, string>[2];
        //    switch (page)
        //    {

        //        case "LoginPage":
        //            iDictionary = Constant.LoginDictionary;
        //            break;
        //        case "NewPanelPage":
        //            iDictionary = Constant.NewPanelDictionary;
        //            break;
        //        case "PanelManagerPage":
        //            iDictionary = Constant.NewPanelDictionary;
        //            break;
        //        case "EditPage":
        //        case "NewPage":
        //            iDictionary = Constant.NewPageDictionary;
        //            break;
        //        case "DataProfileManagerPage":
        //            iDictionary = Constant.DataProfileDictionary;
        //            break;
        //        case "NewDataProfilePage":
        //            iDictionary = Constant.NewDataProfileDictionary;
        //            break;
        //        case "GeneralPage":
        //            iDictionary = Constant.GeneralDictionary;
        //            break;

        //    }
        //    //    Dictionary<string, string>[] iDictionary = ReadXMlFile(filename);
        //    return BYFindElementFromXML(locator, iDictionary);
        //}

        //public IWebElement FindElementFromXML(string key, Dictionary<string, string>[] iDictionary)
        //{
        //    return FindElementByType(iDictionary[0][key], iDictionary[1][key]);
        //}

        //public IWebElement FindElementByType(string type, string value)
        //{
        //    By element = null;
        //    switch (type.ToLower())
        //    {
        //        case "id":
        //            element = By.Id(value);
        //            break;
        //        case "xpath":
        //            element = By.XPath(value);
        //            break;
        //        case "class":
        //            element = By.ClassName(value);
        //            break;
        //        case "name":
        //            element = By.Name(value);
        //            break;
        //    }

        //    return FindElement(element);
        //}

        #endregion

    }
}
