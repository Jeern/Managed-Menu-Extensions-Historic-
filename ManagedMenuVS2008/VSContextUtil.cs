using System;
using System.Collections.Generic;
using System.Text;
using ManagedMenuHostViews;

namespace ManagedMenuVS2008
{
    public static class VSContextUtil
    {
        public static bool IsSolution()
        {
            return false;
        }

        public static string ContextToVSContext(ContextLevels level)
        {
            switch (level)
            {
                case ContextLevels.Solution:
                    return VSContextConstants.Solution;
                case ContextLevels.Project:
                    return VSContextConstants.Project;
                default:
                    return VSContextConstants.OtherItem;
            }
        }
        public static int ContextToVSContextIndex(ContextLevels level)
        {
            switch (level)
            {
                case ContextLevels.Solution:
                    return 1;
                case ContextLevels.Project:
                    return 2;
                default:
                    return 1;
            }
        }

        public static ContextLevels VSContextToContext(string level)
        {
            switch (level)
            {
                case VSContextConstants.Solution:
                    return ContextLevels.Solution;
                case VSContextConstants.Project:
                    return ContextLevels.Project;
                default:
                    return ContextLevels.OtherItem; 
            }
        }

    }
}
