namespace ExcelsiorConsole.Global
{
    class ClearCmd : Command
    {
        public ClearCmd(Console c) : base(c)
        {
            Label = "clear";
        }

        public override void Execute()
        {
            Console.ClearConsole();
        }
    }
}
