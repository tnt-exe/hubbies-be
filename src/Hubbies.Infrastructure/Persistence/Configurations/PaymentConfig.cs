namespace Hubbies.Infrastructure.Persistence.Configurations;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.PaymentReference);
    }
}
