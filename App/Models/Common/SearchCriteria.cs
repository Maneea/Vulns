namespace Vulns.App;

public abstract class SearchCriteria
{
    public int Page { get; set; } = 1;

    public int Size { get; set; } = 10;

    public int Offset { get => (Page - 1) * Size; }

    public String? Phrase { get; set; }
}