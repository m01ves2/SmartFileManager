using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFileManager.Core.Interfaces
{
    public interface ICommandsAware
    {
        void SetCommands(IReadOnlyList<ICommand> commands);
    }
}
