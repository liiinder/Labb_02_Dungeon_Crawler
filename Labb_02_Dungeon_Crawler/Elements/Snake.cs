class Snake : Enemy
{
    public Snake(Position pos)
    {
        ElementPos = pos;
        Color = ConsoleColor.Green;
        Character = 's';
        AttackDice = new Dice(3, 4, 2);
        DefenceDice = new Dice(1, 8, 5);
        Health = 25;
        Name = "snake";
    }
    public override void Update(LevelData data)
    {
        if (ElementPos.DistanceTo(data.ThePlayer.ElementPos) < 2)
        {
            Position next = new Position(ElementPos.X, ElementPos.Y);
            if (ElementPos.X == data.ThePlayer.ElementPos.X)
            {
                if (ElementPos.Y > data.ThePlayer.ElementPos.Y) next.Y++;
                else next.Y--;
            }
            else if (ElementPos.X > data.ThePlayer.ElementPos.X) next.X++;
            else next.X--;

            LevelElement nextElement = data.Elements.FirstOrDefault(x => x.ElementPos.Equals(next));

            if (nextElement is null) Move(next);
        }
    }
}