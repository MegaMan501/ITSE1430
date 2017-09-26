﻿using System;
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
        //public readonly Product None = new Product();           // set at the moment the instance is created; readonly does not cascade 

        /// <summary>Get or Set the Name</summary>
        public string Name
        {
            // String get_Name ()
            get                                             // Get {} always has a return statement
            {
                return _name ?? "";     
            }

            // void set_Name ( string value )
            set
            {
                _name = value?.Trim();
            }
        }

        /// <summary>Get or Set the Description</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value?.Trim(); }
        }

        /// <summary>Get or Set the Price</summary>
        public decimal Price { get; set; } = 0;                 // auto property

        /// <summary>Is it Discontinued</summary>
        public bool IsDiscontinued { get; set; }                //auto property

        public const decimal DiscontinuedDiscountRate = 0.10M;
        
        /// <summary> Gets the discounted price, if applicable. </summary>
        public decimal DiscountedPrice                          // calculated property, there is no backing field
        {
            get {
                //if (IsDiscontinued)
                if (this.IsDiscontinued)
                    return Price * DiscontinuedDiscountRate;

                return Price;
            }
        }

        public int ICanOnlySetIt { get; private set; }          // Limitation: the get or set need to be more private the field
        public int ICanOnlySetIt2 { get; }

        public void Foo (string name)
        {
            name = "Hello"; 
        }

        private string _name;
        private string _description;

        private readonly double _someValueICannotChange = 10;   // Fix at moment that there is an instance of this class; readonly does not need to inialized
    }
}