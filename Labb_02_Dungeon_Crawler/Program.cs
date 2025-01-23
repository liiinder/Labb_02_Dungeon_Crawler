using Labb_02_Dungeon_Crawler.Utils;

string connectionString = "mongodb://localhost:27017/";
string databaseName = "KristofferLinder";
MongoDbService database = new(connectionString, databaseName);

string map = "Level1";
//string map = "Debug";

var loadData = database.GetSavedGames();
var loadScores = database.GetHighScores(map);

List<LevelDataLight> savedgames;
List<HighScore> scores;

Console.CursorVisible = false;
Console.Clear();
Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.Gray;

GameLoop game = new GameLoop(database);

Print.Intro();
int choice = Menu.StartLoop();
LevelData level = new();

if (choice == 0)
{
    level = new LevelData(Print.NewGame());
    Console.Clear();
    level.LoadFile(map);
}
else if (choice == 1)
{
    savedgames = await loadData;
    var gameid = Menu.SavedGames(savedgames);
    var loaded = database.LoadGame(gameid);
    level.LoadGame(loaded);
}

if (choice != 2)
{
    scores = await loadScores;
    game.Start(level, scores);
    Console.ReadKey(true);
}

Console.CursorVisible = true;
Console.ResetColor();