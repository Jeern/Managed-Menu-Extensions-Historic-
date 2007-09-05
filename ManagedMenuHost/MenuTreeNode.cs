using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuHostViews;

namespace ManagedMenuHost
{
    public class MenuTreeNode
    {
        public MenuTreeNode(string mainMenuCaption)
        {
            m_MenuItem = new MenuItem(mainMenuCaption, Guid.NewGuid(), false, null); 
        }

        public MenuTreeNode(MenuItemView menuItem)
        {
            m_MenuItem = menuItem;
        }

        private MenuItemView m_MenuItem;
        private Dictionary<Guid, MenuTreeNode> m_Children = new Dictionary<Guid, MenuTreeNode>();

        public MenuItemView MenuItem
        {
            get { return m_MenuItem; }
        }

        public Dictionary<Guid, MenuTreeNode> Children
        {
            get { return m_Children; }
        }

    }
}
