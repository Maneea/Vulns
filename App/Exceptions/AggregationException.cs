namespace Vulns.App;
public class AggregationException : Exception
{
    public AggregationException()
    {
    }

    public AggregationException(string message)
        : base(message)
    {
    }

    public AggregationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}