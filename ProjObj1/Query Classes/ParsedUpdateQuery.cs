namespace PROJOBJ1;

public class ParsedUpdateQuery
{
    public Dictionary<string, IComparable> propertyValues;
    public List<string> operators;
    public Dictionary<string, List<Expression>> conditions;
    public string source;

    public ParsedUpdateQuery(Dictionary<string, IComparable> prVals, List<string> op,
        Dictionary<string, List<Expression>> cond, string s)
    {
        propertyValues = prVals;
        operators = op;
        conditions = cond;
        source = s;
    }
}