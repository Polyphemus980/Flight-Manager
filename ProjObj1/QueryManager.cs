using DynamicData;

namespace PROJOBJ1;

public class QueryManager
{
    
    public IQuery concreteQuery;
    public QueryManager(string[] args)
    {
        string[] parsingArgs = args[1..args.Length];
        switch (args[0])
        {
            case "display":
                concreteQuery = new DisplayQuery(parsingArgs);
                break;
        }
        
    }
}