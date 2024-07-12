namespace PROJOBJ1;

public interface IQueryFactory
{
    public IQuery CreateInstance(string[] query);
}