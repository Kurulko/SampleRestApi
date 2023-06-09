using System.ComponentModel.DataAnnotations;

namespace SampleRestApi.ValidationAttributes;

public class NameAttribute : RegularExpressionAttribute
{
    public NameAttribute() : base("[A-Za-zЯА-Яа-яїє]{2,}")
        => ErrorMessage = "Incorrect name";
}
