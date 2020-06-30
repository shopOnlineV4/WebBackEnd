using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.DBConfig
{
    internal class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Quality).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.TypeId).IsRequired();
            builder.HasOne(x => x.Oder).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.TypeProduct).WithMany(x => x.OrderDetails).HasForeignKey(x => x.TypeId);

        }
    }
}