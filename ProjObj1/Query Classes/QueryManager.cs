using DynamicData;

namespace PROJOBJ1;

public class QueryManager
{
    private Dictionary<string, IQueryFactory> queryFactories = new Dictionary<string, IQueryFactory>
    {
        { "display", new DisplayQueryFactory() },
        { "add" , new AddQueryFactory()},
        { "update",new UpdateQueryFactory()},
        { "delete", new DeleteQueryFactory()}
    };
    public IQuery query;
    public QueryManager(string[] args)
    {
        string source = args[0];
        string[] parsingArgs = args[1..args.Length];
        try
        {
            query = queryFactories[source].CreateInstance(parsingArgs);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void ExecuteQuery()
    {
        try
        {
            query.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}