abstract class MovingElement : LevelElement
{
    private int _health;
    public int Health
    {
        get => _health;
        set => _health = Math.Max(0, value);
    }
    public string Name { get; set; }
    public Dice AttackDice { get; set; }
    public Dice DefenceDice { get; set; }
    public void Attack(MovingElement defender, bool mainAttack = true)
    {
        int attackThrow = AttackDice.Throw();
        int defenceThrow = defender.DefenceDice.Throw();
        int damageTaken = Math.Max(0, attackThrow - defenceThrow);
        
        defender.Health -= damageTaken;

        if (this is Player player) player.DamageDone += damageTaken;
        if (defender is not Player && defender.Health <= 0) defender.Remove();

        string attacking = (this is Player) ? "You" : $"The {Name}";
        string defending = (defender is Player) ? "you" : $"the {defender.Name}";
        string defendersHP = (defender.Health > 0) ? $"{defender.Health} hp left." : $"{defender} died.";

        string print = $"{attacking} (ATK: {AttackDice} => {attackThrow}) attacked " +
                       $"{defending} (DEF: {defender.DefenceDice} => {defenceThrow}), " +
                       $"{attacking} hit {defending} for {damageTaken} damage, {defendersHP}";

        Console.ForegroundColor = (damageTaken > 0) ? ConsoleColor.Red : ConsoleColor.Green;
        UpdateStatus(print, mainAttack);

        if (mainAttack && defender.Health > 0) defender.Attack(this, mainAttack: false);
    }
    public void MoveTo(Position nextPosition)
    {
        Hide();
        Position = nextPosition;
    }
    internal void UpdateStatus(string status = "", bool bothFields = true)
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