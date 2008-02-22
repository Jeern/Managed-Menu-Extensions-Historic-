using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Pipeline;
using System.AddIn.Contract;

namespace ManagedMenuContracts
{
    public interface IMenuContext : IContract
    {
        string Name { get; }
        string FileName { get; }
        string Path { get; }
        string FullPath { get; }
        ContextLevels Levels { get; }
    }
}
