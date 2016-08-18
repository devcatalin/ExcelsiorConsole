﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelsiorConsole.Global;

namespace ExcelsiorConsole
{
    public partial class MainWindow : Form
    {
        int consoleStartingLinePosition = 0;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        ConsoleWindow console = null;

        public MainWindow(ConsoleWindow console = null)
        {
            if (console == null) console = new ConsoleWindow();
            this.console = console;

            InitializeComponent();

            const int VK_Z = 0x6A;
            const int MOD_ALT = 0x0001;
            RegisterHotKey(this.Handle, this.GetType().GetHashCode(), MOD_ALT, VK_Z);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                console.Focus();
            }
            base.WndProc(ref m);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, this.GetType().GetHashCode());
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            console.Dock = DockStyle.Fill;
            this.Controls.Add(console);
            console.Commands.Add(new ClearCmd(console));
            console.Commands.Add(new CalculateCmd(console));
            console.Commands.Add(new ComputerCmd(console));
            console.Commands.AddRange(CommandsGenerator.GetCommands(console));
        }

    }
}
