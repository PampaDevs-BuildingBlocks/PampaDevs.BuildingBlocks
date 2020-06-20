using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PampaDevs.BuildingBlocks.Infrastructure;

namespace WebStore.ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        [Route("info")]
        public ActionResult<SystemInfo> Info()
        {
            return new JsonResult(Assembly.GetExecutingAssembly().GetSystemInfo());
        }
    }
}
