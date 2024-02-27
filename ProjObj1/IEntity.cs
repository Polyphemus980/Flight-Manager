using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    [JsonDerivedType(typeof(Airport), typeDiscriminator: nameof(Airport))]
    [JsonDerivedType(typeof(Cargo), typeDiscriminator: nameof(Cargo))]
    [JsonDerivedType(typeof(Flight), typeDiscriminator: nameof(Flight))]
    [JsonDerivedType(typeof(Crew), typeDiscriminator: nameof(Crew))]
    [JsonDerivedType(typeof(CargoPlane), typeDiscriminator: nameof(CargoPlane))]
    [JsonDerivedType(typeof(PassengerPlane), typeDiscriminator: nameof(PassengerPlane))]
    [JsonDerivedType(typeof(Passenger), typeDiscriminator: nameof(Passenger))]
    public interface IEntity
    {
        public UInt64 ID { get; set; }
    }

}
