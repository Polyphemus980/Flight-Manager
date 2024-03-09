
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
        DataHandler dataHandler = new DataHandler();
        string command;
        string serializePath;
        NetworkSourceSimulator.NetworkSourceSimulator s = new NetworkSourceSimulator.NetworkSourceSimulator(inPath, 1, 2);
        Task.Run(s.Run);
        s.OnNewDataReady += dataHandler.EventHandler;
        while ((command=Console.ReadLine())!= "exit")
        {
            if (command=="print")
            {
                DateTime t= DateTime.Now;
                serializePath = "snapshot_" + t.Hour + "_" + t.Minute + "_" + t.Second + ".json";
                lock (dataHandler.objects)
                {
                    dataHandler.SaveToPath(serializePath);
                }
                serializePath = "";
            }
        }
        return 0;

    }
}