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

        public List<InputLine> InputHistory { get; set; }
        private int _indexInputHistory;
        private bool _isAutoFilling;
        private List<string> _filteredFillingCommands;
        private int _indexFilteredFillingCommands;

        public event EventHandler<CommandEventArgs> RecievedCommand;

        public ConsoleWindow()
        {
            ConsoleColor = Color.FromArgb(70, 131, 187);
            InputHistory = new List<InputLine>();;
            _filteredFillingCommands = new List<string>();
            
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

        private void ClearInputLine()
        {
            Select(_commandPosition, Text.Length - _commandPosition);
            SelectedText = string.Empty;
        }

        private void FilterCommands(string input)
        {
            _filteredFillingCommands = (from command in Commands
                                        where command.CommandLabel.StartsWith(input)
                                        orderby command.CommandLabel
                                        select command.CommandLabel).ToList();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                Select(_commandPosition, Text.Length - _commandPosition);
                string input = SelectedText;

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var inputHistory = InputHistory.LastOrDefault(ih => ih.Text.StartsWith(input) && ih.IsCommand);

                    if (inputHistory != null)
                    {
                        ClearInputLine();

                        FilterCommands(input);
                        AppendText(inputHistory.CommandLabel);

                        _indexFilteredFillingCommands = -1;
                        _isAutoFilling = true;
                    }
                    else
                    {
                        ClearInputLine();
                        FilterCommands(input);

                        if (_filteredFillingCommands.Any())
                        {
                            _indexFilteredFillingCommands = 0;

                            AppendText(_filteredFillingCommands[_indexFilteredFillingCommands]);

                            _isAutoFilling = true;
                        }
                    }
                }

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
                        InputLine inputLine = new InputLine();

                        Select(_commandPosition, Text.Length - _commandPosition);
                        inputLine.Text = SelectedText;
                        DeselectAll();

                        string[] inputParts = inputLine.Text.Split(' ');
                        inputLine.CommandLabel = inputParts[0];

                        if (inputParts.Count() > 1)
                            inputLine.CommandArgs = inputParts.Skip(1).ToArray();

                        if (string.IsNullOrWhiteSpace(inputLine.CommandLabel))
                        {
                            NewLine();
                            e.Handled = true;
                            break;
                        }

                        if (InExecution)
                        {
                            CommandEventArgs eventArgs = new CommandEventArgs();
                            eventArgs.Command = inputLine.CommandLabel;
                            eventArgs.Args = inputLine.CommandArgs;
                            RecievedCommand(this, eventArgs);
                        }
                        else
                        {
                            Command command = Commands.FirstOrDefault(c => c.CommandLabel.ToLower() == inputLine.CommandLabel.ToLower() || 
                                                                           c.Aliases.Any(alias => alias.ToLower() == inputLine.CommandLabel.ToLower()));
                            if (command != null)
                            {
                                command.Args = inputLine.CommandArgs;
                                if (command.CanExecute())
                                    command.Execute();
                                inputLine.IsCommand = true;
                            }
                            else
                            {
                                Write("Command not found.", Color.DarkRed);
                                inputLine.IsCommand = false;
                            }
                        }

                        InputHistory.Add(inputLine);
                        _indexInputHistory = InputHistory.Count;
                        _isAutoFilling = false;
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
                case Keys.Up:
                    {
                        if (_isAutoFilling)
                        {
                            if (_indexFilteredFillingCommands >= 0)
                            {
                                if (_indexFilteredFillingCommands - 1 >= 0)
                                    _indexFilteredFillingCommands--;
                                Select(_commandPosition, Text.Length);
                                SelectedText = string.Empty;
                                AppendText(_filteredFillingCommands[_indexFilteredFillingCommands]);
                            }
                            else if (_indexFilteredFillingCommands == -1)
                            {
                                _indexFilteredFillingCommands = _filteredFillingCommands.Count - 1;
                                Select(_commandPosition, Text.Length);
                                SelectedText = string.Empty;
                                AppendText(_filteredFillingCommands[_indexFilteredFillingCommands]);
                            }
                        }
                        else
                        {
                            _indexInputHistory--;
                            if (_indexInputHistory >= 0)
                            {
                                Select(_commandPosition, Text.Length);
                                SelectedText = string.Empty;
                                AppendText(InputHistory[_indexInputHistory].Text);
                            }
                            else
                                _indexInputHistory = 0;
                        }  

                    e.Handled = true;
                    break;
                    }
                case Keys.Down:
                    {
                        if (_isAutoFilling)
                        {
                            if (_indexFilteredFillingCommands < _filteredFillingCommands.Count())
                            {
                                if (_indexFilteredFillingCommands + 1 < _filteredFillingCommands.Count())
                                    _indexFilteredFillingCommands++;
                                Select(_commandPosition, Text.Length);
                                SelectedText = string.Empty;
                                AppendText(_filteredFillingCommands[_indexFilteredFillingCommands]);
                            }
                        }
                        else
                        {
                            _indexInputHistory++;

                            Select(_commandPosition, Text.Length);
                            SelectedText = string.Empty;

                            if (_indexInputHistory < InputHistory.Count)
                            {
                                AppendText(InputHistory[_indexInputHistory].Text);
                                e.Handled = true;
                            }
                            else
                                _indexInputHistory = InputHistory.Count;
                        }

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
                    _indexInputHistory = InputHistory.Count;
                    _indexFilteredFillingCommands = 0;
                    _isAutoFilling = false;
                    //_filteredFillingCommands.Clear(); - do I need this ?
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
