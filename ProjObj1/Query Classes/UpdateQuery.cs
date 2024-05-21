namespace PROJOBJ1;

public class UpdateQuery:IQuery
{
    private ParsedQuery parsedQuery { get; set; }
    private List<IEntity> matching { get; set; }
    
    public UpdateQuery(ParsedQuery parsedQuery)
    {
        this.parsedQuery=parsedQuery;
        matching = QueryUtility.GetMatching(parsedQuery);
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