namespace PROJOBJ1;

public class ParsedDisplayQuery
{
    public List<string> properties;
    public List<string> operators;
    public Dictionary<string, List<Expression>> conditions;
    public string source;

    public ParsedDisplayQuery(List<string> pr, List<string> op, Dictionary<string, List<Expression>> cond,string s)
    {
        properties = pr;
        operators = op;
        conditions = cond;
        source = s;
    }
}