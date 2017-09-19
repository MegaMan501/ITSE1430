using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary> Represents a product. </summary>
    /// <remarks> 
    /// This will represent a product with other stuff.
    /// </remarks>
    public class Product
    {
        /// <summary>Get or Set the Name</summary>
        public string Name;

        /// <summary>Get or Set the Description</summary>
        public string Description;

        /// <summary>Get or Set the Price</summary>
        public decimal Price;

        /// <summary>Is it Discontinued</summary>
        public bool IsDiscontinued;
    }
}
