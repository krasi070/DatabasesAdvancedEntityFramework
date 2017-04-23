namespace PlanetHunters.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PositiveAttribute : ValidationAttribute
    {
        public static ValidationResult IsValuePositive(decimal? value)
        {
            if (value > 0 || value == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Value is not positive");
            }
        }
    }
}