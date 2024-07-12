namespace PROJOBJ1;

public class AddParser
{
    
    public static ParsedAddQuery ParseQuery(string[] args) 
    {
        string source = args[0];
        int newIndex = Array.IndexOf(args,"new");
        if (newIndex == -1)
            throw new Exception();
        string[] keyValues = args[newIndex + 1].Trim('{', '(','}',')').Split(['=',',']);
        List<string> values = new List<string>();
        foreach (string property in QueryUtility.propertyLists[source])
        { 
            int index = Array.IndexOf(keyValues, property);
            values.Add(keyValues[index+1]);
        }
        if (values.Count != QueryUtility.propertyLists[source].Count)
             throw new ArgumentException("Need all properties");

         return new ParsedAddQuery(values, source);
    }
}