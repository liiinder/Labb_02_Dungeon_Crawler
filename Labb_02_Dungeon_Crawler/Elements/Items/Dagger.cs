public class Dagger : Item
{
    public Dagger(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkGray;
        Icon = '-';
    }

    public override void PickUp(LevelData level)
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
        var newDie = new Dice(3, 6, 2);

        Looted = true;
        level.Remove(this);
        level.Log.Add(new LogMessage(Utils.GetRandom(messages), level.Player.Turn));
        level.Log.Add(new LogMessage($"Attack upgraded from {level.Player.AttackDice} to {newDie}", level.Player.Turn, ConsoleColor.Green));
        level.Player.AttackDice = newDie; // 5 -> 20
    }
}