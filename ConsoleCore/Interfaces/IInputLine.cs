using System.Collections.Generic;

namespace ConsoleCore.Interfaces
{
    public interface IInputLine
    {
        string Text { get; set; }
        bool IsCommand { get; set; }
        string CommandLabel { get; set; }
        List<string> CommandArgs { get; set; }
    }
}
