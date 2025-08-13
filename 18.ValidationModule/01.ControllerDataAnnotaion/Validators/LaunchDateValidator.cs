using System.ComponentModel.DataAnnotations;

namespace ControllerDataAnnotation.Validators;


public class LaunchDateValidator
{
    public static ValidationResult? validateDate(DateTime targetDate)
    {
        return (targetDate.Date >= DateTime.UtcNow.Date) ? ValidationResult.Success :
        new ValidationResult("The Launch date must be Current date or more than.");
    }
}
