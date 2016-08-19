
namespace ExcelsiorConsole.Users.ioanb7
{
    class calculatorGUICmd : Command
    {
        public calculatorGUICmd(Console c) : base(c)
        {
            Label = "calculatorGUI";
            Aliases.Add("calculatorGUIII");
        }

        public override void Execute()
        {
            CalculatorGUI.CalculatorGUIForm calculatorGUIForm = new CalculatorGUI.CalculatorGUIForm();
            calculatorGUIForm.Show();
        }
    }
}
