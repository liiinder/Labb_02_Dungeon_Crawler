struct Position
{
    public int X { get; set; }
    public int Y { get; set; }
    public Position(Position pos) : this(pos.X, pos.Y) { }
    public Position(int x, int y) { X = x; Y = y; }

    public void SetCursor() => Console.SetCursorPosition(X, Y);
    public int VerticalDistanceTo(Position pos) => Math.Abs(pos.X - X);
    public int HorizontalDistanceTo(Position pos) => Math.Abs(pos.Y - Y);
    public double DistanceTo(Position pos)
    {
        double a = VerticalDistanceTo(pos);
        double b = HorizontalDistanceTo(pos);

        if (a > 0 && b > 0) return Math.Sqrt((a * a) + (b * b));
        else if (a > 0) return a;
        return b;
    }
    public override string ToString() => $"({X}, {Y})";
}