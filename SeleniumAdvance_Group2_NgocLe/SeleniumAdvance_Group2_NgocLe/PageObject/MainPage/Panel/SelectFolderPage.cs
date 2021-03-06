﻿using SeleniumAdvance_Group2.Common;
using SeleniumAdvance_Group2.PageObject.General;
using OpenQA.Selenium;

namespace SeleniumAdvance_Group2.PageObject.MainPage.Panel
{
    public class SelectFolderPage : GeneralPage
    {
        public SelectFolderPage()
        {
            if (Constant.SelectFolderPageDictionary == null)
                Constant.SelectFolderPageDictionary = ReadXML();
        }

        public PanelConfigurationPage SelectFolder(string path)
        {
            GotoFolder(path);
            ClickControl("ok button");
            return new PanelConfigurationPage();
        }

        public void GotoFolder(string pathFolder)
        {
            WaitForPageLoad();
            string[] allPathFolders = pathFolder.Split('/');
            By lastFolder = By.XPath("");
            string currentFolderXpath = "//div[@id='async_html_2']//tbody/tr/td[2]//input[@value='/" + allPathFolders[0] + "']";

            if (allPathFolders.Length == 1)
            {
                lastFolder = By.XPath(currentFolderXpath);
                if (Constant.Browser == "ie" || Constant.Browser == "edgewin")
                {
                    ClickControlByJS(lastFolder);
                }
                else
                {
                    ClickControl(lastFolder);
                }
            }
            else
            {
                string next = string.Empty;
                for (int b = 1; b < allPathFolders.Length; b++)
                {
                    if (Constant.Browser == "ie" || Constant.Browser == "edgewin")
                    {
                        ClickControlByJS(By.XPath(currentFolderXpath + "/../a[1]"));
                    }
                    else
                    {
                        ClickControl(By.XPath(currentFolderXpath + "/../a[1]"));
                    }

                    next += "/" + allPathFolders[b];
                    currentFolderXpath = "//div[@id='async_html_2']//tbody/tr/td[2]//input[@value='/" + allPathFolders[0] + next + "']";
                }

                lastFolder = By.XPath(currentFolderXpath + "/../a[2]");

                if (Constant.Browser == "ie" || Constant.Browser == "edgewin")
                {
                    ClickControlByJS(lastFolder);
                }
                else
                {
                    ClickControl(lastFolder);
                }
            }
        }
    }
}