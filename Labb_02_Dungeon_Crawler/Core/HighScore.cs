using System.Text.Json;

class HighScore
{
    private static string pathToFile = LevelData.Path + "Highscores\\" + LevelData.Level + ".dat";
    public static List<HighScore> scores = new List<HighScore>();

    public int Score { get; set; }
    public string Name { get; set; }
    public string Achievement { get; set; }
    public DateTime Time { get; set; }

    public HighScore(string name, int score, string achievement, DateTime time)
    {
        Name = name;
        Score = score;
        Achievement = achievement;
        Time = time;
    }

    public static int GetScore()
    {
        Player player = LevelData.Player;
        int score = (player.DamageDone * 5) - (player.Turn / 2) + player.Health - player.MaxHP;
        return Math.Max(0, score);
    }
    public static void FinalScore()
    {
        Load();
        int top = Console.GetCursorPosition().Top;
        HighScore current;

        bool dungeoneer = LevelData.Elements.All(x => (x is Wall w) ? w.IsVisable : true);
        bool exterminator = LevelData.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
        bool loothoarder = LevelData.Elements.All(x => (x is Item i) ? i.Looted : true);
 
        int finalScore = GetScore();

        if (dungeoneer) finalScore += 100;
        if (exterminator) finalScore += 500;
        if (loothoarder) finalScore += 250;

        string message = $"Name: {LevelData.Player.Name}   Score: {finalScore} " +
            $"{((dungeoneer || loothoarder || exterminator) ? "   Achievements:" : "")}" +
            $"{(dungeoneer ? " Dungeoneer (+100)," : "")}" +
            $"{(loothoarder ? " Loothoarder (+250)," : "")}" +
            $"{(exterminator ? " Exterminator (+500)," : "")}";

        string bonus = $"{(dungeoneer ? "Dungeoneer, " : "")}" +
            $"{(loothoarder ? "Loothoarder, " : "")}" +
            $"{(exterminator ? "Exterminator, " : "")}";
        if (bonus.Length > 0) bonus = "   " + bonus[..^2];

        current = new HighScore(LevelData.Player.Name, finalScore, bonus, DateTime.Now);
        Save(current);

        Console.SetCursorPosition(Utils.PadCenter(message[..^1]), 1);
        Console.Write(message[..^1]);
        
        Console.SetCursorPosition(0, top);
        Print(current);
    }

    public static void Save(HighScore score)
    {
        scores.Add(score);
        scores = scores.OrderByDescending(x => x.Score).ThenBy(x => x.Time).ToList();

        string jsonString = JsonSerializer.Serialize(new HighScores(scores));

        using (StreamWriter writer = new StreamWriter(pathToFile, false)) writer.WriteLine(jsonString);
    }

    public static void Load()
    {
        try
        {
            using (StreamReader reader = new StreamReader(pathToFile))
            {
                while (!reader.EndOfStream)
                {
                    string jsonString = reader.ReadLine();

                    scores = JsonSerializer.Deserialize<HighScores>(jsonString).Scores;
                }
            }
        }
        catch { }
    }

    public static void Print(HighScore score, bool all = false)
    {
        int place = 0;
        int count = scores.Count;

        if (all) scores.Reverse();
        if (!all && count > 5) count = 5;
        int extraDigits = count.ToString().Length - 1;

        string title = new string(' ', 4 + extraDigits) + "Name               Score   Achievements                          ";
        string line = new String('-', title.Length);
        int x = Utils.PadLeftCenter(title);
        int left = x - title.Length;
            
        Console.Write("\n" + title.PadLeft(x));
        Console.Write("\n" + line.PadLeft(Utils.PadLeftCenter(line)));

        for (int i = 0; i < count; i++)
        {
            if (all) place = count - i;
            else place = i + 1;
            HighScore curr = scores[i];

            Console.Write("\n" + "".PadLeft(left));
            if (curr.Equals(score)) Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.Write(($" {place}.".PadLeft(3 + extraDigits) + $" {curr.Name.PadRight(18)}" +
                $"{curr.Score.ToString().PadLeft(6)}{curr.Achievement} ").PadRight(line.Length));

            if (curr.Equals(score)) Console.BackgroundColor = ConsoleColor.Black;
        }

        Console.WriteLine("\n");

        if (all)
        {
            string[] thankYouMessages = {
    "Thank you for braving the depths of our dark dungeons!",
    "Your courage brought light to the darkest corners. Thank you!",
    "Thanks for playing and surviving our challenging dungeons!",
    "You've conquered the dungeon! Thanks for the adventure!",
    "The monsters fall, and we have you to thank, brave adventurer!",
    "Thanks for exploring the unknown and surviving its dangers!",
    "Your journey was legendary! Thank you for playing!",
    "You’ve faced the darkness and triumphed. Thanks for playing!",
    "The dungeon is silent, but your story echoes. Thank you!",
    "Thank you for your bravery, adventurer. See you next time!"
};
            string message = Utils.GetRandom(thankYouMessages);
            Console.Write(message.PadLeft(Utils.PadLeftCenter(message)) + "\n");
            Console.ReadKey(true);
        }
        else
        {
            string space = "Press Space for HighScores!";
            Console.Write(space.PadLeft(Utils.PadLeftCenter(space)) + "\n");
            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Spacebar) Print(score, true);
        }
    }
}

record HighScores
{
    public List<HighScore> Scores { get; init; }
    public HighScores(List<HighScore> scores) => Scores = scores;
}