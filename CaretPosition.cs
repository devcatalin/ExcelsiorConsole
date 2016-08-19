using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public class CaretPosition
    {
        public int LineStart { get; set; }
        public int CommandStart { get; set; }

        private readonly Console _console;

        public CaretPosition(Console c)
        {
            _console = c;
        }

        public void Reset()
        {
            LineStart = _console.Text.Length + 1;
            CommandStart = LineStart + _console.Name.Length + 3;
        }
    }
}
