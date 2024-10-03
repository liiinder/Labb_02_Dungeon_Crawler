class Player : MovingElement
{
    public int Vision { get; set; }
    public int MaxHP { get; private set; }
    public int StatusBarTimer { get; set; }
    public int DamageDone { get; set; }
    public int Turn { get; private set; }
    public Player(string playerName)
    {
        Color = ConsoleColor.Yellow;
        Icon = '&';
        AttackDice = new Dice(1, 6, 1);
        DefenceDice = new Dice(1, 6, 0);
        Health = 100;
        MaxHP = Health;
        Vision = 2;
        Name = (playerName.Length <= 15) ? playerName : playerName[..15];
        if (Name == "") Name = "Drax Ironfist";
    }

    public bool Update(LevelData data)
    {
        Position nextPosition = new Position(Position.X, Position.Y);

        ConsoleKeyInfo input = Console.ReadKey(true);
        
        if      (input.Key == ConsoleKey.Escape) return false;
        else if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow)      nextPosition.Y--;
        else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow)    nextPosition.X--;
        else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow)    nextPosition.Y++;
        else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow)   nextPosition.X++;

        LevelElement elementAtNext = data.Elements.FirstOrDefault(x => x.Position.Equals(nextPosition));

        if      (elementAtNext is Wall) UpdateStatus(WallQuote());
        else if (elementAtNext is Enemy enemy) Attack(enemy);
        else if (elementAtNext is Item item)
        {
            UpdateStatus(item.PickUp(this));
            MoveTo(nextPosition);
        }
        else
        {
            StatusBarTimer--;
            if (StatusBarTimer == 0) UpdateStatus();
            MoveTo(nextPosition);
        }

        Turn++;
        return true;
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