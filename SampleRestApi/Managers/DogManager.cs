using Microsoft.EntityFrameworkCore;
using SampleRestApi.Database;
using SampleRestApi.Database.Models;
using SampleRestApi.Services;

namespace SampleRestApi.Managers;

public class DogManager : Manager<Dog, long>, IDogService
{
    public DogManager(DogsContext db) : base(db) {}

    public override DbSet<Dog> Models => db.Dogs;

    public override Dog CreateModelById(long modelId)
        => new() { Id = modelId };

    public override long GetIdFromModel(Dog model)
        => model.Id;
}
