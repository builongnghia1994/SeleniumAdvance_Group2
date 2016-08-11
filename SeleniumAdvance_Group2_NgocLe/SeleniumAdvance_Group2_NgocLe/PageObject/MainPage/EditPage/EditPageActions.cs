﻿using SeleniumAdvance_Group2.PageObject.GeneralPage;
using SeleniumAdvance_Group2.PageObject.MainPage.NewPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvance_Group2.PageObject.MainPage.EditPage
{
  public  class EditPageActions:GeneralPageActions
    {
        public GeneralPageActions EditPage(string status, string pagename, string parentname, string afterpage, string numbercolum)

        {
            switch (status.ToLower())
            {
                case "public":
                    ClickControl(NewPageUI.rdPublic);
                    break;
                default:
                    break;
            }
            if (pagename != null)
            { TypeValue(NewPageUI.txtPageName, pagename); }
            if (parentname != null)
            { SelectItemByDropdownList(NewPageUI.ddlParentName, parentname); }
            if (numbercolum != null)
            { SelectItemByDropdownList(NewPageUI.ddlnumbercolum, numbercolum); }
            if (afterpage != null)
            { SelectItemByDropdownList(NewPageUI.ddlDisplayAfter, afterpage); }
            ClickControl(NewPageUI.btnOk);

            return new GeneralPageActions();
        }
    }
}