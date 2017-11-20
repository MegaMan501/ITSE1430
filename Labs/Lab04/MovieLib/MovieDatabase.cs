using System;
using System.Collections.Generic;

namespace MovieLib
{
    /// <summary>Base class for movie database.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added product.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidateException"><paramref name="movie"/> is invalid.</exception>
        public Movie Add( Movie movie )
        {
            // Validate
            if (movie == null)
                throw new ArgumentNullException(nameof(movie), "Movie was null.");
            
            ObjectValidator.Validate(movie);
         
            try
            {
                return AddCore(movie);
            } catch (Exception e)
            {
                throw new Exception("Add failed", e);
                throw;
            };
        }

        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> must be greater than or equal to 0.</exception>
        public Movie Get( int id )
        {
            // Validate
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0");

            return GetCore(id);
        }

        /// <summary>Gell all of the movies.</summary>
        /// <returns>The movies.</returns>
        public IEnumerable<Movie> GetAll ()
        {
            return GetAllCore();
        }

        /// <summary>Removes the movie.</summary>
        /// <param name="id">The movie to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> must be greater than or equal to 0.</exception>
        public void Remove( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0");

            RemoveCore(id);
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to be updated.</param>
        /// <returns>The updated movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="Exception">Movie not found.</exception>
        public Movie Update( Movie movie )
        {
            // Validate
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            ObjectValidator.Validate(movie);

            // Get existing movie
            var existing = GetCore(movie.Id) ?? throw new Exception("Movie not found.");

            return UpdateCore(existing, movie);
        }

        #region Protected Members
        
        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected abstract Movie AddCore( Movie movie );

        /// <summary>Get a movie given its ID.</summary>
        /// <param name="id">The ID.</param>
        /// <returns>The movie, if any.</returns>
        protected abstract Movie GetCore( int id );

        /// <summary>Get all of the movies.</summary>
        /// <returns>All of the movies.</returns>
        protected abstract IEnumerable<Movie> GetAllCore();
        
        /// <summary>Removes a movie given an ID.</summary>
        /// <param name="id">The ID.</param>
        protected abstract void RemoveCore( int id );

        /// <summary>Updates a movie.</summary>
        /// <param name="existing">The existing movie.</param>
        /// <param name="newMovie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected abstract Movie UpdateCore( Movie existing, Movie newMovie );
               
        #endregion
    }
}
