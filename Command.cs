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
        public Console Console { get; set; }
        public string Label { get; set; }
        public List<string> Aliases { get; set; }
        public List<string> Args { get; set; }

        public string[] Dependencies { get; set; }     

        public Command(Console c)
        {
            Aliases = new List<string>();
            this.Console = c;
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

        public virtual void Console_RecievedCommand(object sender, Console.CommandEventArgs e)
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
            System.Console.WriteLine(output);
            string err = process.StandardError.ReadToEnd();
            System.Console.WriteLine(err);
            process.WaitForExit();

            return output;
        }
    }
}
