using System.Diagnostics;
using System.Drawing;
using ConsoleCore;
using ConsoleCore.Interfaces;

namespace ExcelsiorConsole.Users.Stunt3r
{
    class StopwatchCmd : Command
    {
        public StopwatchCmd(IConsole c) : base(c)
        {
            Label = "stopwatch";
        }

        readonly Stopwatch _stopwatch = new Stopwatch();

        public override void Execute()
        {
            base.Execute();
            _stopwatch.Start();
            Console.WriteLine("Stopwatch started.", Color.DarkCyan);
        }

        public override void Exit()
        {
            base.Exit();
            Console.WriteLine("Elapsed time: " + _stopwatch.Elapsed.ToString(), Color.DarkCyan);
            _stopwatch.Reset();
        }

        public override void RecievedCommand(object sender, CommandEventArgs e)
        {
            switch (e.Label)
            {
                case "stop": case "exit": case "end": Exit(); break;
                default: Console.WriteLine("Invalid command. Type 'stop' to end the Stopwatch.", Color.DarkRed); break;
            }
        }
    }
}
