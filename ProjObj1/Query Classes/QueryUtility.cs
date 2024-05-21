namespace PROJOBJ1;

public class QueryUtility
{
    public static Dictionary<string, Dictionary<string, Func<string, IComparable>>> objectParsers;
    public static Dictionary<string, List<string>> propertyLists;
    public static readonly Dictionary<string, IFactory> factories;
    public static Dictionary<string, Func<List<IEntity>>> databaseValues;

    static QueryUtility()
    {
        factories = new Dictionary<string, IFactory>
        {
            { "Cargos", new CargoFactory() },
            { "Crews", new CrewFactory() },
            { "Passengers", new PassengerFactory() },
            { "CargoPlanes", new CargoPlaneFactory() },
            { "PassengerPlanes", new PassengerPlaneFactory() },
            { "Airports", new AirportFactory() },
            { "Flights", new FlightFactory() }
        };
        databaseValues = new Dictionary<string, Func<List<IEntity>>>
        {
            { "Cargos", () => Database.Cargos.Values.ToList<IEntity>() },
            { "Airports", () => Database.Airports.Values.ToList<IEntity>() },
            { "Crews", () => Database.Crews.Values.ToList<IEntity>() },
            { "Passengers", () => Database.Passengers.Values.ToList<IEntity>() },
            { "PassengerPlanes", () => Database.PassengerPlanes.Values.ToList<IEntity>() },
            { "CargoPlanes", () => Database.CargoPlanes.Values.ToList<IEntity>() },
            { "Flights", () => Database.Flights.Values.ToList<IEntity>() }
        };
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
                    { "WorldPosition.Lat", DataHandler.ParseFloat },
                    { "WorldPosition.Long", DataHandler.ParseFloat },
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
                    { "ID", DataHandler.ParseUInt64 },
                    { "Serial", DataHandler.ParseString },
                    { "Country", DataHandler.ParseString },
                    { "Model", DataHandler.ParseString },
                    { "FirstClassSize", DataHandler.ParseUInt16 },
                    { "EconomicClassSize", DataHandler.ParseUInt16 },
                    { "BusinessClassSize", DataHandler.ParseUInt16 }
                }

            },
            {
                "CargoPlanes", new Dictionary<string, Func<string, IComparable>>
                {
                    { "ID", DataHandler.ParseUInt64 },
                    { "Serial", DataHandler.ParseString },
                    { "Country", DataHandler.ParseString },
                    { "Model", DataHandler.ParseString },
                    { "MaxLoad", DataHandler.ParseFloat }
                }
            },
            {
                "Flights", new Dictionary<string, Func<string, IComparable>>
                {
                    { "ID", DataHandler.ParseUInt64 },
                    { "Origin", DataHandler.ParseUInt64 },
                    { "Target", DataHandler.ParseUInt64 },
                    { "LandingTime", DataHandler.ParseString },
                    { "TakeoffTime", DataHandler.ParseString },
                    { "WorldPosition.Long", DataHandler.ParseFloat },
                    { "WorldPosition.Lat", DataHandler.ParseFloat },
                    { "AMSL", DataHandler.ParseFloat },
                    { "PlaneID", DataHandler.ParseUInt64 }
                }
            }
        };
        propertyLists = new Dictionary<string, List<string>>
        {
            {
                "Flights",
                [
                    "ID", "Origin", "Target", "TakeoffTime", "LandingTime", "WorldPosition.Long", "WorldPosition.Lat",
                    "AMSL", "PlaneID"
                ]
            },
            { "CargoPlanes", ["ID", "Serial", "Country", "Model", "MaxLoad"] },
            {
                "PassengerPlanes",
                ["ID", "Serial", "Country", "Model", "FirstClassSize", "BusinessClassSize", "EconomicClassSize"]
            },
            { "Cargos", ["ID", "Weight", "Code", "Description"] },
            { "Airports", ["ID", "Name", "Code", "WorldPosition.Long", "WorldPosition.Lat", "AMSL", "Country"] },
            { "Crews", ["ID", "Name", "Age", "Phone", "Email", "Practice", "Role"] },
            { "Passengers", ["ID", "Name", "Age", "Phone", "Email", "Class", "Miles"] }
        };
    }

    public static void ParseConditions(string[] args, List<string> operators,
        Dictionary<string, List<Expression>> conditions, string source)
    {
        for (int i = 0; i < args.Length; i += 4)
        {
            if (args.Length < i + 3)
                throw new Exception("Incorrect condition synthax -example 'propertyName > propertyValue");
            string property = args[i];
            string logOperator = args[i + 1];
            string val = args[i + 2];
            IComparable value = QueryUtility.objectParsers[source][property](val);
            Expression exp = new Expression(logOperator, value);
            if (!conditions.ContainsKey(property))
            {
                List<Expression> pexp = new List<Expression>();
                pexp.Add(exp);
                conditions.Add(property, pexp);
            }
            else
            {
                conditions[property].Add(exp);
            }

            if (args.Length == i + 3)
                break;
            operators.Add(args[i + 3]);
        }
    }

    public static List<IEntity> GetMatching(ParsedQuery parsedQuery)
    {
    return GetMatching(QueryUtility.databaseValues[parsedQuery.source](), parsedQuery);
    }

    public static List<IEntity> GetMatching(List<IEntity> source,ParsedQuery parsedQuery)
    {

            var results = new List<IEntity>();
            List<bool> logic = new List<bool>();
            if (parsedQuery.conditions.Count == 0)
            {
                foreach (var entity in source)
                {
                    results.Add(entity);
                }
            }
            else
            {
                foreach (var entity in source)
                {
                    foreach (var exprList in parsedQuery.conditions)
                    {
                        foreach (var expr in exprList.Value)
                        {
                            logic.Add(expr.TestExpression(entity.values[exprList.Key]()));
                        }
                    }

                    if (Test(logic, parsedQuery.operators))
                        results.Add(entity);
                    logic.Clear();
                }
            }

        return results;//results;
    }
    public static bool Test(List<bool> bools, List<string> operators)
    {

        bool result = bools[0];
        for (int i = 0; i < operators.Count; i++)
        {
            bool operand = bools[i + 1];
            string op = operators[i];

            switch (op.ToLower())
            {
                case "and":
                    result = result && operand;
                    break;
                case "or":
                    result = result || operand;
                    break;
                default:
                    throw new ArgumentException($"Invalid operator: {op}");
            }
        }
        return result;
    }
}