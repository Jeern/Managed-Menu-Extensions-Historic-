using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;

namespace ManagedMenuAddInViews
{
    [AddInBase]
    public abstract class MenuManagerAddInView
    {
        public abstract string MainMenu(ApplicationTypes types);
        public abstract List<MenuItemView> CreateMenus(MenuContextView context);
        public abstract void MenuClicked(MenuItemView clickedMenu, MenuContextView menuContext);
    }
}
