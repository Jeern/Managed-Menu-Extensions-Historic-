using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedMenuHostViews
{
    public abstract class MenuManagerHostView
    {
        public abstract string MainMenu(ApplicationTypes types);
        public abstract List<MenuItemView> GetMenus(MenuContextView context);
        public abstract void MenuClicked(MenuItemView clickedMenu, MenuContextView menuContext);
    }
}
