public static class Log
{
    public static Queue<LogMessage> MessageQueue = new Queue<LogMessage>();
    public static int x = 57;
    public static int y = 1;
    public static int rows;
    public static int timer;
    public static List<LogMessage> MessageLog = new();


    public static void Add(LogMessage message)
    {
        if (MessageQueue.Count == 0) AddLine();
        if (MessageQueue.Count < 27) MessageQueue.Enqueue(message);
    }
    public static void Add(string message) => Add(new LogMessage(message));
    public static void Add(string message, ConsoleColor color) => Add(new LogMessage(message, color));
    public static void AddLine()
    {
        MessageQueue.Enqueue(new LogMessage(new String('-', Console.BufferWidth - x), ConsoleColor.DarkGray));
    }

    public static void Print()
    {
        if (MessageQueue.Any())
        {
            Clear();
            while (MessageQueue.Any())
            {

                Console.SetCursorPosition(x, y + rows);
                LogMessage message = MessageQueue.Dequeue();
                MessageLog.Add(message);
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
public class LogMessage
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