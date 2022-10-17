using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Rawaa_Api.Validation;

public class ProductRequestValidationAttribute : ValidationAttribute
{
    public byte[] allowNum { get; set; }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(allowNum.Contains((byte)value))
            return ValidationResult.Success;
        return new ValidationResult($"m not equals{value}");
    }
}

