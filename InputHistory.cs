using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public class InputHistory
    {
        public List<InputLine> Lines { get; set; } = new List<InputLine>();
        public int Index;

        public InputLine GetLine()
        {
            return Lines[Index];
        }
    }
}
