using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn;
using System.Windows.Forms;
using ManagedMenuAddInViews;

namespace ManagedMenuAddInSample1
{
    /// <summary>
    /// This sample is very simple. It consists of 2 Menu Items ("Hardy 1", and "Hardy 2") 
    /// under a Main menu called "Managed Menu Extensions". The menus are only shown
    /// in VS2008, but on all levels of the Solution Explorer.
    /// Both Menus displays "Hardy Rocks" in a MessageBox if clicked.
    /// </summary>
    [AddIn("ManagedMenuAddInSample1", Version = "0.1.5.0")]
    public class MenuManager : MenuManagerAddInView 
    {
        public override List<MenuItemView> CreateMenus(MenuContextView context)
        {
            List<MenuItemView> menuItems = new List<MenuItemView>(2);
            MenuItem m1 = new MenuItem("Hardy 1");
            MenuItem m2 = new MenuItem("Hardy 2");
            menuItems.Add(m1);
            menuItems.Add(m2);
            return menuItems;
        }

        public override void MenuClicked(MenuItemView clickedMenu, MenuContextView menuContext)
        {
            MessageBox.Show("Hardy Rocks");
        }

        public override string MainMenu(ApplicationTypes types)
        {
            if (types != ApplicationTypes.VS2008)
                return null;

            return "Managed Menu Extensions";
        }
    }
}
