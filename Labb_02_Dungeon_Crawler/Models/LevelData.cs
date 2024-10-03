class LevelData
{
    private List<LevelElement> _elements = new List<LevelElement>();
    public List<LevelElement> Elements { get => _elements; }
    public Player Player { get; set; }
    public LevelData(string playerName) => Player = new Player(playerName);
    public void Load(string pathToFile)
    {
        if (!File.Exists(pathToFile))
        {
            throw new FileNotFoundException($"File not found on: {pathToFile}");
        }

        using (FileStream stream = File.OpenRead(pathToFile))
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data);

            int x = 3, y = 6;
            foreach (char c in data)
            {
                if (c == '\n')
                {
                    y++;
                    x = 3;
                    continue;
                }
                else if (c == '&')
                {
                    Player.Position = new Position(x, y);
                    _elements.Add(Player);
                }
                else if (c == 'r') _elements.Add(new Rat(new Position(x, y)));
                else if (c == 's') _elements.Add(new Snake(new Position(x, y)));
                else if (c == '#') _elements.Add(new Wall(new Position(x, y)));
                else if (c == '*') _elements.Add(new Torch(new Position(x, y)));
                else if (c == '-') _elements.Add(new Dagger(new Position(x, y)));
                else if (c == 'o') _elements.Add(new Shield(new Position(x, y)));

                x++;
            }
        }
    }  
    public void DrawPlayerVision()
    {
        foreach (var element in Elements)
        {
            if      (element is Enemy enemy && enemy.Health <= 0) continue;
            else if (element is Wall && element.IsVisable) continue;
            else if (element.Position.InVisionOf(Player)) element.Draw();
            else if (element.IsVisable) element.Hide();
        }
        Console.ResetColor();
    }
}