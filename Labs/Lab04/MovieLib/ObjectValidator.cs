using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib
{
    public class ObjectValidator
    {
        /// <summary>Validates an object.</summary>
        /// <param name="value">The object to validate.</param>
        public static void ValidateObject( IValidatableObject value )
        {
            Validator.ValidateObject(value, new ValidationContext(value), true);
        }

        /// <summary>Tries to validate an object and returns the results.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The validation results.</returns>
        public static IEnumerable<ValidationResult> TryValidateObject ( IValidatableObject value)
        {
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(value, new ValidationContext(value), results))
                return results; 

            return Enumerable.Empty<ValidationResult>();
        }
    }
}
