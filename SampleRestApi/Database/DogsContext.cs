using Microsoft.EntityFrameworkCore;
using SampleRestApi.Database.Models;
using System;
using System.Drawing;

namespace SampleRestApi.Database;

public class DogsContext : DbContext
{
    public DogsContext(DbContextOptions<DogsContext> opts) : base(opts)
         => Database.EnsureCreated();

    public DbSet<Dog> Dogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        IList<Dog> dogs = new List<Dog>();

        KnownColor[] colors = Enum.GetValues<KnownColor>()!;
        for (int i = 1; i <= 50; i++)
        {
            Random random = new();
            dogs.Add(new() { Id = i, Color = colors[random.Next(colors.Length)].ToString(), Name = $"Name{(char)('a' + i)}", TailLength = random.Next(1, 100), Weight = random.Next(1, 100) });
        }

        modelBuilder.Entity<Dog>().HasData(dogs);

        base.OnModelCreating(modelBuilder);
    }
}
