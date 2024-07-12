

namespace PROJOBJ1;

public class DisplayParser 
{
    
    public static ParsedDisplayQuery ParseQuery(string[] args)
    {
            List<string> properties = new List<string>();
            List<string> operators = new List<string>();
            Dictionary<string, List<Expression>> conditions = new Dictionary<string, List<Expression>>();
            int fromIndex = Array.IndexOf(args, "from");
            string source = args[fromIndex + 1];
            if (!QueryUtility.propertyLists.ContainsKey(source))
            {
                throw new Exception($"Incorrect source - no {source} in database");
            }

            if (args[0] == "*")
            {
                properties = QueryUtility.propertyLists[source];
            }
            else
            {
                string[] pr = args[0].Split(',');
                foreach (string prop in pr)
                {
                    properties.Add(prop);
                }
            }

            int conditionIndex = Array.IndexOf(args, "where");
            if (conditionIndex == -1 && args.Length > fromIndex + 2)
                throw new Exception("Incorrect condition synthax - need 'where'");
            if (conditionIndex != -1)
            {
                QueryUtility.ParseConditions(args[(conditionIndex + 1)..], operators, conditions, source);
            }

            if (properties.Contains("WorldPosition"))
            {
                properties.Remove("WorldPosition");
                properties.Add("WorldPosition.Long");
                properties.Add("WorldPosition.Lat");
            }

            return new ParsedDisplayQuery(properties, operators, conditions,source);
    }
}