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
        ConsoleColor color = ConsoleColor.Yellow;
        
        defender.Health -= damageTaken;

        if (this is Player player) player.DamageDone += damageTaken;
        if (defender.Health <= 0 && defender is not Player) defender.Remove();

        string attacking = (this is Player) ? "You" : $"A {Name}";
        string defending = (defender is Player) ? "you" : $"a {defender.Name}";
        string defendersHP = (defender.Health > 0) ? $"{defender.Health} hp left." : $"{defending} died.";

        string message = $" {attacking} attacked ({AttackDice} » {attackThrow})" +
                       $" {defending} ({defender.DefenceDice} » {defenceThrow}) for {damageTaken} damage";

        if (this is not Player) color = (damageTaken > 0) ? ConsoleColor.Red : ConsoleColor.Green;

        Status.Add(message, color);

        if (mainAttack && defender.Health > 0) defender.Attack(this, mainAttack: false);
        else Status.AddLine();
    }
    public void MoveTo(Position newPosition)
    {
        Hide();
        Position = newPosition;
    }
}