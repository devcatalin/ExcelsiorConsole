namespace ConsoleCore.Interfaces
{
    public enum ExecutionState { RecieveCommands, ExecuteCommand }
    public interface IExecution
    {
        ExecutionState State { get; set; }
        string CommandLabel { get; set; }
    }
}
