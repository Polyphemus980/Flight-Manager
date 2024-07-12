using System.Runtime.Intrinsics.X86;

namespace PROJOBJ1;

public class UpdateParser
{
    public static ParsedUpdateQuery ParseQuery(string[] args) 
    {
        List<string> operators = new List<string>();
        Dictionary<string, List<Expression>> conditions = new Dictionary<string, List<Expression>>();
        string source = args[0];
        Dictionary<string, IComparable> propertyValues=new Dictionary<string, IComparable>();
        int setIndex = Array.IndexOf(args,"set");
        if (setIndex == -1)
            throw new Exception("Incorrect synthax - need 'set' ");
        string[] keyValues = args[setIndex + 1].Trim('{', '(','}',')').Split(['=',',']);
        List<string> values = new List<string>();
        for (int i = 0; i < keyValues.Length; i += 2)
        {
            propertyValues.Add(keyValues[i], QueryUtility.objectParsers[source][keyValues[i]](keyValues[i + 1]));
        }
        int conditionIndex = Array.IndexOf(args, "where");
        if (conditionIndex != -1)
        {
            QueryUtility.ParseConditions(args[(conditionIndex + 1)..],operators,conditions,source);
        }

        return new ParsedUpdateQuery(propertyValues, operators, conditions, source);

    }
}