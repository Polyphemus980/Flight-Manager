using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BruTile.Wmts.Generated;

namespace PROJOBJ1
{
    public  class ParsedQuery
    {
        public List<string> properties { get; set; }
        public List<string>? operators { get; set; }
        public Dictionary<string, List<Expression>>? conditions { get; set; }
        public Dictionary<string, IComparable>? propertyValues;
        public string source { get; set; }
        

        public ParsedQuery(List<string> Properties,List<string> Operators, Dictionary<string, List<Expression>> Conditions,string source) 
        {
            this.properties = Properties;
            this.operators = Operators;
            this.conditions = Conditions;
            this.source = source;
        }
    }
}
