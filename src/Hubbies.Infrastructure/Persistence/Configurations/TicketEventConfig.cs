using Hubbies.Domain.Enums;

namespace Hubbies.Infrastructure.Persistence.Configurations;

public class TicketEventConfig : IEntityTypeConfiguration<TicketEvent>
{
    public void Configure(EntityTypeBuilder<TicketEvent> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasMany(x => x.Feedbacks)
            .WithOne(x => x.TicketEvent)
            .HasForeignKey(x => x.TicketEventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.EventCategory)
            .WithMany(x => x.TicketEvents)
            .HasForeignKey(x => x.EventCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Status)
            .HasConversion(
                v => v.ToString(),
                v => (TicketEventStatus)Enum.Parse(typeof(TicketEventStatus), v)
            );

        builder.Property(x => x.ApprovalStatus)
            .HasConversion(
                v => v.ToString(),
                v => (TicketApprovalStatus)Enum.Parse(typeof(TicketApprovalStatus), v)
            );
    }
}
