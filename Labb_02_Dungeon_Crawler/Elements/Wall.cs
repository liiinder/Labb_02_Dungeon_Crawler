class Wall : LevelElement
{
    public Wall(Position position)
    {
        Position = position;
        Color = ConsoleColor.White;
        Character = '#';
    }
}