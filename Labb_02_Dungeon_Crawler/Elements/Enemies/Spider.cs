class Spider : Enemy
{
    public Spider(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkMagenta;
        Icon = '*';
        AttackDice = new Dice(1, 8, 0);     // 1 -> 8
        DefenceDice = new Dice(1, 6, 0);    // 1 -> 6
        Health = 5;
        Name = "spider";
        Vision = 1.5;
    }
    public override void Update(LevelData level)
    {
        if (Position.DistanceTo(level.Player) < 7)
        {
            if (HasVisualOn(level.Player)) Attack(level.Player, level);
            else
            {
                var elementsNear = level.Elements.Where(x => HasVisualOn(x)).ToList();

                int newX = Math.Sign(level.Player.Position.X - Position.X);
                int newY = Math.Sign(level.Player.Position.Y - Position.Y);

                List<Position> newPositions =
                [
                    new Position(Position.X + newX, Position.Y + newY),
                    new Position(Position.X + newX, Position.Y),
                    new Position(Position.X, Position.Y + newY),
                ];

                foreach (Position newPos in newPositions)
                {
                    LevelElement elementAtNewPosition = elementsNear.FirstOrDefault(x => x.Position.Equals(newPos));
                    if (elementAtNewPosition is null)
                    {
                        MoveTo(newPos);
                        break;
                    }
                }
            }
        }
    }
}