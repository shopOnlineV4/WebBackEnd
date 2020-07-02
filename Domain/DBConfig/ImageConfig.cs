using Domain.Models.Entities;
using Domain.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Domain.DBConfig
{
    internal class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.CreateBy).IsRequired();
            builder.Property(x => x.DateCreate).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreateBy).IsRequired();
            builder.Property(x => x.ModifiedBy);
            builder.Property(x => x.DateModified);
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}