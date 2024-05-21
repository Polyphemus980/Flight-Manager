namespace PROJOBJ1;

public interface IQueryParser
{
    public List<string> properties { get; set; }
    public Dictionary<string,List<Expression>> conditions { get; set; }
    public List<string> operators { get; set; }
    string source { get; set; }
}