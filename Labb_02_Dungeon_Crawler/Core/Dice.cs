public class Dice
{
    public int NumberOfDice { get; set; }
    public int SidesPerDice { get; set; }
    public int Modifier { get; set; }
    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        NumberOfDice = numberOfDice;
        SidesPerDice = sidesPerDice;
        Modifier = modifier;
    }
    public override string ToString() => $"{NumberOfDice}d{SidesPerDice}+{Modifier}";
    public int Throw()
    {
        int sumOfThrows = 0;
        for (int i = 0; i < NumberOfDice; i++)
        {
            sumOfThrows += new Random().Next(SidesPerDice) + 1;
        }
        return sumOfThrows + Modifier;
    }
}