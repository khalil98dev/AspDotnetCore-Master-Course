using System.Data;
using MinimalFluentValidation.Requests;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ControllerFluentValidation.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(pro => pro.Name)
            .NotEmpty().WithMessage("The name Field is required.")
            .MaximumLength(255).MinimumLength(5).WithMessage("The name must between 5 and 255");

        RuleFor(pro => pro.SKU)
        .NotEmpty().WithMessage("The SKU is required")
        .Matches("^PRD-\\d{5}$").WithMessage("The Sku must be 'PRD-XXXXX'");

        RuleFor(p => p.Price)
        .GreaterThan(0)
        .WithMessage("The price must greater than 0.01");

        RuleFor(p => p.StockQuantity)
        .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be a non-negative integer");

        RuleFor(pr => pr.LaunchDate)
        .Must((DateTime date)=>date>=DateTime.UtcNow.Date)
        .WithMessage("Launch date must been on the future");

        RuleFor(p => p.ImageUrl)
        .Must((path)=>Uri.TryCreate(path, UriKind.Absolute, out _))
        .WithMessage("The image must be valid URL");

        RuleFor(p => p.Tags)
        .Must(tg => tg.Count <= 5).WithMessage("Max tags is 5");

        RuleFor(p => p.ProductCategory)
        .IsInEnum().WithMessage("Invalid Category");

        RuleFor(p => p.Wieght)
        .InclusiveBetween(0.01m, 1000m)
        .WithMessage("The weight must been between 0 and 1000 kg");


        RuleFor(p => p.WarrantyPersiodMonths)
        .Must((v) => (v == 0 || v == 12 || v == 24 || v == 36))
        .WithMessage("The warranty must been 0,12,24 or 36");

        When(p => p.IsReturnable, () =>
        {
            RuleFor(pr => pr.ReturnPolicyDescription).NotEmpty().WithMessage("The Return policy Description is required");
        });
    }
}