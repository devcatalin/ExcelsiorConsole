using System.Collections.Generic;

namespace ConsoleCore.Interfaces
{
    public interface ICommand
    {
        IConsole Console { get; set; }
        string Label { get; set; }
        List<string> Args { get; set; }
        List<string> Aliases { get; set; }
        void Execute();
        bool CanExecute();
        void Exit();
        void RecievedCommand(object sender, CommandEventArgs e);
    }
}
