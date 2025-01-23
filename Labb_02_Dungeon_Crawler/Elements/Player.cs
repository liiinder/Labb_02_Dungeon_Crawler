public class Player : MovingElement
{
    public int DamageDone { get; set; }
    public int StatusBarTimer { get; set; }
    public int MaxHP { get; private set; }
    public int Turn { get; private set; }

    public Player(string playerName)
    {
        Name = playerName;
        Icon = '&';
        Health = 100;
        Vision = 2;
        Color = ConsoleColor.Yellow;
        AttackDice = new Dice(2, 6, 0);     // 2 -> 12
        DefenceDice = new Dice(1, 6, 2);    // 3 -> 8
        MaxHP = Health;
    }

    public void Update(LevelData level, ConsoleKeyInfo input)
    {
        Position newPosition = new Position(Position);

        if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow) newPosition.Y--;
        else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow) newPosition.X--;
        else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow) newPosition.Y++;
        else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow) newPosition.X++;

        LevelElement nextElement = level.Elements.FirstOrDefault(x => x.Position.Equals(newPosition));

        if (nextElement is Enemy enemy) Attack(enemy, level);
        else if (nextElement is Wall) level.Log.Add(new LogMessage(Wall.Quote(), Turn));
        else if (nextElement is Item item)
        {
            item.PickUp(level);
            MoveTo(newPosition);
        }
        else MoveTo(newPosition);

        Turn++;
    }
}