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
                case ContextLevels.SolutionFolder:
                    return VSContextConstants.SolutionFolder;
                case ContextLevels.References:
                    return VSContextConstants.References;
                case ContextLevels.Item:
                    return VSContextConstants.Item;
                case ContextLevels.WebReferences:
                    return VSContextConstants.WebReferences;
                case ContextLevels.Folder:
                    return VSContextConstants.Folder;
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
                case ContextLevels.SolutionFolder:
                    return 1;
                case ContextLevels.References:
                    return 1;
                case ContextLevels.Item:
                    return 1;
                case ContextLevels.WebReferences:
                    return 1;
                case ContextLevels.Folder:
                    return 1;
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
