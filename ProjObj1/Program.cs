
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


        //ServerHandler Server = new ServerHandler(inPath, min, max);
        //Server.StartServer();
        ////while ((Command = Console.ReadLine()) != null)
        ////{
        ////    if (Command == "exit")
        ////    {
        ////        break;
        ////    }
        ////    if (Command == "print")
        ////    {
        ////        Server.MakeSnapshot();
        ////    }
        ////}
        ////return 1;

        //FTRHandler handler = new FTRHandler();
        //handler.LoadObjects("example_data.ftr");
        //Dictionary<ulong,Airport> airports;
        //List<Flight> flights;
        //(airports, flights) = (handler.visitor.airports, handler.visitor.flights);

        //FlightMG flightManager = new FlightMG(flights, airports);

        //Task.Run(() => FlightTrackerGUI.Runner.Run());
        //while (true)
        //{
        //    flightManager.UpdateFlights();
        //    FlightTrackerGUI.Runner.UpdateGUI(flightManager.flightData);
        //    Thread.Sleep(1000);
        //}

        Runner runner = new Runner(new ServerHandler(inPath, 1, 2));
        runner.Run();
        return 1;
    }
}