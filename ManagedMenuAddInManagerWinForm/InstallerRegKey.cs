using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Microsoft.Win32;
using System.Windows.Forms;


namespace ManagedMenuAddInManagerWinForm
{
    [RunInstaller(true)]
    public partial class InstallerRegKey : Installer
    {
        public InstallerRegKey()
        {
            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            //Makes sure that the key 
            //HKEY_LOCAL_MACHINE\SOFTWARE\Jern\ManagedMenuExtensions Exists
            try
            {
                RegistryKey key = Registry.LocalMachine.CreateSubKey(AddInProperties.AddInRegKey);
                key.SetValue(AddInProperties.AddInRegValueKey, @"C:\Program Files\Jern\ManagedMenuExtensions\");
                base.OnBeforeInstall(savedState);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hej: " + ex.ToString());
            }
        }
    }
}
