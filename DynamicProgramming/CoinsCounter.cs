namespace DynamicProgramming;

public static class CoinsCounter
{
    public static int GetNumberOfWays(int cents)
    {
        if (cents < 0)
        {
            throw new InvalidOperationException("Invalid amount of cents");
        }

        int[] coins = { 25, 10, 5, 1 };

        return GetNumberOfWays(cents, coins, 0);
    }

    private static int GetNumberOfWays(int cents, int[] coins, int i)
    {
        if (cents == 0)
        {
            return 1;
        }

        if (i == coins.Length)
        {
            return 0;
        }

        var maxNumberOfCoinsUsed = cents / coins[i];
        if (i == coins.Length - 1)
        {
            return GetNumberOfWays(cents - maxNumberOfCoinsUsed * coins[i], coins, i + 1);
        }

        int result = 0;
        for (int k = 0; k <= maxNumberOfCoinsUsed; ++k)
        {
            result += GetNumberOfWays(cents - k * coins[i], coins, i + 1);
        }

        return result;
    }
}
