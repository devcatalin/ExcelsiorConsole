using ConsoleCore;
using ConsoleCore.Interfaces;

namespace ExcelsiorConsole.Global
{
    class ClearCmd : Command
    {
        public ClearCmd(IConsole c) : base(c)
        {
            Label = "clear";
        }

        public override void Execute()
        {
            Console.ClearConsole();
        }
    }
}
