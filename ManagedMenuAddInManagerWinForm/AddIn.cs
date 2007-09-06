using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedMenuAddInManagerWinForm
{
    public class AddIn
    {
        private string m_Name;
        private bool m_Enabled;
        public const string EnabledFolder = @"AddIns\";
        public const string DisabledFolder = @"AddInsDisabled\";
        
        public AddIn(string name, bool enabled)
        {
            m_Name = name;
            m_Enabled = enabled;
        }

        public bool Enabled
        {
            get { return m_Enabled; }
        }

        public string Name
        {
            get { return m_Name; }
        }

        public string EnabledFolderName
        {
            get { return EnabledFolder + Name; }
        }

        public string DisabledFolderName
        {
            get { return DisabledFolder + Name; }
        }
    }
}
