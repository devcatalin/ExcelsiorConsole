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

        int TextLength { get; }
        int SelectionStart { get; set; }
        int SelectionLength { get; set; }
        System.Drawing.Font SelectionFont { get; set; }
        System.Windows.Forms.DockStyle Dock { get; set; }

        bool Focus();
        string GetInputText();
        void ClearConsole();
        void WriteLine(string line, System.Drawing.Color color);
        void NewLine();
        void HandleInput(IInputLine inputLine);
    }
}
