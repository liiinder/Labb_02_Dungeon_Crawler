class GameLoop
{
    private int turn = 0;
    private LevelData level;
    public void Start()
    {
        Console.Write("Name: ");
        string playerName = Console.ReadLine().Trim();

        level = new LevelData(playerName);
        level.Load("Levels\\Level1.txt");
        level.DrawPlayerVision();

        for (turn = 0; level.ThePlayer.Health > 0; turn++)
        {
            PrintPlayerStatus();

            level.ThePlayer.Update(level);

            foreach (LevelElement enemy in level.Elements) (enemy as Enemy)?.Update(level);

            level.DrawPlayerVision();
        }

        PrintPlayerStatus();
    }

    public void PrintPlayerStatus()
    {
        string status = (
            $"Name: {level.ThePlayer.Name}  -  " +
            $"Health: {level.ThePlayer.Health}/{level.ThePlayer.MaxHP}  -  " +
            $"Turn: {turn}").PadRight(Console.BufferWidth);

        Console.SetCursorPosition(0, 0);
        Console.Write(status);
    }
}