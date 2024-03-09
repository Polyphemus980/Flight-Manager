
using NetworkSourceSimulator;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;

using System.Text.Json.Serialization;


namespace PROJOBJ1
{
    public class DataHandler
    {
        private List<IEntity> objects = new List<IEntity>();
        public string in_path { get; set; }
        public string out_path { get; set; }
        public void EventHandler(object sender, NewDataReadyArgs args)
        {

        }
        private readonly Dictionary<string, IFactory> Factories = new Dictionary<string, IFactory>();

        public DataHandler(string in_path, string out_path)
        {
            this.in_path = in_path;
            this.out_path = out_path;
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
            objects = new List<IEntity>();
        }

        public DataHandler(Dictionary<string, IFactory> factories, string in_path, string out_path)
        {
            this.in_path = in_path;
            this.out_path = out_path;
            objects = new List<IEntity>();
            Factories = factories;
        }

        public void LoadObjects()
        {
            List<string[]> propertiesList = ParseFromFile();
            foreach (string[] properties in propertiesList)
            {
                string name = properties[0];
                IEntity objectInstance = Factories[name].CreateInstance(properties[1..properties.Length]);
                objects.Add(objectInstance);
            }
        }

        private List<string> SerializeObjects()
        {
            List<string> serializedObjects = new List<string>();
            foreach (IEntity objectInstance in objects)
            {
                serializedObjects.Add(JsonSerializer.Serialize<IEntity>(objectInstance));
            }
            return serializedObjects;
        }
        public void SaveToPath()
        {
            List<string> serializedObjects = SerializeObjects();
            using (StreamWriter writer = new StreamWriter(out_path))
            {
                foreach (string objectInstance in serializedObjects)
                {
                    writer.WriteLine(objectInstance);
                }
            }
        }


        public List<string[]> ParseFromFile()
        {
                List<string[]> parsedLines = new List<string[]>();
                List<string> lines = ReadFromFile();
                foreach (string line in lines)
                {
                    parsedLines.Add(line.Split(','));
                }
                return parsedLines;
        }

        private List<string> ReadFromFile()
        {
               List<string> lineList = new List<string>();
                if (!File.Exists(in_path))
                {
                    return lineList;
                }
                using (StreamReader lineReader = new StreamReader(in_path))
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
