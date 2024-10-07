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
        AttackDice = new Dice(1, 6, 2);
        DefenceDice = new Dice(1, 6, 0);
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

        if      (nextElement is Wall) UpdateStatusBar(Print.GetWallQuote()); //TODO: Clean this? (with new queue printstatus...)
        else if (nextElement is Enemy enemy) Attack(enemy);
        else if (nextElement is Item item)
        {
    //TODO: Move the UpdateStatusBar from Moving element to Print and call it directly from item.PickUp / attack etc.
            UpdateStatusBar(item.PickUp());
            MoveTo(newPosition);
        }
        else
        {
            //TODO: Maybe change print class to not static, add a turn timer there instead...?
            StatusBarTimer--; 
            //TODO: Maybe fix another way of having the statusbar cleared?
            if (StatusBarTimer == 0) UpdateStatusBar(); //TODO: Maybe fix a separate clear statusbar?
            MoveTo(newPosition);
        }

        Turn++;

        return Health > 0;
    }
}