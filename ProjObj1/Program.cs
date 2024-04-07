
using DynamicData.Aggregation;
using NetworkSourceSimulator;
using PROJOBJ1;
using System.Runtime.CompilerServices;
using System.Text.Json;


public class Program
{
    public static int Main(string[] args)
    {
        string inPath = "example_data.ftr";
        int min = 1, max = 1;
        if (args.Length != 3)
        {
            //Console.WriteLine("Usage: [string] input file path, [int] min message time, [int] max message time");
            //return 1;
        }
        else
        {
            inPath = args[0];
            min = int.Parse(args[1]);
            max = int.Parse(args[2]);
        }

        Runner runner = new Runner(new FTRHandler(inPath));
        runner.Run();
        return 1;
    }
}