class LevelData
{
    private List<LevelElement> _elements = new List<LevelElement>();
    public List<LevelElement> Elements { get => _elements; }
    public Player ThePlayer { get; set; }
    public LevelData(string playerName) => ThePlayer = new Player(playerName);
    public void Load(string pathToFile)
    {
        using (FileStream stream = File.OpenRead(pathToFile))
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data);

            int x = 0;
            int y = 3; // Saves all positions 3 rows for status bars.
            for (int i = 0; i < stream.Length; i++)
            {
                if (data[i] == 10) // 10, ascii for newline
                {
                    y++;
                    x = 0;
                    continue;
                }

                if ((char)data[i] == '@')
                {
                    ThePlayer.ElementPos = new Position(x, y);
                    _elements.Add(ThePlayer);
                }
                else if ((char)data[i] == 'r') _elements.Add(new Rat(new Position(x, y)));
                else if ((char)data[i] == 's') _elements.Add(new Snake(new Position(x, y)));
                else if ((char)data[i] == '#') _elements.Add(new Wall(new Position(x, y)));
                else if ((char)data[i] == '*') _elements.Add(new Torch(new Position(x, y)));

                x++;
            }
        }
    }  
    public void DrawPlayerVision() 
    {
        foreach (var element in Elements)
        {
            if (element is Enemy enemy && enemy.Health <= 0) continue;
            else if (ThePlayer.ElementPos.DistanceTo(element.ElementPos) <= ThePlayer.Vision) element.Draw();
            else if (element is Wall wall && wall.IsVisable) element.Draw();
            else element.Erase();
        }
        Console.ResetColor();
    }
}