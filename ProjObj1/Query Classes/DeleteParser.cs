namespace PROJOBJ1;

public class DeleteParser
{
    public static ParsedDeleteQuery ParseQuery(string[] args)
    {
        List<string> operators = new List<string>();
        Dictionary<string, List<Expression>> conditions = new Dictionary<string, List<Expression>>();
        string source = args[0];
        int conditionIndex = Array.IndexOf(args, "where");
        if (conditionIndex != -1 && conditionIndex + 3 < args.Length)
        {
            QueryUtility.ParseConditions(args[(conditionIndex + 1)..],operators,conditions,source);
        }
        return new ParsedDeleteQuery(operators, conditions, source);
    }
}