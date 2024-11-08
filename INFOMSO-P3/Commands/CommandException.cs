namespace INFOMSO_P3.Commands;

public class CommandException(int line, string message) : Exception($"Error on line {line}: {message}");