using Domain.Models.Entities;
using Domain.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.DBConfig
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.StatusOrder).HasDefaultValue(StatusOrder.Ordered);
            builder.Property(x => x.PayWays).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
          

        }
    }
}