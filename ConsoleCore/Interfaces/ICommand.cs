using System.Collections.Generic;
using Console = ConsoleCore.Console;

namespace ConsoleCore.Interfaces
{
    public interface ICommand
    {
        Console Console { get; set; }
        string Label { get; set; }
        List<string> Args { get; set; }
        List<string> Aliases { get; set; }
        void Execute();
        bool CanExecute();
    }
}
