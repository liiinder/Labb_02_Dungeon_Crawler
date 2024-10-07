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
        Vision = 0;
    }
    public override void Update()
    {
        Position newPosition = new Position(Position);

        string direction = Utils.GetRandom(["north", "east", "south", "west"]);

        if      (direction == "north") newPosition.Y--;
        else if (direction == "east")  newPosition.X++;
        else if (direction == "south") newPosition.Y++;
        else if (direction == "west")  newPosition.X--;

        LevelElement elementAtNewPosition = LevelData.Elements.FirstOrDefault(x => x.Position.Equals(newPosition));

        if (elementAtNewPosition is Player player) Attack(player);
        else if (elementAtNewPosition is null) MoveTo(newPosition);
    }
}