using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public enum ExecutionState { RecieveCommands, ExecuteCommand }
    public class Execution
    {
        public ExecutionState State { get; set; } = ExecutionState.ExecuteCommand;
        public string CommandLabel { get; set; }
    }
}
