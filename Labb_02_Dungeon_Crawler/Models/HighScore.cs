class HighScore
{
    public int CalculateScore(LevelData level)
    {
        bool dungeoneer = level.Elements.All(x => (x is Wall w) ? w.IsVisable : true);
        bool exterminator = level.Elements.All(x => (x is Enemy e) ? e.Health == 0 : true);
        bool loothoarder = level.Elements.All(x => (x is Item i) ? i.Looted : true);
        int damageDone = level.Player.DamageDone;
        int turns = level.Player.Turn;
        int healthLost = level.Player.MaxHP - level.Player.Health;

        //TODO: Add some kind of scoring system to return...

        return 10;
    }

    //public int PrintTop5();
    //TODO: Add a Highscore print

    //public void save(level)
    //public 
}
