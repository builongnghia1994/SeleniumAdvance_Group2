﻿using OpenQA.Selenium;
using SeleniumAdvance_Group2.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace SeleniumAdvance_Group2.PageObject
{
    public class GeneralPage : CommonActions
    {
        private readonly By menuUser = By.XPath("//a[@href='#Welcome']");
        private readonly By itemLogOut = By.XPath("//a[@href='logout.do']");
        private readonly By menuAdminister = By.XPath("//a[@href='#Administer']");
        private readonly By itemDataProfile = By.XPath("//a[@href='profiles.jsp']");
        private readonly By itemPanel = By.XPath("//a[@href='panels.jsp']");

        private readonly By menuGlobalSetting = By.XPath("//li[@class='mn-setting']/a[@href='javascript:void(0);']");
        private readonly By itemAddPage = By.XPath("//a[@class='add' and text()='Add Page']");




        private readonly By txtPageName = By.Id("name");
        private readonly By rdIsPublic = By.Id("ispublic");
        private readonly By drdparentname = By.Id("parent");
        private readonly By drdafterpage = By.Id("afterpage");
        private readonly By drdnumberclm = By.Id("columnnumber");
        private readonly By btnOK = By.Id("OK");







        public LoginPage LogOut()
        {
            ClickControl(menuUser);
            ClickControl(itemLogOut);
            return new LoginPage();
        }

        public DataProfilePage GotoDataProfilePage()
        {
            if (Constant.Browser == "ie")
            {

                IWebElement webElement = FindElement(menuAdminister);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)Constant.WebDriver;
                executor.ExecuteScript("arguments[0].click();", webElement);

                IWebElement webElement1 = FindElement(itemDataProfile);
                executor = (IJavaScriptExecutor)Constant.WebDriver;
                executor.ExecuteScript("arguments[0].click();", webElement1);

            }
            else
            {
                ClickControl(menuAdminister);
                ClickControl(itemDataProfile);
            }

            return new DataProfilePage();
        }

        public PanelManagerPage GotoPanelManagerPage()
        {


            if (Constant.Browser == "ie")
            {

                IWebElement webElement = FindElement(menuAdminister);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)Constant.WebDriver;
                executor.ExecuteScript("arguments[0].click();", webElement);

                IWebElement webElement1 = FindElement(itemPanel);
                executor = (IJavaScriptExecutor)Constant.WebDriver;
                executor.ExecuteScript("arguments[0].click();", webElement1);
            }

            else
            {
                ClickControl(menuAdminister);
                ClickControl(itemPanel);
            }

            return new PanelManagerPage();
        }

        public PanelPage GotoPanelPage()
        {
            PanelManagerPage panelManagerPage = GotoPanelManagerPage();
            return panelManagerPage.GoToPanelPage();
        }

        public void GotoPage(string way)
        {
            WaitForControl(menuUser, 5);
            string[] allpages = way.Split('/');
            By lastpage = By.XPath("");
            int b = 0;
            string currentpagexpath = "//ul/li/a[text()='" + allpages[b] + "']";

            if (allpages.Length == 1)
            {
                //cover trường hợp tới 1 page chính nào đó mà k qua bất kì 1 page nào nữa
                lastpage = By.XPath(currentpagexpath);
                ClickControl(lastpage);
            }
            else
            {
                //trường hợp nếu phải thông qua nhiều page
                for (b = 0; (b + 1) < allpages.Length; b++)
                {
                    Actions builder = new Actions(Constant.WebDriver);
                    Actions hoverClick = builder.MoveToElement(FindElement(By.XPath(currentpagexpath)));
                    hoverClick.Build().Perform();
                    string next = "/following-sibling::ul/li/a[text()='" + allpages[b + 1] + "']";
                    currentpagexpath = currentpagexpath + next;
                    lastpage = By.XPath(currentpagexpath);
                }
                ClickControl(lastpage);
            }
        }

        public void VerifyWelComeUser(string username)
        {
            VerifyText(username, menuUser);
        }


        public void GlobalSetting(string settingname)
        {
            By a = By.XPath("//li/a[text()='" + settingname + "']");
            ClickControl(menuGlobalSetting);
            ClickControl(a);
        }


        public void CreatePage(string pagename, string ispublic, string parentname, string numberclm, string afterpage)
        {
            FindElement(txtPageName).SendKeys(pagename);
            switch (ispublic)
            {
                case "public":
                    FindElement(rdIsPublic).Click();
                    break;
                default:
                    break;
            }
            if (parentname != "")
            {
                new SelectElement(FindElement(drdparentname)).SelectByText(parentname);
            }
            if (afterpage != "")
            {
                new SelectElement(FindElement(drdafterpage)).SelectByText(afterpage);
            }
            if (numberclm != "")
            {
                new SelectElement(FindElement(drdnumberclm)).SelectByText(numberclm);
            }
            ClickControl(btnOK);
        }


        public NewPage GotoNewPage()
        {
            GlobalSetting("Add Page");
            return new NewPage();
        }







    }
}
