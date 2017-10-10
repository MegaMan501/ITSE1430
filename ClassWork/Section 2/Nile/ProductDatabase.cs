using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary> Base class for public database </summary>
    public class ProductDatabase
    {
        public ProductDatabase()
        {
            _products[0] = new Product();
            _products[0].Name = "Blu Phone X";
            _products[0].Price = 250;

            _products[1] = new Product();
            _products[1].Name = "Iphone X";
            _products[1].Price = 1000;
            _products[1].IsDiscontinued = true; 

            _products[2] = new Product();
            _products[2].Name = "Windows Phone";
            _products[2].Price = 650;

            _products[3] = new Product();
            _products[3].Name = "Samsung Galaxy";
            _products[3].Price = 300;
            _products[3].IsDiscontinued = false;
        }

        /// <summary> Adds a product </summary>
        /// <param name="product"> The product </param>
        /// <returns> The added product </returns>
        public Product Add ( Product product)
        {
            // TODO: Implement Add
            return null; 
        }

        /// <summary> Get a specific product 
        /// <returns>The product, if it exists </returns>
        public Product Get ()
        {
            // TODO: Implement Get
            return null; 
        }

        /// <summary> Gets all products </summary>
        /// <returns> The products</returns>
        public Product[] GetAll()
        {
            var items = new Product[_products.Length];
            var index = 0; 

            foreach (var product in _products)
            {
                items[index++] = CopyProduct(product);
            };

            return items;
        }

        /// <summary> Removes the product </summary>
        /// <param name="product"> The product to remove</param>
        public void Remove(Product product)
        {
            // TODO: Implement Remove
        }

        /// <summary> Updates a product </summary>
        /// <param name="product">The product to update</param>
        /// <returns>The updated product</returns>
        public Product Update ( Product product)
        {
            // TODO: Implement Update
            return null;
        }

        private Product CopyProduct ( Product product)
        {
            if (product == null)
                return null; 

            var newProduct = new Product();
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.IsDiscontinued = product.IsDiscontinued;

            return newProduct;
        }
        private Product[] _products = new Product[100];

    }
}
