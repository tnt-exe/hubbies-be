using Hubbies.Application.Features.Feedbacks;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface IFeedbackService
{
    Task<PaginationResponse<FeedbackDto>> GetFeedbacksAsync(FeedbackQueryParameter queryParameter);

    Task<FeedbackDto> GetFeedbackAsync(Guid userId, Guid ticketEventId);

    Task<FeedbackDto> CreateFeedbackAsync(CreateFeedbackRequest request);

    Task<FeedbackDto> UpdateFeedbackAsync(Guid ticketEventId, UpdateFeedbackRequest request);

    Task DeleteFeedbackAsync(Guid userId, Guid ticketEventId);

    Task FeedbackApprovalAsync(Guid userId, Guid ticketEventId, FeedbackStatus approvalStatus);
}
