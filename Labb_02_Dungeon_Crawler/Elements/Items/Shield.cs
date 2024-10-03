class Shield : Item
{
    public Shield(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkGray;
        Icon = 'o';
    }

    public override string PickUp(Player player)
    {
        Looted = true;
        string oldDie = player.DefenceDice.ToString();
        player.DefenceDice = new Dice(2, 6, 2);
        Remove();
        return $"That's a weird piece of scrap! Your defence got upgraded from {oldDie} to {player.DefenceDice}";
    }
}