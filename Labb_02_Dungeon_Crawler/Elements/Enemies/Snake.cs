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
    public override void Update()
    {
        if (HasVisualOn(LevelData.Player))
        {
            Position newPosition = new Position(Position.X, Position.Y);
            if (Position.X == LevelData.Player.Position.X)
            {
                if (Position.Y > LevelData.Player.Position.Y) newPosition.Y++;
                else newPosition.Y--;
            }
            else if (Position.X > LevelData.Player.Position.X) newPosition.X++;
            else newPosition.X--;

            LevelElement elementAtNewPosition = LevelData.Elements.FirstOrDefault(x => x.Position.Equals(newPosition));

            if (elementAtNewPosition is null) MoveTo(newPosition);
        }
    }
}