Console.CursorVisible = false;
Console.Clear();
Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.Gray;

string level = "Level1";
//string level = "Debug";

new GameLoop().Start(level);

Console.CursorVisible = true;