using Hubbies.Application.Features.Feedbacks;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface IFeedbackService
{
    /// <summary>
    /// Get feedbacks with pagination
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns>
    /// The task result contains a <see cref="PaginationResponse{FeedbackDto}"/> object.
    /// </returns>
    Task<PaginationResponse<FeedbackDto>> GetFeedbacksAsync(FeedbackQueryParameter queryParameter);

    /// <summary>
    /// Get feedback by user id and ticket event id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ticketEventId"></param>
    /// <returns>
    /// The task result contains a <see cref="FeedbackDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Feedback not found</exception>
    Task<FeedbackDto> GetFeedbackAsync(Guid userId, Guid ticketEventId);

    /// <summary>
    /// Create feedback
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// The task result contains a <see cref="FeedbackDto"/> object.
    /// </returns>
    /// <exception cref="ValidationException">Validation error</exception>
    Task<FeedbackDto> CreateFeedbackAsync(CreateFeedbackRequest request);

    /// <summary>
    /// Update feedback
    /// </summary>
    /// <param name="ticketEventId"></param>
    /// <param name="request"></param>
    /// <returns>
    /// The task result contains a <see cref="FeedbackDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Feedback not found</exception>
    /// <exception cref="ValidationException">Validation error</exception>
    Task<FeedbackDto> UpdateFeedbackAsync(Guid ticketEventId, UpdateFeedbackRequest request);

    /// <summary>
    /// Delete feedback
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ticketEventId"></param>
    /// <exception cref="NotFoundException">Feedback not found</exception>
    Task DeleteFeedbackAsync(Guid userId, Guid ticketEventId);

    /// <summary>
    /// Feedback approval
    /// </summary>
    /// <param name="approvalRequest"></param>
    /// <exception cref="NotFoundException">Feedback not found</exception>
    Task FeedbackApprovalAsync(FeedbackApprovalRequest approvalRequest);
}
