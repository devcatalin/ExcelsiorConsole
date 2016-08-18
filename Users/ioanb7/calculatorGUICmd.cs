
namespace ExcelsiorConsole.Users.ioanb7
{
    class calculatorGUICmd : Command
    {
        public calculatorGUICmd(ConsoleWindow c) : base(c)
        {
            CommandLabel = "calculatorGUI";
            Aliases.Add("calculatorGUIII");
        }

        public override void Execute()
        {
            CalculatorGUI.CalculatorGUIForm calculatorGUIForm = new CalculatorGUI.CalculatorGUIForm();
            calculatorGUIForm.Show();
        }
    }
}
