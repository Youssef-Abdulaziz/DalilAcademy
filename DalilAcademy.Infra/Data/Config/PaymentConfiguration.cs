using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Data.Config
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount).HasPrecision(10, 2);
            builder.Property(p => p.Currency).IsRequired().HasMaxLength(3);
            builder.Property(p => p.PayPalOrderId).IsRequired().HasMaxLength(100);
            builder.HasIndex(p => p.PayPalOrderId).IsUnique();
            builder.Property(p => p.Status).HasConversion<string>().HasMaxLength(20);
            builder.HasOne(p => p.Enrollment)
                .WithMany(e => e.Payments)
                .HasForeignKey(p => p.EnrollmentId);
        }
    }
}
