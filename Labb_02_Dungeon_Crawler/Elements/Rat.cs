class Rat : Enemy
{
    public Rat(Position pos)
    {
        ElementPos = pos;
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
            Position next = new Position(ElementPos.X, ElementPos.Y);
            int dir = new Random().Next(4);
            if      (dir == 0) next.Y--;
            else if (dir == 1) next.X--;
            else if (dir == 2) next.Y++;
            else if (dir == 3) next.X++;

            LevelElement nextElement = data.Elements.FirstOrDefault(x => x.ElementPos.Equals(next));

            if (nextElement is null) Move(next);
            else if (nextElement is Player player) Attack(player);
        }
    }
}