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
    public override void Update()
    {
        if (Position.DistanceTo(LevelData.Player) < 7)
        {
            if (HasVisualOn(LevelData.Player)) Attack(LevelData.Player);
            else
            {
                var elementsNear = LevelData.Elements.Where(x => HasVisualOn(x)).ToList();

                int newX = Math.Sign(LevelData.Player.Position.X - Position.X);
                int newY = Math.Sign(LevelData.Player.Position.Y - Position.Y);

                List<Position> newPositions = new List<Position>();

                newPositions.Add(new Position(Position.X + newX, Position.Y + newY));
                newPositions.Add(new Position(Position.X + newX, Position.Y));
                newPositions.Add(new Position(Position.X, Position.Y + newY));

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