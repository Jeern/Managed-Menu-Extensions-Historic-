using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuHostViews;

namespace ManagedMenuHost
{
    public class MenuContext : MenuContextView
    {

        private string m_Name;
        private string m_Path;
        private ContextLevels m_Levels;

        public MenuContext(string name, string path, ContextLevels levels)
        {
            m_Name = name;
            m_Path = path;
            m_Levels = levels;
        }

        public MenuContext(ContextLevels level) : this("", "", level)
        {
        }

        public override string Name
        {
            get { return m_Name; }
        }

        public override string Path
        {
            get { return m_Path; }
        }

        public override ContextLevels Levels
        {
            get { return m_Levels; }
        }

        private string ContextKey(MenuContextView context)
        {
            return context.Levels.ToString();
        }

        public override string ToString()
        {
            return ContextKey(this);
        }

    }
}
