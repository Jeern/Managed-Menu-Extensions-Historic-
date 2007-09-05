using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;
using ManagedMenuContracts;
using ManagedMenuHostViews;

namespace ManagedMenuHostAdapters
{
    public class MenuItemViewToContract : ContractBase, IMenuContract
    {
        private MenuItemView m_MenuItemView;

        public MenuItemViewToContract(MenuItemView menuItemView)
        {
            m_MenuItemView = menuItemView;
        }

        public string Caption
        {
            get { return m_MenuItemView.Caption; }
        }

        public Guid Id
        {
            get { return m_MenuItemView.Id; }
        }

        public bool Seperator
        {
            get { return m_MenuItemView.Seperator; }
        }

        public IMenuContract Parent
        {
            get 
            {
                if(m_MenuItemView.Parent == null)
                    return null;

                return new MenuItemViewToContract(m_MenuItemView.Parent); 
            }
        }
    }
}
