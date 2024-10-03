class Dagger : Item
{
    public Dagger(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkGray;
        Icon = '-';
    }

    public override string PickUp(Player player)
    {
        Looted = true;
        string oldDie = player.AttackDice.ToString();
        player.AttackDice = new Dice(2, 8, 3);
        Remove();
        return $"You found a metal pipe, damage got upgraded from {oldDie} to {player.AttackDice}";
    }
}