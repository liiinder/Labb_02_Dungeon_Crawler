static class Utils
{
    /// <summary>
    /// Method <c>GetRandom()</c> takes an array of strings and returns one of the strings by random.
    /// </summary>
    /// <param name="strings">["Hello", "World", "What", "Can", "We", "Do", "For", "You?"]</param>
    /// <returns>A random string from the array</returns>
    public static string GetRandom(String[] strings) => strings[new Random().Next(0, strings.Length)];

    /// <summary>
    /// Take a string as parameter and returns where the pointer should be placed to make the string printed center
    /// </summary>
    /// <param name="s">A string that you probably wants to center</param>
    /// <returns>an int that could be used with <c>Console.SetCursorPosition().Left to center string</c></returns>
    public static int PadCenter(string s) => (Console.BufferWidth - s.Length) / 2;

    //Returns the int needed for padLeft to be centered.
    public static int PadLeftCenter(string s) => PadCenter(s) + s.Length;
}