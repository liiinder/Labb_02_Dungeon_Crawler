struct Position
{
    public int X { get; set; }
    public int Y { get; set; }
    public Position(int x, int y) { X = x; Y = y; }
    public Position(Position pos) { X = pos.X; Y = pos.Y; }
    public void SetCursor() => Console.SetCursorPosition(X, Y);
    public double DistanceTo(Position pos)
    {
        double a = Math.Abs(pos.X - X);
        double b = Math.Abs(pos.Y - Y);

        return Math.Sqrt((a * a) + (b * b));
    }
    public double DistanceTo(LevelElement element) => DistanceTo(element.Position);
}