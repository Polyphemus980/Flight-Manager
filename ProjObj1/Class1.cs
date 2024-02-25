using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PROJOBJ1
{
    public interface Factory
    {
        AbstractProduct createClass(string[] list);
    }
    [JsonDerivedType(typeof(Airport))]
    [JsonDerivedType(typeof(Cargo))]
    [JsonDerivedType(typeof(Flight))]
    [JsonDerivedType(typeof(Crew))]
    [JsonDerivedType(typeof(CargoPlane))]
    [JsonDerivedType(typeof(PassengerPlane))]
    [JsonDerivedType(typeof(Passenger))]
    public interface AbstractProduct
    {
        public UInt64 ID { get; set; }
    }
    
    public class Crew:AbstractProduct
    {
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public UInt16 Practice { get; set; }
        public string Role { get; set; }

        public Crew(UInt64 ID_,string Name_,UInt64 Age_,string Phone_,string Email_,UInt16 Practice_,string Role_) 
        {
            ID = ID_;Name=Name_;Age=Age_;Phone=Phone_;Email=Email_;Practice = Practice_; Role=Role_;
        }

    }
    public class CrewFactory : Factory
    {
        public AbstractProduct createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            UInt64 Age= UInt64.Parse(list[2]);
            UInt16 Practice= UInt16.Parse(list[5]);
            return new Crew(ID, list[1], Age, list[3], list[4], Practice, list[6]);
        }
    }

    public class Passenger:AbstractProduct
    {
        public UInt64 ID { get; set; }
        public string? Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public UInt64 Role { get; set; }

        public Passenger(UInt64 ID_,string Name_,UInt64 Age_,string Phone_,string Email_,string Class_,UInt64 Role_)
        {
            ID=ID_;Name=Name_;Age=Age_;Phone=Phone_;Email=Email_;Class=Class_;Role=Role_;
        }
    }
    
    public class PassengerFactory : Factory
    {
        public AbstractProduct createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            UInt64 Age = UInt64.Parse(list[2]);
            UInt64 Role = UInt64.Parse(list[list.Length-1]);
            return new Passenger(ID, list[1], Age, list[3], list[4], list[5], Role);
        }
    }

    public class Cargo : AbstractProduct
    {
        public UInt64 ID { get; set; }
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(ulong ID_, float Weight_, string Code_, string Description_)
        {
            ID = ID_;
            Weight = Weight_;
            Code = Code_;
            Description = Description_;
        }

        public override string ToString()
        {
            return $"Cargo ID: {ID}, Weight: {Weight}, Code: {Code}, Description: {Description}";
        }
    }
    public class CargoFactory : Factory
    { 
        public AbstractProduct createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            Single Weigth = Single.Parse(list[1], CultureInfo.InvariantCulture);
            return new Cargo(ID, Weigth, list[2], list[3]);
        }
    }
    public class CargoPlane:AbstractProduct
    {
        public UInt64 ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public Single MaxLoad { get; set; }
        
        public CargoPlane(UInt64 ID_,string Serial_,string Country_,string Model_,Single MaxLoad_)
        {
            ID = ID_;Serial= Serial_;Country= Country_; Model= Model_;MaxLoad= MaxLoad_;
        }
    }
    public class CargoPlaneFactory : Factory
    {
        public AbstractProduct createClass(string[] list)
        {
            UInt64 ID= UInt64.Parse(list[0]);
            Single MaxLoad = Single.Parse(list[list.Length-1], CultureInfo.InvariantCulture);
            return new CargoPlane(ID, list[1], list[2], list[3], MaxLoad);
        }
    }
    public class PassengerPlane : AbstractProduct
    {
        public UInt64 ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public UInt16 FirstClassSize { get; set; }
        public UInt16 EconomicClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        
        public PassengerPlane(UInt64 ID_, string Serial_, string Country_, string Model_, UInt16 FirstClassSize_,UInt16 EconomicClassSize_,UInt16 BusinessClassSize_)
        {
            ID = ID_; Serial = Serial_; Country = Country_; Model = Model_; FirstClassSize = FirstClassSize_;EconomicClassSize= EconomicClassSize_;BusinessClassSize= BusinessClassSize_;
        }
    }
    public class PassengerPlaneFactory : Factory
    {
        public AbstractProduct createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            UInt16 FirstClassSize= UInt16.Parse(list[4]);
            UInt16 EconomicClassSize= UInt16.Parse(list[5]);
            UInt16 BusinessClassSize= UInt16.Parse(list[6]);
            return new PassengerPlane(ID,list[1],list[2],list[3],FirstClassSize,EconomicClassSize, BusinessClassSize);
        }
    }

    public class Airport: AbstractProduct
    {
        
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public string Country { get; set; }
        
        public Airport(UInt64 ID_,string Name_,string Code_,Single Longitude_,Single Latitude_,Single AMSL_,string Country_)
        {
            ID = ID_;Name= Name_;Code = Code_;Longitude= Longitude_;Latitude = Latitude_;AMSL= AMSL_;Country= Country_;
        }
    }
    public class AirportFactory : Factory
    {
        public AbstractProduct createClass(string[] list)
        {
            UInt64 ID= UInt64.Parse(list[0]);
            Single Longitude= Single.Parse(list[3],CultureInfo.InvariantCulture);
            Single Latitude = Single.Parse(list[4],CultureInfo.InvariantCulture);
            Single AMSL = Single.Parse(list[5],CultureInfo.InvariantCulture);
            return new Airport(ID, list[1], list[2],Longitude,Latitude, AMSL, list[list.Length-1]);

        }
    }

    public class Flight: AbstractProduct
    {
        public UInt64 ID { get; set; }
        public UInt64 Origin { get; set; }
        public UInt64 Target { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public UInt64 PlaneID { get; set; }
        public UInt64[] CrewIDs { get; set; }
        public UInt64[] LoadIDs { get; set; }
        
        public Flight(UInt64 ID_, UInt64 Origin_,UInt64 Target_,string TakeoffTime_,string LandingTime_,Single Longitude_,
        Single Latitude_,Single AMSL_,UInt64 PlaneID_,UInt64[] CrewIDs_, UInt64[] LoadIDs_)
        {
            ID= ID_;Origin= Origin_; Target= Target_; TakeoffTime= TakeoffTime_;LandingTime= LandingTime_;Longitude=Longitude_;
            Latitude = Latitude_;AMSL= AMSL_;PlaneID = PlaneID_;CrewIDs= CrewIDs_;LoadIDs= LoadIDs_;
        }
    }
    public class FlightFactory : Factory
    {
        public AbstractProduct createClass(string[] list)
        {
          UInt64 ID= UInt64.Parse(list[0]);
          UInt64 Origin= UInt64.Parse(list[1]);
          UInt64 Target= UInt64.Parse(list[2]);
          Single Longitude = Single.Parse(list[5],CultureInfo.InvariantCulture);
          Single Latitude = Single.Parse(list[6],CultureInfo.InvariantCulture);
          Single AMSL= Single.Parse(list[7],CultureInfo.InvariantCulture);
          UInt64 PlaneID = UInt64.Parse(list[8]);
          string[] CrewIDs = list[9].Trim('[',']').Split(';');
          
          UInt64[] Crew= new UInt64[CrewIDs.Length];
          for (int i=0;i<CrewIDs.Length;i++)
          {
            Crew[i] = UInt64.Parse(CrewIDs[i]);
          }
          
          string[] LoadIDs= list[10].Trim('[',']').Split(";");
          UInt64[] Load = new UInt64[LoadIDs.Length];
          for (int i = 0; i < LoadIDs.Length; i++)
          {
          Load[i] = UInt64.Parse(LoadIDs[i]);
          }

          return new Flight(ID, Origin, Target, list[3], list[4],Longitude,Latitude,AMSL,PlaneID,Crew,Load);
        }
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
