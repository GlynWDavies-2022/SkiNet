using System.Linq.Expressions;

namespace SkiNet.Core.Interfaces;

public interface ISpecification<T>
{
    public Expression<Func<T,bool>> Criteria { get; }
}
