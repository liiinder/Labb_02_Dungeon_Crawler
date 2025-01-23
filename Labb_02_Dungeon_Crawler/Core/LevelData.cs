using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class LevelData
{
    private Queue<LevelElement> deathRow = new();

    public ObjectId Id { get; set; }
    public List<LevelElement> Elements { get; set; } = new();
    public Player Player { get; set; }
    public string Path { get; set; }
    public string Level { get; set; }
    public DateTime Saved { get; set; }
    public List<LogMessage> Log { get; set; } = new();

    public LevelData() { }
    public LevelData(string playerName) => Player = new Player(playerName);

    public void LoadFile(string file)
    {
        Path = Directory.GetCurrentDirectory().Split("bin")[0] + "Levels\\";
        Level = file;
        string fileEnding = ".txt";

        if (!File.Exists(Path + Level + fileEnding)) throw new FileNotFoundException($"File not found: {Path}{Level}{fileEnding}");

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
                    Elements.Add(Player);
                }
                else if (c == '#') Elements.Add(new Wall(new Position(x, y)));
                // Enemies
                else if (c == 'r') Elements.Add(new Rat(new Position(x, y)));
                else if (c == 's') Elements.Add(new Snake(new Position(x, y)));
                else if (c == '*') Elements.Add(new Spider(new Position(x, y)));
                // Items
                else if (c == '¥') Elements.Add(new Torch(new Position(x, y)));
                else if (c == '-') Elements.Add(new Dagger(new Position(x, y)));
                else if (c == 'o') Elements.Add(new Shield(new Position(x, y)));
                else if (c == '+') Elements.Add(new Potion(new Position(x, y)));

                x++;
            }
        }
    }
    public void LoadGame(LevelData loaded)
    {
        Id = loaded.Id;
        Elements = loaded.Elements;
        Player = (Player)Elements.First(x => x is Player);
        Path = loaded.Path; // förmodligen inte används till denna nu när man byter till databas...
        Level = loaded.Level;
        Log = loaded.Log;

        Console.Clear();
        Print.VisibleWalls(this);
    }
    public void ExecuteDeathRow()
    {
        while (deathRow.Any())
        {
            LevelElement nextInLine = deathRow.Dequeue();
            Elements.Remove(nextInLine);
        }
    }
    public void Remove(LevelElement element)
    {
        element.Draw(false);
        deathRow.Enqueue(element);
    }
}

[BsonIgnoreExtraElements]
public class LevelDataLight
{
    public ObjectId id { get; set; }
    public Player Player { get; set; }
    public DateTime Saved { get; set; }
    public string Level { get; set; }
}