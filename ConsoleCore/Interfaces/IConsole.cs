using System;
using System.Collections.Generic;

namespace ConsoleCore.Interfaces
{
    public interface IConsole
    {
        string Name { get; }

        ICaretPosition CaretPosition { get; set; }

        IExecution Execution { get; set; }

        List<ICommand> Commands { get; set; }

        IInputHistory InputHistory { get; set; }

        IAutoFill AutoFill { get; set; }

        event EventHandler<CommandEventArgs> RecievedCommand;

        void ClearConsole();
        //void WriteLine(string line);
        void NewLine();
        string GetInputText();
        void HandleInput(IInputLine inputLine);

    }
}
