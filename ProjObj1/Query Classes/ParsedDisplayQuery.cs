namespace PROJOBJ1;

public class DisplayParsedQuery
{
    public List<string> properties;
    public List<string> operators;
    public Dictionary<string, List<Expression>> conditions;
    public string source;

    public DisplayParsedQuery(List<string> pr, List<string> op, Dictionary<string, List<Expression>> cond,string s)
    {
        properties = pr;
        operators = op;
        conditions = cond;
        source = s;
    }
}