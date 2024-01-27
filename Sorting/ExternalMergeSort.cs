namespace Sorting;

public interface IExternalSort
{
    void Apply(string filePath);
}

public class ExternalMergeSort : IExternalSort
{
    public void Apply(string filePath)
    {
        // throw new NotImplementedException();
        using var sr = new StreamReader(filePath);
        sr.Read();

    }
}
