using System.Collections.Generic;
using System.Linq;
using ConsoleCore.Interfaces;

namespace ConsoleCore
{
    public class InputLine : IInputLine
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
