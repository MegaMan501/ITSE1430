using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLib.Data.Memory
{
    public class MemoryMovieDatabase : MovieDatabase
    {
        /// <summary>Add a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added product.</returns>
        protected override Movie AddCore( Movie movie )
        {
            // Return null if the movie already exists
            foreach (var _movies in GetAll())
            {
                if (_movies.Title == movie.Title)
                    return null;
            };

            var newMovie = CopyMovie(movie);
            _movies.Add(newMovie);

            if (newMovie.Id <= 0)
                newMovie.Id = _nextId++;
            else if (newMovie.Id >= _nextId)
                _nextId = newMovie.Id + 1;

            return CopyMovie(newMovie);
        }

        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        protected override Movie GetCore( int id )
        {
            var movie = FindMovie(id);

            return (movie != null) ? CopyMovie(movie) : throw new Exception("Movie not in memory."); 
        }

        /// <summary>Get all movies.</summary>
        /// <returns>The movies.</returns>
        protected override IEnumerable<Movie> GetAllCore ()
        {
            return from item in _movies
                   select CopyMovie(item);
        }

        /// <summary>Removes the movie.</summary>
        /// <param name="id">The movie to remove.</param>
        protected override void RemoveCore( int id )
        {
            var movie = FindMovie(id);
            if (movie != null)
                _movies.Remove(movie);
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to update</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore( Movie existing, Movie newMovie )
        {
            existing = FindMovie(newMovie.Id);
            _movies.Remove(existing);

            var copyMovie = CopyMovie(newMovie);
            _movies.Add(copyMovie);

            return CopyMovie(newMovie);
        }
        
        // Copies one movie to another
        private Movie CopyMovie ( Movie movie)
        {
            if (movie == null)
                return null;

            var newMovie = new Movie() {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Length = movie.Length,
                IsOwned = movie.IsOwned
            };

            return newMovie; 
        }

        // Find a movie by ID
        private Movie FindMovie ( int id )
        {
            return (from movie in _movies
                    where movie.Id == id
                    select movie).FirstOrDefault(); 
        }

        private List<Movie> _movies = new List<Movie>();
        private int _nextId = 1; 
    }
}
