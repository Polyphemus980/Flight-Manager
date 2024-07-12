namespace PROJOBJ1;

public class ParsedDeleteQuery
{
    public List<string> operators;
    public Dictionary<string, List<Expression>> conditions;
    public string source;

    public ParsedDeleteQuery(List<string> op,Dictionary<string, List<Expression>> cond,string src)
    {
        operators = op;
        conditions = cond;
        source = src;
    }
}