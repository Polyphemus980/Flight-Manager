namespace PROJOBJ1;

public class DeleteQuery : IQuery
{
    private ParsedDeleteQuery parsedQuery { get; set; }
    private List<IEntity> matching { get; set; }

    public DeleteQuery(ParsedDeleteQuery parsedQuery)
    {
        this.parsedQuery = parsedQuery;
        matching = QueryUtility.GetMatching(parsedQuery.source,parsedQuery.conditions,parsedQuery.operators);
    }

    public void Execute()
    {
        foreach (IEntity deleted in matching)
        {
            Database.deleteFunctions[parsedQuery.source](deleted.ID);
        }
    }
}