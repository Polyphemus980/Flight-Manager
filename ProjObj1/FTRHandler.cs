using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class FTRHandler
    {
        public List<IEntity> objects=new List<IEntity>();
        public Visitor visitor = new Visitor();
        public void LoadObjects(string path)
        {
            List<string[]> propertiesList = ParseFromFile(path);
            foreach (string[] properties in propertiesList)
            {
                string name = properties[0];
                IEntity objectInstance = DataHandler.Factories[name].CreateInstance(properties[1..properties.Length]);
                objectInstance.accept(visitor);
                objects.Add(objectInstance);
            }
        }
       
        private List<string[]> ParseFromFile(string path)
        {
            List<string[]> parsedLines = new List<string[]>();
            List<string> lines = DataHandler.ReadFromFile(path);
            foreach (string line in lines)
            {
                parsedLines.Add(line.Split(','));
            }
            return parsedLines;
        }

    }
}
