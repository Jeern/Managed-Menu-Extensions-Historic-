using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuHostViews;
using ManagedMenuContracts;

namespace ManagedMenuHostAdapters
{
    public class MenuItemContractToView : MenuItemView
    {
        private IMenuContract m_MenuContract;

        public MenuItemContractToView(IMenuContract menuContract)
        {
            m_MenuContract = menuContract;
        }

        public override string Caption
        {
            get { return m_MenuContract.Caption; }
        }

        public override Guid Id
        {
            get { return m_MenuContract.Id; }
        }

        public override bool Seperator
        {
            get { return m_MenuContract.Seperator; }
        }

        public override MenuItemView Parent
        {
            get 
            {
                if (m_MenuContract.Parent == null)
                    return null;

                return new MenuItemContractToView(m_MenuContract.Parent); 
            }
        }
    }
}
