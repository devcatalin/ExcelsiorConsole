using System.Collections.Generic;
using System.Drawing;
using org.mariuszgromada.math.mxparser;
using ConsoleCore;
using ConsoleCore.Interfaces;

namespace ExcelsiorConsole.Global
{
    class CalculateCmd : Command
    {
        private double lastResult = 0;
        private List<char> operators = new List<char>(new [] { '*', '/', '+', '-' });

        public CalculateCmd(IConsole c) : base(c)
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

        public override void RecievedCommand(object sender, CommandEventArgs e)
        {
            if (e.Label == "exit" || e.Label == "quit" || e.Label == "close")
                Exit();
            else
            {
                string expressionString = Args.Count > 0 ? e.Args[0] : e.Label;


                if (operators.Contains(expressionString[0]) && !double.IsNaN(lastResult))
                {
                    expressionString = expressionString.Insert(0, lastResult.ToString());
                }

                Expression expression = new Expression(expressionString);
                double result = expression.calculate();

                Console.WriteLine(result.ToString(), Color.DarkCyan);
                lastResult = result;
            }
        }

        private void Calculate(string expressionString)
        {
            Expression expression = new Expression(expressionString);
            Console.WriteLine(expression.calculate().ToString(), Color.DarkCyan);
        }
    }
}
