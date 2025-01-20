using Labb_02_Dungeon_Crawler.Utils;

Console.CursorVisible = false;
Console.Clear();
Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.Gray;

//string map = "Level1";
string map = "Debug";

GameLoop game = new GameLoop();

Print.Intro();
int choice = Menu.StartLoop();
LevelData level = new();

if (choice == 0)
{
    level = new LevelData(Print.NewGame());
    Console.Clear();
    level.LoadFile(map);
    game.Start(level);
}
else if (choice == 1)
{
    var loaded = new MongoDb().LoadGame();
    level.LoadGame(loaded);
    game.Start(level);
}

Console.CursorVisible = true;
Console.ResetColor();