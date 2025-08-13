using System.ComponentModel.DataAnnotations;

namespace ControllerDataAnnotation.Validators;

public class WarrantyValidator
{
    public static ValidationResult? ValidateWarranty(int value) => (value == 0 || value == 12 ||value == 24 ||  value == 36) ?ValidationResult.Success
     : new ValidationResult("The waranty value must been 0,12,24 or 36"); 
   
    
}