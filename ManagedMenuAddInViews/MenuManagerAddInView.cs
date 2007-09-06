using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;

namespace ManagedMenuAddInViews
{
    /// <summary>
    /// Inherit this class and the MenuItemView and implement them.
    /// When you have done that and placed the resulting assembly
    /// in the ..\AddIns\YourAddInName folder, your AddIn will "kick in".
    /// </summary>
    [AddInBase]
    public abstract class MenuManagerAddInView
    {
        /// <summary>
        /// Returns the name of the Main menu for this AddIn. Each AddIn has just
        /// one MainMenu. If several AddIns has MainMenus with the same name, their
        /// SubMenus will all be placed under the same Main Menu.
        /// </summary>
        /// <param name="types">The ApplicationTypes parameter makes it possible to
        /// enable your AddIn under certain Applications only. The ManagedMenuVS2008 sends
        /// the value ApplicationTypes.VS2008 for instance. You can check on this in your
        /// MainMenu implementation.</param>
        /// <returns></returns>
        public abstract string MainMenu(ApplicationTypes types);
        /// <summary>
        /// When implementing this method, you create your SubMenus as list of 
        /// MenuItemView's.
        /// </summary>
        /// <param name="context">The context is provided to make it possible for
        /// you to enable SubMenus only under certain conditions.</param>
        /// <returns></returns>
        public abstract List<MenuItemView> CreateMenus(MenuContextView context);
        /// <summary>
        /// This method is called when a SubMenus clickEvent is fired.
        /// </summary>
        /// <param name="clickedMenu">The SubMenu that fired the event</param>
        /// <param name="menuContext">The Context in which the Menu was fired. I.e
        /// solution level / project level / ...</param>
        public abstract void MenuClicked(MenuItemView clickedMenu, MenuContextView menuContext);
    }
}
