using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib
{
    /// <summary> Represents a Movie. </summary>
    /// <remarks> This class with hold everything that a movie could hold. </remarks>
    public class Movies
    {
        public Movies ()
        {
            // Cross Field initialization
        }

        /// <summary> Get or sets the Movie Title. </summary>
        /// <value> Never returns null. </value>
        public string MovieTitle
        {
            get { return _movieTitle ?? ""; }
            set { _movieTitle = value?.Trim(); }
        }

        /// <summary> Gets or sets the Movie Description. </summary>
        public string MovieDescription
        {
            get { return _movieDescription ?? ""; }
            set { _movieDescription = value?.Trim(); }
        }

        /// <summary> Gets or sets the Movies Length </summary>
        public int MovieLength { get; set; } = 0; 

        /// <summary> Determines if Movie is Owned. </summary> 
        public bool MovieIsOwned { get; set; }

        /// <summary> Validates the object. </summary>
        /// <returns> The error message or null. </returns>
        public virtual string Validate()
        {
            // Title cannot be empty
            if (String.IsNullOrEmpty(MovieTitle))
                return "Title Cannot Be Empty!";

            // Length >= 0
            if (MovieLength < 0)
                return "Length must be >= 0";

            return null; 
        }

        private string _movieTitle;
        private string _movieDescription;
    }
}
