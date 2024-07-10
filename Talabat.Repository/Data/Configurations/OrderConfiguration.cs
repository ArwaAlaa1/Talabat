﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, SA => SA.WithOwner());

            builder.Property(o => o.Status)
                .HasConversion(
                os => os.ToString(),
                os => (OrderStatus)Enum.Parse(typeof(OrderStatus),os));

            builder.Property(o=>o.SubTotal)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.deliverymethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
