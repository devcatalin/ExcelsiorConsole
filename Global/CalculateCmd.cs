using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;

namespace ExcelsiorConsole.Global
{
    class CalculateCmd : Command
    {
        public CalculateCmd(Console c) : base(c)
        {
            Label = "calculate";
            Aliases.Add("calc");
        }

        public override void Execute()
        {
            if (Args.Count == 0)
            {
                base.Execute();
                return;
            }

            string expressionString = Args[0];
            Calculate(expressionString);
        }

        public override void Console_RecievedCommand(object sender, Console.CommandEventArgs e)
        {
            if (e.Label == "exit" || e.Label == "quit" || e.Label == "close")
                Exit();
            else
            {
                string expressionString = Args != null ? e.Args[0] : e.Label;

                Expression expression = new Expression(expressionString);
                Console.WriteLine(expression.calculate().ToString(), Color.DarkCyan);
            }
        }

        private void Calculate(string expressionString)
        {
            Expression expression = new Expression(expressionString);
            Console.WriteLine(expression.calculate().ToString(), Color.DarkCyan);
        }
    }
}
