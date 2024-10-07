namespace Hubbies.Infrastructure.Persistence.Configurations;

public class FeedbackConfig : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(x => new { x.UserId, x.TicketEventId });

        builder.HasOne(x => x.TicketEvent)
            .WithMany(x => x.Feedbacks)
            .HasForeignKey(x => x.TicketEventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Feedbacks)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Status)
            .HasConversion(
                v => v.ToString(),
                v => (FeedbackStatus)Enum.Parse(typeof(FeedbackStatus), v)
            );
    }
}
