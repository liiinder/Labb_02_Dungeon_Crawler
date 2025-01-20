public static class Menu
{
    static int selected = 0;
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

    public static int PauseLoop()
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
        return selected % 3;
        // 0: Continue
        // 1: Save
        // 2: Quit
    }

    public static void Pause()
    {
        string[] options = {
          "!!!!!!!!!!!!!!!",
         "!               !",
        "!    Continue     !",
        "!   Save & Exit   !",
        "!      Exit       !",
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
}