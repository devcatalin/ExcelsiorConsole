using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public class InputLine
    {
        public InputLine(string inputText)
        {
            Text = inputText;

            string[] inputParts = Text.Split(' ');
            CommandLabel = inputParts[0];

            if (inputParts.Count() > 1)
                CommandArgs = inputParts.Skip(1).ToArray();
        }

        public string Text { get; set; }
        public bool IsCommand { get; set; }
        public string CommandLabel { get; set; }
        public string[] CommandArgs { get; set; }
    }
}
