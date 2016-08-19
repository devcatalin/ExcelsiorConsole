using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public interface ICommand
    {
        ConsoleWindow Console { get; set; }
        string Label { get; set; }
        string[] Args { get; set; }
        void Execute();
    }
}
