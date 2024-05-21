namespace PROJOBJ1;

public class DeleteQuery : IQuery
{
    private ParsedQuery parsedQuery { get; set; }
    private List<IEntity> matching { get; set; }

    public DeleteQuery(ParsedQuery parsedQuery)
    {
        this.parsedQuery = parsedQuery;
        matching = QueryUtility.GetMatching(parsedQuery);
    }

    public void Execute()
    {
        foreach (IEntity deleted in matching)
        {
            Database.deleteFunctions[parsedQuery.source](deleted.ID);
        }
    }
}