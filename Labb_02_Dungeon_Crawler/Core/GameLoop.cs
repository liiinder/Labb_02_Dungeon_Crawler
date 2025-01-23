using Labb_02_Dungeon_Crawler.Utils;

class GameLoop
{
    private MongoDbService Database { get; init; }
    private int logCheck = 0;
    private bool oldLogs = false;
    public GameLoop(MongoDbService db) => Database = db;

    public void Start(LevelData level, List<HighScore> scores)
    {
        bool noEnemiesAlive = false;
        string option = string.Empty;

        while (level.Player.Health > 0)
        {
            option = string.Empty;
            Print.PlayerStatus(level);
            Print.PlayerView(level);
            Print.Log(level, logCheck, oldLogs);

            ConsoleKeyInfo input = Console.ReadKey(true);

            switch (input.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.A:
                case ConsoleKey.S:
                case ConsoleKey.D:
                case ConsoleKey.UpArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.RightArrow:
                    level.Player.Update(level, input);
                    break;
                case ConsoleKey.Escape:
                    option = Menu.PauseLoop();
                    Print.VisibleWalls(level);
                    break;
                case ConsoleKey.I:
                    logCheck++;
                    oldLogs = true;
                    if (logCheck == level.Log.Count) logCheck--;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine($"Logcheck: {logCheck}");
                    continue;
                case ConsoleKey.J:
                    logCheck--;
                    oldLogs = true;
                    if (logCheck < 0) logCheck = 0;
                    continue;
            }

            logCheck = 0;
            oldLogs = false;

            if (option == "continue") continue;
            else if (option == "save" || option == "surrender") break;

            foreach (Enemy enemy in level.Elements.Where(x => x is Enemy e && e.Health > 0)) enemy.Update(level);

            level.ExecuteDeathRow();

            noEnemiesAlive = level.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
            if (noEnemiesAlive) break;
        }
        Console.Clear();
        Console.ResetColor();

        if (noEnemiesAlive) Print.Victory();
        else if (level.Player.Health <= 0 || option == "surrender") Print.GameOver();
        else if (option == "save")
        {
            Print.Saved();
            Database.SaveGame(level);
        }

        if (level.Player.Health <= 0 || noEnemiesAlive || option == "surrender")
        {
            HighScore.FinalScore(level, scores, Database);
            Database.RemoveGame(level.Id);
        }
    }
}