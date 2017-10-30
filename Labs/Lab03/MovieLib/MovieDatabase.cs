using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib
{
    /// <summary>Base class for movie database.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        /// <summary>Adds a movie.</summary>
        /// <param name="movies">The movie to add.</param>
        /// <returns>The added product.</returns>
        public Movies Add( Movies movies )
        {
            // Validate
            if (movies == null)
                return null;

            // Using IValidatableObject
            if (!ObjectValidator.TryValidate(movies, out var errors))
                return null;

            return AddCore(movies);
        }

        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        public Movies Get( int id )
        {
            // Validate
            if (id <= 0)
                return null;

            return GetCore(id);
        }

        /// <summary>Gell all of the movies.</summary>
        /// <returns>The movies.</returns>
        public IEnumerable<Movies> GetAll ()
        {
            return GetAllCore();
        }

        /// <summary>Removes the movie.</summary>
        /// <param name="id">The movie to remove.</param>
        public void Remove( int id )
        {
            if (id <= 0)
                return;

            RemoveCore(id);
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movies">The movie to be updated.</param>
        /// <returns>The updated movie.</returns>
        public Movies Update( Movies movies )
        {
            // Validate
            if (movies == null)
                return null;

            // Using IValidatableObject
            if (!ObjectValidator.TryValidate(movies, out var errors))
                return null;

            // Get existing movie
            var existing = GetCore(movies.Id);
            if (existing == null)
                return null;

            return UpdateCore(existing, movies);
        }

        #region Protected Members

        protected abstract Movies GetCore( int id );

        protected abstract IEnumerable<Movies> GetAllCore();

        protected abstract void RemoveCore( int id );

        protected abstract Movies UpdateCore( Movies existing, Movies movie );

        protected abstract Movies AddCore( Movies movies );

        #endregion
    }
}
