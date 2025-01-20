public abstract class MovingElement : LevelElement
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
    public void Attack(MovingElement defender, LevelData level, bool mainAttack = true)
    {
        int attackThrow = AttackDice.Throw();
        int defenceThrow = defender.DefenceDice.Throw();
        int damageTaken = Math.Max(0, attackThrow - defenceThrow);
        defender.Health -= damageTaken;

        if (this is Player player) player.DamageDone += damageTaken;
        if (defender is Enemy && defender.Health <= 0) level.Remove(defender);

        string attacking = (this is Player) ? "You" : $"A {Name}";
        string defending = (defender is Player) ? "you" : $"a {defender.Name}";
        string defending2 = (defender is Player) ? "you" : "it";
        string defenderStatus = (defender.Health > 0) ? $"for {damageTaken} dmg ({defender.Health} hp)" : $"{defending2} died!";

        string message = $" {attacking} ({AttackDice} » {attackThrow}) ATK" +
                       $" {defending} ({defender.DefenceDice} » {defenceThrow}) {defenderStatus}";

        ConsoleColor color = ConsoleColor.Yellow;
        if (this is not Player) color = (damageTaken > 0) ? ConsoleColor.Red : ConsoleColor.Green;

        Log.Add(message, color);

        if (mainAttack && defender.Health > 0) defender.Attack(this, level, mainAttack: false);
        else Log.AddLine();
    }
    public void MoveTo(Position newPosition)
    {
        Hide();
        Position = newPosition;
    }
}