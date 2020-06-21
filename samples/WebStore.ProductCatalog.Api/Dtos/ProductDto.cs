using PampaDevs.BuildingBlocks.Application;
using System;
using System.ComponentModel.DataAnnotations;
using WebStore.ProductCatalog.Domain.Usecases.CreateProduct;

namespace WebStore.ProductCatalog.Api.Dtos
{
    public class ProductDto : IDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        public static CreateProductCommand CreateProductCommand(ProductDto productDto)
        {
            return new CreateProductCommand()
            {
                CategoryId = productDto.CategoryId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                ImageUrl = productDto.ImageUrl
            };
        }
    }
}