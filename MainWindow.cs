using System;
using System.Windows.Forms;
using ExcelsiorConsole.Global;
using ExcelsiorConsole.Users.Stunt3r;
using ExcelsiorConsole.Users.JColdFear;
using ConsoleCore;
using ConsoleCore.Interfaces;

namespace ExcelsiorConsole
{
    public partial class MainWindow : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private IConsole Console { get; }

        public MainWindow(IConsole console = null)
        {
            InitializeComponent();

            if (console == null) console = new ConsoleCore.Console();
            Console = console;

            const int num0Key = 0x6A;
            const int modAlt = 0x0001;
            RegisterHotKey(Handle, GetType().GetHashCode(), modAlt, num0Key);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                WindowState = FormWindowState.Minimized;
                Show();
                WindowState = FormWindowState.Normal;
                Console.Focus();
            }
            base.WndProc(ref m);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(Handle, GetType().GetHashCode());
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Console.Dock = DockStyle.Fill;
            Controls.Add((Control)Console);
            Console.Commands.Add(new ClearCmd(Console));
            Console.Commands.Add(new CalculateCmd(Console));
            Console.Commands.Add(new ComputerCmd(Console));
            Console.Commands.Add(new HardwareCmd(Console));
            Console.Commands.Add(new TimezoneCmd(Console));
            Console.Commands.AddRange(CommandsGenerator.GetCommands(Console));
        }

    }
}
