﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelsiorConsole
{
    public class AutoFill
    {
        private ConsoleWindow console;

        public AutoFill(ConsoleWindow c)
        {
            console = c;
        }

        public int Index { get; set; }
        public bool Enabled { get; set; }
        public List<string> Commands { get; set; } = new List<string>();

        public string GetCommand()
        {
            return Commands[Index];
        }

        public void GoUp()
        {
            if (Index - 1 >= 0)
                Index--;
        }

        public void GoDown()
        {
            if (Index + 1 < Commands.Count())
                Index++;
        }

        public void Reset()
        {
            Index = 0;
            Enabled = false;
            Commands.Clear();
        }

        public void FilterCommands(string input)
        {
            Commands = (from command in console.Commands
                        where command.Label.StartsWith(input)
                        orderby command.Label
                        select command.Label).ToList();
        }
    }
}
