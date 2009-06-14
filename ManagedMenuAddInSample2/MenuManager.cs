using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn;
using System.Diagnostics;
using System.Windows.Forms;
using ManagedMenuAddInViews;
using System.Text.RegularExpressions;

namespace ManagedMenuAddInSample2
{
    /// <summary>
    /// This sample shows 4 menuitems "New", "Open", "Add" and "Close" if a solution is rightclicked
    /// And 3 if anything else is rightclicked. All Items are under the Main Menu "Managed Menu Extensions"
    /// If A menu is clicked. Info about the menus name, and the context in which it was clicked is displayed.
    /// </summary>
    [AddIn("ManagedMenuAddInSample2", Version = "0.1.4.1")]
    public class MenuManager : MenuManagerAddInView
    {
        public override List<MenuItemView> CreateMenus(MenuContextView context)
        {
            List<MenuItemView> menuItems;
            if (context.Levels == ContextLevels.Solution)
            {
                menuItems = new List<MenuItemView> { new MenuItem("New"), new MenuItem("Open"), new MenuItem(true), new MenuItem("Add"), new MenuItem("Close") };
            }
            else  //hej.ico
            {
                menuItems = new List<MenuItemView> { new MenuItem("New"), new MenuItem("Open"), new MenuItem("Close"), new MenuItem("OnlyIco", new Regex(@"\.ico")) };
            }
            return menuItems;
        }

        public override void MenuClicked(MenuItemView clickedMenu, MenuContextView menuContext)
        {
            MessageBox.Show("Menu: " + clickedMenu.Caption + 
                "\nContext: " + menuContext.Levels.ToString() + 
                "\nName: " + menuContext.Name +
                "\nFileName: " + menuContext.FileName +
                "\nPath: " + menuContext.Path +
                "\nFullPath: " + menuContext.FullPath);
        }

        public override string MainMenu(ApplicationTypes types)
        {
            if (types != ApplicationTypes.VS2008)
                return null;

            return "Managed Menu Extensions";
        }
    }
}
