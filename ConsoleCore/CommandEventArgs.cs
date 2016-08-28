using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore
{
    public class CommandEventArgs : EventArgs
    {
        public string Label { get; set; }
        public List<string> Args { get; set; }
    }
}
