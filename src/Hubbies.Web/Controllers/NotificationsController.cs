using Hubbies.Application.Features.Notifications;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class NotificationsController(INotificationService notificationService)
    : ControllerBase
{
    /// <summary>
    /// Get all notifications of the current user
    /// </summary>
    /// <response code="200">Returns all notifications</response>
    [Authorize]
    [HttpGet(Name = "GetNotifications")]
    [ProducesResponseType(typeof(IEnumerable<NotificationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotificationsAsync([FromQuery] NotificationQueryParameter queryParameter)
    {
        var response = await notificationService.GetNotificationsAsync(queryParameter);

        return Ok(response);
    }

    /// <summary>
    /// Get a notification by id of the current user
    /// </summary>
    /// <param name="notificationId"></param>
    /// <response code="200">Returns notification</response>
    /// <response code="404">Notification not found</response>
    [Authorize]
    [HttpGet("{notificationId:guid}", Name = "GetNotification")]
    [ProducesResponseType(typeof(NotificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotificationAsync(Guid notificationId)
    {
        var response = await notificationService.GetNotificationAsync(notificationId);

        return Ok(response);
    }

    /// <summary>
    /// Mark a notification as read
    /// </summary>
    /// <param name="notificationId"></param>
    /// <response code="204">Notification marked as read</response>
    /// <response code="404">Notification not found</response>
    [Authorize]
    [HttpPut("{notificationId:guid}", Name = "MarkNotificationAsRead")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkNotificationAsReadAsync(Guid notificationId)
    {
        await notificationService.MarkNotificationAsReadAsync(notificationId);

        return NoContent();
    }

    /// <summary>
    /// Mark all unread notifications as read
    /// </summary>
    /// <response code="204">All unread notifications marked as read</response>
    [Authorize]
    [HttpPut(Name = "MarkAllNotificationsAsRead")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> MarkAllNotificationsAsReadAsync()
    {
        await notificationService.MarkAllNotificationsAsReadAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a notification
    /// </summary>
    /// <param name="notificationId"></param>
    /// <response code="204">Notification deleted</response>
    /// <response code="404">Notification not found</response>
    [Authorize]
    [HttpDelete("{notificationId:guid}", Name = "DeleteNotification")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNotificationAsync(Guid notificationId)
    {
        await notificationService.DeleteNotificationAsync(notificationId);

        return NoContent();
    }
}
