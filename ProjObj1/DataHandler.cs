
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
            List < IEntity > objects = new List<IEntity>();
            List<string[]> propertiesList = ParseFromFile(path);
            foreach (string[] properties in propertiesList)
            {
                string name = properties[0];
                IEntity objectInstance =  Factories[name].CreateInstance(properties[1..properties.Length]);
                objects.Add(objectInstance);
            }              
            return objects;
        }

        public static void SerializeObjects(List<IEntity> objects, string savepath) 
        {
            using (StreamWriter writer = new StreamWriter(savepath))
            {
                foreach (IEntity objectInstance in objects)
                {
                    writer.WriteLine(JsonSerializer.Serialize<IEntity>(objectInstance));
                }
            }
        }
        
        public static List<string[]> ParseFromFile(string path)
        {
            List<string[]> lineList = new List<string[]>();
            if (!File.Exists(path))
            {
                return lineList; 
            }
            using (StreamReader lineReader = new StreamReader(path))
            {
                while(!lineReader.EndOfStream)
                {
                    string[] line = lineReader.ReadLine().Split(',');
                    lineList.Add(line);
                }    
            }
            return lineList;
        }

    }
}
