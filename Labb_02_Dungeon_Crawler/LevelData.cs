class LevelData
{
    private List<LevelElement> _elements = new List<LevelElement>();
    public List<LevelElement> Elements { get => _elements; }
    public Player Player { get; set; }
    public LevelData(string playerName) => Player = new Player(playerName);
    public void Load(string pathToFile)
    {
        //TODO: Byta till streamreader och läsa in rad för rad...
        using (FileStream stream = File.OpenRead(pathToFile))
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data);

            int x = 0;
            int y = 3; // Saves all positions 3 rows for status bars. will get better with other input...
            for (int i = 0; i < stream.Length; i++)
            {
                if (data[i] == 10) // 10, ascii for newline... will not be needed in streamreader readline
                {
                    y++;
                    x = 0;
                    continue;
                }

                if ((char)data[i] == '@')
                {
                    Player.Position = new Position(x, y);
                    _elements.Add(Player);
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
            else if (element.Position.InVision(Player)) element.Draw();
            else if (element is Wall && element.IsVisable) element.Draw();
            else element.Hide();
        }
        Console.ResetColor();
    }
}