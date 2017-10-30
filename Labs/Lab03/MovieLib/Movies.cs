using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib
{
    /// <summary> Represents a Movie. </summary>
    /// <remarks> This class with hold everything that a movie could hold. </remarks>
    public class Movies : IValidatableObject
    {
        /// <summary>Get or sets the unique identifier.</summary>
        public int Id { get; set; }

        /// <summary> Get or sets the Movie Title. </summary>
        /// <value> Never returns null. </value>
        public string Title
        {
            get { return _movieTitle ?? ""; }
            set { _movieTitle = value?.Trim(); }
        }

        /// <summary> Gets or sets the Movie Description. </summary>
        public string Description
        {
            get { return _movieDescription ?? ""; }
            set { _movieDescription = value?.Trim(); }
        }

        /// <summary> Gets or sets the Movies Length </summary>
        public int Length { get; set; } = 0; 

        /// <summary> Determines if Movie is Owned. </summary> 
        public bool IsOwned { get; set; }

        /// <summary> Validates the object. </summary>
        /// <returns> The error message or null. </returns>
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            // Title cannot be empty
            if (String.IsNullOrEmpty(Title))
                yield return new ValidationResult("Title Cannot Be Empty!", new[] { nameof(Title) });

            // Length >= 0
            if (Length < 0)
                yield return new ValidationResult("Length must be >= 0", new[] { nameof(Length) });
        }

        private string _movieTitle;
        private string _movieDescription;
    }
}
