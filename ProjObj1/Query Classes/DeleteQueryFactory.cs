namespace PROJOBJ1;

public class DeleteQueryFactory:IQueryFactory
{
    public IQuery CreateInstance(string[] query)
    {
        ParsedDeleteQuery parsedQuery = DeleteParser.ParseQuery(query);
        return new DeleteQuery(parsedQuery);
    }
}