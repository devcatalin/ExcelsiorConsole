using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public class InputLine
    {
        public string Text { get; set; }
        public bool IsCommand { get; set; }
        public string CommandLabel { get; set; }
        public string[] CommandArgs { get; set; }
    }
}
