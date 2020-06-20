using PampaDevs.BuildingBlocks.Domain;
using PampaDevs.Utils;
using System;
using WebStore.ProductCatalog.Domain.Usecases.CreateProduct;
using static PampaDevs.Utils.Helpers.IdHelper;

namespace WebStore.ProductCatalog.Domain.Models
{
    public class Product : AggregateRoot<Guid>
    {
        private Product(Guid categoryId, string name, string description, decimal price, string imageUrl)
            : base(NewId())
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl; 
            IsActive = true;

            ValidateCreation();
        }

        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; }
        public bool IsActive { get; private set; }

        public virtual Category Category { get; set; }

        public void Active() => IsActive = true;
        public void Deactive() => IsActive = false;
        public void UpdateCategory(Category category)
        {
            Ensure.NotNull(category);
            Ensure.NotEqual(CategoryId, category.Id);

            CategoryId = category.Id;
            Category = category;
        }

        public static Product Of(CreateProductCommand command)
        {
            return new Product(
                command.CategoryId, 
                command.Name, 
                command.Description,
                command.Price,
                command.ImageUrl);
        }

        protected override void ValidateCreation()
        {
            Ensure.NotEqual(Guid.Empty, CategoryId, "CategoryId cannot be empty");
            Ensure.NotNullOrEmpty(Name, "Name cannot be empty");
            Ensure.NotNullOrEmpty(Description, "Description cannot be empty");
            Ensure.That(Price > 0, "Price cannot be smaller or equal than 0");
            Ensure.NotNullOrEmpty(ImageUrl, "The image url cannot be empty");
            Ensure.That(IsActive, "The product cannot be created inactive");
        }
    }
}
