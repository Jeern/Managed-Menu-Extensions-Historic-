using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;


namespace ManagedMenuAddInSamplesInstaller
{
    [RunInstaller(true)]
    public partial class InstallerRemoveFolders : Installer
    {
        public InstallerRemoveFolders()
        {
            InitializeComponent();
        }

        protected override void OnAfterUninstall(IDictionary savedState)
        {
            base.OnAfterUninstall(savedState);
            string mainDir = GetMainDir(Context.Parameters["assemblypath"]);
            DeleteAddIns(mainDir);
            DeleteAddInsDisabled(mainDir);
        }

        private const string AddIns = @"\AddIns";
        private const string AddInsDisabled = @"\AddInsDisabled";

        private string GetMainDir(string assemblypath)
        {
            if (assemblypath.EndsWith(".dll"))
            {
                return GetMainDir(RemovePath(assemblypath));
            }

            if (assemblypath.Contains(AddIns))
            {
                return GetMainDir(RemovePath(assemblypath));
            }

            return assemblypath;
        }

        private string RemovePath(string path)
        {
            return path.Substring(0, path.LastIndexOf(@"\"));
        }

        private void DeleteAddIns(string mainDir)
        {
            DeleteFromSubFolder(mainDir, AddIns);
        }

        private void DeleteAddInsDisabled(string mainDir)
        {
            DeleteFromSubFolder(mainDir, AddInsDisabled);
        }

        private void DeleteFromSubFolder(string mainDir, string subFolder)
        {
            string dir = mainDir + subFolder + @"\";
            DirectoryInfo directory = new DirectoryInfo(dir);
            foreach (FileInfo file in directory.GetFiles("ManagedMenuAddInSample*.dll", SearchOption.AllDirectories))
            {
                file.Delete();
            }
        }

    }
}
