class LevelData
{
    public static Queue<LevelElement> deathRow = new Queue<LevelElement>();
    private static List<LevelElement> _elements = new List<LevelElement>();

    public static List<LevelElement> Elements { get => _elements; }
    public static Player Player { get; set; }
    public static string Path { get; set; }
    public static string Level { get; set; }

    public LevelData(string playerName) => Player = new Player(playerName);

    public static void Load(string file)
    {
        Path = Directory.GetCurrentDirectory().Split("bin")[0] + "Levels\\";
        Level = file;
        string fileEnding = ".txt";

        if (!File.Exists(Path + Level + fileEnding)) throw new FileNotFoundException($"File not found: {Path}{Level}{fileEnding}");

        //TODO: StreamReader... - byta till ...
        using (FileStream stream = File.OpenRead(Path + Level + ".txt"))
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data);

            int x = 3, y = 4;
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
                else if (c == '#') _elements.Add(new Wall(new Position(x, y)));
                // Enemies
                else if (c == 'r') _elements.Add(new Rat(new Position(x, y)));
                else if (c == 's') _elements.Add(new Snake(new Position(x, y)));
                else if (c == '*') _elements.Add(new Spider(new Position(x, y)));
                // Items
                else if (c == '¥') _elements.Add(new Torch(new Position(x, y)));
                else if (c == '-') _elements.Add(new Dagger(new Position(x, y)));
                else if (c == 'o') _elements.Add(new Shield(new Position(x, y)));
                else if (c == '+') _elements.Add(new Potion(new Position(x, y)));

                x++;
            }
        }
    }  

    public static void ExecuteDeathRow()
    {
        while (deathRow.Any())
        {
            LevelElement nextInLine = deathRow.Dequeue();
            Elements.Remove(nextInLine);
        }
    }
}