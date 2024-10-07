class Dice
{
    private int numberOfDice;
    private int sidesPerDice;
    private int modifier;
    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        this.numberOfDice = numberOfDice;
        this.sidesPerDice = sidesPerDice;
        this.modifier = modifier;
    }
    public override string ToString() => $"{numberOfDice}d{sidesPerDice}+{modifier}";
    public int Throw()
    {
        int sumOfThrows = 0;
        for (int i = 0; i < numberOfDice; i++)
        {
            sumOfThrows += new Random().Next(sidesPerDice) + 1;
        }
        return sumOfThrows + modifier;
    }
}