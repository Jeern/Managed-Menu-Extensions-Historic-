using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ManagedMenuVS2008
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
	public class Connect : IDTExtensibility2
	{
        //private DTE2 m_VSStudio;
        private VSMenuUtil m_VSMenuUtil;
        //private AddIn m_AddInInstance;
        //private MMHost m_Host = new MMHost();
        //private Dictionary<string, MenuItemView> m_AddedMenus = new Dictionary<string, MenuItemView>();
        //private List<CommandBarEvents> menuItemHandlerList = new List<CommandBarEvents>();

        //private MenuContext m_TestContext = new MenuContext("", "", ContextLevel.Solution); 
        
        /// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
		public Connect()
		{
		}

		/// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
            try
            {
                m_VSMenuUtil = new VSMenuUtil((DTE2)application);
                m_VSMenuUtil.BuildMenus();
                //m_VSStudio = (DTE2)application;
                //m_AddInInstance = (AddIn)addInInst;
                //MenuTree menus = m_Host.GetMenus(m_TestContext);
                //TraverseTree(menus);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect.OnConnection(): " + ex.ToString());
            }
        }

		/// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		/// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
        }

		/// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
		}

        //public void TraverseTree(MenuTree tree)
        //{
        //    foreach (MenuTreeNode node in tree.RootNodes.Values)
        //    {
        //        CommandBarPopup menu = AddVSMainMenuItem("Solution", node.MenuItem.Caption);
        //        TraverseChildren(menu, node);
        //    }
        //}

        //public void TraverseChildren(CommandBarPopup vsmainMenu, MenuTreeNode treeNode)
        //{
        //    if (treeNode.Children == null)
        //        return;

        //    int menuNumber = 1;
        //    bool seperator = false;
        //    foreach (MenuTreeNode node in treeNode.Children.Values)
        //    {
        //        if (!node.MenuItem.Seperator)
        //        {
        //            CommandBarControl vsmenuItem = AddVSMenuItem(vsmainMenu, node.MenuItem, menuNumber, seperator);
        //            AddClickEventHandler(vsmenuItem);
        //            menuNumber++;
        //            TraverseChildren(vsmainMenu, node);
        //            seperator = false;
        //        }
        //        else
        //        {
        //            seperator = true;
        //        }
        //    }
        //}

        //private CommandBar GetVSMainMenu(string commandBarName)
        //{
        //    return ((CommandBars)m_VSStudio.DTE.CommandBars)[commandBarName];
        //}

        //public CommandBarPopup AddVSMainMenuItem(string commandBarName, string menuName)
        //{
        //    CommandBarPopup vsmainMenu = GetVSMainMenu(commandBarName).Controls.Add(MsoControlType.msoControlPopup, Missing.Value, Missing.Value, 1, true) as CommandBarPopup;
        //    vsmainMenu.Caption = menuName;
        //    vsmainMenu.TooltipText = "This is " + menuName;
        //    return vsmainMenu;
        //}

        //private CommandBarControl AddVSMenuItem(CommandBarPopup vsmainMenu, MenuItemView menuToAdd, int position, bool seperator)
        //{
        //    CommandBarControl vsmenuItem = vsmainMenu.Controls.Add(MsoControlType.msoControlButton, 1, "", position, true);
        //    vsmenuItem.BeginGroup = seperator;
        //    vsmenuItem.Tag = Guid.NewGuid().ToString();
        //    vsmenuItem.Caption = menuToAdd.Caption;
        //    vsmenuItem.TooltipText = "This is " + menuToAdd.Caption;
        //    m_AddedMenus.Add(vsmenuItem.Tag, menuToAdd);
        //    return vsmenuItem;
        //}
        
        //private void AddClickEventHandler(CommandBarControl menuItem)
        //{
        //    CommandBarEvents menuItemHandler = (EnvDTE.CommandBarEvents)m_VSStudio.DTE.Events.get_CommandBarEvents(menuItem);
        //    menuItemHandler.Click += new _dispCommandBarControlEvents_ClickEventHandler(menuItemHandler_Click);
        //    menuItemHandlerList.Add(menuItemHandler);
        //}

        //internal void menuItemHandler_Click(object CommandBarControl, ref bool Handled, ref bool CancelDefault)
        //{
        //    m_Host.MenuClicked(m_AddedMenus[((CommandBarControl)CommandBarControl).Tag].Id, m_TestContext);
        //}		

	}
}