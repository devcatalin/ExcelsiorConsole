namespace ExcelsiorConsole.Users.ioanb7
{
    class udkCmd : Command
    {
        public udkCmd(ConsoleWindow c) : base(c)
        {
            CommandLabel = "udk";
        }

        public override void Execute()
        {
            string path = @"C:\Users\484327\Documents\Unreal Projects\LeapBoxMovement\LeapBoxMovement.uproject";
            System.Diagnostics.Process.Start(path);
        }
    }
}
