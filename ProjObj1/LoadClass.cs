
using System.Globalization;
using System.Text.Json;

using System.Text.Json.Serialization;


namespace PROJOBJ1
{
    public interface IFactory
    {
        IEntity createClass(string[] list);
    }

    [JsonDerivedType(typeof(Airport),typeDiscriminator:nameof(Airport))]
    [JsonDerivedType(typeof(Cargo),typeDiscriminator:nameof(Cargo))]
    [JsonDerivedType(typeof(Flight),typeDiscriminator:nameof(Flight))]
    [JsonDerivedType(typeof(Crew),typeDiscriminator:nameof(Crew))]
    [JsonDerivedType(typeof(CargoPlane), typeDiscriminator: nameof(CargoPlane))]
    [JsonDerivedType(typeof(PassengerPlane), typeDiscriminator: nameof(PassengerPlane))]
    [JsonDerivedType(typeof(Passenger), typeDiscriminator: nameof(Passenger))]
    public interface IEntity
    {
        public UInt64 ID { get; set; }
    }

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
            if (!File.Exists(path))
            {
                return null;
            }
            List<string[]> list= new List<string[]>();
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
