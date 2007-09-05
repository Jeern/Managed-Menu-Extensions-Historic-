using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedMenuHostViews
{
    [Flags]
    public enum ApplicationTypes
    {
        None = 0,
        VS2008 = 1,
        VS2005 = 2,
        SharpDevelop = 4,
        Explorer = 8
    }
}
