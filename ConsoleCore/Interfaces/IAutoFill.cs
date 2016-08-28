using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore.Interfaces
{
    public interface IAutoFill
    {
        int Index { get; set; }
        bool Enabled { get; set; }
        List<string> Commands { get; set; }
        string GetCommand();
        void GoUp();
        void GoDown();
        void Reset();
        void FilterCommands(string input);
    }
}
