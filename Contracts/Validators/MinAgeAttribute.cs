using System;
using System.ComponentModel.DataAnnotations;

public class MinAgeAttribute : ValidationAttribute {
    private readonly int _minimumAge;

    public MinAgeAttribute(int minimumAge = 18) {
        _minimumAge = minimumAge;
        ErrorMessage = $"Person must be at least {_minimumAge} years old.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
        if (value is DateOnly birthDate) {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            int age = currentDate.Year - birthDate.Year;

            // Überprüfen, ob das Geburtsdatum schon im aktuellen Jahr war
            if (currentDate < birthDate.AddYears(age)) {
                age--;
            }

            if (age >= _minimumAge) {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }

        return new ValidationResult("Ungültiges Geburtsdatum.");
    }
}
