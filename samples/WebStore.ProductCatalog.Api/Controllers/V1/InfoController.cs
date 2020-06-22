using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PampaDevs.BuildingBlocks.Infrastructure;

namespace WebStore.ProductCatalog.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult<SystemInfo> Info()
        {
            return new JsonResult(Assembly.GetExecutingAssembly().GetSystemInfo());
        }
    }
}
