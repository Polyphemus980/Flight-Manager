
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
            Console.WriteLine("Usage: [string] input file path, [int] min message time, [int] max message time");
            //return 1;
        }
        else
        {
            inPath = args[0];
            min = int.Parse(args[1]);
            max = int.Parse(args[2]);
        }

        Runner runner = new Runner(new FTRHandler(inPath));//new ServerHandler(inPath, 1, 2));
        runner.Run();
        return 1;
        //FTRHandler handler = new FTRHandler(inPath);
        //handler.Start();
        //List<IReporter> reporters = new List<IReporter>
        //{
        //    new Television("Telewizja Abelowa"),
        //    new Television("Kanał TV-tensor"),
        //    new Radio("Radio Kwantyfikator"),
        //    new Radio("Radio Shmem"),
        //    new Newspaper("Gazeta Kategoryczna"),
        //    new Newspaper("Dziennik Politechniczny")
        //};
        //List<IReportable> subjects = new List<IReportable>();
        //foreach (var Airport in Database.airports)
        //{
        //    subjects.Add(Airport.Value);
        //}
        //foreach (var cargoPlane in Database.cargoPlanes)
        //{
        //    subjects.Add(cargoPlane);
        //}
        //foreach (var passengerPlane in Database.passengerPlanes)
        //{
        //    subjects.Add(passengerPlane);
        //}
        //NewsGenerator generator = new NewsGenerator(reporters, subjects);
        //foreach (string s in generator.GenerateNextNews())
        //{
        //    Console.WriteLine(s);
        //}
        //return 0;
    }
}