using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public class Command : ICommand
    {
        public ConsoleWindow console { get; set; }
        public string CommandLabel { get; set; }
        public List<string> Aliases { get; set; }
        public string[] Args { get; set; }

        public string[] Dependencies { get; set; }     

        public Command(ConsoleWindow c)
        {
            Aliases = new List<string>();
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
        
        public string RunProcess(string filename, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = arguments; // Note the /c command (*)
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //* Read the output (or the error)
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            string err = process.StandardError.ReadToEnd();
            Console.WriteLine(err);
            process.WaitForExit();

            return output;
        }
    }
}
