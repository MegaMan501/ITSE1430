using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLib.Data.Memory
{
    /// <summary>Provides an implementation of <see cref="IMovieDatabase"/> using an in-memory collection.</summary>
    public class MemoryMovieDatabase : MovieDatabase
    {
        /// <summary>Add a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added product.</returns>
        protected override Movie AddCore( Movie movie )
        {
            var newMovie = Clone(movie);
            newMovie.Id = _id++;
            _movies.Add(newMovie);

            return Clone(newMovie);
        }

        /// <summary>Finds a movie by its title.</summary>
        /// <param name="title">The title to find.</param>
        /// <returns>The movie, if any.</returns>
        protected override Movie FindByTitleCore( string title )
        {
            return _movies.FirstOrDefault(m => String.Compare(m.Title, title, true) == 0);
        }


        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        protected override Movie GetCore( int id )
        {
            var movie = FindMovie(id);

            return Clone(movie); 
        }

        /// <summary>Get all movies.</summary>
        /// <returns>The movies.</returns>
        protected override IEnumerable<Movie> GetAllCore ()
        {
            return from item in _movies
                   select Clone(item);
        }

        /// <summary>Removes the movie.</summary>
        /// <param name="id">The movie to remove.</param>
        protected override void RemoveCore( int id )
        {
            var exisiting = FindMovie(id);
            if (exisiting != null)
                _movies.Remove(exisiting);
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to update</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore( Movie movie )
        {
            var existing = FindMovie(movie.Id);
            if (existing == null)
                throw new ArgumentException("Movie not found.", nameof(movie));

            Copy(existing, movie);
            
            return Clone(existing);
        }

        #region Private Members
        // Copies one movie to another
        private Movie Clone( Movie movie )
        {
            if (movie == null)
                return null;

            var target = new Movie();

            Copy(target, movie);
            target.Id = movie.Id;

            return target;
        }

        // Updates a target with the values from the source, except the Id
        private void Copy( Movie target, Movie source )
        {
            target.Title = source.Title;
            target.Description = source.Description;
            target.IsOwned = source.IsOwned;
            target.Length = source.Length;
        }

        // Find a movie by ID
        private Movie FindMovie( int id )
        {
            return _movies.FirstOrDefault(m => m.Id == id);
        }

        private List<Movie> _movies = new List<Movie>();
        private int _id = 1;

        #endregion

    }
}
