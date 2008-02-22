using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;
using ManagedMenuContracts;
using ManagedMenuHostViews;

namespace ManagedMenuHostAdapters
{
    public class MenuContextViewToContract : ContractBase, IMenuContext
    {
        private MenuContextView m_MenuContextView;

        public MenuContextViewToContract(MenuContextView menuContextView)
        {
            m_MenuContextView = menuContextView;
        }

        public string Name
        {
            get { return m_MenuContextView.Name; }
        }

        public string FileName
        {
            get { return m_MenuContextView.FileName; }
        }

        public string Path
        {
            get { return m_MenuContextView.Path; }
        }

        public string FullPath
        {
            get { return m_MenuContextView.FullPath; }
        }

        public ManagedMenuContracts.ContextLevels Levels
        {
            get { return (ManagedMenuContracts.ContextLevels)m_MenuContextView.Levels; }
        }
    }
}
