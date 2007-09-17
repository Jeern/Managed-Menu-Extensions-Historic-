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

namespace ManagedMenuVS2008
{
    public class VSMenuUtil
    {
        private DTE2 m_VSStudio;
        private Dictionary<string, MenuItemView> m_AddedMenus = new Dictionary<string, MenuItemView>();
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
            BuildMenuTree(ContextLevels.Project);
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
                CommandBarPopup menu = AddVSMainMenuItem(VSContextUtil.ContextToVSContext(level), VSContextUtil.ContextToVSContextIndex(level), node.MenuItem.Caption);
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
        public CommandBarPopup AddVSMainMenuItem(string commandBarName, int menuIndex, string menuName)
        {
            CommandBarPopup vsmainMenu = GetVSMainMenu(commandBarName, menuIndex).Controls.Add(MsoControlType.msoControlPopup, Missing.Value, Missing.Value, 1, true) as CommandBarPopup;
            vsmainMenu.Caption = menuName;
            vsmainMenu.TooltipText = "";
            return vsmainMenu;
        }

        private CommandBarControl AddVSMenuItem(CommandBarPopup vsmainMenu, MenuItemView menuToAdd, int position, bool seperator, ContextLevels level)
        {
            CommandBarControl vsmenuItem = vsmainMenu.Controls.Add(MsoControlType.msoControlButton, 1, "", position, true);
            vsmenuItem.BeginGroup = seperator;
            vsmenuItem.Tag = Guid.NewGuid().ToString();
            vsmenuItem.Caption = menuToAdd.Caption;
            vsmenuItem.TooltipText = "";
            SaveMenuInformation(vsmenuItem.Tag, menuToAdd, level); 
            return vsmenuItem;
        }

        private void SaveMenuInformation(string Id, MenuItemView menuToAdd, ContextLevels level)
        {
            m_AddedMenus.Add(Id, menuToAdd);
            m_ContextsFromMenus.Add(Id, level);
        }

        private void AddClickEventHandler(CommandBarControl menuItem)
        {
            CommandBarEvents menuItemHandler = (EnvDTE.CommandBarEvents)m_VSStudio.DTE.Events.get_CommandBarEvents(menuItem);
            menuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(menuItemHandler_Click);
            menuItemHandlerList.Add(menuItemHandler);
        }

        internal void menuItemHandler_Click(object CommandBarControl, ref bool Handled, ref bool CancelDefault)
        {
            try
            {
                CommandBarControl cbc = (CommandBarControl)CommandBarControl;
                string id = ((CommandBarControl)CommandBarControl).Tag;
                m_Host.MenuClicked(m_AddedMenus[id].Id, new MenuContext(cbc.DescriptionText, "", m_ContextsFromMenus[id]));
            }
            catch (Exception ex)
            {
                MessageBox.Show("VSMenuUtil.menuItemHandler_Click(): " + ex.ToString());
            }
        }		

    }
}
