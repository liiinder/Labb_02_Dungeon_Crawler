class HighScore
{
    public int Score { get; set; }
    public string Player { get; set; }
    public string Message { get; set; }
    public string Level { get; set; }

    public static List<HighScore> highScores = new List<HighScore>();

    public HighScore(string name, int score, string message, string level)
    {
        Player = name;
        Score = score;
        Message = message;
        Level = level;
    }

    public static int GetScore()
    {
        Player player = LevelData.Player;
        int score = (player.DamageDone * 5) - (player.Turn / 2) + player.Health - player.MaxHP;
        return Math.Max(0, score);
    }
    public static void FinalScore()
    {
        bool dungeoneer = LevelData.Elements.All(x => (x is Wall w) ? w.IsVisable : true);
        bool exterminator = LevelData.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
        bool loothoarder = LevelData.Elements.All(x => (x is Item i) ? i.Looted : true);
 
        int finalScore = GetScore();

        if (dungeoneer) finalScore += 100;
        if (exterminator) finalScore += 500;
        if (loothoarder) finalScore += 250;

        if (finalScore > 0)
        {
            string message = $"Name: {LevelData.Player.Name}  -  Score: {finalScore}" +
                $"{((dungeoneer || loothoarder || exterminator) ? "  -  Achievements:" : "")}" +
                $"{(dungeoneer ? " Dungeoneer (+100)," : "")}" +
                $"{(loothoarder ? " Loothoarder (+250)," : "")}" +
                $"{(exterminator ? " Exterminator (+500)," : "")}";

            string save = $"Name: {LevelData.Player.Name}  -  Score: {finalScore}" +
                $"{((dungeoneer || loothoarder || exterminator) ? "  -  Achievements: " : "")}" +
                $"{(dungeoneer ? " Dungeoneer," : "")}" +
                $"{(loothoarder ? " Loothoarder," : "")}" +
                $"{(exterminator ? " Exterminator," : "")}";

            highScores.Add(new HighScore(LevelData.Player.Name, finalScore, save[..^1], LevelData.Path));

            int top = Console.GetCursorPosition().Top + 1;

            //Console.SetCursorPosition(Utils.PaddToCenter(message[..^1]), top);
            Console.SetCursorPosition(Utils.PaddToCenter(message[..^1]), 1);
            Console.Write(message[..^1]);
            Console.SetCursorPosition(0, top - 1);
            Test();
        }
    }

    public static void Test()
    {
        // Test example of what I want the output to look like...
        string[] test = {
            "    Name              Score   Achievements                          ",
            "--------------------------------------------------------------------",
            " 1. Liiinder           1241   Dungeoneer, Loothoarder, Exterminator ",
            " 2. Elana the Elf      1000   Dungeoneer                            ",
            " 3. Drax Ironfist       973   Loothoarder, Exterminator             ",
            " 4. Kaj Kajak           830   Exterminator                          ",
            " 3. Bosse               230                                         ",};
        foreach (string s in test)
        {
            int top = Console.GetCursorPosition().Top + 1;
            Console.SetCursorPosition(Utils.PaddToCenter(s), top);
            Console.Write(s);
        }
    }
    //TODO: Make a savefile for Top 5 highscores and then load/print it like the above and save scores after each game.
    // Sort scores after highest score...
    //public void save() // somehow save highscore to file
    //public 
}
