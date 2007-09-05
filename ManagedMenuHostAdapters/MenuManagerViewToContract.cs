using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;
using ManagedMenuHostViews;
using ManagedMenuContracts;
using ManagedMenuHostAdapters;

namespace MenuManagerHostAdapters
{
    [HostAdapter]
    public class MenuManagerViewToContract : MenuManagerHostView
    {
        private IMenuManagerContract m_MenuManagerContract;
        private ContractHandle m_Handle;

        public MenuManagerViewToContract(IMenuManagerContract menuManagerContract)
        {
            m_MenuManagerContract = menuManagerContract;
            m_Handle = new ContractHandle(m_MenuManagerContract);
        }

        public override List<MenuItemView> GetMenus(MenuContextView context)
        {
            IMenuContract[] menus = m_MenuManagerContract.GetMenus(new MenuContextViewToContract(context));
            List<MenuItemView> menuItems = new List<MenuItemView>(menus.Length);
            foreach (IMenuContract menu in menus)
            {
                menuItems.Add(new MenuItemContractToView(menu));
            }
            return menuItems;
        }

        public override void MenuClicked(MenuItemView clickedMenu, MenuContextView menuContext)
        {
            m_MenuManagerContract.MenuClicked(new MenuItemViewToContract(clickedMenu), new MenuContextViewToContract(menuContext)); 
        }

        public override string MainMenu(ManagedMenuHostViews.ApplicationTypes types)
        {
            return m_MenuManagerContract.MainMenu((ManagedMenuContracts.ApplicationTypes)types); 
        }
    }
}
