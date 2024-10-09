class GameLoop
{
    public void Start(string path)
    {
        bool noEnemiesAlive = true;
        bool runGame = true;
        
        new LevelData(Print.Intro());
        LevelData.Load(path);
        Console.Clear();

        while (runGame)
        {
            Print.PlayerStatus();
            Print.PlayerView();
            Log.Print();

            runGame = LevelData.Player.Update();

            foreach (Enemy enemy in LevelData.Elements.Where(x => x is Enemy e && e.Health > 0)) enemy.Update();

            LevelData.ExecuteDeathRow();

            noEnemiesAlive = LevelData.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
            if (noEnemiesAlive) break;
        }
        Console.Clear();

        if (noEnemiesAlive) Print.Victory();
        else Print.GameOver();

        HighScore.FinalScore();
    }
}