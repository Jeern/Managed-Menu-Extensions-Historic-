using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Reflection;
using EnvDTE;
using System.Windows.Forms;
using System.IO;
using ManagedMenuHostViews;
using ManagedMenuHost;
using System.Text.RegularExpressions;

namespace ManagedMenuVS2008
{
    public class VSMenuUtil
    {
        private DTE2 m_VSStudio;
        private Dictionary<string, MenuItemView> m_VSMenuToMenuItem = new Dictionary<string, MenuItemView>();
        private Dictionary<Guid, CommandBarControl> m_MenuItemToVSMenu = new Dictionary<Guid, CommandBarControl>();
        private Dictionary<string, MenuTreeNode> m_VSMainMenuToMenuTreeNode = new Dictionary<string, MenuTreeNode>();
        private Dictionary<string, ContextLevels> m_ContextsFromMenus = new Dictionary<string, ContextLevels>();
        private List<CommandBarEvents> menuItemHandlerList = new List<CommandBarEvents>();
        private MMHost m_Host = new MMHost(ApplicationTypes.VS2008);

        public VSMenuUtil(DTE2 vsStudio)
        {
            m_VSStudio = vsStudio;
        }

        public void BuildMenus()
        {
            //ShowVSMainMenus();
            BuildMenuTree(ContextLevels.Solution);
            BuildMenuTree(ContextLevels.SolutionFolder);
            BuildMenuTree(ContextLevels.Project);
            BuildMenuTree(ContextLevels.Folder);
            BuildMenuTree(ContextLevels.References);
            BuildMenuTree(ContextLevels.Item);
            BuildMenuTree(ContextLevels.WebReferences);
        }

        private void BuildMenuTree(ContextLevels level)
        {
            MenuTree menus = m_Host.GetMenus(new MenuContext(level));
            TraverseTree(menus, level);
        }

        private void TraverseTree(MenuTree tree, ContextLevels level)
        {
            foreach (MenuTreeNode node in tree.RootNodes.Values)
            {
                CommandBarPopup menu = AddVSMainMenuItem(VSContextUtil.ContextToVSContext(level), VSContextUtil.ContextToVSContextIndex(level), node.MenuItem.Caption, node);
                AddMainMenuClickEventHandler(menu);
                TraverseChildren(menu, node, level);
            }
        }

        private void TraverseChildren(CommandBarPopup vsmainMenu, MenuTreeNode treeNode, ContextLevels level)
        {
            if (treeNode.Children == null)
                return;

            int menuNumber = 1;
            bool seperator = false;
            foreach (MenuTreeNode node in treeNode.Children.Values)
            {
                if (!node.MenuItem.Seperator)
                {
                    CommandBarControl vsmenuItem = AddVSMenuItem(vsmainMenu, node.MenuItem, menuNumber, seperator, level);
                    AddClickEventHandler(vsmenuItem);
                    menuNumber++;
                    TraverseChildren(vsmainMenu, node, level);
                    seperator = false;
                }
                else
                {
                    seperator = true;
                }
            }
        }

        private CommandBar GetVSMainMenu(string commandBarName, int menuIndex)
        {
            //int idx = 0;
            //foreach (CommandBar bar in (CommandBars)m_VSStudio.DTE.CommandBars)
            //{
            //    if (bar.Name == commandBarName)
            //    {
            //        idx++;
            //        MessageBox.Show("GetMain: " + commandBarName + " Index: " + idx.ToString());
            //    }
            //}

            CommandBar theBar = null;
            int index = 0;
            foreach (CommandBar bar in (CommandBars)m_VSStudio.DTE.CommandBars)
            {
                if (bar.Name == commandBarName)
                {
                    theBar = bar;
                    index++;
                    if (index == menuIndex)
                    {
                        return theBar;
                    }
                }
            }
            return theBar;
//            return ((CommandBars)m_VSStudio.DTE.CommandBars)[commandBarName];
        }

        private void ShowVSMainMenus()
        {
            using(FileStream fs = new FileStream(@"C:\menus.txt", FileMode.Create))
            {
               using(StreamWriter sw = new StreamWriter(fs))
               {
                    foreach (CommandBar bar in (CommandBars)m_VSStudio.DTE.CommandBars)
                    {
                        if(bar.Name != bar.NameLocal) 
                            sw.WriteLine(bar.Name + " ; " + bar.NameLocal); 
                        else
                            sw.WriteLine(bar.Name + " : ");
                    }
               }
            }
        }
        public CommandBarPopup AddVSMainMenuItem(string commandBarName, int menuIndex, string menuName, MenuTreeNode node)
        {
            CommandBarPopup vsmainMenu = GetVSMainMenu(commandBarName, menuIndex).Controls.Add(MsoControlType.msoControlPopup, Missing.Value, Missing.Value, 1, true) as CommandBarPopup;
            vsmainMenu.Caption = menuName;
            vsmainMenu.TooltipText = "";
            vsmainMenu.Tag = Guid.NewGuid().ToString();
            SaveMainMenuInformation(vsmainMenu.Tag, node);
            return vsmainMenu;
        }

        private CommandBarControl AddVSMenuItem(CommandBarPopup vsmainMenu, MenuItemView menuToAdd, int position, bool seperator, ContextLevels level)
        {
            CommandBarControl vsmenuItem = vsmainMenu.Controls.Add(MsoControlType.msoControlButton, 1, "", position, true);
            vsmenuItem.BeginGroup = seperator;
            vsmenuItem.Tag = Guid.NewGuid().ToString();
            vsmenuItem.Caption = menuToAdd.Caption;
            vsmenuItem.TooltipText = "";
            SaveMenuInformation(vsmenuItem, menuToAdd, level); 
            return vsmenuItem;
        }

