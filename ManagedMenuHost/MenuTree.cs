using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuHostViews;

namespace ManagedMenuHost
{
    public class MenuTree
    {
        Dictionary<string, MenuTreeNode> m_RootNodes = new Dictionary<string, MenuTreeNode>();
        Dictionary<Guid, MenuTreeNode> m_AllNodes = new Dictionary<Guid, MenuTreeNode>();

        public Dictionary<string, MenuTreeNode> RootNodes
        {
            get { return m_RootNodes; }
        }

        public Dictionary<Guid, MenuTreeNode> AllNodes
        {
            get { return m_AllNodes; }
        }

        public int Count
        {
            get
            {
                if (m_AllNodes == null)
                    return 0;

                return m_AllNodes.Count;
            }
        }

        public int CountRoots
        {
            get
            {
                if (m_RootNodes == null)
                    return 0;

                return m_RootNodes.Count;
            }
        }

        public MenuTreeNode Add(string mainMenuCaption)
        {
            if (!m_RootNodes.ContainsKey(mainMenuCaption))
                return AddNode(mainMenuCaption);
            
            return m_RootNodes[mainMenuCaption];
        }

        public MenuTreeNode Add(string mainMenuCaption, MenuItemView menuItem)
        {
            if (!m_AllNodes.ContainsKey(menuItem.Id))
                return AddNode(mainMenuCaption, menuItem);
            
            return m_AllNodes[menuItem.Id];
        }

        private MenuTreeNode AddNode(string mainMenuCaption)
        {
            MenuTreeNode menuTreeNode = new MenuTreeNode(mainMenuCaption);
            m_RootNodes.Add(mainMenuCaption, menuTreeNode);
            m_AllNodes.Add(menuTreeNode.MenuItem.Id, menuTreeNode);
            return menuTreeNode;
        }

        private MenuTreeNode AddNode(string mainMenuCaption, MenuItemView menuItem)
        {
            MenuTreeNode menuTreeNode = new MenuTreeNode(menuItem);
            m_AllNodes.Add(menuTreeNode.MenuItem.Id, menuTreeNode);
            if (menuItem.Parent != null)
            {
                MenuTreeNode parentTreeNode = Add(mainMenuCaption, menuItem.Parent);
                parentTreeNode.Children.Add(menuItem.Id, menuTreeNode);
            }
            else
            {
                MenuTreeNode parentTreeNode = Add(mainMenuCaption);
                parentTreeNode.Children.Add(menuItem.Id, menuTreeNode);
            }
            return menuTreeNode;
        }
    }
}
