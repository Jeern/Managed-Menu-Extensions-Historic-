using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn;
using System.Diagnostics;
using ManagedMenuAddInViews;
using System.Text.RegularExpressions;
using System.CodeDom;
using System.IO;
using System.CodeDom.Compiler;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;

namespace ManagedMenuAddInSampleXMLExtractOfClassFile
{
    /// <summary>
    /// This sample gets the first Class of a cs file and generates an XML Document for it.
    /// Only works if the Assembly is in a subdirectory of the class files directory, and if the Assembly is compiled.
    /// Since it uses reflection.
    /// Uses as can be seen a VERY primitive parsing :O)
    /// </summary>
    [AddIn("ManagedMenuAddInSampleXMLExtractOfClassFile", Version = "0.1.4.2")]
    public class MenuManager : MenuManagerAddInView
    {
        public override List<MenuItemView> CreateMenus(MenuContextView context)
        {
            List<MenuItemView> menuItems = null;
            if (context.Levels == ContextLevels.Item)
            {
                menuItems = new List<MenuItemView> { new MenuItem("XML from Class", new Regex(@"\.cs")) };
            }
            return menuItems;
        }

        public override void MenuClicked(MenuItemView clickedMenu, MenuContextView menuContext)
        {
            string className = GetClassName(menuContext.FullPath);
            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("The file apparently does not contain a class");
                return;
            }

            Type theType = null;
            foreach (Assembly asm in GetAssemblies(menuContext.Path))
            {
                theType = GetTypeOfClass(className, asm);
                if (theType != null)
                    break;
            }

            if (theType == null)
            {
                MessageBox.Show("The type " + className + "could not be loaded");
            }

            GenerateXML(menuContext.FullPath, theType);
        }

        public override string MainMenu(ApplicationTypes types)
        {
            if (types != ApplicationTypes.VS2008)
                return null;

            return "XML Extracts";
        }

        private string GetClassName(string fullPath)
        {
            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while(!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if(line.Contains("class") && !(line.Contains("//") && line.IndexOf("//") < line.IndexOf("class")))
                        {
                            string source = line.Substring(line.IndexOf("class")+6);
                            if(source.Contains(" "))
                            {
                                source = source.Substring(0, source.IndexOf(" "));  
                            }
                            return source;
                        }
                    }
                    return String.Empty;
                }
            }
        }

        private List<Assembly> GetAssemblies(string path)
        {
            var assemblies =
                (from file in GetFiles(path, "*.dll")
                 select GetAssembly(file.FullName)).
                 Union(from file in GetFiles(path, "*.exe")
                       select GetAssembly(file.FullName)).ToList();
            return assemblies;
        }

        /// <summary>
        /// Gets the Assembly by loading it with ReflectionOnlyLoad. If it is already loaded. The Assembly is
        /// returned from the AppDomain
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Assembly GetAssembly(string fileName)
        {
            try
            {
                return Assembly.LoadFile(fileName);
            }
            catch (FileLoadException)
            {
                AssemblyName asmName = AssemblyName.GetAssemblyName(fileName);
                foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (asm.GetName().FullName == asmName.FullName)
                    {
                        return asm;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private FileInfo[] GetFiles(string path, string searchpattern)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            return di.GetFiles(searchpattern, SearchOption.AllDirectories);
        }

        private Type GetTypeOfClass(string className, Assembly asm)
        {
            if (string.IsNullOrEmpty(className) || asm == null)
                return null;

            foreach (Type t in asm.GetTypes())
            {
                if (t.Name == className)
                    return t;
            }
            return null;
        }

        private void GenerateXML(string fileName, Type t)
        {
            BindingFlags bind = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            XElement x =
                new XElement("Class",
                    new XAttribute("Name", t.Name),
                    from mi in t.GetMethods(bind)
                    where !mi.Name.StartsWith("get_") && !mi.Name.StartsWith("set_")
                    select new XElement("Method", new XAttribute("Name", mi.Name)),

                    from pi in t.GetProperties(bind)
                    select new XElement("Property", new XAttribute("Name", pi.Name)),

                    from mi in t.GetMembers(bind)
                    where mi.MemberType == MemberTypes.Field 
                    select new XElement("Member", new XAttribute("Name", mi.Name))

             
                    ); 
            fileName = fileName + ".xml";
            x.Save(fileName);
            Process.Start(fileName);

        }

    }
}
