using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class FTRHandler:IDataSource
    {
        public List<IEntity> objects { get; set; }
        public string inPath { get; set; }
        public FTRHandler(string inPath)
        {
            this.inPath = inPath;
            objects= new List<IEntity>();
        }
        public void Start()
        {
            List<string[]> propertiesList = ParseFromFile(inPath);
            foreach (string[] properties in propertiesList)
            {
                string name = properties[0];
                IEntity objectInstance = DataHandler.Factories[name].CreateInstance(properties[1..properties.Length]);
                objectInstance.accept();
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
