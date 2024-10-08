﻿abstract class LevelElement
{
    public bool IsVisable { get; set; }
    public Position Position { get; set; }
    public char Icon { get; protected set; }
    public ConsoleColor Color { get; protected set; }
    public void Draw(bool drawIcon = true)
    {
        Position.SetCursor();
        if (this is Wall) Console.BackgroundColor = ConsoleColor.DarkGray;
        else Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = Color;
        Console.Write((drawIcon) ? Icon : ' ');
        IsVisable = drawIcon;
    }
    public void Hide() => Draw(false);
    public void Remove()
    {
        Hide();
        LevelData.deathRow.Enqueue(this);
    }
}