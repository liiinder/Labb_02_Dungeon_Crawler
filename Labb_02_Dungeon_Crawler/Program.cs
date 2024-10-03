Console.CursorVisible = false;
Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.Gray;

string path = "Levels\\Level1.txt";
string path2 = "Levels\\Debug.txt";

new GameLoop().Start(path);

Thread.Sleep(1000);
Console.CursorVisible = true;
Console.SetCursorPosition(0, Console.BufferHeight - 7);