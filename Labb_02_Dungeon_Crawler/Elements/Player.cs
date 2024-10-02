class Player : MovingElement
{
    public int MaxHP { get; private set; }
    public int Vision { get; set; }
    public Player(string playerName)
    {
        Color = ConsoleColor.Yellow;
        Character = '@';
        AttackDice = new Dice(2, 6, 2);
        DefenceDice = new Dice(2, 6, 0);
        Health = 100;
        MaxHP = Health;
        Name = playerName;
        Vision = 2;
    }

    public void Update(LevelData data)
    {
        Position next = new Position(ElementPos.X, ElementPos.Y);

        ConsoleKeyInfo input = Console.ReadKey(true);
        
        if      (input.Key == ConsoleKey.Escape) Health = 0;
        else if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow)      next.Y--;
        else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow)    next.X--;
        else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow)    next.Y++;
        else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow)   next.X++;
        else if (input.Key == ConsoleKey.I) Vision--;
        else if (input.Key == ConsoleKey.O) Vision++;

        LevelElement nextElement = data.Elements.FirstOrDefault(x => x.ElementPos.Equals(next));

        if (nextElement is Wall) UpdateStatus(WallQuote());
        else if (nextElement is Enemy enemy) Attack(enemy);
        else if (nextElement is Torch torch)
        {
            torch.ElementPos = new Position(5, 0);
            Vision += 3;
            UpdateStatus("You found a torch that will make it easier to explore the dungeon!");
            Move(next);
        }
        else
        {
            UpdateStatus();
            Move(next);
        }
    }

    private string WallQuote()
    {
        string[] quotes = {
            "You thud against the cold stone.",
            "The wall stands firm, unyielding.",
            "Dust swirls as you bump the surface.",
            "A faint whisper echoes from the wall.",
            "Your shoulder meets solid rock.",
            "The wall mocks your attempts to pass.",
            "A dull thump reverberates in the air.",
            "You stumble back, the wall unmoved.",
            "The ancient stone holds its secrets tight.",
            "Rubbing your forehead, you realize it’s a dead end."
        };
        return quotes[new Random().Next(quotes.Length)];
    }
}