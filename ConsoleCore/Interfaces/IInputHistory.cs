using System.Collections.Generic;

namespace ConsoleCore.Interfaces
{
    public interface IInputHistory
    {
        List<IInputLine> Lines { get; set; }
        int Index { get; set; }
        IInputLine GetLine();
    }
}
