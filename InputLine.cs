using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public class InputLine
    {
        public string Text { get; set; }
        public bool IsCommand { get; set; }
        public string CommandLabel { get; set; }
        public List<string> CommandArgs { get; set; } = new List<string>();

        public InputLine(string inputText)
        {
            Text = inputText;

            string[] inputParts = Text.Split(' ');
            CommandLabel = inputParts[0];

            if (inputParts.Count() > 1)
            {
                CommandArgs.Add(inputText.Substring(CommandLabel.Length + 1));
                CommandArgs.AddRange(inputParts.Skip(1).ToList());
            }
        }
    }
}
