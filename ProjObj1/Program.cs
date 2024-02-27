
using PROJOBJ1;
using System.Text.Json;


public class Program
{
    public static int Main(string[] args)
    {
        string InPath = "";
        string OutPath = "";
        if (args.Length != 2)
        {
            Console.WriteLine("Argumenty: [string] input file path, [string] output file path");
            return 1;
        }
        else
        {
            InPath= args[0];
            OutPath= args[1];
        }

        DataHandler DataHandler=new DataHandler();
        List<IEntity> list=DataHandler.LoadObjects(InPath);
        DataHandler.SerializeObjects(list, OutPath);
        return 0;
        
    }
}