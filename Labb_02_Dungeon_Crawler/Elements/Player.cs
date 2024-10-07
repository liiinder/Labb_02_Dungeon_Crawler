class Player : MovingElement
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
        AttackDice = new Dice(1, 8, 2);     // 3 -> 10
        DefenceDice = new Dice(1, 6, 2);    // 3 -> 8
        MaxHP = Health;
    }

    public bool Update()
    {
        ConsoleKeyInfo input = Console.ReadKey(true);
        
        if (input.Key == ConsoleKey.Escape) return false;

        Position newPosition = new Position(Position);

        if      (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow)      newPosition.Y--;
        else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow)    newPosition.X--;
        else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow)    newPosition.Y++;
        else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow)   newPosition.X++;

        LevelElement nextElement = LevelData.Elements.FirstOrDefault(x => x.Position.Equals(newPosition));

        if      (nextElement is Wall wall) Wall.Quote();
        else if (nextElement is Enemy enemy) Attack(enemy);
        else if (nextElement is Item item)
        {
            item.PickUp();
            MoveTo(newPosition);
        }
        else MoveTo(newPosition);

        Turn++;
        return Health > 0;
    }
}