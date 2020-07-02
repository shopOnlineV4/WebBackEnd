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
         
        }
    }
}
