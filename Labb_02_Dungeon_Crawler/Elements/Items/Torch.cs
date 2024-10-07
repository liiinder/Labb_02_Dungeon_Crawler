class Torch : Item
{
    public Torch(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkYellow;
        Icon = '¥';
    }

    public override string PickUp()
    {
        int gain = 2;
        LevelData.Player.Vision += gain;
        string[] messages = {
    "You found a torch! The darkness recedes slightly.",
    "A torch flickers to life. Your path is now visible.",
    "You pick up a torch. Light floods the gloomy halls.",
    "A torch is found! The shadows seem less threatening.",
    "You grab a torch. Its flame banishes the dark around you.",
    "A torch lights up the room. You feel a bit safer now.",
    "You found a torch! Its glow guides your way forward.",
    "A torch lies ahead. With it, you can see further now.",
    "You pick up a torch. The flame burns brightly.",
    "A torch illuminates the darkness. Your vision clears."
};

        Looted = true;
        Remove();
        return Utils.GetRandom(messages) + $" +{gain} vision.";
    }
}