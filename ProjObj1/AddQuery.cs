

namespace PROJOBJ1;

public class AddQuery:IQuery
{
    private ParsedQuery parsedQuery { get; set; }
    
    public AddQuery(ParsedQuery parsedQuery)
    {
        this.parsedQuery=parsedQuery;
    }

    public void Execute()
    {
        IEntity addedObject =
            QueryUtility.factories[parsedQuery.source].CreateInstance(parsedQuery.properties.ToArray());
        addedObject.addToDatabase();
    }
}