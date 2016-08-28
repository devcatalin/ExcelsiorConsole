using ConsoleCore.Interfaces;

namespace ConsoleCore
{
    public class Execution : IExecution
    {
        public ExecutionState State { get; set; } = ExecutionState.ExecuteCommand;
        public string CommandLabel { get; set; }
    }
}
