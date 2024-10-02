abstract class LevelElement
{
    public Position Position { get; set; }
    public char Character { get; set; }
    public ConsoleColor Color { get; set; }
    public bool IsVisable { get; set; }
    public void Draw(bool drawChar = true)
    {
        Position.SetCursor();
        Console.ForegroundColor = Color;
        Console.Write((drawChar) ? Character : ' ');
        IsVisable = drawChar;
    }
    public void Hide() => Draw(false);
    public void Remove()
    {
        Hide();
        Position = new Position(5, 0);
    }
}