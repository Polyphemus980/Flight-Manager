namespace PROJOBJ1;

public class AddQueryFactory:IQueryFactory
{
    public IQuery CreateInstance(string[] query)
    {
        ParsedAddQuery parsedQuery = AddParser.ParseQuery(query);
        return new AddQuery(parsedQuery);
    }
}