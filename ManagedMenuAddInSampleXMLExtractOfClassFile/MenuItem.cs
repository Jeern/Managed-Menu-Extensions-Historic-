using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuAddInViews;
using System.Text.RegularExpressions;

namespace ManagedMenuAddInSampleXMLExtractOfClassFile
{
    public class MenuItem : MenuItemView
    {
        private string m_Caption = "";
        private Guid m_Id = Guid.NewGuid();
        private bool m_Seperator = false;
        private Regex m_VisibleWhenCompliantName = null;

        public MenuItem(bool seperator)
        {
            m_Seperator = seperator;
        }

        public MenuItem(string caption)
        {
            m_Caption = caption;
        }

        public MenuItem(string caption, Regex visibleWhenCompliantName)
        {
            m_Caption = caption;
            m_VisibleWhenCompliantName = visibleWhenCompliantName;
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
            get { return null; }
        }

        public override Regex VisibleWhenCompliantName
        {
            get { return m_VisibleWhenCompliantName; }
        }
    }
}
