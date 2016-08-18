using System.Collections.Generic;
<<<<<<< HEAD
namespace ExcelsiorConsole {
public static class CommandsGenerator {
	public static List<Command> GetCommands(ConsoleWindow cw) {
		List<Command> commands = new List<Command>();
				
		commands.Add(new ExcelsiorConsole.Users.Stunt3r.StopwatchCmd(cw));
		
		return commands;
	}
=======
namespace ExcelsiorConsole
{
public static class CommandsGenerator
{
public static List<Command> GetCommands(ConsoleWindow cw)
{
List<Command> commands = new List<Command>();

return commands;
}
>>>>>>> bc871868c4e4f10183ef76cb90b82519e96838fa
}
}