static class Log
{
    private static Queue<LogMessage> Messages = new Queue<LogMessage>();
    private static int x = 57;
    private static int y = 1;
    private static int rows;
    private static int timer;

    public static void Add(LogMessage message)
    {
        if (Messages.Count == 0) AddLine();
        if (Messages.Count < 27) Messages.Enqueue(message);
    }
    public static void Add(string message) => Add(new LogMessage(message));
    public static void Add(string message, ConsoleColor color) => Add(new LogMessage(message, color));
    public static void AddLine()
    {
        Messages.Enqueue(new LogMessage(new String('-', Console.BufferWidth - x), ConsoleColor.DarkGray));
    }
    
    public static void Print()
    {
        if (Messages.Any())
        {
            Clear();
            while (Messages.Any())
            {
                Console.SetCursorPosition(x, y + rows);
                LogMessage message = Messages.Dequeue();
                Console.ForegroundColor = message.Color;
                Console.WriteLine(message);
                rows++;
            }
            Console.ResetColor();
            timer = 5;
        }
        else timer--;

        if (timer == 0) Clear();
    }

    private static void Clear()
    {
        for (int i = 0; i < rows; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(new String(' ', Console.BufferWidth - x));
        }
        rows = 0;
    }
}
class LogMessage
{
    public ConsoleColor Color { get; init; }
    public string Message { get; init; }
    public LogMessage(string message, ConsoleColor color = ConsoleColor.Yellow)
    {
        Message = message;
        Color = color;
    }
    public override string ToString() => Message;
}