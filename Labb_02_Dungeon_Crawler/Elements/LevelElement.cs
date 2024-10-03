abstract class LevelElement
{
    public Position Position { get; set; }
    public char Icon { get; set; }
    public ConsoleColor Color { get; set; }
    public bool IsVisable { get; set; }
    public void Draw(bool drawIcon = true)
    {
        Position.SetCursor();
        Console.ForegroundColor = Color;
        Console.Write((drawIcon) ? Icon : ' ');
        IsVisable = drawIcon;
    }
    public void Hide() => Draw(false);
    public void Remove()
    {
        Hide();
        Position = new Position(5, 0);
    }
}