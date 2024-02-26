
using System.Globalization;
using System.Text.Json;

using System.Text.Json.Serialization;


namespace PROJOBJ1
{
    public interface Factory
    {
        AbstractProduct createClass(string[] list);
    }

    [JsonDerivedType(typeof(Airport),typeDiscriminator:nameof(Airport))]
    [JsonDerivedType(typeof(Cargo),typeDiscriminator:nameof(Cargo))]
    [JsonDerivedType(typeof(Flight),typeDiscriminator:nameof(Flight))]
    [JsonDerivedType(typeof(Crew),typeDiscriminator:nameof(Crew))]
    [JsonDerivedType(typeof(CargoPlane), typeDiscriminator: nameof(CargoPlane))]
    [JsonDerivedType(typeof(PassengerPlane), typeDiscriminator: nameof(PassengerPlane))]
    [JsonDerivedType(typeof(Passenger), typeDiscriminator: nameof(Passenger))]
    public interface AbstractProduct
    {
        public UInt64 ID { get; set; }
    }

    public class LoadUtil
    {
        private Dictionary<string, Factory> Factories = new Dictionary<string, Factory>();
        public LoadUtil()
        {
            Factories=new Dictionary<string,Factory>();
            Factories.Add("CA", new CargoFactory());
            Factories.Add("C", new CrewFactory());
            Factories.Add("P", new PassengerFactory());
            Factories.Add("CP", new CargoPlaneFactory());
            Factories.Add("PP", new PassengerPlaneFactory());
            Factories.Add("AI", new AirportFactory());
            Factories.Add("FL", new FlightFactory());
        }
        public LoadUtil(Dictionary<string, Factory> factories)
        {
            foreach (var fact in factories)
            {
                Factories.Add(fact.Key, fact.Value);
            }
        }
        //Dictionary<string, Factory> FactoryDict
        public  List<AbstractProduct> LoadObjects(string path)
        {
            List < AbstractProduct > list= new List<AbstractProduct>();
            List<string[]> objects = ParseFromFile(path);
            if (objects==null)
            {
                return null;
            }
            foreach (string[] obj in objects)
            {
                string name = obj[0];
                AbstractProduct prod =  Factories[name].createClass(obj[1..obj.Length]);
                list.Add(prod);
            }              
            return list;
        }
        public static void SerializeList(List<AbstractProduct> list, string savepath) 
        {
            using (StreamWriter writer = new StreamWriter(savepath))
            {
                foreach (AbstractProduct prod in list)
                {
                    //writer.WriteLine(JsonSerializer.Serialize(prod, typeof(Object)));
                    writer.WriteLine(JsonSerializer.Serialize<AbstractProduct>(prod));
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
