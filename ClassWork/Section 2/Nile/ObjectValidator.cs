using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary>Validate object</summary>
    public class ObjectValidator
    {
        public static bool TryValidate( IValidatableObject value, IEnumerable<ValidationResult> errors )
        {
            var context = new ValidationContext(value);
            var results = new List<ValidationResult>();

            errors = results;
            return Validator.TryValidateObject(value, context, results);
        }
    }
}
