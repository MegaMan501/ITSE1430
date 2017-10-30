using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib.Data.Memory
{
    public class MemoryMovieDatabase : MovieDatabase
    {
        /// <summary>Add a movie.</summary>
        /// <param name="movies">The movie to add.</param>
        /// <returns>The added product.</returns>
        protected override Movies AddCore( Movies movies )
        {
            var newMovie = CopyMovie(movies);
            _movies.Add(newMovie);

            if (newMovie.Id <= 0)
                newMovie.Id = _nextId++;
            else if (newMovie.Id >= _nextId)
                _nextId = newMovie.Id + 1;

            return CopyMovie(newMovie);
        }

        /// <summary>Get a specific movie.</summary>
        /// <returns>The movie, if it exists.</returns>
        protected override Movies GetCore( int id )
        {
            var movie = FindMovie(id);

            return (movie != null) ? CopyMovie(movie) : null; 
        }

        /// <summary>Get all movies.</summary>
        /// <returns>The movies.</returns>
        protected override IEnumerable<Movies> GetAllCore ()
        {
            foreach (var movie in _movies)
                yield return CopyMovie(movie);
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
        protected override Movies UpdateCore( Movies existing, Movies movie )
        {
            existing = FindMovie(movie.Id);
            _movies.Remove(existing);

            var newMovie = CopyMovie(movie);
            _movies.Add(newMovie);

            return CopyMovie(newMovie);
        }
        
        // Copies one movie to another
        private Movies CopyMovie ( Movies movies)
        {
            if (movies == null)
                return null;

            var newMovie = new Movies();
            newMovie.Id = movies.Id;
            newMovie.Title = movies.Title;
            newMovie.Description = movies.Description;
            newMovie.Length = movies.Length;
            newMovie.IsOwned = movies.IsOwned;

            return newMovie; 
        }

        // Find a movie by ID
        private Movies FindMovie ( int id )
        {
            foreach (var movies in _movies)
            {
                if (movies.Id == id)
                    return movies; 
            }

            return null; 
        }

        private List<Movies> _movies = new List<Movies>();
        private int _nextId = 1; 
    }
}
