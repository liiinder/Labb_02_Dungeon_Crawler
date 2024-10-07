Console.CursorVisible = false;
Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.Gray;

//string path = "Levels\\Level1.txt";
string path = "Levels\\Debug.txt";

new GameLoop().Start(path);

Console.CursorVisible = true;
Console.SetCursorPosition(0, Console.BufferHeight - 1);