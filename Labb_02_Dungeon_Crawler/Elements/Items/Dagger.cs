class Dagger : Item
{
    public Dagger(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkGray;
        Icon = '-';
    }

    public override string PickUp()
    {
        string[] messages = {
    "You found a sharp dagger! Perfect for close combat.",
    "A sleek dagger lies ahead. You grip it tightly.",
    "You pick up a dagger. Swift and deadly in your hand.",
    "A dagger is found! Lightweight and ready to strike.",
    "You discover a dagger. It gleams with a keen edge.",
    "A dagger rests here. You feel more dangerous now.",
    "You grab a dagger. Perfect for quick, silent strikes.",
    "A finely crafted dagger is now in your possession.",
    "You found a dagger! Speed and precision at your side.",
    "A dagger glints in the shadows. Time to strike fast!"
};
        string oldDie = LevelData.Player.AttackDice.ToString();
        LevelData.Player.AttackDice = new Dice(2, 8, 3);

        Looted = true;
        Remove();
        return Utils.GetRandom(messages) +
            $" Attack upgraded from {oldDie} to { LevelData.Player.AttackDice}";
    }
}