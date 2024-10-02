class Wall : LevelElement
{
    public Wall(Position pos)
    {
        ElementPos = pos;
        Color = ConsoleColor.White;
        Character = '#';
    }
}