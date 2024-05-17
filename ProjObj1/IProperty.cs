namespace PROJOBJ1;

public interface IPropertable
{
    public static Dictionary<string, Func<IComparable>> values { get; set; }
    public static Dictionary<string, Func<IComparable>> parsers { get; set; }
}