using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;
using ManagedMenuContracts;
using ManagedMenuAddInViews;

namespace ManagedMenuAddInAdapters
{
    [AddInAdapterAttribute]
    public class MenuManagerContractToView : ContractBase, IMenuManagerContract
    {
        private MenuManagerAddInView m_MenuManagerView;

        public MenuManagerContractToView(MenuManagerAddInView menuManagerView)
        {
            m_MenuManagerView = menuManagerView;
        }

        public IMenuContract[] GetMenus(IMenuContext menuContext)
        {
            List<MenuItemView> menus = m_MenuManagerView.CreateMenus(new MenuContextContractToView(menuContext));
            List<IMenuContract> menuContracts = new List<IMenuContract>(menus==null?0:menus.Count);
            if (menus != null)
            {
                foreach (MenuItemView menu in menus)
                {
                    menuContracts.Add(new MenuItemViewToContract(menu));
                }
            }
            return menuContracts.ToArray();
        }

        public void MenuClicked(IMenuContract clickedMenu, IMenuContext menuContext)
        {
            m_MenuManagerView.MenuClicked(new MenuItemContractToView(clickedMenu), new MenuContextContractToView(menuContext));
        }

        public string MainMenu(ManagedMenuContracts.ApplicationTypes types)
        {
            return m_MenuManagerView.MainMenu((ManagedMenuAddInViews.ApplicationTypes)types);
        }
    }
}
