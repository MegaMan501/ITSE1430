﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLib
{
    /// <summary> Represents a Movie. </summary>
    /// <remarks> This class with hold everything that a movie could hold. </remarks>
    public class Movie : IValidatableObject
    {
        /// <summary>Get or sets the unique identifier.</summary>
        public int Id { get; set; }

        /// <summary> Get or sets the Movie Title. </summary>
        /// <value> Never returns null. </value>
        public string Title
        {
            get => _tile ?? ""; 
            set => _tile = value?.Trim(); 
        }

        /// <summary> Gets or sets the Movie Description. </summary>
        public string Description
        {
            get => _description ?? ""; 
            set => _description = value?.Trim();
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

        #region Private Members

        private string _tile;
        private string _description;
        
        #endregion
    }
}
