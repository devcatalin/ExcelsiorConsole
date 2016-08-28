using System.Collections.Generic;
using ConsoleCore.Interfaces;

namespace ConsoleCore
{
    public class InputHistory : IInputHistory
    {
        public List<IInputLine> Lines { get; set; } = new List<IInputLine>();
        public int Index { get; set; }

        public IInputLine GetLine()
        {
            return Lines[Index];
        }
    }
}
