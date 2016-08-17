using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole.Global.Commands
{
    class ClearCmd : Command
    {
        public ClearCmd(ConsoleWindow c) : base(c)
        {
            CommandLabel = "clear";
        }

        public override void Execute()
        {
            console.ClearConsole();
        }
    }
}
