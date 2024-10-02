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
        Position nextPosition = new Position(Position.X, Position.Y);

        ConsoleKeyInfo input = Console.ReadKey(true);
        
        if      (input.Key == ConsoleKey.Escape) Health = 0;
        else if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow)      nextPosition.Y--;
        else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow)    nextPosition.X--;
        else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow)    nextPosition.Y++;
        else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow)   nextPosition.X++;
        else if (input.Key == ConsoleKey.I) Vision--;
        else if (input.Key == ConsoleKey.O) Vision++;

        LevelElement elementAtNext = data.Elements.FirstOrDefault(x => x.Position.Equals(nextPosition));

        if      (elementAtNext is Wall) UpdateStatus(WallQuote());
        else if (elementAtNext is Enemy enemy) Attack(enemy);
        else if (elementAtNext is Torch torch)
        {
            torch.Remove();
            Vision += 3;
            UpdateStatus("You find a torch, its light flickering in the dark.");
            MoveTo(nextPosition);
        }
        else
        {
            UpdateStatus();
            MoveTo(nextPosition);
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