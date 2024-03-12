
using NetworkSourceSimulator;
using PROJOBJ1;
using System.Runtime.CompilerServices;
using System.Text.Json;


public class Program
{
    public static int Main(string[] args)
    {
        string inPath = "example_data.ftr";
        string? Command;
        int min=1, max=1;
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: [string] input file path, [int] min message time, [int] max message time");
            return 1;
        }
        else
        {
            inPath = args[0];
            min = int.Parse(args[1]);
            max = int.Parse(args[2]);
        }

        Server Server = new Server(inPath, min, max);
        Server.StartServer();
        while ((Command = Console.ReadLine()) != null)
        {
            if (Command == "exit")
            {
                break;
            }
            if (Command == "print")
            {
                Server.MakeSnapshot();
            }
        }
        return 0;
    }
}