using System;
using System.Collections.Generic;
using System.Text;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace ManagedMenuContracts
{
    [AddInContract]
    public interface IMenuManagerContract : IContract
    {
        string MainMenu(ApplicationTypes types);
        IMenuContract[] GetMenus(IMenuContext menuContext);
        void MenuClicked(IMenuContract clickedMenu, IMenuContext menuContext);
    }
}
