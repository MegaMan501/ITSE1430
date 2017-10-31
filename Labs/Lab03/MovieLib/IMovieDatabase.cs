using System.Collections.Generic;


namespace MovieLib
{
    /// <summary> Provide a database of <see cref="Movies"/> items. </summary>
    public interface IMovieDatabase
    {
        /// <summary>Add a movie to database.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The newly added movie.</returns>
        Movies Add( Movies movie );

        /// <summary>Get a specific movie from the database.</summary>
        /// <param name="id">The unique ID of the movie.</param>
        /// <returns>The movie with the given ID.</returns>
        Movies Get( int id );

        /// <summary>Get all the movies from the database</summary>
        /// <returns>An enumerable list of movies</returns>
        IEnumerable<Movies> GetAll();

        /// <summary>Removes a specific movie from database.</summary>
        /// <param name="id">The unique ID of the movie.</param>
        void Remove( int id );

        /// <summary>Updates a movie in the database</summary>
        /// <param name="movie">The updated movie information.</param>
        /// <returns>The updated movie.</returns>
        Movies Update( Movies movie );

    }
}
