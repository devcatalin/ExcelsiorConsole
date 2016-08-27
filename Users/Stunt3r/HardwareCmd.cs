using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMI_Hardware;
using WMI_Hardware.Hardware;

namespace ExcelsiorConsole.Users.Stunt3r
{
    class HardwareCmd : Command
    {
        public HardwareCmd(Console c) : base(c)
        {
            Label = "hardware";
        }

        public override void Execute()
        {
            if (Args.Count == 2 && Args[1] == "memory")
            {
                string path = System.Environment.CurrentDirectory + @"\hardwareSettings.xml";
                Connection wmiConnection = new Connection(path);
                Win32_PhysicalMemory soundDevice = new Win32_PhysicalMemory(wmiConnection);
                foreach (var property in soundDevice.GetPropertyValues())
                {
                    int selectionStart = Console.TextLength;
                    Console.WriteLine(property, Color.White);
                    int selectionEnd = Console.TextLength;
                    Console.SelectionStart = selectionStart;
                    Console.SelectionLength = selectionEnd;
                    Console.SelectionFont = new System.Drawing.Font("Consolas", 10);

                    Console.SelectionStart = Console.TextLength;
                    Console.SelectionLength = 0;
                    Console.SelectionFont = new Font("Consolas", 20);

                }
            }
        }
    }
}
