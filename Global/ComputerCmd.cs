using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole.Global
{
    class ComputerCmd : Command
    {
        public ComputerCmd(ConsoleWindow c) : base(c)
        {
            CommandLabel = "computer";
            Aliases.Add("comp");
        }

        public override void Execute()
        {
            if (Args == null)
            {
                base.Execute();
                return;
            }

            if (Args.Count() == 1)
            {

            }
        }

    }
}