        private void SaveMenuInformation(CommandBarControl vsMenu, MenuItemView menuToAdd, ContextLevels level)
        {
            m_VSMenuToMenuItem.Add(vsMenu.Tag, menuToAdd);
            m_MenuItemToVSMenu.Add(menuToAdd.Id, vsMenu);
            m_ContextsFromMenus.Add(vsMenu.Tag, level);
        }

        private void SaveMainMenuInformation(string id, MenuTreeNode node)
        {
            m_VSMainMenuToMenuTreeNode.Add(id, node);
        }


        private void AddClickEventHandler(CommandBarControl menuItem)
        {
            CommandBarEvents menuItemHandler = (EnvDTE.CommandBarEvents)m_VSStudio.DTE.Events.get_CommandBarEvents(menuItem);
            menuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(menuItemHandler_Click);
            menuItemHandlerList.Add(menuItemHandler);
        }

        private void AddMainMenuClickEventHandler(CommandBarPopup mainMenu)
        {
            CommandBarEvents mainmenuItemHandler = (EnvDTE.CommandBarEvents)m_VSStudio.DTE.Events.get_CommandBarEvents(mainMenu);
            mainmenuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(mainmenuItemHandler_Click);
            menuItemHandlerList.Add(mainmenuItemHandler);
        }

        internal void menuItemHandler_Click(object CommandBarControl, ref bool Handled, ref bool CancelDefault)
        {
            try
            {
                CommandBarControl cbc = (CommandBarControl)CommandBarControl;
                string id = ((CommandBarControl)CommandBarControl).Tag;
                //MessageBox.Show(m_VSStudio.FileName);
                //MessageBox.Show(cbc.get_accName(null));
                //MessageBox.Show(cbc.OnAction);
                //MessageBox.Show(cbc.Parameter);
                //MessageBox.Show(cbc.ToString());
                //MessageBox.Show(cbc.get_accValue(null));
                ////MessageBox.Show(cbc.accSelection.GetType().FullName);
                //MessageBox.Show(cbc.Control.GetType().FullName);
                //MessageBox.Show(cbc.Type.ToString());
                //int left;
                //int top;
                //int width;
                //int height;
                //cbc.accLocation(out left, out top, out width, out height, null);
                //MessageBox.Show(left.ToString() + " : " + top.ToString() + " : " + width.ToString() + " : " + height.ToString());
                //MessageBox.Show(m_VSStudio.ActiveWindow.Caption);
                //MessageBox.Show(cbc.Index.ToString());
                //MessageBox.Show(cbc.Id.ToString());
                m_Host.MenuClicked(m_VSMenuToMenuItem[id].Id, new MenuContext(SelectedItemName, SelectedItemPath, m_ContextsFromMenus[id]));
            }
            catch (Exception ex)
            {
                MessageBox.Show("VSMenuUtil.menuItemHandler_Click(): " + ex.ToString());
            }
        }

        internal void mainmenuItemHandler_Click(object CommandBarPopup, ref bool Handled, ref bool CancelDefault)
        {
            try
            {
                CommandBarPopup cbp = (CommandBarPopup)CommandBarPopup;
                SetVisibilityChildren(cbp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("VSMenuUtil.mainmenuItemHandler_Click(): " + ex.ToString());
            }
        }

        private UIHierarchyItem SelectedItem
        {
            get
            {
                UIHierarchy uiHierarchy = m_VSStudio.ToolWindows.SolutionExplorer;
                if(uiHierarchy == null)
                    return null;

                object[] items = uiHierarchy.SelectedItems as object[];
                if(items == null || items.Length == 0)
                    return null;

                return items[0] as UIHierarchyItem;
            }
        }

        private string SelectedItemName
        {
            get
            {
                if (SelectedItem == null)
                    return string.Empty;

                return SelectedItem.Name;
            }
        }

        private string SelectedItemPath
        {
            get
            {
                return string.Empty;
            }
        }

        private void SetVisibilityChildren(CommandBarPopup vsmainMenu)
        {
            if (m_VSMainMenuToMenuTreeNode == null || m_VSMainMenuToMenuTreeNode.Count == 0)
                return;

            MenuTreeNode node = m_VSMainMenuToMenuTreeNode[vsmainMenu.Tag];
            SetVisibilityChildren(node);
        }

        private void SetVisibilityChildren(MenuTreeNode node)
        {
            if (node == null)
                return;

            if (node.Children == null)
                return;

            foreach (MenuTreeNode childNode in node.Children.Values)
            {
                SetVisibility(childNode);
                SetVisibilityChildren(childNode);
            }
        }


        /// <summary>
        /// Set visibility of menuitem to true if the selected item complies with the
        /// Regular Expression
        /// </summary>
        /// <param name="vsmenuItem"></param>
        /// <param name="visibleWhenCompliantName"></param>
        private void SetVisibility(MenuTreeNode node)
        {
            if (!node.MenuItem.Seperator)
            {
                m_MenuItemToVSMenu[node.MenuItem.Id].Visible = CheckRegex(node.MenuItem.VisibleWhenCompliantName, SelectedItemName);  
            }
        }

        private bool CheckRegex(Regex regex, string name)
        {
            if (regex == null)
                return true;

            return regex.IsMatch(name);
        }
    }
}
