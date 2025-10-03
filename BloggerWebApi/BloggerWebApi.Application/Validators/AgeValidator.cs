using System.ComponentModel.DataAnnotations;

namespace BloggerWebApi.Validators;

public class AgeValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return (int?) value < 12 ? new  ValidationResult("sorry, you must be above 11") : ValidationResult.Success;
    }
}