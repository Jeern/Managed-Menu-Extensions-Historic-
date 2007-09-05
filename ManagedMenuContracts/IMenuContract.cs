using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace ManagedMenuContracts
{
    public interface IMenuContract : IContract
    {
        string Caption { get; }
        Guid Id { get; }
        bool Seperator { get; }
        IMenuContract Parent { get; }
    }
}
