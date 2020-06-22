using FluentValidation;
using PampaDevs.Bus;
using PampaDevs.Bus.InProc;
using System;
using WebStore.ProductCatalog.Domain.Models;

namespace WebStore.ProductCatalog.Domain.Usecases.CreateProduct
{
    public class CreateProductCommand : Command<CreateProductCommandValidator, CreateProductCommandResponse>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }        
    }

    public class CreateProductCommandResponse
    {
        public Product Product { get; set; }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.CategoryId).NotEqual(Guid.Empty).WithMessage("The category Id cannot be empty");
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("The price cannot be 0");
        }
    }
}
