﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance_Group2.Common;
using OpenQA.Selenium;

namespace SeleniumAdvance_Group2.TestCases
{
    [TestClass]
    public class DemoThi : TestBases
    {

        private string username = "thi.nguyen";
        private string pass = "1";
       // private string statuspublic = "public";
        //private string pagename = "Thiaddpage3";

        [TestMethod]

       public void Demo_Thi()

        {
            loginPage = OpenURL(Constant.DashboardURL);
            generalPage = loginPage.Login(Constant.Respository, username, pass);
            generalPage.DeletePages();
                       
        }
    }
}
