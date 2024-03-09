
using NetworkSourceSimulator;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

using System.Text.Json.Serialization;


namespace PROJOBJ1
{
    public class DataHandler
    {
        public List<IEntity> objects = new List<IEntity>();
        
        private readonly Dictionary<string, IFactory> Factories = new Dictionary<string, IFactory>();

        public void EventHandler(object sender, NewDataReadyArgs args)
        {
            Int32 length;
            string nm;
            byte[] restofmessage;
            NetworkSourceSimulator.NetworkSourceSimulator server = sender as NetworkSourceSimulator.NetworkSourceSimulator;
            Message msg=server.GetMessageAt(args.MessageIndex);
            using (MemoryStream memoryStream = new MemoryStream(msg.MessageBytes))
            {
                using (BinaryReader read=new BinaryReader(memoryStream))
                {
                    byte[] s = read.ReadBytes(3);
                    string name=Encoding.UTF8.GetString(s);
                    nm = CodeParser(name);
                    length = read.ReadInt32();
                    restofmessage = read.ReadBytes(length);
                }
            }
            lock (objects)
            {
                objects.Add(Factories[nm].CreateInstance(restofmessage));
            }
            return;

        }
        public static string CodeParser(string s)
        {
            switch (s)
            {
                case "NCR":return "C";
                case "NPA":return "P";
                case "NCA":return "CA";
                case "NCP":return "CP";
                case "NPP":return "PP";
                case "NAI":return "AI";
                case "NFL":return "FL";
                default: return "";
            }
        }

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
            objects = new List<IEntity>();
        }

        public DataHandler(Dictionary<string, IFactory> factories)
        {

            objects = new List<IEntity>();
            Factories = factories;
        }

        public void LoadObjects(string path)
        {
            List<string[]> propertiesList = ParseFromFile(path);
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
        public void SaveToPath(string path)
        {
            List<string> serializedObjects = SerializeObjects();
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string objectInstance in serializedObjects)
                {
                    writer.WriteLine(objectInstance);
                }
            }
        }


        public List<string[]> ParseFromFile(string path)
        {
                List<string[]> parsedLines = new List<string[]>();
                List<string> lines = ReadFromFile(path);
                foreach (string line in lines)
                {
                    parsedLines.Add(line.Split(','));
                }
                return parsedLines;
        }

        private List<string> ReadFromFile(string path)
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
