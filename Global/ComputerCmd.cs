using ConsoleCore;
using ConsoleCore.Interfaces;

namespace ExcelsiorConsole.Global
{
    class ComputerCmd : Command
    {
        public ComputerCmd(IConsole c) : base(c)
        {
            Label = "computer";
            Aliases.Add("comp");
        }

        public override void Execute()
        {
            if (Args == null)
            {
                base.Execute();
                return;
            }

            if (Args.Count == 1)
            {

            }
        }

    }
}
