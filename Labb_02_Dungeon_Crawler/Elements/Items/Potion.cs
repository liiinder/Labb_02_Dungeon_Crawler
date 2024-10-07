class Potion : Item
{
    public Potion(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkRed;
        Icon = '+';
    }

    public override void PickUp()
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
        int healthLost = LevelData.Player.MaxHP - LevelData.Player.Health;

        if (healthLost >= gain) LevelData.Player.Health += gain;
        else LevelData.Player.Health = LevelData.Player.MaxHP;

        Looted = true;
        Remove();
        Status.Add(" " + Utils.GetRandom(messages));
        Status.Add($" You gain +{(healthLost < gain ? healthLost : gain)} hp!", ConsoleColor.Green);
        Status.AddLine();
    }
}