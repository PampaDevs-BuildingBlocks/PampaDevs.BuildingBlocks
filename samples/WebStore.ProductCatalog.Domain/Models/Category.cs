using PampaDevs.BuildingBlocks.Domain;
using PampaDevs.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using static PampaDevs.Utils.Helpers.IdHelper;

namespace WebStore.ProductCatalog.Domain.Models
{
    public class Category : Entity<Guid>
    {
        public Category(string name, int code) : base(NewId())
        {
        }

        public string Name { get; private set; }
        public int Code { get; private set; }
        public ICollection<Product> Products { get; set; }

        protected override void ValidateCreation()
        {
            Ensure.NotNullOrEmpty(Name);
            Ensure.That(Code > 0);
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }
    }
}
