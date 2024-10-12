namespace Hubbies.Application.Features.Notifications;

public class NotificationRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
    : BaseRepository(context, mapper, serviceProvider), INotificationService
{
    public async Task DeleteNotificationAsync(Guid id)
    {
        var notification = await Context.Notifications
            .Where(x => x.UserId == Guid.Parse(user.Id!))
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(Notification), id);

        Context.Notifications.Remove(notification);
        await Context.SaveChangesAsync();
    }

    public async Task<NotificationDto> GetNotificationAsync(Guid id)
    {
        var notification = await Context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == Guid.Parse(user.Id!))
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(Notification), id);

        return Mapper.Map<NotificationDto>(notification);
    }

    public async Task<IEnumerable<NotificationDto>> GetNotificationsAsync(NotificationQueryParameter queryParameter)
    {
        var notifications = await Context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == Guid.Parse(user.Id!))
            .Where(x => !x.IsDeleted)
            .Filter(queryParameter)
            .ToListAsync();

        return Mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task MarkAllNotificationsAsReadAsync()
    {
        await Context.Notifications
            .Where(x => x.UserId == Guid.Parse(user.Id!))
            .Where(x => !x.IsDeleted)
            .Where(x => !x.IsRead)
            .ExecuteUpdateAsync(x => x.SetProperty(y => y.IsRead, true));
    }

    public async Task MarkNotificationAsReadAsync(Guid id)
    {
        var notification = await Context.Notifications
            .Where(x => x.UserId == Guid.Parse(user.Id!))
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(Notification), id);

        notification.IsRead = true;

        await Context.SaveChangesAsync();
    }
}
