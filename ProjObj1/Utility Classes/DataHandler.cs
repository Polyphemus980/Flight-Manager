
using NetworkSourceSimulator;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

using System.Text.Json.Serialization;


namespace PROJOBJ1
{
    public static class DataHandler
    {
        
        public static readonly Dictionary<string, IFactory> Factories;
        static DataHandler()
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
        public static List<string> SerializeObjects(List<IEntity> objects)
        {
            List<string> serializedObjects = new List<string>();
            foreach (IEntity objectInstance in objects)
            {
                serializedObjects.Add(JsonSerializer.Serialize<IEntity>(objectInstance));
            }
            return serializedObjects;
        }
        public static void SaveToPath(string path, List<IEntity> objects)
        {
            List<string> serializedObjects = SerializeObjects(objects);
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string objectInstance in serializedObjects)
                {
                    writer.WriteLine(objectInstance);
                }
            }
        }
        public static List<string> ReadFromFile(string path)
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
        public static void MakeSnapshot(IDataSource source)
        {

            lock (source.objects)
            {
                DataHandler.SaveToPath(SnapshotName(), source.objects);
            }
        }

        public static string SnapshotName()
        {
            DateTime CurrentTime = DateTime.Now;
            string SnapshotName = "snapshot_" + CurrentTime.Hour + "_" + CurrentTime.Minute + "_" + CurrentTime.Second + ".json";
            return SnapshotName;
        }

        public static IComparable ParseUInt16(string parsedObject)
        {
            return UInt16.Parse(parsedObject);
        }
        public static IComparable ParseUInt64(string parsedObject)
        {
            return UInt64.Parse(parsedObject);
        }

        public static IComparable ParseFloat(string parsedObject)
        {
            return float.Parse(parsedObject,CultureInfo.InvariantCulture);
        }

        public static IComparable ParseString(string parsedObject)
        {
            return parsedObject;
        }
    }


}
    
 
