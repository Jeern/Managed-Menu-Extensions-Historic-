using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.AddIn.Hosting;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ManagedMenuHostViews;

namespace ManagedMenuHost
{
    public class MMHost  
    {
        List<MenuManagerHostView> m_MenuManagers = new List<MenuManagerHostView>();
        Dictionary<Guid, MenuManagerHostView> m_AssociatedMenuManagers = new Dictionary<Guid, MenuManagerHostView>();
        Dictionary<string, MenuTree> m_MenusByContext = new Dictionary<string, MenuTree>();
        private ApplicationTypes m_Types;
 
        public MMHost(ApplicationTypes types)
        {
            try
            {
                m_Types = types;
                Collection<AddInToken> addIns = GetAddIns();
                m_MenuManagers = GetMenuManagers(addIns);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MMHost.Ctor(): " + ex.ToString());
            }
        }

        public MenuTree GetMenus(MenuContext context)
        {
            string key = "NULLCONTEXT";
            if (context != null)
            {
                key = context.ToString();
            }
            if (!m_MenusByContext.ContainsKey(key))
                m_MenusByContext[key] = GetMenusFromAddIns(context);
    
            return m_MenusByContext[key];
        }

        private MenuTree GetMenusFromAddIns(MenuContext context)
        {
            MenuTree menuTree = new MenuTree();
            foreach (MenuManagerHostView menuManager in m_MenuManagers)
            {
                string mainMenu = menuManager.MainMenu(m_Types);
                if (mainMenu != null && mainMenu.Trim().Length > 0)
                {
                    menuTree.Add(mainMenu);
                    List<MenuItemView> menuList = menuManager.GetMenus(context);
                    foreach (MenuItemView menu in menuList)
                    {
                        menuTree.Add(mainMenu, menu);
                        if (!m_AssociatedMenuManagers.ContainsKey(menu.Id))
                            m_AssociatedMenuManagers.Add(menu.Id, menuManager);
                    }
                }
            }
            return menuTree;
        }

        public void MenuClicked(Guid clickedMenuId, MenuContext menuContext)
        {
            try
            {
                MenuManagerHostView menuManagerHost = m_AssociatedMenuManagers[clickedMenuId];
                menuManagerHost.MenuClicked(GetMenus(menuContext).AllNodes[clickedMenuId].MenuItem, menuContext);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MMHost.MenuClicked(): " + ex.ToString());
            }
        }

        private Collection<AddInToken> GetAddIns()
        {
            string addInRoot = AddInProperties.MainDirectory;
            string[] messages = AddInStore.Rebuild(addInRoot);
            WriteMessages(messages);
            return AddInStore.FindAddIns(typeof(MenuManagerHostView), addInRoot);
        }

        private List<MenuManagerHostView> GetMenuManagers(Collection<AddInToken> addIns)
        {
            List<MenuManagerHostView> menuManagers = new List<MenuManagerHostView>();
            if (addIns != null && addIns.Count > 0)
            {
                foreach (AddInToken addIn in addIns)
                {
                    menuManagers.Add(addIn.Activate<MenuManagerHostView>(AppDomain.CurrentDomain));
                }
            }
            return menuManagers;
        }

        [Conditional("DEBUG")]
        private void WriteMessages(string[] messages)
        {
            if (messages != null && messages.Length > 0)
            {
                int taeller = 0;
                foreach (string message in messages)
                {
                    taeller++;
                    MessageBox.Show(string.Format("Message {0}: {1}", taeller, message));
                    Debug.WriteLine(string.Format("Message {0}: {1}", taeller, message));
                }
            }
        }
    }
}
