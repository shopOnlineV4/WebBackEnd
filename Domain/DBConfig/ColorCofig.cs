using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DBConfig
{
    class ColorCofig : IEntityTypeConfiguration<ColorCode>
    {
        public void Configure(EntityTypeBuilder<ColorCode> builder)
        {
            builder.ToTable("ColorCodes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ColorData).IsRequired().HasMaxLength(50);

        }
    }
}
