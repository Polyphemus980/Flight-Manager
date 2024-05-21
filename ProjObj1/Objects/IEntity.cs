using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    [JsonDerivedType(typeof(Airport), typeDiscriminator: "AI")]
    [JsonDerivedType(typeof(Cargo), typeDiscriminator: "CA")]
    [JsonDerivedType(typeof(Flight), typeDiscriminator: "FL")]
    [JsonDerivedType(typeof(Crew), typeDiscriminator: "C")]
    [JsonDerivedType(typeof(CargoPlane), typeDiscriminator: "CP")]
    [JsonDerivedType(typeof(PassengerPlane), typeDiscriminator: "PP")]
    [JsonDerivedType(typeof(Passenger), typeDiscriminator: "P")]
    public interface IEntity
    {
        public UInt64 ID { get; set; }
        public Dictionary<string, Func<IComparable>> values { get; set; }
        

        public void addToDatabase();

        public void changeID(UInt64 prevID, UInt64 newID);
        public void changeContactInfo(UInt64 ID, string emailAddress, string phoneNumber);
    }

}
