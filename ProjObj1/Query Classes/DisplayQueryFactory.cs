namespace PROJOBJ1;

public class DisplayQueryFactory:IQueryFactory
{
    public IQuery CreateInstance(string[] query)
    {
        ParsedDisplayQuery parsedQuery = DisplayParser.ParseQuery(query);
        return new DisplayQuery(parsedQuery);
    }
}