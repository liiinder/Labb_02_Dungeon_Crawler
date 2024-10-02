class Rat : Enemy
{
    public Rat(Position pos)
    {
        Position = pos;
        Color = ConsoleColor.Red;
        Character = 'r';
        AttackDice = new Dice(1, 6, 3);
        DefenceDice = new Dice(1, 6, 1);
        Health = 10;
        Name = "rat";
    }
    public override void Update(LevelData data)
    {
        if (Health > 0)
        {
            Position nextPosition = new Position(Position.X, Position.Y);
            int dir = new Random().Next(4);
            if      (dir == 0) nextPosition.Y--;
            else if (dir == 1) nextPosition.X--;
            else if (dir == 2) nextPosition.Y++;
            else if (dir == 3) nextPosition.X++;

            LevelElement elementAtNext = data.Elements.FirstOrDefault(x => x.Position.Equals(nextPosition));

            if (elementAtNext is Player player) Attack(player);
            else if (elementAtNext is null) MoveTo(nextPosition);
        }
    }
}