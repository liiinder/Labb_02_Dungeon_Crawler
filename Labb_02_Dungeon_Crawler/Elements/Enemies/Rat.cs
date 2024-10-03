class Rat : Enemy
{
    public Rat(Position pos)
    {
        Position = pos;
        Color = ConsoleColor.Red;
        Icon = 'r';
        AttackDice = new Dice(1, 6, 3);
        DefenceDice = new Dice(1, 6, 1);
        Health = 10;
        Name = "rat";
    }
    public override void Update(LevelData data)
    {
        Position newPosition = new Position(Position.X, Position.Y);

        int randomDirection = new Random().Next(4);
        if      (randomDirection == 0) newPosition.Y--;
        else if (randomDirection == 1) newPosition.Y++;
        else if (randomDirection == 2) newPosition.X--;
        else if (randomDirection == 3) newPosition.X++;

        LevelElement elementAtNewPosition = data.Elements.FirstOrDefault(x => x.Position.Equals(newPosition));

        if (elementAtNewPosition is Player player) Attack(player);
        else if (elementAtNewPosition is null) MoveTo(newPosition);
    }
}