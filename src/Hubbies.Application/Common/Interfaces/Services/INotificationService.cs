using Hubbies.Application.Features.Notifications;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface INotificationService
{
    /// <summary>
    /// Get all notifications of the current user
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns>
    /// The task result contains a collection of <see cref="NotificationDto"/> objects.
    /// </returns>
    Task<IEnumerable<NotificationDto>> GetNotificationsAsync(NotificationQueryParameter queryParameter);

    /// <summary>
    /// Get a notification by id of the current user
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    /// The task result contains a <see cref="NotificationDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown when the notification is not found.</exception>
    Task<NotificationDto> GetNotificationAsync(Guid id);

    /// <summary>
    /// Mark a notification as read
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundException">Thrown when the notification is not found.</exception>
    Task MarkNotificationAsReadAsync(Guid id);

    /// <summary>
    /// Mark all unread notifications as read
    /// </summary>
    Task MarkAllNotificationsAsReadAsync();

    /// <summary>
    /// Delete a notification
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundException">Thrown when the notification is not found.</exception>
    Task DeleteNotificationAsync(Guid id);
}
