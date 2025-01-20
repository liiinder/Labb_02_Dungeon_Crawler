class Rat : Enemy
{
    public Rat(Position pos)
    {
        Position = pos;
        Color = ConsoleColor.Red;
        Icon = 'r';
        AttackDice = new Dice(1, 6, 3);     // 4 -> 9
        DefenceDice = new Dice(1, 6, 1);    // 2 -> 7
        Health = 10;
        Name = "rat";
        Vision = 0;
    }
    public override void Update(LevelData levelData)
    {
        Position newPosition = new Position(Position);

        string direction = Utils.GetRandom(["north", "east", "south", "west"]);

        if (direction == "north") newPosition.Y--;
        else if (direction == "east") newPosition.X++;
        else if (direction == "south") newPosition.Y++;
        else if (direction == "west") newPosition.X--;

        LevelElement elementAtNewPosition = levelData.Elements.FirstOrDefault(x => x.Position.Equals(newPosition));

        if (elementAtNewPosition is Player player) Attack(player, levelData);
        else if (elementAtNewPosition is null) MoveTo(newPosition);
    }
}