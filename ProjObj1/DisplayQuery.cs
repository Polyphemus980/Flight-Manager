namespace PROJOBJ1;

public class DisplayQuery:IQuery
{
    public DisplayParser parser { get; set; }
    public List<IEntity> matching { get; set; }

    public DisplayQuery(string[] parsingArgs)
    {
        try
        {
            parser = new DisplayParser(parsingArgs);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        matching = GetMatching();
    }

    public List<IEntity> GetMatching()
    {
        switch (parser.source)
        {
            case "Cargos":
            {
                return GetMatching(Database.Cargos.Values.ToList<IEntity>());
            }
            case "Airports":
            {
                return GetMatching(Database.Airports.Values.ToList<IEntity>());
            }
            case "Crews":
            {
                return GetMatching(Database.Crews.Values.ToList<IEntity>());
            }
            case "Passengers":
            {
                return GetMatching(Database.Passengers.Values.ToList<IEntity>());
            }
            case "PassengerPlanes":
            {
                return GetMatching(Database.PassengerPlanes.Values.ToList<IEntity>());
            }
            case "CargoPlanes":
            {
                return GetMatching(Database.CargoPlanes.Values.ToList<IEntity>());
            }
            case "Flights":
            {
                return GetMatching(Database.Flights.Values.ToList<IEntity>());
            }
                
            default:
            {
                return new List<IEntity>();
            }
        }
    }

    public List<IEntity> GetMatching(List<IEntity> source)
    {
        var results = new List<IEntity>();
        List<bool> logic = new List<bool>();
        if (parser.conditions.Count == 0)
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
                foreach (var exprList in parser.conditions) 
                {
                    foreach (var expr in exprList.Value)
                    {
                        logic.Add(expr.TestExpression(entity.values[exprList.Key]()));
                    }
                }

                if (Test(logic, parser.operators))
                    results.Add(entity);
                logic.Clear();
            }
        }

        return results;
    }

  

    public void Execute()
    {
        var maxWidths = new Dictionary<string, int>();
        
        foreach (var property in parser.properties)
        {
            maxWidths[property] = property.Length;
        }

        foreach (var entry in matching)
        {
            foreach (var property in parser.properties)
            {
                if (!(entry.values[property]() is null))
                {
                    maxWidths[property] = Math.Max(maxWidths[property], entry.values[property]().ToString().Length);
                }
                else
                {
                    maxWidths[property] = Math.Max(maxWidths[property], "Null".Length);
                }
            }
        }
        foreach (var property in parser.properties)
        {
            Console.Write(property.PadRight(maxWidths[property]) + " | ");
        }
        Console.WriteLine();

        foreach (var property in parser.properties)
        {
            Console.Write(new string('-', maxWidths[property]) + " | ");
        }
        Console.WriteLine();

        foreach (var entry in matching)
        {
            foreach (var property in parser.properties)
            {
                if (entry.values[property]() is null)
                {
                    Console.Write("Null".PadLeft(maxWidths[property]) + " | ");
                    continue;
                }
                Console.Write(entry.values[property]().ToString().PadLeft(maxWidths[property]) + " | ");
            }
            Console.WriteLine();
        }
    }
    
    public bool Test(List<bool> bools, List<string> operators)
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
