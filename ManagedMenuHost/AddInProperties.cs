using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace ManagedMenuHost
{
    /// <summary>
    /// Class to hold different properties of the AddIn
    /// </summary>
    public static class AddInProperties
    {
        /// <summary>
        /// This method returns the Main directory of the AddIn from the registry
        /// throws an error if the registry setting does not exist.
        /// </summary>
        /// <returns></returns>
        public static string MainDirectory
        {
            get
            {
#if DEBUG
                return Environment.CurrentDirectory + @"\output\";
#else
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Jern\ManagedMenuExtensions");
                return Convert.ToString(key.GetValue("AddInMainDirectory"));
#endif
            }
        }
    }
}
