using SampleRestApi.Database;
using System.ComponentModel.DataAnnotations;

namespace SampleRestApi.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class UniqueAttribute : ValidationAttribute
{
    readonly string idName;
    public UniqueAttribute(string idName)
        => this.idName = idName;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        DogsContext db = validationContext.GetService<DogsContext>()!;

        if (value is string name)
        {
            long id = (long)validationContext.ObjectType!.GetProperty(idName)!.GetValue(validationContext.ObjectInstance)!;

            if (!db.Dogs.Any(d => d.Name == name && d.Id != id))
                return ValidationResult.Success;
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
}