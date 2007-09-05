using System;

namespace ManagedMenuHostViews
{
    public abstract class MenuItemView
    {
        public abstract string Caption  {  get; }

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
