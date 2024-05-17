namespace PROJOBJ1;

public class ParsingUtility
{
    public static Dictionary<string, Dictionary<string, Func<string,IComparable>>> objectParsers;
    public static Dictionary<string, List<string>> propertyLists;
    static ParsingUtility()
    {
        objectParsers = new Dictionary<string, Dictionary<string, Func<string, IComparable>>>
        {
            {
                "Cargos", new Dictionary<string, Func<string, IComparable>>
                {
                    { "ID", DataHandler.ParseUInt64 },
                    { "Weight", DataHandler.ParseFloat },
                    { "Code", DataHandler.ParseString },
                    { "Description", DataHandler.ParseString }

                }
            },
            {
                "Airports", new Dictionary<string, Func<string, IComparable>>
                {
                    { "ID", DataHandler.ParseUInt64 },
                    { "Country", DataHandler.ParseString },
                    { "Code", DataHandler.ParseString },
                    { "AMSL", DataHandler.ParseFloat },
                    { "Latitude", DataHandler.ParseFloat },
                    { "Longitude", DataHandler.ParseFloat },
                    { "Name", DataHandler.ParseString }
                }
            },
            {
                "Crews", new Dictionary<string, Func<string, IComparable>>
                {
                    { "Practice", DataHandler.ParseUInt16 },
                    { "Role", DataHandler.ParseString },
                    { "ID", DataHandler.ParseUInt64 },
                    { "Name", DataHandler.ParseString },
                    { "Age", DataHandler.ParseUInt64 },
                    { "Phone", DataHandler.ParseString },
                    { "Email", DataHandler.ParseString }
                }
            },
            {
                "Passengers", new Dictionary<string, Func<string, IComparable>>
                {
                    { "Class", DataHandler.ParseString },
                    { "Miles", DataHandler.ParseUInt64 },
                    { "ID", DataHandler.ParseUInt64 },
                    { "Name", DataHandler.ParseString },
                    { "Age", DataHandler.ParseUInt64 },
                    { "Phone", DataHandler.ParseString },
                    { "Email", DataHandler.ParseString }
                }
            },
            {
                "PassengerPlanes", new Dictionary<string, Func<string, IComparable>>
                {
                    {"ID",DataHandler.ParseUInt64},
                    {"Serial",DataHandler.ParseString},
                    {"Country",DataHandler.ParseString},
                    {"Model",DataHandler.ParseString},
                    {"FirstClassSize",DataHandler.ParseUInt16},
                    {"EconomicClassSize",DataHandler.ParseUInt16},
                    {"BusinessClassSize",DataHandler.ParseUInt16}
                }

            },
            {
                "CargoPlanes", new Dictionary<string, Func<string, IComparable>>
                {
                    {"ID",DataHandler.ParseUInt64},
                    {"Serial",DataHandler.ParseString},
                    {"Country",DataHandler.ParseString},
                    {"Model",DataHandler.ParseString},
                    {"MaxLoad",DataHandler.ParseFloat}
                }
            },
            {
                "Flights", new Dictionary<string, Func<string, IComparable>>
                {
                    {"ID",DataHandler.ParseUInt64},
                    {"Origin",DataHandler.ParseUInt64},
                    {"Target",DataHandler.ParseUInt64},
                    {"LandingTime",DataHandler.ParseString},
                    {"TakeoffTime",DataHandler.ParseString},
                    {"Longitude",DataHandler.ParseFloat},
                    {"Latitude",DataHandler.ParseFloat},
                    {"AMSL",DataHandler.ParseFloat},
                    {"PlaneID",DataHandler.ParseUInt64}
                }
            }
        };
        propertyLists = new Dictionary<string, List<string>>
        {
            {"Flights",
                ["ID", "Origin", "Target", "LandingTime", "TakeoffTime", "Longitude", "Latitude", "AMSL", "PlaneID"]
            },
            {"CargoPlanes", ["ID", "Serial", "Country", "Model", "MaxLoad"] },
            {"PassengerPlanes",
                ["ID", "Serial", "Country", "Model", "FirstClassSize", "EconomicClassSize", "BusinessClassSize"]
            },
            { "Cargos", ["ID", "Weight", "Code", "Description"] },
            { "Airports", ["ID", "Name", "Code", "Longitude", "Latitude", "AMSL", "Country"] },
            { "Crews" , ["ID", "Name", "Age", "Phone", "Email", "Practice", "Role"] },
            { "Passengers" , ["ID", "Name", "Age", "Phone", "Email", "Class", "Miles"] }
        };
    }
}