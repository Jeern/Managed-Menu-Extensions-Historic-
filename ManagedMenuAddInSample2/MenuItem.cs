using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuAddInViews;

namespace ManagedMenuAddInSample2
{
    public class MenuItem : MenuItemView
    {
        private string m_Caption = "";
        private Guid m_Id = Guid.NewGuid();
        private bool m_Seperator = false;
        private MenuItem m_Parent = null;

        public MenuItem(bool seperator, MenuItem parent)
        {
            m_Seperator = seperator;
            m_Parent = parent;
        }

        public MenuItem(string caption, MenuItem parent)
        {
            m_Caption = caption;
            m_Parent = parent;
        }

        public MenuItem(string caption)
        {
            m_Caption = caption;
        }

        public MenuItem(bool seperator)
        {
            m_Seperator = seperator;
        }

        public override string Caption
        {
            get { return m_Caption; }
        }

        public override Guid Id
        {
            get { return m_Id; }
        }

        public override bool Seperator
        {
            get { return m_Seperator; }
        }

        public override MenuItemView Parent
        {
            get { return m_Parent; }
        }
    }
}
