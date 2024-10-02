Console.CursorVisible = false;
Console.ResetColor();

new GameLoop().Start();

Console.CursorVisible = true;
Console.ResetColor();
Console.SetCursorPosition(0, 21);