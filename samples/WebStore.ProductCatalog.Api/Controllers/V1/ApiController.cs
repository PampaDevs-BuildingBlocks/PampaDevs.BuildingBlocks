using MediatR;
using Microsoft.AspNetCore.Mvc;
using PampaDevs.BuildingBlocks.Application;
using PampaDevs.Bus;
using PampaDevs.Bus.InProc.Notifications;

namespace WebStore.ProductCatalog.Api.Controllers.V1
{
    public abstract class ApiController : ControllerBase
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IDomainDispatcher _dispatcher;

        public ApiController(INotificationHandler<DomainNotification> notifications, IDomainDispatcher dispatcher)
        {
            _notifications = notifications as DomainNotificationHandler;
            _dispatcher = dispatcher;
        }

        protected bool IsValidOperation() => !_notifications.HasNotifications();

        protected ActionResult GetResponse(object result = null)
        {
            if (IsValidOperation()) return Ok(new ApiResponse(result, "The resource has been fetched successfully"));

            return BadRequest(new ApiResponse(_notifications.GetNotificationErrors()));
        }

        protected ActionResult PostResponse(string actionName, object result = null)
        {
            if (IsValidOperation()) return CreatedAtAction(actionName, new ApiResponse(result, "The resource was successfully created."));

            return BadRequest(new ApiResponse(_notifications.GetNotificationErrors()));
        }

        protected ActionResult PutResponse()
        {
            if (IsValidOperation()) return Ok(new ApiResponse("The resource was updated successfully"));

            return BadRequest(new ApiResponse(_notifications.GetNotificationErrors()));
        }

        protected ActionResult DeleteResponse()
        {
            if (IsValidOperation()) return Ok(new ApiResponse("The resource was deleted successfully"));

            return BadRequest(new ApiResponse(_notifications.GetNotificationErrors()));
        }
    }
}