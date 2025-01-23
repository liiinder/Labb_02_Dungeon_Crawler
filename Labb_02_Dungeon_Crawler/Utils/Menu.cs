using MongoDB.Bson;

public static class Menu
{
    static int selected = 0;
    static string[] options = ["continue", "save", "surrender"];
    public static int StartLoop()
    {
        while (true)
        {
            Start();
            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Enter) break;
            else if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow) selected--;
            else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow) selected++;

            if (selected < 0) selected = 2;
        }
        Console.ResetColor();
        Console.Clear();
        return selected % 3;
    }

    public static void Start()
    {
        string[] options = {
          "!!!!!!!!!!!!!!",
         "!              !",
        "!    New Game    !",
        "!    Load Game   !",
        "!      Exit      !",
         "!              !",
          "!!!!!!!!!!!!!!"
        };

        ConsoleColor Color = ConsoleColor.Black;

        for (int y = 0; y < options.Length; y++)
        {
            int left = Utils.PadCenter(options[y]);
            Console.SetCursorPosition(left, y + 18);

            for (int x = 0; x < options[y].Length; x++)
            {
                char c = options[y][x];

                if (c == ' ') Color = ConsoleColor.DarkGray;
                else if (c == '@') Color = ConsoleColor.Magenta;
                else if (c == '!') Color = ConsoleColor.DarkMagenta;
                else if (c == ':') Color = ConsoleColor.DarkRed;


                Console.BackgroundColor = Color;
                if (Char.IsLetter(c)) Console.ForegroundColor = ConsoleColor.Black;
                else Console.ForegroundColor = Color;

                if ((Math.Abs(selected) % 3) == y - 2)
                {
                    if (x >= 3 && x <= 14)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                }

                Console.Write(c);
            }
        }
    }

    public static string PauseLoop()
    {
        while (true)
        {
            Pause();
            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Enter) break;
            else if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow) selected--;
            else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow) selected++;

            if (selected < 0) selected = 2;
        }
        Console.ResetColor();
        Console.Clear();

        return options[selected % 3];
    }

    public static void Pause()
    {
        string[] options = {
          "!!!!!!!!!!!!!!!",
         "!               !",
        "!    Continue     !",
        "!   Save & Exit   !",
        "!    Surrender    !",
         "!               !",
          "!!!!!!!!!!!!!!!"
        };

        ConsoleColor Color = ConsoleColor.Black;

        for (int y = 0; y < options.Length; y++)
        {
            int left = Utils.PadCenter(options[y]);
            Console.SetCursorPosition(left, y + 10);

            for (int x = 0; x < options[y].Length; x++)
            {
                char c = options[y][x];

                if (c == ' ') Color = ConsoleColor.DarkGray;
                else if (c == '@') Color = ConsoleColor.Magenta;
                else if (c == '!') Color = ConsoleColor.DarkMagenta;
                else if (c == ':') Color = ConsoleColor.DarkRed;


                Console.BackgroundColor = Color;
                if (Char.IsLetter(c) || c == '&') Console.ForegroundColor = ConsoleColor.Black;
                else Console.ForegroundColor = Color;

                if ((Math.Abs(selected) % 3) == y - 2)
                {
                    if (x >= 3 && x <= 15)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                }

                Console.Write(c);
            }
        }
    }

    public static void PrintSavedGames(List<LevelDataLight> games, int index, int hover)
    {
        string header = $"Saved Games: {games.Count}";
        //Console.Clear();
        Console.SetCursorPosition(Utils.PadCenter(header), 8);
        Console.WriteLine(header);
        Console.WriteLine();
        for (int i = 0; i < 10; i++)
        {
            if (i + (index * 10) >= games.Count)
            {
                Console.WriteLine(new String(' ', Console.BufferWidth));
            }
            else
            {
                var game = games[i + (index * 10)];

                if (i == hover - (index * 10))
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                string message = $"  {(i + (index * 10) + 1),3}. {game.Player.Name}".PadRight(27) +
                    $"Turn: {game.Player.Turn,4}".PadRight(13) +
                    $"Health: {game.Player.Health,3}/{game.Player.MaxHP,3}".PadRight(17) +
                    $"Map: {game.Level,7}".PadRight(15) +
                    $"Saved: {game.Saved.ToString("yy-MM-dd hh:mm")}  ";
                Console.SetCursorPosition(Utils.PadCenter(message), Console.GetCursorPosition().Top);
                Console.WriteLine(message);
                if (i == hover - (index * 10)) Console.ResetColor();
            }
        }
        int max = ((index * 10) + 10);
        max = (max > games.Count) ? games.Count : max;
        string amountof = $"    Shows: {(index * 10) + 1,2} - {max,2}    ";

        Console.SetCursorPosition(Utils.PadCenter(amountof), Console.GetCursorPosition().Top + 1);
        Console.WriteLine(amountof);
    }

    public static ObjectId SavedGames(List<LevelDataLight> games)
    {
        int hover = 0;
        int index = 0;

        while (true)
        {
            PrintSavedGames(games, index, hover);

            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Enter) break;
            else if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow) hover--;
            else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow) hover++;

            if (hover < 0) hover = games.Count - 1;
            else if (hover == games.Count) hover = 0;
            index = hover / 10;
        }

        return games[hover].id;
    }
}