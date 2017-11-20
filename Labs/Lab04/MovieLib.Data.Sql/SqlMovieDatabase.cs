using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MovieLib.Data.Sql
{
    /// <summary>Provides an implemenation of <cref="IMovieDatabase"> using SQL Server.</summary>
    public class SqlMovieDatabase : MovieDatabase
    {
        #region Construction

        public SqlMovieDatabase( string connectionString )
        {
            _connectionString = connectionString; 
        }

        private readonly string _connectionString;

        #endregion

        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected override Movie AddCore( Movie movie )
        {
            var id = 0;
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("AddMovie", conn) { CommandType = CommandType.StoredProcedure};

                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@isOwned", movie.IsOwned);
                cmd.Parameters.AddWithValue("@description", movie.Description);

                id = Convert.ToInt32(cmd.ExecuteScalar());
            };

            return GetCore(id);
        }

        /// <summary>Get all of the movies.</summary>
        /// <returns>All of the movies.</returns>
        protected override IEnumerable<Movie> GetAllCore()
        {
            var movies = new List<Movie>();
            using (var connection = OpenDatabase())
            {
                var cmd = new SqlCommand("GetAllMovies", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var movie = new Movie() 
                        {
                            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Title = reader.GetFieldValue<string>(1),
                            Description = reader.GetFieldValue<string>(2),
                            Length = reader.GetInt32(3),
                            IsOwned = reader.GetBoolean(4)
                        };
                        movies.Add(movie);
                    };
                };

                return movies; 
            };

        }

        /// <summary>Get a movie given an id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The movie if any.</returns>
        protected override Movie GetCore( int id )
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("GetMovie", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@id", id);

                var ds = new DataSet();
                var da = new SqlDataAdapter() { SelectCommand = cmd, };

                da.Fill(ds);

                var table = ds.Tables.OfType<DataTable>().FirstOrDefault();
                if (table != null)
                {
                    var row = table.AsEnumerable().FirstOrDefault();
                    if (row != null)
                    {
                        return new Movie() 
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Title = row.Field<string>("Title"),
                            Description = row.Field<string>("Description"),
                            Length = row.Field<int>("Length"),
                            IsOwned = row.Field<bool>("IsOwned")
                        };
                    };
                };
            };
      
            return null;
        }

        /// <summary>Removes a movie given an ID.</summary>
        /// <param name="id">The ID.</param>
        protected override void RemoveCore( int id )
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("RemoveMovie", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery(); 
            };
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="existing">The existing movie.</param>
        /// <param name="newMovie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore( Movie existing, Movie newMovie )
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("UpdateMovie", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@id", newMovie.Id);
                cmd.Parameters.AddWithValue("@title", newMovie.Title);
                cmd.Parameters.AddWithValue("@length", newMovie.Length);
                cmd.Parameters.AddWithValue("@isOwned", newMovie.IsOwned);
                cmd.Parameters.AddWithValue("@description", newMovie.Description);

                cmd.ExecuteNonQuery();
            };

            return GetCore(existing.Id);
        }

        private SqlConnection OpenDatabase()
        {
            var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection;
        }
    }
}
