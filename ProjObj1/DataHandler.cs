
using System.Globalization;
using System.Text.Json;

using System.Text.Json.Serialization;


namespace PROJOBJ1
{
    public class DataHandler
    {
        private readonly Dictionary<string, IFactory> Factories = new Dictionary<string, IFactory>();
        public DataHandler()
        {
            Factories = new Dictionary<string, IFactory>
            {
                { "CA", new CargoFactory() },
                { "C", new CrewFactory() },
                { "P", new PassengerFactory() },
                { "CP", new CargoPlaneFactory() },
                { "PP", new PassengerPlaneFactory() },
                { "AI", new AirportFactory() },
                { "FL", new FlightFactory() }
            };
        }
        public DataHandler(Dictionary<string,IFactory> factories)
        {
            Factories = factories;
        }
        
        public List<IEntity> LoadObjects(string path)
        {
            List < IEntity > list= new List<IEntity>();
            List<string[]> objects = ParseFromFile(path);
            foreach (string[] obj in objects)
            {
                string name = obj[0];
                IEntity prod =  Factories[name].createClass(obj[1..obj.Length]);
                list.Add(prod);
            }              
            return list;
        }
        public static void SerializeObjects(List<IEntity> list, string savepath) 
        {
            using (StreamWriter writer = new StreamWriter(savepath))
            {
                foreach (IEntity prod in list)
                {
                    writer.WriteLine(JsonSerializer.Serialize<IEntity>(prod));
                }
            }
        }

        public static List<string[]> ParseFromFile(string path)
        {
            List<string[]> list = new List<string[]>();
            if (!File.Exists(path))
            {
                return list; 
            }
            using (StreamReader linereader = new StreamReader(path))
            {
                while(! linereader.EndOfStream)
                {
                    string[] line = linereader.ReadLine().Split(',');
                    list.Add(line);
                }    
            }
            return list;
            

        }
    }
}
