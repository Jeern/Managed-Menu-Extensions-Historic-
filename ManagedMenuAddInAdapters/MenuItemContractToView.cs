using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuAddInViews;
using ManagedMenuContracts;
using System.Text.RegularExpressions;

namespace ManagedMenuAddInAdapters
{
    /// <summary>
    /// A class that converts the Contracts version of MenuItem (IMenuContract) 
    /// to the AddInViews version (MenuItemView)
    /// </summary>
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

        public override Regex VisibleWhenCompliantName
        {
            get {  return m_MenuContract.VisibleWhenCompliantName; }
        }
    }
}
