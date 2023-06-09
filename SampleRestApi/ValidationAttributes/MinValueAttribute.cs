using System.ComponentModel.DataAnnotations;

namespace SampleRestApi.ValidationAttributes;

public class MinValueAttribute : RangeAttribute
{
    public MinValueAttribute(int minimumValue) : base(minimumValue, int.MaxValue) { }
}
