public class Potion : Item
{
    public Potion(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkRed;
        Icon = '+';
    }

    public override void PickUp(LevelData level)
    {
        int gain = 25;
        string[] messages = [
    "You found a health potion! You feel better already.",
    "A red potion restores your health instantly!",
    "You drink the health potion. Wounds begin to heal.",
    "Potion found! Your strength starts to return.",
    "You find a healing potion and quickly feel renewed.",
    "A health potion! You feel the warmth of healing.",
    "A glowing potion revives you. Health restored!",
    "Potion acquired! Your health is replenished.",
    "You found a health potion. Time to heal up!"
];
        Player player = level.Player;
        int healthLost = player.MaxHP - player.Health;

        if (healthLost >= gain) player.Health += gain;
        else player.Health = player.MaxHP;

        Looted = true;
        level.Remove(this);
        level.Log.Add(new LogMessage(Utils.GetRandom(messages), level.Player.Turn));
        level.Log.Add(new LogMessage($"You gain +{(healthLost < gain ? healthLost : gain)} hp!", level.Player.Turn, ConsoleColor.Green));
    }
}