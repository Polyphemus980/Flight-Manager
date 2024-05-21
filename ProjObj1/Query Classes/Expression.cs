namespace PROJOBJ1;

public class Expression(string Operator, IComparable value)
{
    public string Operator { get; set; } = Operator;
    public IComparable Value { get; set; } = value;
    public bool TestExpression(IComparable val)
    {
            switch (Operator)
            {
                case ">":
                    return Value.CompareTo(val) < 0;
                case "<":
                    return Value.CompareTo(val) > 0;
                case "=":
                    return Value.CompareTo(val) == 0;
                case ">=":
                    return Value.CompareTo(val) <= 0;
                case "<=":
                    return Value.CompareTo(val) >= 0;
                case "!=":
                    return Value.CompareTo(val) != 0;
                default:
                    throw new Exception($"Incorrect operator '{Operator}'");
            }
    }
}