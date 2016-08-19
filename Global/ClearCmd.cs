namespace ExcelsiorConsole.Global
{
    class ClearCmd : Command
    {
        public ClearCmd(ConsoleWindow c) : base(c)
        {
            Label = "clear";
        }

        public override void Execute()
        {
            Console.ClearConsole();
        }
    }
}
