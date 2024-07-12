

namespace PROJOBJ1;

public class AddQuery:IQuery
{
    private ParsedAddQuery parsedQuery { get; set; }
    
    public AddQuery(ParsedAddQuery parsedQuery)
    {
        this.parsedQuery=parsedQuery;
    }

    public void Execute()
    {
        IEntity addedObject =
            QueryUtility.factories[parsedQuery.source].CreateInstance(parsedQuery.values.ToArray());
        addedObject.addToDatabase();
    }
}