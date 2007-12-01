using System;
using System.AddIn.Pipeline;
using System.Text.RegularExpressions;

namespace ManagedMenuAddInViews
{
    /// Inherit this class and the MenuManagerAddInView and implement them.
    /// When you have done that and placed the resulting assembly
    /// in the ..\AddIns\YourAddInName folder, your AddIn will "kick in".
    /// I suggest that you make a constructor in the class that inherits
    /// this one, that takes the 5 property types as parameters.
    /// However Id could just be set to a new guid in the Constructor,
    /// Seperator is often not needed.
    /// Parent is always null in a Visual Studio AddIn
    /// VisibleWhenCompliantName is null, unless you need advanced cases, for instance you only want to show Menu for *.cs files.
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

        public abstract Regex VisibleWhenCompliantName
        {
            get;
        }
    }
}
