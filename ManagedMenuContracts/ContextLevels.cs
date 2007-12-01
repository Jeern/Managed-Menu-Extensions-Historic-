using System;

namespace ManagedMenuContracts
{
    [Flags]
    public enum ContextLevels
    {
        None = 0,
        Solution = 1,
        Project = 2,
        SolutionFolder = 4,
        Folder = 8,
        References = 32,
        WebReferences = 64,
        AssemblyInfo = 128,
        Item = 256,
        OtherItem = 512
    }
}
