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
    public class Product : IValidatableObject
    {
        /// <summary> Gets or sets the unique identifier </summary>
        public int Id { get; set; }

        /// <summary>Get or Set the Name</summary>
        public string Name
        {
            // String get_Name ()
            get { return _name ?? ""; }
            // void set_Name ( string value )
            set{_name = value?.Trim();}
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
      
        //IEnumerable<ValidationResult> IValidatableObject.Validate( ValidationContext validationContext )
        //{

        //}

        /// <summary> Validate the object</summary>
        /// <returns> The error message for null.</returns>
        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
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

        private string _name;
        private string _description;
    }
}
