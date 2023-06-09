using Microsoft.EntityFrameworkCore;
using SampleRestApi.Database;
using SampleRestApi.Database.Models;
using SampleRestApi.Services;

namespace SampleRestApi.Managers;

public abstract class Manager<T, K> : IService<T, K> where T : class
{
    protected readonly DogsContext db;
    public Manager(DogsContext db)
        => this.db = db;
    
    public abstract DbSet<T> Models { get; }
    public abstract T CreateModelById(K modelId);
    public abstract K GetIdFromModel (T model);

    public virtual async Task CreateModelAsync(T model)
    {
        await Models.AddAsync(model);
        await SaveChangesAsync();
    }

    public virtual async Task DeleteModelAsync(K modelId)
    {
        Models.Remove(CreateModelById(modelId));
        await SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> GetModelsAsync()
        => await Models.ToListAsync();

    public virtual async Task UpdateModelAsync(T model)
    {
        Models.Update(model);
        await SaveChangesAsync();
    }

    protected virtual async Task SaveChangesAsync()
        => await db.SaveChangesAsync();

    public virtual async Task<T> GetModelByIdAsync(K modelId)
        => (await GetModelsAsync()).First(m => GetIdFromModel(m)!.Equals(modelId));
}
