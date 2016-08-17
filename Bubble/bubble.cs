using System.Linq;
using System.Collections.Generic;
namespace ExcelsiorConsole {
public static class CommandsGenerator {
	public static List<Command> GetCommands(ConsoleWindow cw) {
		List<Command> commands = new List<Command>();
				
		commands.Add(new ExcelsiorConsole.Users.Stunt3r.StopwatchCmd(cw));

		return commands;
	}
}
}