using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PampaDevs.BuildingBlocks.Application;
using PampaDevs.Bus;
using PampaDevs.Bus.InProc.Notifications;
using System.Threading.Tasks;
using WebStore.ProductCatalog.Api.Dtos;
using WebStore.ProductCatalog.Domain.Usecases.CreateProduct;

namespace WebStore.ProductCatalog.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
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
        /// <returns>A newly created Product</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If there was a error processing the request</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> CreateProduct([FromBody] ProductDto dto)
        {
            var command = ProductDto.CreateProductCommand(dto);

            var result = await _dispatcher.DispatchCommand<CreateProductCommand, CreateProductCommandResponse>(command);

            return PostResponse(nameof(CreateProduct), result);
        }
    }
}