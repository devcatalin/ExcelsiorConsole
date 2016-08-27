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

        public void PrintProperties(IList<string> properties )
        {
            int selectionStart = Console.TextLength;

            foreach (var property in properties)
                Console.WriteLine(property, Color.White);

            int selectionEnd = Console.TextLength;
            Console.SelectionStart = selectionStart;
            Console.SelectionLength = selectionEnd;
            Console.SelectionFont = new System.Drawing.Font("Consolas", 10);
            Console.SelectionStart = Console.TextLength;
            Console.SelectionLength = 0;
            Console.SelectionFont = new Font("Consolas", 20);
        }

        public override void Execute()
        {
            string path = System.Environment.CurrentDirectory + @"\hardwareSettings.xml";
            Connection wmiConnection = new Connection(path);
            

            if (Args.Count != 2)
            {
                return;
            }

            switch (Args[1])
            {
                case "memory":
                    Win32_PhysicalMemory physicalMemory = new Win32_PhysicalMemory(wmiConnection);
                    PrintProperties(physicalMemory.GetPropertyValues());
                    break;
                case "bios":
                    Win32_BIOS bios = new Win32_BIOS(wmiConnection);
                    PrintProperties(bios.GetPropertyValues());
                    break;
                case "processor":
                    Win32_Processor processor = new Win32_Processor(wmiConnection);
                    PrintProperties(processor.GetPropertyValues());
                    break;
                case "sound":
                    Win32_SoundDevice soundDevice = new Win32_SoundDevice(wmiConnection);
                    PrintProperties(soundDevice.GetPropertyValues());
                    break;
                //case "temperature":
                //    Win32_TemperatureProbe temperature = new Win32_TemperatureProbe(wmiConnection);
                //    PrintProperties(temperature.GetPropertyValues());
                //    break;
                case "video":
                    Win32_VideoController video = new Win32_VideoController(wmiConnection);
                    PrintProperties(video.GetPropertyValues());
                    break;

                    
            }
            
        }
    }
}
