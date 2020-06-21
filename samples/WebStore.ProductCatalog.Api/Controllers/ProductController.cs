using MediatR;
using Microsoft.AspNetCore.Mvc;
using PampaDevs.BuildingBlocks.Application;
using PampaDevs.Bus;
using PampaDevs.Bus.InProc.Notifications;
using System.Threading.Tasks;
using WebStore.ProductCatalog.Api.Dtos;
using WebStore.ProductCatalog.Domain.Usecases.CreateProduct;

namespace WebStore.ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ApiController
    {
        public ProductController(INotificationHandler<DomainNotification> notifications, IDomainDispatcher dispatcher)
            : base(notifications, dispatcher)
        {

        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ApiResponse>> CreateProduct([FromBody] ProductDto dto)
        {
            var command = ProductDto.CreateProductCommand(dto);

            var result = await _dispatcher.DispatchCommand(command);

            return PostResponse(nameof(CreateProduct), result);
        }
    }
}