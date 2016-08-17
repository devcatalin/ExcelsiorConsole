using System.Diagnostics;
using System.Drawing;

namespace ExcelsiorConsole.Users.Stunt3r
{
    class StopwatchCmd : Command
    {
        public StopwatchCmd(ConsoleWindow c) : base(c)
        {
            CommandLabel = "stopwatch";
        }

        readonly Stopwatch stopwatch = new Stopwatch();

        public override void Execute()
        {
            base.Execute();
            stopwatch.Start();
            console.Write("Stopwatch started.", Color.DarkCyan);
        }

        public override void Exit()
        {
            base.Exit();
            console.Write("Elapsed time: " + stopwatch.Elapsed.ToString(), Color.DarkCyan);
            stopwatch.Reset();
        }

        public override void Console_RecievedCommand(object sender, ConsoleWindow.CommandEventArgs e)
        {
            switch (e.Command)
            {
                case "stop": case "exit": case "end": Exit(); break;
                default: console.Write("Invalid command. Type 'stop' to end the Stopwatch.", Color.DarkRed); break;
            }
        }
    }
}
