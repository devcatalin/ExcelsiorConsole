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
            if (Args == null)
            {
                base.Execute();
                return;
            }

            string expressionString = string.Join("", Args);
            Calculate(expressionString);
        }

        public override void Console_RecievedCommand(object sender, Console.CommandEventArgs e)
        {
            if (e.Label == "exit" || e.Label == "quit" || e.Label == "close")
                Exit();
            else
            {
                string expressionString = "";

                if (Args != null)
                    expressionString = e.Label + string.Join("", Args);
                else
                    expressionString = e.Label;

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
