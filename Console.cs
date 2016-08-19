using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ExcelsiorConsole
{
    public class Console : RichTextBox
    {
        public new string Name { get { return "excelsior"; } }

        public Color Color { get; set; }

        public CaretPosition CaretPosition { get; set; }

        public Execution Execution { get; set; }

        public List<Command> Commands = new List<Command>();

        public InputHistory InputHistory { get; set; } = new InputHistory();

        public AutoFill AutoFill { get; set; }

        public event EventHandler<CommandEventArgs> RecievedCommand;

        public Console(ConsoleSettings settings = null)
        {
            if (settings == null) settings = new ConsoleSettings();

            Execution = new Execution();

            Color = settings.ConsoleColor;

            AutoFill = new AutoFill(this);

            CaretPosition = new CaretPosition(this);

            Font = settings.Font;
            BackColor = settings.BackColor;
            ForeColor = settings.ForeColor;
            Dock = DockStyle.Fill;

            AcceptsTab = true;

            string date = DateTime.Now.ToString();
            AppendText("Excelsior Console - " + date, Color.DarkCyan);
            NewLine();
        }

        public void ClearConsole()
        {
            Clear();
            string date = DateTime.Now.ToString();
            AppendText("Console cleared - " + date, Color.DarkCyan);
            CaretPosition.Reset();
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

        private string GetIndentation()
        {
            if (Execution.State == ExecutionState.RecieveCommands)
                return new string(' ', Execution.CommandLabel.Length + 3);

            if (Execution.State == ExecutionState.ExecuteCommand)
                return new string(' ', Name.Length + 3);

            return "error";
        }

        public void WriteLine(string message, Color color)
        {
            AppendText(Environment.NewLine + GetIndentation() + message, color);
        }

        private void ShowIndentingWord(string word, Color color)
        {
            AppendText(Environment.NewLine + "<" + word + "> ", color);
            CaretPosition.CommandStart = CaretPosition.LineStart + word.Length + 3;
        }

        public void NewLine()
        {
            CaretPosition.LineStart = Text.Length + 1;

            if (Execution.State == ExecutionState.ExecuteCommand)
                ShowIndentingWord(Name, Color);
            else if (Execution.State == ExecutionState.RecieveCommands)
                ShowIndentingWord(Execution.CommandLabel, Color.DodgerBlue);
        }

        private void SelectInputLine()
        {
            Select(CaretPosition.CommandStart, Text.Length - CaretPosition.CommandStart);
        }

        private void ClearInputLine()
        {
            SelectInputLine();
            SelectedText = string.Empty;
        }

        private string GetInputText()
        {
            SelectInputLine();
            return SelectedText;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                string input = GetInputText();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var inputHistory = InputHistory.Lines.LastOrDefault(line => line.Text.StartsWith(input) && line.IsCommand);

                    if (inputHistory != null)
                    {
                        ClearInputLine();

                        AutoFill.FilterCommands(input);
                        AppendText(inputHistory.CommandLabel);

                        AutoFill.Index = -1;
                        AutoFill.Enabled = true;
                    }
                    else
                    {
                        ClearInputLine();
                        AutoFill.FilterCommands(input);

                        if (AutoFill.Commands.Any())
                        {
                            AutoFill.Index = 0;
                            AppendText(AutoFill.GetCommand());
                            AutoFill.Enabled = true;
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
                SelectInputLine();
                e.Handled = true;
            }

            switch (e.KeyData)
            {
                case Keys.Enter:
                    {

                        if (Text.Length <= CaretPosition.CommandStart)
                        {
                            NewLine();
                            e.Handled = true;
                            break;
                        }

                        InputLine inputLine = new InputLine(GetInputText());
                        DeselectAll();

                        if (string.IsNullOrWhiteSpace(inputLine.CommandLabel))
                        {
                            NewLine();
                            e.Handled = true;
                            break;
                        }

                        if (Execution.State == ExecutionState.RecieveCommands)
                        {
                            CommandEventArgs eventArgs = new CommandEventArgs();
                            eventArgs.Label = inputLine.CommandLabel;
                            eventArgs.Args = inputLine.CommandArgs;
                            RecievedCommand(this, eventArgs);
                        }
                        else
                        {
                            Command command = Commands.FirstOrDefault(c => c.Label.ToLower() == inputLine.CommandLabel.ToLower() || 
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
                                WriteLine("Command not found.", Color.DarkRed);
                                inputLine.IsCommand = false;
                            }
                        }

                        InputHistory.Lines.Add(inputLine);
                        InputHistory.Index = InputHistory.Lines.Count;
                        AutoFill.Enabled = false;
                        NewLine();

                        e.Handled = true;
                        break;
                    }
                case Keys.Back:
                    {
                        if (Text.Length - CaretPosition.CommandStart <= 0)
                            e.Handled = true;
                        break;
                    }
                case Keys.Up:
                    {
                        if (AutoFill.Enabled)
                        {
                            if (AutoFill.Index >= 0)
                            {
                                AutoFill.GoUp();
                                ClearInputLine();
                                AppendText(AutoFill.GetCommand());
                            }
                            else if (AutoFill.Index == -1)
                            {
                                AutoFill.Index = AutoFill.Commands.Count - 1;
                                ClearInputLine();
                                AppendText(AutoFill.GetCommand());
                            }
                        }
                        else
                        {
                            InputHistory.Index--;

                            if (InputHistory.Index >= 0)
                            {
                                ClearInputLine();
                                AppendText(InputHistory.GetLine().Text);
                            }
                            else
                                InputHistory.Index = 0;
                        }  

                    e.Handled = true;
                    break;
                    }
                case Keys.Down:
                    {
                        if (AutoFill.Enabled)
                        {
                            if (AutoFill.Index < AutoFill.Commands.Count())
                            {
                                AutoFill.GoDown();
                                ClearInputLine();
                                AppendText(AutoFill.GetCommand());
                            }
                        }
                        else
                        {
                            InputHistory.Index++;

                            ClearInputLine();

                            if (InputHistory.Index < InputHistory.Lines.Count)
                            {
                                AppendText(InputHistory.GetLine().Text);
                                e.Handled = true;
                            }
                            else
                                InputHistory.Index = InputHistory.Lines.Count;
                        }

                        e.Handled = true;
                        break;
                    }
                case Keys.Left:
                    {
                        if (SelectionStart <= CaretPosition.CommandStart)
                            e.Handled = true;
                        break;
                    }
                default:
                    InputHistory.Index = InputHistory.Lines.Count;
                    AutoFill.Reset();
                    break;
            }
        }

        public class CommandEventArgs : EventArgs
        {
            public string Label { get; set; }
            public string[] Args { get; set; }
        }
    }
}
