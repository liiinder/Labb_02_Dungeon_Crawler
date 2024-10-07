abstract class MovingElement : LevelElement
{
    private int _health;
    public int Health
    {
        get => _health;
        set => _health = Math.Max(0, value);
    }
    public Dice AttackDice { get; set; }
    public Dice DefenceDice { get; set; }
    public string Name { get; protected set; }
    public double Vision { get; set; }

    public bool HasVisualOn(LevelElement element)
    {
        return (Position.DistanceTo(element) <= Vision && element.Position.Y > 0);
    }
    public void Attack(MovingElement defender, bool mainAttack = true)
    {
        int attackThrow = AttackDice.Throw();
        int defenceThrow = defender.DefenceDice.Throw();
        int damageTaken = Math.Max(0, attackThrow - defenceThrow);
        
        defender.Health -= damageTaken;

        if (this is Player player) player.DamageDone += damageTaken;
        if (defender.Health <= 0 && defender is not Player) defender.Remove();

        string attacking = (this is Player) ? "You" : $"The {Name}";
        string defending = (defender is Player) ? "you" : $"the {defender.Name}";
        string defendersHP = (defender.Health > 0) ? $"{defender.Health} hp left." : $"{defending} died.";

        string message = $"{attacking} (ATK: {AttackDice} => {attackThrow}) attacked " +
                       $"{defending} (DEF: {defender.DefenceDice} => {defenceThrow}), " +
                       $"{attacking} hit {defending} for {damageTaken} damage, {defendersHP}";

        Console.ForegroundColor = (damageTaken > 0) ? ConsoleColor.Red : ConsoleColor.Green;
        UpdateStatusBar(message, mainAttack); //TODO: Enqueue message instead...

        if (mainAttack && defender.Health > 0) defender.Attack(this, mainAttack: false);
    }
    public void MoveTo(Position newPosition)
    {
        Hide();
        Position = newPosition;
    }
    
    //TODO: work on moving this to Print
    internal void UpdateStatusBar(string status = "", bool bothFields = true)
    {
        if (this is Player player)
        {
            Console.ForegroundColor = Color;
            player.StatusBarTimer = 3;
        }
        
        Console.SetCursorPosition(1, bothFields ? 3 : 4);
        Console.Write(status.PadRight(Console.BufferWidth * (bothFields ? 2 : 1)));
        Console.ResetColor();
    }
}