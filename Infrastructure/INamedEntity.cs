namespace Solidex.Core.Data.Infrastructure
{
    public interface INamedEntity
    {
        string Name { get; set; }
        string Fullname { get; set; }
        string Description { get; set; }
    }
}