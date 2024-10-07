class GameLoop
{
    public void Start(string path)
    {
        bool killedAllEnemies = true;
        bool runGame = true;
        
        new LevelData(Print.Intro());
        LevelData.Load(path);
        Console.Clear();

        while (runGame)
        {
            Print.PlayerStatus();
            Print.PlayerView();

            runGame = LevelData.Player.Update();

            //amountOfEnemies = 0;
            //foreach (Enemy enemy in level.Elements.Where(x => x is Enemy e && e.Health > 0))
            //{
            //    amountOfEnemies++; 
            //    enemy.Update(level);
            //}
            //if (amountOfEnemies == 0)
            //{
            //    killedAllEnemies = true;
            //    break;
            //}

            //TODO: Compare this to the above and below

            foreach (Enemy enemy in LevelData.Elements.Where(x => x is Enemy e && e.Health > 0)) enemy.Update();

            LevelData.ExecuteDeathRow();

            killedAllEnemies = LevelData.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
            if (killedAllEnemies) break;

            //TODO: when queue version of statusbar is fixed ... Print.StatusBar();
        }

        HighScore.GetEndScore();

        Print.PlayerStatus();

        if (killedAllEnemies) Print.Victory();
        else Print.GameOver();
    }
}