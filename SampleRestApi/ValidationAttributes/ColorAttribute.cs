using Microsoft.EntityFrameworkCore.Diagnostics;
using SampleRestApi.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.RegularExpressions;

namespace SampleRestApi.ValidationAttributes;

public class ColorAttribute : ValidationAttribute
{
    public ColorAttribute()
        => ErrorMessage = "Incorrect color";

    public override bool IsValid(object? value)
    {
        if (value is string valueStr)
        {
            string[] colors = valueStr.Replace(" ", string.Empty).Split('&');
            IEnumerable<string> knownColors = Enum.GetValues<KnownColor>().Select(c => c.ToLowerString());
            return colors.All(c => knownColors.Contains(c.ToLower()));
        }

        return false;
    }
}
