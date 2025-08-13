using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using ControllerDataAnnotation.Validators;

namespace ControllerDataAnnotation.Requests;

public class CreateProductRequest
{
    [Required(ErrorMessage = "The name field requaried")]
    [StringLength(255,MinimumLength =5,ErrorMessage ="The name field must been between 5 and 255")]
    public string? Name { get; set; }

    [StringLength(1000,ErrorMessage ="Description max lenght is 1000")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The SKU is requaried")]
    [RegularExpression(@"^PRD-\d{5}$", ErrorMessage = "SKU Must Consists From PRD-XXXXX")]
    public string? SKU { get; set; }
    
    [Range(0.01 , double.MaxValue, ErrorMessage ="Price must be at least 0.01")]
    public decimal? Price { get; set; }

    [Range(0,int.MaxValue,ErrorMessage ="Integer stock value is required ..." )]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "The launche Date is required")]
    
    [CustomValidation(typeof(LaunchDateValidator),nameof(LaunchDateValidator.validateDate))]
    public DateTime LaunchDate { get; set; }

    [EnumDataType(typeof(eProductCategory),ErrorMessage = "Invalid product category.")]
    public eProductCategory ProductCategory { get; set; }

    [Url(ErrorMessage = "The image must be valid URI")]
    public string? ImageUrl { get; set; }

    [Range(0.01,1000,ErrorMessage ="Wieght must been between 0.01 and 1000 -kg")]
    public decimal? Wieght { get; set; }

    [Range(0, 36)]
    [CustomValidation(typeof(WarrantyValidator),nameof(WarrantyValidator.ValidateWarranty))]
    public int WarrantyPersiodMonths { get; set; }

    public bool IsReturnable { get; set; }


    [RequiredIfAttribute(nameof(IsReturnable),true)]
    public string? ReturnPolicyDescription { get; set; }
     
     [MaxLength(5,ErrorMessage = "A maximum of tags is 5 tags is allowed.")]
    public List<string> Tags { get; set; } = new();

}