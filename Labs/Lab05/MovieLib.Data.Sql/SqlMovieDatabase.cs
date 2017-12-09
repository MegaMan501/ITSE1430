using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace MovieLib.Data.Sql
{
    /// <summary>Provides an implemenation of <cref="IMovieDatabase"> using SQL Server.</summary>
    public class SqlMovieDatabase : MovieDatabase
    {
        #region Construction

        public SqlMovieDatabase( string connectionStringOrName )
        {
            var connString = ConfigurationManager.ConnectionStrings[connectionStringOrName];
            _connectionString = connString?.ConnectionString ?? connectionStringOrName; 
        }
        #endregion

        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected override Movie AddCore( Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("AddMovie");

                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@isOwned", movie.IsOwned);
                cmd.Parameters.AddWithValue("@description", movie.Description);

                conn.Open(); 
                movie.Id = cmd.ExecuteScalar<int>();
            };

            return movie;
        }

        /// <summary>Finds a movie by its title.</summary>
        /// <param name="title">The title to find.</param>
        /// <returns>The movie, if any.</returns>
        protected override Movie FindByTitleCore( string title )
        {
            // Not Supported directly
            var movies = GetAllCore();

            return movies.FirstOrDefault(m => String.Compare(m.Title, title, true) == 0);
        }

        /// <summary>Get a movie given an id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The movie if any.</returns>
        protected override Movie GetCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("GetMovie");
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                return cmd.ExecuteReaderWithSingleResult(ReadMovie);
            };
        }

        /// <summary>Get all of the movies.</summary>
        /// <returns>All of the movies.</returns>
        protected override IEnumerable<Movie> GetAllCore()
        {
            
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("GetAllMovies");

                conn.Open();
                return cmd.ExcecuteReaderWithResults(ReadMovie);
            };

        }

        /// <summary>Removes a movie given an ID.</summary>
        /// <param name="id">The ID.</param>
        protected override void RemoveCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("RemoveMovie");
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open(); 
                cmd.ExecuteNonQuery(); 
            };
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="existing">The existing movie.</param>
        /// <param name="newMovie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore( Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = conn.CreateStoredProcedureCommand("UpdateMovie");

                cmd.Parameters.AddWithValue("@id", movie.Id);
                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@isOwned", movie.IsOwned);
                cmd.Parameters.AddWithValue("@description", movie.Description);

                conn.Open(); 
                cmd.ExecuteNonQuery();
            };

            return movie; 
        }

        #region Private Members

        private Movie ReadMovie ( DbDataReader reader)
        {
            return new Movie() 
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                Length = reader.GetInt32(3),
                IsOwned = reader.GetBoolean(4)
            };
        }

        private readonly string _connectionString;

        #endregion
    }
}
