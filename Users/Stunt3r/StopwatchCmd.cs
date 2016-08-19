using System.Diagnostics;
using System.Drawing;

namespace ExcelsiorConsole.Users.Stunt3r
{
    class StopwatchCmd : Command
    {
        public StopwatchCmd(ConsoleWindow c) : base(c)
        {
            Label = "stopwatch";
        }

        readonly Stopwatch stopwatch = new Stopwatch();

        public override void Execute()
        {
            base.Execute();
            stopwatch.Start();
            Console.WriteLine("Stopwatch started.", Color.DarkCyan);
        }

        public override void Exit()
        {
            base.Exit();
            Console.WriteLine("Elapsed time: " + stopwatch.Elapsed.ToString(), Color.DarkCyan);
            stopwatch.Reset();
        }

        public override void Console_RecievedCommand(object sender, ConsoleWindow.CommandEventArgs e)
        {
            switch (e.Label)
            {
                case "stop": case "exit": case "end": Exit(); break;
                default: Console.WriteLine("Invalid command. Type 'stop' to end the Stopwatch.", Color.DarkRed); break;
            }
        }
    }
}
