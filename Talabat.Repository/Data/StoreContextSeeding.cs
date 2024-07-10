using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeeding
    {
        public static async Task SeedAsync( StoreContext context)
        {
            if (!context.productBrands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await context.productBrands.AddAsync(brand);

                       
                    } await context.SaveChangesAsync();
                }
            }

            //types
            if (!context.productTypes.Any())
            {
                var typessData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typessData);

                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await context.productTypes.AddAsync(type);

                        await context.SaveChangesAsync();
                    }
                }
            }

            //products
            if (!context.Product.Any())
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await context.Product.AddAsync(product);

                        await context.SaveChangesAsync();
                    }
                }
            }

            //products
            if (!context.deliveryMethods.Any())
            {
                var deliverysData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
                var deliverys = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverysData);

                if (deliverys?.Count > 0)
                {
                    foreach (var delivery in deliverys)
                    {
                        await context.deliveryMethods.AddAsync(delivery);

                        
                    }
                }await context.SaveChangesAsync();
            }

        }
    }
}
