using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary> Represents a product. </summary>
    /// <remarks> 
    /// This will represent a product with other stuff.
    /// </remarks>
    public class Product  : IValidatableObject
    {
        public Product()
        {
            //Cross field initialization
        }
        //public readonly Product None = new Product();           // set at the moment the instance is created; readonly does not cascade 

        /// <summary> Gets or sets the unique identifier </summary>
        public int Id { get; set; }
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

        public override string ToString()
        {
            return Name; 
        }

        // Size of the product // Copy an array // waste of memory, don't do it
        public int[] Sizes
        {
            get 
            {
                var copySizes = new int[_sizes.Length];
                Array.Copy(_sizes, copySizes, _sizes.Length);

                return copySizes; 
            }
        }

        private int[] _sizes = new int[4];
        
        //public abstract string Validate2();

        /// <summary> Validate the object</summary>
        /// <returns> The error message for null.</returns>
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            //var errors = new List<ValidationResult>();

            // Name cannot be empty
            if (String.IsNullOrEmpty(Name))
                yield return new ValidationResult("Name cannot be empty", new[] { nameof(Name) });
            //errors.Add(new ValidationResult("Name cannot be empty", new[] { nameof(Name) }));

            // Price >= 0
            if (Price < 0)
                yield return new ValidationResult("Price must be >= 0", new[] { nameof(Price) });
            //errors.Add(new ValidationResult("Price must be >= 0", new[] { nameof(Price) }));

            //return errors;

        }

        //public int ICanOnlySetIt { get; private set; }          // Limitation: the get or set need to be more private the field
        //public int ICanOnlySetIt2 { get; }

        private string _name;
        private string _description;

        //private readonly double _someValueICannotChange = 10;   // Fix at moment that there is an instance of this class; readonly does not need to inialized
    }
}
