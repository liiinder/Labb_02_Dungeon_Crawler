class Snake : Enemy
{
    public Snake(Position position)
    {
        Position = position;
        Color = ConsoleColor.Green;
        Icon = 's';
        AttackDice = new Dice(3, 4, 2);     // 5 -> 14 
        DefenceDice = new Dice(1, 8, 3);    // 4 -> 11
        Health = 25;
        Name = "snake";
        Vision = 1.5;
    }
    public override void Update(LevelData level)
    {
        if (HasVisualOn(level.Player))
        {
            Position newPosition = new Position(Position.X, Position.Y);
            if (Position.X == level.Player.Position.X)
            {
                if (Position.Y > level.Player.Position.Y) newPosition.Y++;
                else newPosition.Y--;
            }
            else if (Position.X > level.Player.Position.X) newPosition.X++;
            else newPosition.X--;

            LevelElement elementAtNewPosition = level.Elements.FirstOrDefault(x => x.Position.Equals(newPosition));

            if (elementAtNewPosition is null) MoveTo(newPosition);
        }
    }
}