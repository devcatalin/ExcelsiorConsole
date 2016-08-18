using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ExcelsiorConsole
{
    public class ConsoleWindow : RichTextBox
    {
        int _startingLinePosition = 0;
        int _commandPosition;

        public string ConsoleName { get { return "excelsior"; } }

        public bool InExecution { get; set; }
        public string NameOfExecutingCommand { get; set; }

        public Color ConsoleColor { get; set; }

        public List<Command> Commands = new List<Command>();

        public List<string> InputHistory { get; set; }
        private int _inputHistoryIterator;

        public event EventHandler<CommandEventArgs> RecievedCommand;

        public ConsoleWindow()
        {
            ConsoleColor = Color.FromArgb(70, 131, 187);
            InputHistory = new List<string>();;

            Font = new Font(new FontFamily("Consolas"), 20, FontStyle.Regular);
            BackColor = Color.Black;
            ForeColor = Color.FromArgb(45, 158, 187);
            Dock = DockStyle.Fill;

            AcceptsTab = true;

            string date = DateTime.Now.ToString();
            AppendText("Excelsior Console - " + date + Environment.NewLine, Color.DarkCyan);
            _startingLinePosition = Text.Length;
            _commandPosition = _startingLinePosition + ConsoleName.Length + 3;
            AppendText("<" + ConsoleName + "> ", ConsoleColor);
        }

        public void ClearConsole()
        {
            Clear();
            string date = DateTime.Now.ToString();
            AppendText("Console cleared - " + date, Color.DarkCyan);
            _startingLinePosition = Text.Length + 1;
            _commandPosition = _startingLinePosition + ConsoleName.Length + 3;
        }

        //Disables mouse events;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0201://WM_LBUTTONDOWN
                    {
                        return;
                    }
                case 0x0202://WM_LBUTTONUP
                    {
                        return;
                    }
                case 0x0203://WM_LBUTTONDBLCLK
                    {
                        return;
                    }
                case 0x0204://WM_RBUTTONDOWN
                    {
                        return;
                    }
                case 0x0205://WM_RBUTTONUP
                    {
                        return;
                    }
                case 0x0206://WM_RBUTTONDBLCLK
                    {
                        return;
                    }
            }

            base.WndProc(ref m);
        }

        public void AppendText(string text, Color color)
        {
            SelectionStart = TextLength;
            SelectionLength = 0;

            SelectionColor = color;
            AppendText(text);
            SelectionColor = ForeColor;
        }

        public void Write(string message, Color color)
        {
            string spaces = " ";
            if (string.IsNullOrEmpty(NameOfExecutingCommand))
                spaces = new string(' ', ConsoleName.Length + 3);
            else
                spaces = new string(' ', NameOfExecutingCommand.Length + 3);
            AppendText(Environment.NewLine + spaces + message, color);
        }

        public void NewLine()
        {
            _startingLinePosition = Text.Length + 1;
            if (string.IsNullOrEmpty(NameOfExecutingCommand))
            {
                AppendText(Environment.NewLine + "<" + ConsoleName + "> ", ConsoleColor);
                _commandPosition = _startingLinePosition + ConsoleName.Length + 3;
            }
            else
            {
                AppendText(Environment.NewLine + @"<" + NameOfExecutingCommand + "> ", Color.DodgerBlue);
                _commandPosition = _startingLinePosition + NameOfExecutingCommand.Length + 3;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                Select(_commandPosition, Text.Length);
                string input = SelectedText;
                DeselectAll();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {

            if (e.Control && e.KeyValue == 'A')
            {
                Select(_commandPosition, Text.Length);
                e.Handled = true;
            }

            switch (e.KeyData)
            {
                case Keys.Enter:
                    {
                        if (Text.Length <= _commandPosition)
                        {
                            NewLine();
                            e.Handled = true;
                            break;
                        }

                        //string input = Text.Remove(0, _commandPosition);
                        Select(_commandPosition, Text.Length - _commandPosition);
                        string input = SelectedText;
                        DeselectAll();

                        InputHistory.Add(input);
                        _inputHistoryIterator = InputHistory.Count;

                        string[] inputParts = input.Split(' ');
                        string[] inputArgs = null;
                        string inputCommand = inputParts[0];
                        if (inputParts.Count() > 1)
                            inputArgs = inputParts.Skip(1).ToArray();

                        if (string.IsNullOrWhiteSpace(inputCommand))
                        {
                            NewLine();
                            e.Handled = true;
                            break;
                        }

                        if (InExecution)
                        {
                            CommandEventArgs eventArgs = new CommandEventArgs();
                            eventArgs.Command = inputCommand;
                            eventArgs.Args = inputArgs;
                            RecievedCommand(this, eventArgs);
                        }
                        else
                        {
                            Command command = Commands.FirstOrDefault(c => c.CommandLabel.ToLower() == inputCommand.ToLower() || c.Aliases.Any(alias => alias.ToLower() == inputCommand.ToLower()));
                            if (command != null)
                            {
                                command.Args = inputArgs;
                                if (command.CanExecute())
                                    command.Execute();
                            }
                            else
                            {
                                Write("Command not found.", Color.DarkRed);
                            }
                        }

                        NewLine();

                        e.Handled = true;
                        break;
                    }
                case Keys.Back:
                    {
                        if (Text.Length - _commandPosition <= 0)
                            e.Handled = true;
                        break;
                    }
                case Keys.Tab:
                    {
                        //MessageBox.Show("Test");
                        e.Handled = true;
                        break;
                    }
                case Keys.Up:
                    {
                    _inputHistoryIterator--;
                    if (_inputHistoryIterator >= 0)
                    {
                        Select(_commandPosition, Text.Length);
                        SelectedText = string.Empty;
                        AppendText(InputHistory[_inputHistoryIterator]);
                        e.Handled = true;
                    }
                    else
                        _inputHistoryIterator = 0;

                    e.Handled = true;
                    break;
                    }
                case Keys.Down:
                    {
                        _inputHistoryIterator++;
                        
                        Select(_commandPosition, Text.Length);
                        SelectedText = string.Empty;

                        if (_inputHistoryIterator < InputHistory.Count)
                        {
                            AppendText(InputHistory[_inputHistoryIterator]);
                            e.Handled = true;
                        }
                        else
                            _inputHistoryIterator = InputHistory.Count;

                        e.Handled = true;
                        break;
                    }
                case Keys.Left:
                    {
                        if (SelectionStart <= _commandPosition)
                            e.Handled = true;
                        break;
                    }
                default:
                    _inputHistoryIterator = InputHistory.Count;
                    break;
            }
        }

        public class CommandEventArgs : EventArgs
        {
            public string Command { get; set; }
            public string[] Args { get; set; }
        }
    }
}
