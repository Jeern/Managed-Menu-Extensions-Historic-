using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;

namespace ManagedMenuAddInViews
{
    public abstract class MenuContextView
    {
        public abstract string Name { get; }
        public abstract string FileName { get; }
        public abstract string Path { get; }
        public abstract string FullPath { get; }
        public abstract ContextLevels Levels { get; }
    }
}
