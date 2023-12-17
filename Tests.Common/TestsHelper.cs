namespace Tests.Common;

public static class TestsHelper
{
    public static CancellationToken CreateCancellationToken(int delayMs)
    {
        return new CancellationTokenSource(delayMs).Token;
    }
}
