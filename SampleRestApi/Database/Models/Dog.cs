using SampleRestApi.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleRestApi.Database.Models;

public class Dog
{
    [Key]
    public long Id { get; set; }

    [Name]
    [Required(ErrorMessage = "{0} must be not null!")]
    [Unique(nameof(Id), ErrorMessage = "{0} must be unique!")]
    public string Name { get; set; } = null!;

    [Color]
    [Required(ErrorMessage = "{0} must be not null!")]
    public string Color { get; set; } = null!;

    [MinValue(0)]
    [JsonPropertyName("tail_length")]
    public int TailLength { get; set; }

    [MinValue(0)]
    public int Weight { get; set; }
}
