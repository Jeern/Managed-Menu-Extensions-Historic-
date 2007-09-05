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
        string Path { get; }
        ContextLevels Levels { get; }
    }
}
