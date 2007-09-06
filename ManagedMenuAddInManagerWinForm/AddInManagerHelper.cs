using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ManagedMenuAddInManagerWinForm
{
    public static class AddInManagerHelper
    {
        public static void Refresh()
        {
            Initialize();
        }

        private static void Initialize()
        {
            MakeFolder(Environment.CurrentDirectory + AddIn.EnabledFolder);
            MakeFolder(Environment.CurrentDirectory + AddIn.DisabledFolder);
        }

        public static List<AddIn> GetAddIns(string rootFolder)
        {
            return null;
        }

        private static void MakeFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder); 
            }
        }
    }
}
