using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedMenuHost.Properties
{
    internal sealed partial class Settings
    {
        internal string BaseDirectory
        {
#if DEBUG
            get { return Environment.CurrentDirectory; }
#else
            get { return MainAddInDirectory; }
#endif
        }
    }
}
