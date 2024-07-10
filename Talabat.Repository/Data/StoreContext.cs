﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }

        public DbSet<ProductType> productTypes { get; set; }

        public DbSet<Order> order { get; set; }

        public DbSet<OrderItem> orderItems { get; set; }

        public DbSet<DeliveryMethod> deliveryMethods { get; set; }


    }
}
