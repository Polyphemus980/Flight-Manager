namespace PROJOBJ1;

public class DisplayQuery:IQuery
{
    public ParsedQuery parsedQuery { get; set; }
    public List<IEntity> matching { get; set; }

    public DisplayQuery(ParsedQuery parsedQuery)
    {
        this.parsedQuery=parsedQuery;
        matching = QueryUtility.GetMatching(parsedQuery);
    }
    public void Execute()
    {
        var maxWidths = new Dictionary<string, int>();
        
        foreach (var property in parsedQuery.properties)
        {
            maxWidths[property] = property.Length;
        }

        foreach (var entry in matching)
        {
            foreach (var property in parsedQuery.properties)
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
        foreach (var property in parsedQuery.properties)
        {
            Console.Write(property.PadRight(maxWidths[property]) + " | ");
        }
        Console.WriteLine();

        foreach (var property in parsedQuery.properties)
        {
            Console.Write(new string('-', maxWidths[property]) + " + ");
        }
        Console.WriteLine();

        foreach (var entry in matching)
        {
            foreach (var property in parsedQuery.properties)
            {
                if (entry.values[property]() is null)
                {
                    Console.Write("Null".PadLeft(maxWidths[property]) + " | ");
                    continue;
                }
                Console.Write(entry.values[property]().ToString().PadLeft(maxWidths[property]) + " | ");
            }
            Console.WriteLine();
        }
    }
    
    

  
}
