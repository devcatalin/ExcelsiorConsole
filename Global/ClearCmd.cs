namespace ExcelsiorConsole.Global
{
    class ClearCmd : Command
    {
        public ClearCmd(ConsoleWindow c) : base(c)
        {
            CommandLabel = "clear";
        }

        public override void Execute()
        {
            console.ClearConsole();
        }
    }
}
