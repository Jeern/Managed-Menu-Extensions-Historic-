using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;
using ManagedMenuContracts;
using ManagedMenuAddInViews;

namespace ManagedMenuAddInAdapters
{
    /// <summary>
    /// A class that converts the AddInViews version of MenuItem (MenuItemView) 
    /// to the Contracts version (IMenuContract)
    /// </summary>
    public class MenuItemViewToContract : ContractBase, IMenuContract
    {
        private MenuItemView m_MenuItem;

        public MenuItemViewToContract(MenuItemView menuItem)
        {
            m_MenuItem = menuItem;
        }

        public string Caption
        {
            get { return m_MenuItem.Caption; }
        }

        public Guid Id
        {
            get { return m_MenuItem.Id; }
        }

        public bool Seperator
        {
            get { return m_MenuItem.Seperator; }
        }

        public IMenuContract Parent
        {
            get
            {
                if (m_MenuItem.Parent == null)
                    return null;
                
                return new MenuItemViewToContract(m_MenuItem.Parent); 
            }
        }
    }
}
