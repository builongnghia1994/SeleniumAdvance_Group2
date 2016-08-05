﻿using OpenQA.Selenium;
using SeleniumAdvance_Group2.Common;

namespace SeleniumAdvance_Group2.PageObject
{
    public class GeneralPage:CommonActions
    {
        public readonly By menuUser = By.XPath("//a[@href='#Welcome']");
        public readonly By itemLogOut = By.XPath("//a[@href='logout.do']");
        public readonly By menuAdminister = By.XPath("//a[href='#Administer']");
        public readonly By itemDataProfile = By.XPath("//a[@href='profiles.jsp']");

        public LoginPage LogOut()
        {
            ClickControl(menuUser);
            ClickControl(itemLogOut);
            return new LoginPage();
        }

        public void GotoDataProfilePage()
        {
            ClickControl(menuAdminister);
            ClickControl(itemDataProfile);
        }
    }
}
