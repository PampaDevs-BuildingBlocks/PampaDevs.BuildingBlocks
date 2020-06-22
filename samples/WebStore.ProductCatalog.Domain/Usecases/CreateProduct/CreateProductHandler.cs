using MediatR;
using PampaDevs.BuildingBlocks.Data.EntityFramework;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebStore.ProductCatalog.Domain.Models;

namespace WebStore.ProductCatalog.Domain.Usecases.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        //private readonly IEfUnitOfWork _unitOfWork;

        public CreateProductHandler(/*IEfUnitOfWork unitOfWork*/)
        {
            //_unitOfWork = unitOfWork;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {            
            //var productRepository = _unitOfWork.Repository<Product, Guid>();
            var product = Product.Of(request);
            //var created = await productRepository.AddAsync(product);

            return new CreateProductCommandResponse(Guid.Empty);
        }
    }
}