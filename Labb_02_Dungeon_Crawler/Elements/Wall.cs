public class Wall : LevelElement
{
    public Wall(Position position)
    {
        Position = position;
        Color = ConsoleColor.Black;
        Icon = '#';
    }
    public static void Quote()
    {
        string[] quotes = {
            "You thud against the cold stone.",
            "The wall stands firm, unyielding.",
            "Dust swirls as you bump the surface.",
            "A faint whisper echoes from the wall.",
            "Your shoulder meets solid rock.",
            "The wall mocks your attempts to pass.",
            "A dull thump reverberates in the air.",
            "You stumble back, the wall unmoved.",
            "The ancient stone holds its secrets tight.",
            "Rubbing your forehead, you realize it’s a dead end."
        };
        Log.Add(" " + Utils.GetRandom(quotes));
        Log.AddLine();
    }
}