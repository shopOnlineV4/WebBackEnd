using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.DBConfig
{
    class TypeProductConfig : IEntityTypeConfiguration<TypeProduct>
    {
       public void Configure(EntityTypeBuilder<TypeProduct> builder)
        {
            builder.ToTable("TypeProducts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ColorId).IsRequired();
            builder.Property(x => x.SizeId).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.TypeProducts).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.ColorCode).WithMany(x => x.typeProducts).HasForeignKey(x => x.ColorId);
            builder.HasOne(x => x.Size).WithMany(x => x.typeProducts).HasForeignKey(x => x.SizeId);
        }
    }
}
