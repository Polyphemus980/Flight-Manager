using System.Runtime.InteropServices.JavaScript;

namespace PROJOBJ1;

public class DisplayParser:IQueryParser
{
    public List<string> properties { get; set; }
    public List<string> operators { get; set; } 
    public Dictionary<string,List<Expression>> conditions { get; set; }
    public string source { get; set; }
    public DisplayParser(string[] args)
    {
        properties = new List<string>();
        operators = new List<string>();
        conditions = new Dictionary<string, List<Expression>>();
        int fromIndex = Array.IndexOf(args, "from");
        source = args[fromIndex+ 1];
        if (!ParsingUtility.propertyLists.ContainsKey(source))
        {
            throw new Exception($"Incorrect source - no {source} in database");
        }
        if (args[0] == "*")
        {
            properties = ParsingUtility.propertyLists[source];
        }
        else
        {
            for (int i = 0; i < fromIndex; i++)
            {
                properties.Add(args[i]);
            }
        }

        int conditionIndex=Array.IndexOf(args, "where");
        if (conditionIndex != -1 && conditionIndex+2 < args.Length)
        {
            for (int i = conditionIndex+1; i < args.Length; i+= 4)
            {
                string property = args[i];
                string operator_ = args[i + 1];
                string val = args[i + 2];
                IComparable value = ParsingUtility.objectParsers[source][property](val);
                Expression exp = new Expression(operator_, value);
                if (!conditions.ContainsKey(property))
                {
                    List<Expression> pexp = new List<Expression>();
                    pexp.Add(exp);
                    conditions.Add(property,pexp);
                }
                else
                {
                 conditions[property].Add(exp);   
                }
                if (args.Length == i + 3)
                    break;
                else
                    operators.Add(args[i+3]);
            }
        }
        
    }
}