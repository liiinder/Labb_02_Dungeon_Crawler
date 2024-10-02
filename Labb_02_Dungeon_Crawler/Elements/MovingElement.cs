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

        if (defender is not Player && defender.Health <= 0) defender.Move(new Position(5, 0)); // Move to first whitespace char as list is read only within the assignments ruleset so cant change it.

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
    public void Move(Position next)
    {
        Erase();
        ElementPos = next;
    }
    internal void UpdateStatus(string status = "", bool mainAttack = true)
    {
        if (this is Player) Console.ForegroundColor = Color;
        
        Console.SetCursorPosition(0, (mainAttack) ? 1 : 2);
        Console.Write(status.PadRight(Console.BufferWidth * (mainAttack ? 2 : 1)));
        Console.ResetColor();
    }
}