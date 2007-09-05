using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuHostViews;

namespace ManagedMenuHost
{
    public class MenuItem : MenuItemView
    {
        private string m_Caption;
        private Guid m_Id;
        private bool m_Seperator;
        private MenuItem m_Parent;

        public MenuItem(string caption, Guid id, bool seperator, MenuItem parent)
        {
            m_Caption = caption;
            m_Id = id;
            m_Seperator = seperator;
            m_Parent = parent;
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
