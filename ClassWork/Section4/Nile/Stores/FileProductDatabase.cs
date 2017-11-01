/*
 * ITSE 1430
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace Nile.Stores
{
    /// <summary>Provides an implementation of <see cref="IProductDatabase"/> using a memory collection.</summary>
    public class FileProductDatabase : MemoryProductDatabase
    {
        /// <summary></summary>
        /// <param name="fileName"></param>
        public FileProductDatabase ( string fileName)
        {
            // Validate argument
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("Filename cannot be empty", nameof(fileName));

            _fileName = fileName;

            LoadFile(fileName);
         }
        
        /// <summary>Adds a product.</summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        protected override Product AddCore ( Product product )
        {
            var newProduct = base.AddCore(product);

            SaveFile(_fileName);

            return newProduct;
        }

        /// <summary>Removes the product.</summary>
        /// <param name="product">The product to remove.</param>
        protected override void RemoveCore ( int id )
        {
            base.RemoveCore(id);

            SaveFile(_fileName);
        }

        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        protected override Product UpdateCore ( Product existing, Product product )
        {
            var newProduct = base.UpdateCore(existing, product);
            SaveFile(_fileName);

            return newProduct; 
        }
        
        private void LoadFile( string fileName)
        {
            if (!File.Exists(fileName))
                return; 

            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                if (String.IsNullOrEmpty(line))
                    continue;

                var fields = line.Split(',');
                var product = new Product() {
                    Id = Int32.Parse(fields[0]),
                    Name = fields[1],
                    Description = fields[2],
                    Price = Decimal.Parse(fields[3]),
                    IsDiscontinued = Boolean.Parse(fields[4])
                };

                base.AddCore(product);
            };
        }

        private void SaveFile(string fileName)
        {
            // Streaming Mode
            //StreamWriter writer = null;
            //var stream = File.OpenWrite(fileName);
            //try
            //{
            //    // Write Stuff
            //    writer = new StreamWriter(stream);
            //} finally
            //{
            //    writer?.Dispose();
            //    stream.Close();
            //};
            using (var writer = new StreamWriter(fileName))
            {
                // Write Stuff
                foreach (var product in GetAllCore())
                {
                    var row = String.Join(",", 
                        product.Id, 
                        product.Name, 
                        product.Description, 
                        product.Price, 
                        product.IsDiscontinued);

                    writer.WriteLine(row);
                };
                
            };
            
        }

        private readonly string _fileName; 
    }
}
