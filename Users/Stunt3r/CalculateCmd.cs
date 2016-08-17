using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;

namespace ExcelsiorConsole.Users.Stunt3r
{
    class CalculateCmd : Command
    {
        public CalculateCmd(ConsoleWindow c) : base(c)
        {
            CommandLabel = "calculate";
            Aliases.Add("calc");
        }

        public override void Execute()
        {
            if (Args != null)
            {
                string expressionString = string.Join("", Args);
                Calculate(expressionString);
            }
            else
            {
                base.Execute();
            }
        }

        public override void Console_RecievedCommand(object sender, ConsoleWindow.CommandEventArgs e)
        {
            if (e.Command == "exit" || e.Command == "quit" || e.Command == "close")
                Exit();
            else
            {
                string expressionString = "";

                if (Args != null)
                    expressionString = e.Command + string.Join("", Args);
                else
                    expressionString = e.Command;

                Expression expression = new Expression(expressionString);
                console.Write(expression.calculate().ToString(), Color.DarkCyan);
            }
        }

        private void Calculate(string expressionString)
        {
            Expression expression = new Expression(expressionString);
            console.Write(expression.calculate().ToString(), Color.DarkCyan);
        }
    }
}
