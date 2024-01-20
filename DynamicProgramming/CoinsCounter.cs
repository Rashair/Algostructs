namespace DynamicProgramming;

public static class CoinsCounter
{
    public static int GetNumberOfWays(int cents)
    {
        if (cents < 0)
        {
            throw new InvalidOperationException("Invalid amount of cents");
        }

        int[] coins = [25, 10, 5, 1];

        return GetNumberOfWaysIterative(cents, coins);
    }

    private static int GetNumberOfWaysRecursive(int cents, int[] coins)
    {
        int[,] memoizedResults = new int[coins.Length, cents + 1];
        memoizedResults[0, 0] = 1;
        return GetNumberOfWays(memoizedResults, cents, coins, 0);
    }

    private static int GetNumberOfWays(int[,] memoizedResults, int cents, int[] coins, int i)
    {
        if (cents == 0)
        {
            return 1;
        }

        if (i == coins.Length)
        {
            return 0;
        }

        if (memoizedResults[i, cents] > 0)
        {
            return memoizedResults[i, cents];
        }

        var maxNumberOfCoinsUsed = cents / coins[i];
        if (i == coins.Length - 1)
        {
            return GetNumberOfWays(memoizedResults, cents - maxNumberOfCoinsUsed * coins[i], coins, i + 1);
        }

        int result = 0;
        for (int k = 0; k <= maxNumberOfCoinsUsed; ++k)
        {
            result += GetNumberOfWays(memoizedResults, cents - k * coins[i], coins, i + 1);
        }

        memoizedResults[i, cents] = result;

        return result;
    }

    private static int GetNumberOfWaysIterative(int cents, int[] coins)
    {
        var ways = new int[cents + 1];
        ways[0] = 1;

        foreach (var coin in coins)
        {
            for (int i = coin; i <= cents; ++i)
            {
                ways[i] += ways[i - coin];
            }
        }

        return ways[cents];
    }
}
