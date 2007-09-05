using System;
using System.Collections.Generic;
using System.Text;
using MenuManagerAddInViews;
using System.AddIn;

namespace MenuManagerAddInEksempel
{
    [AddIn("MenuManagerAddInEksempel", Version="2.0.0.0")]
    public class MenuManagerAddIn : MenuManagerAddInView
    {
        public override string GetMenus()
        {
            return "Funky1;Funky2";
        }
        //public override List<MenuItemView> CreateMenus()
        //{
        //    List<MenuItemView> menuItems = new List<MenuItemView>(2);
        //    MenuItem m1 = new MenuItem("Funky 1");
        //    MenuItem m2 = new MenuItem("Funky 2");
        //    menuItems.Add(m1);
        //    menuItems.Add(m2);
        //    return menuItems;
        //}
    }
}
