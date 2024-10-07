static class Utils
{
    /// <summary>
    /// Method <c>GetRandom()</c> takes an array of strings and returns one of the strings by random.
    /// </summary>
    /// <param name="strings">["Hello", "World", "What", "Can", "We", "Do", "For", "You?"]</param>
    /// <returns>A random string from the array</returns>
    internal static string GetRandom(String[] strings) => strings[new Random().Next(0, strings.Length)];

    ///// <summary>
    ///// Method takes two strings as input and center the output with padding<br/>
    ///// on both sides to match the width of the second string.<br/>
    ///// <c>"Hey", "1234567" -> "  Hey  "</c>
    ///// </summary>
    ///// <param name="output">String that get its text centered and the width matched</param>
    ///// <param name="stringToMatch">string that gets its width measured to use for the output</param>
    ///// <returns>
    ///// A centered string with padding on both sides with the same width as the other.<br/>
    ///// <c>"Hey", "1234567" -> "  Hey  "</c>
    ///// </returns>
    //internal static string MatchWidth(string output, string stringToMatch)
    //{
    //    int diff = stringToMatch.Length - output.Length;
        
    //    if (diff <= 0) return output;

    //    string padd = new string(' ', diff / 2);
    //    string reminder = (diff % 2 == 1) ? " " : "";

    //    return $"{padd}{output}{padd}{reminder}";
    //}

    /// <summary>
    /// Take a string as parameter and returns where the pointer should be placed to make the string printed center
    /// </summary>
    /// <param name="s">A string that you probably wants to center</param>
    /// <returns>an int that could be used with <c>Console.SetCursorPosition().Left to center string</c></returns>
    internal static int PaddToCenter(string s) => (Console.BufferWidth - s.Length) / 2;
}