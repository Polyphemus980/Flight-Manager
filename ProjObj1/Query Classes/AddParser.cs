namespace PROJOBJ1;

public class AddParser
{
    private string[] args;
    public AddParser(string[] args)
    {
        this.args = args;
    }

    public ParsedQuery ParseQuery() 
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
             if (index == -1 || index + 1 >= keyValues.Length)
                 throw new ArgumentException("Need all properties");
             values.Add(keyValues[index+1]);
         }

         return new ParsedQuery(values, null, null, source);
    }
}