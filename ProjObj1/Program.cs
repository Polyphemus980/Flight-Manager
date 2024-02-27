

using PROJOBJ1;
using System.Text.Json;


public class Program
{
    public static int Main(string[] args)
    {
        
        string InPath = "example_data.ftr";
        string OutPath = "kokskokskoks.json";
        if (args.Length != 2)
        {
            Console.WriteLine("Argumenty: [string] input file path, [string] output file path");
            
        }
        else
        {
            InPath= args[0];
            OutPath= args[1];
        }
        DataHandler util=new DataHandler();
        List<IEntity> list=util.LoadObjects(InPath);
        DataHandler.SerializeObjects(list, OutPath);
        return 0;
        
    }
}