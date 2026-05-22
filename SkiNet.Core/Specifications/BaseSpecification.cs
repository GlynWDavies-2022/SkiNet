using SkiNet.Core.Interfaces;
using System.Linq.Expressions;

namespace SkiNet.Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T,bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Criteria => criteria;
}
