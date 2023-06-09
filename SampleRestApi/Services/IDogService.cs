using SampleRestApi.Database.Models;
using SampleRestApi.Enums;

namespace SampleRestApi.Services;

public interface IDogService : IService<Dog, long>
{
}
