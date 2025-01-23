public class LogMessage
{
    public ConsoleColor Color { get; init; }
    public string Message { get; init; }
    public int Turn { get; init; }

    public LogMessage(string message, int turn, ConsoleColor color = ConsoleColor.Yellow)
    {
        Message = message;
        Turn = turn;
        Color = color;
    }
    public override string ToString() => Message;
}