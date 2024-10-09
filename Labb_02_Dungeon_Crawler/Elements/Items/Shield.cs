class Shield : Item
{
    public Shield(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkGray;
        Icon = 'o';
    }

    public override void PickUp() 
    {
        string[] messages = {
    "You discover a sturdy shield! Your defense increases.",
    "A gleaming shield lies ahead, ready to protect you.",
    "You found a shield! Time to block incoming attacks.",
    "A heavy shield rests on the ground. You feel safer.",
    "You pick up a shield. Your chances of survival rise!",
    "A shield is uncovered! Your defense grows stronger.",
    "You found a shield! This will help in tough battles.",
    "A shield shines in the corner. Your guard is raised.",
    "You equip a shield. Enemies will have a harder time!",
    "A well-crafted shield is now in your possession!"
};
        string oldDie = LevelData.Player.DefenceDice.ToString();
        LevelData.Player.DefenceDice = new Dice(2, 6, 3); // 5 -> 15

        Looted = true;
        Remove();
        Log.Add(" " + Utils.GetRandom(messages));
        Log.Add($" Defence upgraded from {oldDie} to {LevelData.Player.DefenceDice}", ConsoleColor.Green);
        Log.AddLine();
    }
}