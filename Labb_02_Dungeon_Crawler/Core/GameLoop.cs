using Labb_02_Dungeon_Crawler.Utils;

class GameLoop
{
    public void Start(LevelData level)
    {
        bool noEnemiesAlive = true;
        int option = 0;

        while (true)
        {
            Print.PlayerStatus(level);
            Print.PlayerView(level);
            Log.Print();

            option = level.Player.Update(level);

            if (option == 0) continue; //  0: Continue
            else if (option == 3) { }
            else break;                // -1: GameOver , 1: Save , 2: Quit

            foreach (Enemy enemy in level.Elements.Where(x => x is Enemy e && e.Health > 0)) enemy.Update(level);

            level.ExecuteDeathRow();

            noEnemiesAlive = level.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
            if (noEnemiesAlive) break;
        }
        Console.Clear();
        Console.ResetColor();

        if (option == 3) Print.Victory();
        else if (option == -1) Print.GameOver();
        else if (option == 1)
        {
            Console.WriteLine("Gamesaved"); //Print.Thanks();
            new MongoDb().SaveGame(level);
        }

        if (option == 3 || option == -1) HighScore.FinalScore(level);

    }
}