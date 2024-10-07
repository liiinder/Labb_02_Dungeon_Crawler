static class HighScore
{
    public static int GetScore()
    {
        Player player = LevelData.Player;
        int score = (player.DamageDone * 5) - (player.Turn / 2) + player.Health - player.MaxHP;
        return Math.Max(0, score);
    }

    public static int GetEndScore()
    {
        bool dungeoneer = LevelData.Elements.All(x => (x is Wall w) ? w.IsVisable : true);
        bool exterminator = LevelData.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
        bool loothoarder = LevelData.Elements.All(x => (x is Item i) ? i.Looted : true);
        int damageDone = LevelData.Player.DamageDone;
        int turns = LevelData.Player.Turn;
        int healthLost = LevelData.Player.MaxHP - LevelData.Player.Health;

        int score = GetScore();
        if (dungeoneer) score += 100;
        if (exterminator) score += 100;
        if (loothoarder) score += 100;

        return score;
    }

    public static void PrintFinalScore()
    {

    }

    //TODO: Add a Highscore print to PlayerStatus...

    //public void save() // somehow save highscore to file
    //public 
}
