﻿class GameLoop
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

            foreach (Enemy enemy in LevelData.Elements.Where(x => x is Enemy e && e.Health > 0)) enemy.Update();

            LevelData.ExecuteDeathRow();

            killedAllEnemies = LevelData.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
            if (killedAllEnemies) break;

            Status.Print();
        }

        //Print.PlayerStatus();
        Console.Clear();
        if (killedAllEnemies) Print.Victory();
        else Print.GameOver();
        HighScore.FinalScore();

        Console.ReadKey(true);
    }
}