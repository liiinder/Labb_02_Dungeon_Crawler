abstract class LevelElement
{
    public Position ElementPos { get; set; }
    public char Character { get; set; }
    public ConsoleColor Color { get; set; }
    public bool IsVisable { get; set; }
    public void Draw(bool drawChar = true)
    {
        ElementPos.SetCursor();
        Console.ForegroundColor = Color;
        Console.Write((drawChar) ? Character : ' ');
        IsVisable = drawChar;
    }
    public void Erase() => Draw(false);
}