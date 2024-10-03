class Torch : Item
{
    public Torch(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkYellow;
        Icon = '¥';
    }

    public override string PickUp(Player player)
    {
        Looted = true;
        player.Vision += 3;
        Remove();
        return "You found a torch, its light flickering in the dark!";
    }
}