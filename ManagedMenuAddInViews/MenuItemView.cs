using System;
using System.AddIn.Pipeline;

namespace ManagedMenuAddInViews
{
    /// Inherit this class and the MenuManagerAddInView and implement them.
    /// When you have done that and placed the resulting assembly
    /// in the ..\AddIns\YourAddInName folder, your AddIn will "kick in".
    /// I suggest that you make a constructor in the class that inherits
    /// this one, that takes the 4 property types as parameters.
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
