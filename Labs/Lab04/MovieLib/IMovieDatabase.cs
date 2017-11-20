using System.Collections.Generic;


namespace MovieLib
{
    /// <summary> Provide a database of <see cref="Movie"/> items. </summary>
    public interface IMovieDatabase
    {
        /// <summary>Add a movie to database.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The newly added movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        Movie Add( Movie movie );

        /// <summary>Get a specific movie from the database.</summary>
        /// <param name="id">The unique ID of the movie.</param>
        /// <returns>The movie with the given ID.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> must be greater than or equal to 0.</exception> 
        Movie Get( int id );

        /// <summary>Get all the movies from the database</summary>
        /// <returns>An enumerable list of movies</returns>
        IEnumerable<Movie> GetAll();

        /// <summary>Removes a specific movie from database.</summary>
        /// <param name="id">The unique ID of the movie.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> must be greater than or equal to 0.</exception>
        void Remove( int id );

        /// <summary>Updates a movie in the database</summary>
        /// <param name="movie">The updated movie information.</param>
        /// <returns>The updated movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="Exception">Product not found.</exception>
        Movie Update( Movie movie );

    }
}
