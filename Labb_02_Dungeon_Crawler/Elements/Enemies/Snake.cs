class Snake : Enemy
{
    public Snake(Position position)
    {
        Position = position;
        Color = ConsoleColor.Green;
        Icon = 's';
        AttackDice = new Dice(3, 4, 2);
        DefenceDice = new Dice(1, 8, 5);
        Health = 25;
        Name = "snake";
    }
    public override void Update(LevelData data)
    {
        if (Position.DistanceTo(data.Player.Position) < 2)
        {
            Position newPosition = new Position(Position.X, Position.Y);
            if (Position.X == data.Player.Position.X)
            {
                if (Position.Y > data.Player.Position.Y) newPosition.Y++;
                else newPosition.Y--;
            }
            else if (Position.X > data.Player.Position.X) newPosition.X++;
            else newPosition.X--;

            LevelElement elementAtNewPosition = data.Elements.FirstOrDefault(x => x.Position.Equals(newPosition));

            if (elementAtNewPosition is null) MoveTo(newPosition);
        }
    }
}