namespace Solidex.Core.Data.Infrastructure
{
    public interface IEntityProperty
    {
        string PropertyName { get; set; }
        string Value { get; set; }
    }
}