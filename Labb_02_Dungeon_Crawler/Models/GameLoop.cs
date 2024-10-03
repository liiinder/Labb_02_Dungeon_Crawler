class GameLoop
{
    private LevelData level;
    public void Start(string path)
    {
        int amountOfEnemies = 0;
        bool runGame = true;
        
        PrintLogo(welcome);

        level = new LevelData(NameInput());
        level.Load(path);
        Console.Clear();
        
        while(runGame)
        {
            amountOfEnemies = 0;

            level.DrawPlayerVision();

            PrintPlayerStatus();

            runGame = level.Player.Update(level);

            foreach (Enemy enemy in level.Elements.Where(x => x is Enemy e && e.Health > 0))
            {
                amountOfEnemies++;
                enemy.Update(level);
            }
            if (amountOfEnemies == 0) runGame = false;
        }

        HighScore score = new HighScore();
        score.CalculateScore(level);

        PrintPlayerStatus();
        PrintLogo(amountOfEnemies == 0 ? victory : gameover);
    }

    private void PrintPlayerStatus()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        string status = (
            $"Name: {level.Player.Name}  -  " +
            $"Health: {level.Player.Health}/{level.Player.MaxHP}  -  " +
            $"Turn: {level.Player.Turn}").PadRight(Console.BufferWidth);

        Console.SetCursorPosition(1, 1);
        Console.Write(status);
    }
    private string[] welcome = {
  "                                                                         ",
 "   @@@@@@@   @@@  @@@  @@@  @@@   @@@@@@@@  @@@@@@@@   @@@@@@   @@@  @@@   ",
"    @@@@@@@@  @@@  @@@  @@@@ @@@  @@@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@ @@@    ",
"    @@!  @@@  @@!  @@@  @@!@!@@@  !@@        @@!       @@!  @@@  @@!@!@@@    ",
"    !@!  @!@  !@!  @!@  !@!!@!@!  !@!        !@!       !@!  @!@  !@!!@!@!    ",
"    @!@  !@!  @!@  !@!  @!@ !!@!  !@! @!@!@  @!!!:!    @!@  !@!  @!@ !!@!    ",
"    !@!  !!!  !@!  !!!  !@!  !!!  !!! !!@!!  !!!!!:    !@!  !!!  !@!  !!!    ",
"    !!:  !!!  !!:  !!!  !!:  !!!  :!!   !!:  !!:       !!:  !!!  !!:  !!!    ",
"    :!:  !:!  :!:  !:!  :!:  !:!  :!:   !::  :!:       :!:  !:!  :!:  !:!    ",
"    :::: ::    :::: ::   ::   ::   ::: ::::   :: ::::  ::::: ::   ::   ::    ",
 "   :: :  :     : :  :   ::    :    :: :: :   : :: ::    : :  :   ::    :   ",
  "                                                                         "};
    private string[] gameover = {
  "                                                                                        ",
 "   @@@@@@@    @@@@@    @@@  @@@     @@@@@@@@      @@@@@@   @@@  @@@  @@@@@@@@  @@@@@@@    ",
"   @@@@@@@@@  @@@@@@@@  @@@@@@@@@@   @@@@@@@@     @@@@@@@@  @@@  @@@  @@@@@@@@  @@@@@@@@    ",
"   !@@        @@!  @@@  @@! @@! @@!  @@!          @@!  @@@  @@!  @@@  @@!       @@!  @@@    ",
"   !@!        !@!  @!@  !@! !@! !@!  !@!          !@!  @!@  !@!  @!@  !@!       !@!  @!@    ",
"   !@! @!@!@  @!@!@!@!  @!! !!@ @!@  @!!!:!       @!@  !@!  @!@  !@!  @!!!:!    @!@!!@!     ",
"   !!! !!@!!  !!!@!!!!  !@!   ! !@!  !!!!!:       !@!  !!!  !@!  !!!  !!!!!:    !!@!@!      ",
"   :!!   !!:  !!:  !!!  !!:     !!:  !!:          !!:  !!!  :!:  !!:  !!:       !!: :!!     ",
"   :!:   !::  :!:  !:!  :!:     :!:  :!:          :!:  !:!   ::!!:!   :!:       :!:  !:!    ",
"    ::: ::::  ::   :::  :::     ::    :: ::::     ::::: ::    ::::     :: ::::  ::   :::    ",
 "   :: :: :    :   : :   :      :    : :: ::       : :  :      :      : :: ::    :   : :   ",
  "                                                                                        "};
    private string[] victory =
    {
  "                                                                   ",
 "  @@@@@@@  @@@@@@@   @@@  @@@  @@@  @@@@@@@@@@   @@@@@@@   @@@  @@@  ",
"   @@@@@@@  @@@@@@@@  @@@  @@@  @@@  @@@@@@@@@@@  @@@@@@@@  @@@  @@@   ",
"     @@!    @@!  @@@  @@!  @@!  @@@  @@! @@! @@!  @@!  @@@  @@!  @@@   ",
"     !@!    !@!  @!@  !@!  !@!  @!@  !@! !@! !@!  !@!  @!@  !@!  @!@   ",
"     @!!    @!@!!@!   !!@  @!@  !@!  @!! !!@ @!@  @!@@!@!   @!@!@!@!   ",
"     !!!    !!@!@!    !!!  !@!  !!!  !@!   ! !@!  !!@!!!    !!!@!!!!   ",
"     !!:    !!: :!!   !!:  !!:  !!!  !!:     !!:  !!:       !!:  !!!   ",
"     :!:    :!:  !:!  :!:  :!:  !:!  :!:     :!:  :!:       :!:  !:!   ",
"      ::    ::   :::   ::  ::::: ::  :::     ::    ::       ::   :::   ",
 "      :      :   : :  :     : :  :    :      :     :       :   : :   ",
  "                                                                   "};
    private void PrintLogo(string[] logo)
    {
        int y = 0;
        ConsoleColor Color = ConsoleColor.Black;

        foreach (string s in logo)
        {
            int x = (Console.BufferWidth - logo[y].Length) / 2;
            Console.SetCursorPosition(x, y + 6);
            foreach (char c in s)
            {
                if (c == ' ') Color = ConsoleColor.DarkGray;
                else if (c == '@') Color = ConsoleColor.Magenta;
                else if (c == '!') Color = ConsoleColor.DarkMagenta;
                else if (c == ':') Color = ConsoleColor.DarkRed;

                Console.BackgroundColor = Color;
                Console.ForegroundColor = Color;

                Console.Write(c);
            }
            y++;
        }
        Console.ResetColor();
    }
    private string NameInput()
    {
        string text = "Brave adventurer, what is your name? ";
        string line = "                                     ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
        int paddLeft = (Console.BufferWidth - line.Length) / 2;
        Console.SetCursorPosition(paddLeft, 20);
        Console.Write(line);
        Console.SetCursorPosition(paddLeft, 19);
        Console.Write(text);
        return Console.ReadLine().Trim();
    }
}