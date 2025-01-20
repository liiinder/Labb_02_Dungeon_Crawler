public class Shield : Item
{
    public Shield(Position position)
    {
        Position = position;
        Color = ConsoleColor.DarkGray;
        Icon = 'o';
    }

    public override void PickUp(LevelData level)
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
        var newDie = new Dice(2, 6, 3); // 5 -> 15

        Looted = true;
        level.Remove(this);
        Log.Add(" " + Utils.GetRandom(messages));
        Log.Add($" Defence upgraded from {level.Player.DefenceDice} to {newDie}", ConsoleColor.Green);
        Log.AddLine();
        level.Player.DefenceDice = newDie;
    }
}