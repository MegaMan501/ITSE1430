/*
 * ITSE 1430
 */
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Nile
{
    /// <summary>Provides a <see cref="ProductDatabaseExtensions"/> with products already added.</summary>
    public static class ProductDatabaseExtensions
    {
        /// <summary>Get a product by name.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The product name.</param>
        /// <returns>The product, if found.</returns>
        public static Product GetByName ( this IProductDatabase source, string name)
        {
            foreach ( var item in source.GetAll())
            {
                if (String.Compare(item.Name, name, true) == 0)
                    return item; 
            };

            return null; 
        }

        public static IEnumerable<Product> GetProductByDiscountPrice ( this IProductDatabase source,
                                                                       Func<Product, decimal> priceCalculator )
        {
            // Orderby name, price
            //.OrderBy ( x => x.Name).ThenBy(x => x.price)

            var products = from product in source.GetAll()
                           where product.IsDiscontinued
                           //select new SomeType() {
                           select new {
                               Product = product,
                               AdjustPrice = product.IsDiscontinued ? priceCalculator(product) : product.Price
                           };

            // Instead of anonymous type (using tuple)
            //var tuple = Tuple.Create<Product, decimal>(new Product(), 10M); 

            return from product in products
                   orderby product.AdjustPrice
                   select product.Product;
        }

        //private (Product : Product , AdjustedPrice : decimal) DoSomething ()
        //{
        //    return (new Product, 10M); 
        //}

        //private sealed class SomeType
        //{
        //    public Product Product { get; set; }
        //    public decimal AdjustPrice { get; set; }
        //}

        /// <summary>Initializes an instance of the <see cref="SeedMemoryProductDatabase"/> class.</summary>
        /// <params name="database">The data to seed.</params>
        public static void WithSeedData ( this IProductDatabase source)
        {
            source.Add(new Product() { Name = "Galaxy S7", Price = 650 });
            source.Add(new Product() { Name = "Galaxy Note 7", Price = 150, IsDiscontinued = true });
            source.Add(new Product() { Name = "Windows Phone", Price = 100 });
            source.Add(new Product() { Name = "iPhone X", Price = 1900, IsDiscontinued = true });
        }

    }
}
