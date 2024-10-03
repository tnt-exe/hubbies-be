namespace Hubbies.Infrastructure.Persistence.Configurations;

public class OrderDetailsConfig : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.HasKey(x => new { x.OrderId, x.TicketEventId });

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.TicketEvent)
            .WithMany()
            .HasForeignKey(x => x.TicketEventId);
    }
}
