
using PROJOBJ1;
using System.Text.Json;


public class Program
{
    public static int Main(string[] args)
    {
        string inPath = "";
        string outPath = "";
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: [string] input file path, [string] output file path");
            return 1;
        }
        else
        {
            inPath= args[0];
            outPath= args[1];
        }
        DataHandler DataHandler=new DataHandler();
        List<IEntity> list=DataHandler.LoadObjects(inPath);
        DataHandler.SaveToPath(outPath,list);
        return 0;
    }
}