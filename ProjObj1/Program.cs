
using PROJOBJ1;
using System.Text.Json;


public class Program
{
    public static int Main(string[] args)
    {
        string InPath = "example_data.ftr";
        string OutPath = "koks11.json";
        if (args.Length != 2)
        {
            Console.WriteLine("Argumenty: [string] input file path, [string] output file path");
            //return 1;
        }
        else
        {
            InPath= args[0];
            OutPath= args[1];
        }

        DataHandler DataHandler=new DataHandler();
        List<IEntity> list=DataHandler.LoadObjects(InPath);
        DataHandler.SerializeObjects(list, OutPath);
        List<IEntity> l=new List<IEntity>();
        using (StreamReader s = new StreamReader(OutPath))
        {
            while (!s.EndOfStream) 
            {
                string sLine = s.ReadLine();
                IEntity m=JsonSerializer.Deserialize<IEntity>(sLine);
                l.Add(m);
            }
        }
        Console.WriteLine(l.Count);
                
            return 0;
        
    }
}