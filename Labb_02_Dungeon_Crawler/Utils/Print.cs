﻿static class Print
{
    internal static void PlayerView()
    {
        foreach (var element in LevelData.Elements)
        {
            if (element is Enemy enemy && enemy.Health <= 0) continue;
            else if (element is Wall && element.IsVisable) continue;
            else if (LevelData.Player.HasVisualOn(element)) element.Draw();
            else if (element.IsVisable) element.Hide();
        }
        Console.ResetColor();
    }
    internal static void PlayerStatus()
    {
        Player player = LevelData.Player;

        string status1 = $"Name: {player.Name}".PadRight(41) + "Score:" + $"{HighScore.GetScore()}".PadLeft(7);
        string status2 = $"Health: {player.Health}/{player.MaxHP}".PadRight(42) + "Turn:" + $"{player.Turn}".PadLeft(7);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(1, 1);
        Console.Write(status1);
        Console.SetCursorPosition(1, 2);
        Console.Write(status2);
    }
    
    /// <summary>
    /// Print intro banner and ask user for a playername.
    /// Then waits for a <c>Console.ReadLine()</c>
    /// </summary>
    /// <returns>Hopefully returns a PG-13 rated playername.</returns>
    internal static string Intro()
    {
        string[] intro = {
           "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
          "!                                                                           !",
         "!    @@@@@@@   @@   @@   @@@  @@    @@@@@@@   @@@@@@@    @@@@@@   @@@  @@     !",
        "!     @@@@@@@@  @@@  @@@  @@@@ @@@  @@@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@ @@@     !",
        "!     @@!  @@@  @@!  @@@  @@!@!@@@  !@@        @@!       @@!  @@@  @@!@!@@@     !",
        "!     !@!  @!@  !@!  @!@  !@!!@!@!  !@!        !@!       !@!  @!@  !@!!@!@!     !",
        "!     @!@  !@!  @!@  !@!  @!@ !!@!  !@! @!@!@  @!!!:!    @!@  !@!  @!@ !!@!     !",
        "!     !@!  !!!  !@!  !!!  !@!  !!!  !!! !!@!!  !!!!!:    !@!  !!!  !@!  !!!     !",
        "!     !!:  !!!  !!:  !!!  !!:  !!!  :!!   !!:  !!:       !!:  !!!  !!:  !!!     !",
        "!     :!:  !:!  :!:  !:!  :!:  !:!  :!:   !::  :!:       :!:  !:!  :!:  !:!     !",
        "!     :::::::    :::: ::   ::   ::   ::::::::   :: ::::  ::::: ::   ::   ::     !",
         "!    :: : :      : :  :    :    :    :: : :    : :: ::    : :  :    :    :    !",
          "!                                                                           !",
           "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"};
        string[] messages = [
    "Welcome, brave soul! What shall we call you on this journey?",
    "Greetings, adventurer! Enter your name to begin your quest.",
    "Ah, a new hero arrives! What is your name, traveler?",
    "The dungeon awaits! What name will you go by, adventurer?",
    "Welcome to the realm! What shall your name be, brave one?",
    "Hail, hero! Tell us your name before you begin your journey.",
    "The adventure calls! Enter your name to forge your destiny.",
    "Welcome, dungeon crawler! What name do you go by, warrior?",
    "Enter the dark depths! But first, what is your name, hero?",
    "Your adventure begins now! What name shall we call you by?"];

        int maxNameLength = 15;
        string message = Utils.GetRandom(messages) + "  " + new string('_', maxNameLength);

        Banner(intro);
        BannerMessage(message);

        int left = Console.GetCursorPosition().Left - 1 - maxNameLength;
        int top = Console.GetCursorPosition().Top - 1;

        Console.SetCursorPosition(left, top);
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Write(new String(' ', maxNameLength));

        Console.SetCursorPosition(left, top);
        Console.CursorVisible = true;
        string playerName = Console.ReadLine().Trim();
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.Black;

        if (playerName == "") return "Drax Ironfist";
        return (playerName.Length <= maxNameLength) ? playerName : playerName[..maxNameLength];
    }
    internal static void GameOver()
    {
        string[] messages = [
    "The shadows have claimed you, weary traveler. Will you summon the strength to return?",
    "Your light fades in the abyss, but will you dare to reignite it?",
    "The dungeon consumes your spirit. Will you rise once more to defy it?",
    "You fall into darkness, brave soul. Will you challenge fate again?",
    "The forces of evil have bested you, but the battle is not yet over. Will you rise again?",
    "Your journey is cut short, hero. Will you embrace the challenge anew?",
    "The dungeon grows quiet in your defeat. Will you answer its call once more?",
    "The depths have claimed another victim. Will you fight your way back to the light?",
    "The darkness swallows your strength. Will you return to reclaim your honor?",
    "Your quest ends in ruin, yet the dungeon awaits. Will you try again?"];
        string[] banner = [
   "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
  "!                                                                                         !",
 "!    @@@@@@@    @@@@@    @@@  @@@     @@@@@@@@      @@@@@@   @@@  @@@  @@@@@@@@  @@@@@@@    !",
"!    @@@@@@@@@  @@@@@@@@  @@@@@@@@@@   @@@@@@@@     @@@@@@@@  @@@  @@@  @@@@@@@@  @@@@@@@@    !",
"!    !@@        @@!  @@@  @@! @@! @@!  @@!          @@!  @@@  @@!  @@@  @@!       @@!  @@@    !",
"!    !@!        !@!  @!@  !@! !@! !@!  !@!          !@!  @!@  !@!  @!@  !@!       !@!  @!@    !",
"!    !@! @!@!@  @!@!@!@!  @!! !!@ @!@  @!!!:!       @!@  !@!  @!@  !@!  @!!!:!    @!@!!@!     !",
"!    !!! !!@!!  !!!@!!!!  !@!   ! !@!  !!!!!:       !@!  !!!  !@!  !!!  !!!!!:    !!@!@!      !",
"!    :!!   !!:  !!:  !!!  !!:     !!:  !!:          !!:  !!!  :!:  !!:  !!:       !!: :!!     !",
"!    :!:   !::  :!:  !:!  :!:     :!:  :!:          :!:  !:!   ::!!:!   :!:       :!:  !:!    !",
"!    :::: ::::  ::   :::  :::     ::    :: ::::     :::::::     ::::    ::: ::::  ::   :::    !",
 "!    :: :: :    :   : :   :      :    : :: ::       : : :       :      : ::: :    :   : :   !",
  "!                                                                                         !",
   "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"];

        Banner(banner);
        BannerMessage(Utils.GetRandom(messages));
    }
    internal static void Victory()
    {
        string[] banner = [
           "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
          "!                                                                         !",
         "!    @@@@@@@@@  @@@@@@@   @@@  @@@  @@@  @@@@@@@@@@   @@@@@@@   @@@  @@@    !",
        "!      @@@@@@@   @@@@@@@@  @@@  @@@  @@@  @@@@@@@@@@@  @@@@@@@@  @@@  @@@     !",
        "!        @@!     @@!  @@@  @@!  @@!  @@@  @@! @@! @@!  @@!  @@@  @@!  @@@     !",
        "!        !@!     !@!  @!@  !@!  !@!  @!@  !@! !@! !@!  !@!  @!@  !@!  @!@     !",
        "!        @!!     @!@!!@!   !!@  @!@  !@!  @!! !!@ @!@  @!@@!@!   @!@!@!@!     !",
        "!        !!!     !!@!@!    !!!  !@!  !!!  !@!   ! !@!  !!@!!!    !!!@!!!!     !",
        "!        !!:     !!: :!!   !!:  !!:  !!!  !!:     !!:  !!:       !!:  !!!     !",
        "!        :!:     :!:  !:!  :!:  :!:  !:!  :!:     :!:  :!:       :!:  !:!     !",
        "!         ::     ::   :::   ::  ::::: ::  :::     ::    ::       ::   :::     !",
         "!         :       :   : :  :     : :  :    :      :     :       :   : :     !",
          "!                                                                         !",
           "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"];
        string[] messages =
        {
    "The darkness trembles before your might! Will you push onward to greater glory?",
    "You’ve conquered the depths, hero. But will you dare to face what lies beyond?",
    "With triumph in your grasp, will you seek out even darker mysteries?",
    "Your legend grows, brave warrior! Will you challenge the unknown once more?",
    "The shadows have fallen before you. Will you test your strength again?",
    "The dungeon lies vanquished at your feet, but many more await. Will you answer the call?",
    "The battle is won, and yet the horizon beckons. Will you forge a new path?",
    "Victory sings your name, but more dangers lie ahead. Will you heed the summons?",
    "You stand undefeated, hero. But will your courage carry you into new trials?",
    "The dungeon yields to your power! Will you march forward to face what’s next?"};

        Banner(banner);
        BannerMessage(Utils.GetRandom(messages));
    }
    
    internal static void Banner(string[] banner)
    {
        ConsoleColor Color = ConsoleColor.Black;
        for (int y = 0; y < banner.Length; y++)
        {
            int x = (Console.BufferWidth - banner[y].Length) / 2;
            Console.SetCursorPosition(x, y + 3);
            foreach (char c in banner[y])
            {
                if      (c == ' ') Color = ConsoleColor.DarkGray;
                else if (c == '@') Color = ConsoleColor.Magenta;
                else if (c == '!') Color = ConsoleColor.DarkMagenta;
                else if (c == ':') Color = ConsoleColor.DarkRed;

                Console.BackgroundColor = Color;
                Console.ForegroundColor = Color;
                Console.Write(c);
            }
        }
        Console.ResetColor();
    }
    internal static void BannerMessage(string message)
    {
        message = $" {message} ";
        string padding = new string(' ', message.Length);
        string[] output = [padding, message, padding];

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;

        int top = Console.GetCursorPosition().Top + 1;

        for (int i = 0; i < output.Length; i++)
        {
            Console.SetCursorPosition(Utils.PadCenter(output[i]), top + i);
            Console.Write(output[i]);
        }
    }
    
    // For future updates:
    // Banners are taken from patorjk.com with the font "Poison" then tweaked to personal linking.
    // https://patorjk.com/software/taag/#p=display&h=3&v=3&f=Poison
}