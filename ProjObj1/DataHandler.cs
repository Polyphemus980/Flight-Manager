
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
            List<string[]> properties_list = ParseFromFile(path);
            foreach (string[] properties in properties_list)
            {
                string name = properties[0];
                IEntity object_instance =  Factories[name].createClass(properties[1..properties.Length]);
                objects.Add(object_instance);
            }              
            return objects;
        }
        public static void SerializeObjects(List<IEntity> objects, string savepath) 
        {
            using (StreamWriter writer = new StreamWriter(savepath))
            {
                foreach (IEntity object_instance in objects)
                {
                    writer.WriteLine(JsonSerializer.Serialize<IEntity>(object_instance));
                }
            }
        }

        public static List<string[]> ParseFromFile(string path)
        {
            List<string[]> line_list = new List<string[]>();
            if (!File.Exists(path))
            {
                return line_list; 
            }
            using (StreamReader linereader = new StreamReader(path))
            {
                while(!linereader.EndOfStream)
                {
                    string[] line = linereader.ReadLine().Split(',');
                    line_list.Add(line);
                }    
            }
            return line_list;
            

        }
    }
}
