using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public class Command : ICommand
    {
        public ConsoleWindow console { get; set; }
        public string CommandLabel { get; set; }
        public List<string> Aliases { get; set; } = new List<string>();
        public string[] Args { get; set; }

        public Command(ConsoleWindow c)
        {
            this.console = c;
        }

        public virtual bool CanExecute()
        {
            return true;
        }

        public virtual void Execute()
        {
            console.InExecution = true;
            console.NameOfExecutingCommand = CommandLabel;
            console.RecievedCommand += Console_RecievedCommand;
        }

        public virtual void Exit()
        {
            console.InExecution = false;
            console.NameOfExecutingCommand = null;
            console.RecievedCommand -= Console_RecievedCommand;
        }

        public virtual void Console_RecievedCommand(object sender, ConsoleWindow.CommandEventArgs e)
        {
            
        }
    }
}
