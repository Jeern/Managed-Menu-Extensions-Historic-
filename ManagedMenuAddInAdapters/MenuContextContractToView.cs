using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;
using System.ComponentModel;
using ManagedMenuAddInViews;
using ManagedMenuContracts;

namespace ManagedMenuAddInAdapters
{
    /// <summary>
    /// A class that converts the Contracts version of MenuContext (IMenuContext) 
    /// to the AddInViews version (MenuContextView)
    /// </summary>
    public class MenuContextContractToView : MenuContextView 
    {
        private IMenuContext m_MenuContext;
        public MenuContextContractToView(IMenuContext menuContext)
        {
            m_MenuContext = menuContext;
        }

        public override string Name
        {
            get { return m_MenuContext.Name; }
        }

        public override string Path
        {
            get { return m_MenuContext.Path; }
        }

        public override ManagedMenuAddInViews.ContextLevels Levels
        {
            get { return (ManagedMenuAddInViews.ContextLevels)m_MenuContext.Levels; }
        }
    }
}
