using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.AddIn;
using System.Windows.Forms;
using System.Diagnostics;

namespace ManagedMenuAddInManagerWinForm
{
    public static class AddInManagerHelper
    {
        private static readonly string EnabledFolder = Environment.CurrentDirectory + @"\" + AddIn.EnabledFolder;
        private static readonly string DisabledFolder = Environment.CurrentDirectory + @"\" + AddIn.DisabledFolder;
        private static readonly string AddInViewFolder = Environment.CurrentDirectory + @"\AddInViews";

        public static List<AddIn> Refresh()
        {
            Initialize();
            return GetAddIns(); 
        }

        public static void Save(List<AddIn> list)
        {
            foreach (AddIn addIn in list)
            {
                Save(addIn); 
            }
        }

        private static void Save(AddIn addIn)
        {
            //We doublecheck that the Item is changed before proceeding.
            if (!addIn.Changed)
                return;

            try
            {
                addIn.MoveFile();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private static void Initialize()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(ReflectionOnlyAssemblyResolve);
            MakeFolder(AddInViewFolder);
            MakeFolder(EnabledFolder);
            MakeFolder(DisabledFolder);
            LoadAddInViews();
        }

        /// <summary>
        /// Loads all assemblies in the folder ..\AddInViews\
        /// This is a necessary prerequisite to loading the AddIns themselves.
        /// </summary>
        private static void LoadAddInViews()
        {
            //At first the files in the folder is looped through to find all assemblies
            DirectoryInfo info = new DirectoryInfo(AddInViewFolder);
            foreach (FileInfo fileInfo in info.GetFiles("*.dll"))
            {
                LoadAddInViewAssembly(fileInfo.FullName);
            }

            //Then the subdirectories get the same treatment
            foreach (DirectoryInfo directoryInfo in info.GetDirectories())
            {
                LoadAddInViewAssembly(directoryInfo.FullName); 
            }
        }

        static Assembly ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        public static List<AddIn> GetAddIns()
        {
            List<AddIn> list = GetAddInsEnabled();
            list.AddRange(GetAddInsDisabled());
            return list;
        }

        private static List<AddIn> GetAddInsEnabled()
        {
            return GetAddIns(new List<AddIn>(), EnabledFolder, true);
        }

        private static List<AddIn> GetAddInsDisabled()
        {
            return GetAddIns(new List<AddIn>(), DisabledFolder, false);
        }

        private static List<AddIn> GetAddIns(List<AddIn> list, string folder, bool enabled)
        {
            //At first the files in the folder is looped through to find all assemblies
            DirectoryInfo info = new DirectoryInfo(folder);
            foreach (FileInfo fileInfo in info.GetFiles("*.dll"))
            {
                AddAddInAssembly(list, fileInfo.FullName, fileInfo.Name, enabled);
            }

            //Then the subdirectories get the same treatment
            foreach (DirectoryInfo directoryInfo in info.GetDirectories())
            {
                list = GetAddIns(list, directoryInfo.FullName, enabled);   
            }
            
            return list;
        }

        private static void AddAddInAssembly(List<AddIn> list, string fullFileName, string file, bool enabled)
        {
            try
            {
                Assembly asm = Assembly.ReflectionOnlyLoadFrom(fullFileName);
                Type[] types = asm.GetTypes();
                foreach (Type t in types)
                {
                    IList<CustomAttributeData> attributes = CustomAttributeData.GetCustomAttributes(t);
                    foreach (CustomAttributeData attribute in attributes)
                    {
                        Debug.WriteLine(fullFileName + " contains class with Attribute: " + attribute.ToString());
                        if (attribute.ToString().Contains("System.AddIn.AddInAttribute"))
                        {
                            list.Add(new AddIn(file, asm.Location, enabled));
                            return;
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                Debug.WriteLine("Showing content of ReflectionTypeLoadException and LoaderExceptions:");
                Debug.WriteLine("");
                WriteException(ex);
                foreach (Exception lex in ex.LoaderExceptions)
                {
                    WriteException(lex);
                }
            }
            catch (FileLoadException ex)
            {
                Debug.WriteLine("Showing content of FileLoadException:");
                Debug.WriteLine("");
                WriteException(ex);
            }
        }

        private static void LoadAddInViewAssembly(string fullFileName)
        {
            try
            {
                Assembly asm = Assembly.ReflectionOnlyLoadFrom(fullFileName);
            }
            catch (ReflectionTypeLoadException ex)
            {
                Debug.WriteLine("Showing content of ReflectionTypeLoadException and LoaderExceptions:");
                Debug.WriteLine("");
                WriteException(ex);
                foreach (Exception lex in ex.LoaderExceptions)
                {
                    WriteException(lex);
                }
            }
            catch (FileLoadException ex)
            {
                Debug.WriteLine("Showing content of FileLoadException:");
                Debug.WriteLine("");
                WriteException(ex);
            }
        }

        [Conditional("DEBUG")]
        private static void WriteException(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
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
