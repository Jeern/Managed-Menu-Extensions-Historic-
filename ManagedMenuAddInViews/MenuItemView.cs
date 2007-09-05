using System;
using System.AddIn.Pipeline;

namespace ManagedMenuAddInViews
{
    public abstract class MenuItemView
    {
        public abstract string Caption
        {
            get;
        }

        public abstract Guid Id
        {
            get;
        }

        public abstract bool Seperator
        {
            get;
        }

        public abstract MenuItemView Parent
        {
            get;
        }
    }
}
