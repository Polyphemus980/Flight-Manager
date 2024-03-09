
using NetworkSourceSimulator;
using PROJOBJ1;
using System.Runtime.CompilerServices;
using System.Text.Json;


public class Program
{
    public static int Main(string[] args)
    {
        string inPath = "example_data.ftr";
        string outPath = "kokoksdafdsadsasf.json";
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: [string] input file path, [string] output file path");
            //return 1;
        }
        else
        {
            inPath= args[0];
            outPath= args[1];
        }
        DataHandler dataHandler = new DataHandler(inPath,outPath);
        dataHandler.LoadObjects();
        dataHandler.SaveToPath();
        return 0;

    }
}