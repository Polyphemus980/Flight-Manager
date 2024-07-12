namespace PROJOBJ1;

public class UpdateQueryFactory:IQueryFactory
{
    public IQuery CreateInstance(string[] query)
    {
        ParsedUpdateQuery parsedQuery = UpdateParser.ParseQuery(query);
        return new UpdateQuery(parsedQuery);
    }
}