using System.Collections.Generic;
using System.Diagnostics;
using ConsoleCore.Interfaces;

namespace ConsoleCore
{
    public class Command : ICommand
    {
        public Console Console { get; set; }
        public string Label { get; set; }
        public List<string> Aliases { get; set; }
        public List<string> Args { get; set; }

        public string[] Dependencies { get; set; }     

        public Command(Console c)
        {
            Aliases = new List<string>();
            Console = c;
        }

        public virtual bool CanExecute()
        {
            return true;
        }

        public virtual void Execute()
        {
            Console.Execution.CommandLabel = Label;
            Console.Execution.State = ExecutionState.RecieveCommands;
            Console.RecievedCommand += Console_RecievedCommand;
        }

        public virtual void Exit()
        {
            Console.Execution.CommandLabel = Label;
            Console.Execution.State = ExecutionState.ExecuteCommand;
            Console.RecievedCommand -= Console_RecievedCommand;
        }

        public virtual void Console_RecievedCommand(object sender, CommandEventArgs e)
        {
            
        }
    }
}
