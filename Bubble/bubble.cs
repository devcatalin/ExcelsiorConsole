using System.Collections.Generic;
using ConsoleCore;
using ConsoleCore.Interfaces;

namespace ExcelsiorConsole
{
public static class CommandsGenerator
{
public static List<Command> GetCommands(IConsole cw)
{
List<Command> commands = new List<Command>();
commands.Add(new ExcelsiorConsole.Users.Stunt3r.StopwatchCmd(cw));
return commands;
}
}
}