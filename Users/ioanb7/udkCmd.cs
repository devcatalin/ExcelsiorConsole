namespace ExcelsiorConsole.Users.ioanb7
{
    class udkCmd : Command
    {
        public udkCmd(Console c) : base(c)
        {
            Label = "udk";
        }

        public override void Execute()
        {
            string path = @"C:\Users\484327\Documents\Unreal Projects\LeapBoxMovement\LeapBoxMovement.uproject";
            System.Diagnostics.Process.Start(path);
        }
    }
}
