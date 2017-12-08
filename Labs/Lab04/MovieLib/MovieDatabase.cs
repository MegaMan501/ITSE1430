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
            
            ObjectValidator.ValidateObject(movie);

            // Movie Cannot already exist
            var existing = FindByTitleCore(movie.Title);
            if (existing != null)
                throw new ArgumentException("Movie with same title already exist.", nameof(movie));

            // Create the new movie
            return AddCore(movie);
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
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        public bool Remove( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0");

            var existing = GetCore(id);
            if (existing == null)
                return false; 

            RemoveCore(id);
            return true; 
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

            ObjectValidator.ValidateObject(movie);

            // Get existing movie
            var existing = GetCore(movie.Id); 
            if (existing == null)
                throw new Exception("Movie not found.");

            // Movie Title cannot already exists
            existing = FindByTitleCore(movie.Title);
            if (existing != null && existing.Id != movie.Id)
                throw new ArgumentException("Movie with same title already exists.", nameof(movie));

            return UpdateCore(movie);
        }

        #region Protected Members
        
        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected abstract Movie AddCore( Movie movie );

        /// <summary>Finds a movie by its title.</summary>
        /// <param name="title">The title.</param>
        /// <returns>The movie, if any.</returns>
        protected abstract Movie FindByTitleCore( string title );

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
        /// <param name="movie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected abstract Movie UpdateCore( Movie movie );
               
        #endregion
    }
}
