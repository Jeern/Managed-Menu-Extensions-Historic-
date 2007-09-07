using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ManagedMenuAddInManagerWinForm
{
    public class AddIn
    {
        private string m_Name;
        private string m_EnabledFolderName;
        private string m_DisabledFolderName;
        private bool m_Enabled;
        private bool m_Changed = false;
        public const string EnabledFolder = @"AddIns\";
        public const string DisabledFolder = @"AddInsDisabled\";
        
        public AddIn(string name, string folder, bool enabled)
        {
            m_Name = name;
            m_Enabled = enabled;
            if (enabled)
            {
                m_EnabledFolderName = folder;
                m_DisabledFolderName = folder.Replace(EnabledFolder, DisabledFolder);
            }
            else
            {
                m_DisabledFolderName = folder;
                m_EnabledFolderName = folder.Replace(DisabledFolder, EnabledFolder);   
            }
        }

        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (value != m_Enabled)
                {
                    m_Enabled = value;
                    m_Changed = !m_Changed;
                }
            }
        }

        public bool Changed
        {
            get { return m_Changed; }
        }


        public string Name
        {
            get { return m_Name; }
        }

        public string EnabledFolderFile
        {
            get { return m_EnabledFolderName; }
        }

        public string EnabledFolderFull
        {
            get { return m_EnabledFolderName.Replace(Name, ""); }
        }

        public string DisabledFolderFile
        {
            get { return m_DisabledFolderName; }
        }

        public string DisabledFolderFull
        {
            get { return m_DisabledFolderName.Replace(Name, ""); }
        }

        public void MoveFile()
        {
            if (!Changed)
                return;

            if (Enabled)
            {
                if (!Directory.Exists(EnabledFolderFull))
                {
                    Directory.CreateDirectory(EnabledFolderFull);
                }
                File.Move(DisabledFolderFile, EnabledFolderFile);
            }
            else
            {
                if (!Directory.Exists(DisabledFolderFull))
                {
                    Directory.CreateDirectory(DisabledFolderFull);
                }
                File.Move(EnabledFolderFile, DisabledFolderFile);
            }
            m_Changed = false;

        }
    }
}
