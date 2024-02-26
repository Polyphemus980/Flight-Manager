

using PROJOBJ1;
using System.Text.Json;


public class Program
{
    public static int Main(string[] args)
    {
        
        string InPath = "example_data.ftr";
        string OutPath = "kokskokskoks.json";
        if (args.Length == 0)
        {
            Console.WriteLine("Argumenty: [string] input file path, [string] output file path");
            
        }
        else
        {
            InPath= args[0];
            OutPath= args[1];
        }
        //Dictionary<string,Factory> dic = new Dictionary<string, Factory>();
        //dic.Add("CA", new CargoFactory());
        //dic.Add("C", new CrewFactory());
        //dic.Add("P", new PassengerFactory());
        //dic.Add("CP", new CargoPlaneFactory());
        //dic.Add("PP", new PassengerPlaneFactory());
        //dic.Add("AI", new AirportFactory());
        //dic.Add("FL",new FlightFactory());
        LoadUtil util=new LoadUtil();
        List<AbstractProduct> list=util.LoadObjects(InPath);
        LoadUtil.SerializeList(list, OutPath);
        return 0;
        
    }
}