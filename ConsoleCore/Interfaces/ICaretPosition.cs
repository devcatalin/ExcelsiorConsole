using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore.Interfaces
{
    public interface ICaretPosition
    {
        int LineStart { get; set; }
        int CommandStart { get; set; }
        void Reset();
    }
}
