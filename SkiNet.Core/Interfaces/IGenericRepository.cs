using SkiNet.Core.Entities;

namespace SkiNet.Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity);
    public Task<IReadOnlyList<T>> ListAllAsync();
    public Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    public Task<T?> GetByIdAsync(int id);
    public void Update(T entity);
    public void Remove(T entity);
    public Task<bool> SaveAllAsync();
    public bool Exists(int id);
}
