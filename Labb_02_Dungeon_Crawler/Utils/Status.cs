static class Status
{
    private static Queue<StatusMessage> Messages = new Queue<StatusMessage>();
    private static int x = 59;
    private static int y = 3;
    private static int rows;
    private static int timer;

    public static void Add(StatusMessage message)
    {
        if (Messages.Count == 0) AddLine();
        Messages.Enqueue(message);
    }
    public static void Add(string message) => Add(new StatusMessage(message));
    public static void Add(string message, ConsoleColor color) => Add(new StatusMessage(message, color));
    public static void AddLine() => Messages.Enqueue(new StatusMessage(new String('-', Console.BufferWidth - x - 1), ConsoleColor.DarkGray));
    
    public static void Print()
    {
        if (Messages.Any())
        {
            Clear();

            while (Messages.Any())
            {
                Console.SetCursorPosition(x, y + rows);
                StatusMessage message = Messages.Dequeue();
                Console.ForegroundColor = message.Color;
                Console.WriteLine(message);
                rows++;
            }
            Console.ResetColor();
            timer = 3;
        }
        else timer--;

        if (timer == 0) Clear();
    }

    private static void Clear()
    {
        for (int i = 0; i < rows; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(new String(' ', Console.BufferWidth - x - 1));
        }
        rows = 0;
    }
}
class StatusMessage
{
    public ConsoleColor Color { get; init; }
    public string Message { get; init; }
    public StatusMessage(string message, ConsoleColor color = ConsoleColor.Yellow)
    {
        Message = message;
        Color = color;
    }
    public override string ToString() => Message;
}