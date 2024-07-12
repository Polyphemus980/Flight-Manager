namespace PROJOBJ1;

public class ParsedAddQuery
{
    public List<string> values;
    public string source;

    public ParsedAddQuery(List<string> vals, string src)
    {
        values = vals;
        source = src;
    }
}