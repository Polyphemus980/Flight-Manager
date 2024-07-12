namespace PROJOBJ1;

public class DisplayQuery:IQuery
{
    public ParsedDisplayQuery Query { get; set; }
    public List<IEntity> matching { get; set; }

    public DisplayQuery(ParsedDisplayQuery query)
    {
        this.Query=query;
        matching = QueryUtility.GetMatching(query.source,query.conditions,query.operators);
    }
    public void Execute()
    {
        var maxWidths = new Dictionary<string, int>();
        
        foreach (var property in Query.properties)
        {
            maxWidths[property] = property.Length;
        }

        foreach (var entry in matching)
        {
            foreach (var property in Query.properties)
            {
                if (!(entry.values[property]() is null))
                {
                    maxWidths[property] = Math.Max(maxWidths[property], entry.values[property]().ToString().Length);
                }
                else
                {
                    maxWidths[property] = Math.Max(maxWidths[property], "Null".Length);
                }
            }
        }
        foreach (var property in Query.properties)
        {
            Console.Write(property.PadRight(maxWidths[property]) + " | ");
        }
        Console.WriteLine();

        foreach (var property in Query.properties)
        {
            Console.Write(new string('-', maxWidths[property]) + " + ");
        }
        Console.WriteLine();

        foreach (var entry in matching)
        {
            foreach (var property in Query.properties)
            {
                Console.Write(entry.values[property]().ToString().PadLeft(maxWidths[property]) + " | ");
            }
            Console.WriteLine();
        }
    }
    
    

  
}
