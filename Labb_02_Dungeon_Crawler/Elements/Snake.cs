class Snake : Enemy
{
    public Snake(Position position)
    {
        Position = position;
        Color = ConsoleColor.Green;
        Character = 's';
        AttackDice = new Dice(3, 4, 2);
        DefenceDice = new Dice(1, 8, 5);
        Health = 25;
        Name = "snake";
    }
    public override void Update(LevelData data)
    {
        if (Position.DistanceTo(data.Player.Position) < 2)
        {
            Position nextPosition = new Position(Position.X, Position.Y);
            if (Position.X == data.Player.Position.X)
            {
                if (Position.Y > data.Player.Position.Y) nextPosition.Y++;
                else nextPosition.Y--;
            }
            else if (Position.X > data.Player.Position.X) nextPosition.X++;
            else nextPosition.X--;

            LevelElement elementAtNext = data.Elements.FirstOrDefault(x => x.Position.Equals(nextPosition));

            if (elementAtNext is null) MoveTo(nextPosition);
        }
    }
}