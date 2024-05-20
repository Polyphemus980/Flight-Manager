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
            {
                DisplayParser parser = new DisplayParser(args[1..]);
                try
                {
                    ParsedQuery pQ = parser.ParseQuery();
                    concreteQuery = new DisplayQuery(pQ);
                    concreteQuery.Execute();
                }
                catch (Exception ex)
                { 
                    Console.WriteLine(ex.Message);
                }
                break;
            }
            case "delete":
            {
                DeleteParser parser = new DeleteParser(args[1..]);
                try
                {
                    ParsedQuery pQ = parser.ParseQuery();
                    concreteQuery = new DeleteQuery(pQ);
                    concreteQuery.Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            }
            case "add":
            {
                AddParser parser = new AddParser(args[1..]);
                try
                {
                    ParsedQuery pQ = parser.ParseQuery();
                    concreteQuery = new AddQuery(pQ);
                    concreteQuery.Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            }
        }
        
    }
}