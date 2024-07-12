namespace PROJOBJ1;

public class UpdateQuery:IQuery
{
    private ParsedUpdateQuery parsedQuery { get; set; }
    private List<IEntity> matching { get; set; }
    
    public UpdateQuery(ParsedUpdateQuery parsedQuery)
    {
        this.parsedQuery=parsedQuery;
        matching = QueryUtility.GetMatching(parsedQuery.source,parsedQuery.conditions,parsedQuery.operators);
    }

    public void Execute()
    {

            foreach (IEntity entry in matching)
            {
                foreach (string property in parsedQuery.propertyValues.Keys)
                {
                    Database.updateFunctions[parsedQuery.source][property](entry.ID,
                        parsedQuery.propertyValues[property]);
                }
            }
            
    }
}