
using System.Globalization;
using System.Runtime.CompilerServices;
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

        private static List<string> SerializeObjects(List<IEntity> objects) 
        {
            List<string> serializedObjects=new List<string>();
            foreach(IEntity objectInstance in objects)
            {
                serializedObjects.Add(JsonSerializer.Serialize<IEntity>(objectInstance));
            }
            return serializedObjects;
        }
        public static void SaveToPath(string path,List<IEntity> objects)
        {
            List<string> serializedObjects = SerializeObjects(objects);
            Console.WriteLine(serializedObjects.Count());
            using (StreamWriter writer = new StreamWriter(path)) 
            {
                foreach(string objectInstance in serializedObjects)
                {
                    writer.WriteLine(objectInstance);
                }
            }
        }

        
        public static List<string[]> ParseFromFile(string path)
        {
            List<string[]> parsedLines = new List<string[]>();
            List<string> lines = ReadFromFile(path);
            foreach (string line in lines)
            {
                parsedLines.Add(line.Split(','));
            }
            return parsedLines;
        }
        private static List<string> ReadFromFile(string path)
        {
            List<string> lineList = new List<string>();
            if (!File.Exists(path))
            {
                return lineList;
            }
            using (StreamReader lineReader = new StreamReader(path))
            {
                while (!lineReader.EndOfStream)
                {
                    string line = lineReader.ReadLine();
                    lineList.Add(line);
                }
            }
            return lineList;
        }

    }
}
