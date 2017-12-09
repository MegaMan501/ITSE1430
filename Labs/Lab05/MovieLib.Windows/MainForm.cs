using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using MovieLib.Data.Sql;

namespace MovieLib.Windows
{
    public partial class MainForm : Form
    {
        #region Construction

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                _database = new SqlMovieDatabase("MovieDatabase");

                UpdateMovies();
            };
        }

        #region Event Handlers
        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnMovieAdd( object sender, EventArgs e )
        {
            var child = new MovieDetailForm(_database);

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            UpdateMovies(); 
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            var selectedMovie = GetSelectedMovie(true);
            if (selectedMovie == null)
                return;

            var form = new MovieDetailForm(_database);
            form.Movie = selectedMovie;

            if (form.ShowDialog(this) != DialogResult.OK)
                return; 

            UpdateMovies();
        }

        private void OnMovieDelete( object sender, EventArgs e )
        {
            var selectedMovie = GetSelectedMovie(true);
            if (selectedMovie == null)
                return;
            
            // Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{selectedMovie.Title}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Delete Movie
            try
            {
                _database.Remove(selectedMovie.Id);
            } catch (Exception ex)
            {
                DisplayError(ex.Message, "Delete Failed");
            };

            UpdateMovies();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            new AboutBox().ShowDialog(this);
        }
        
        // Grid
        private void OnEditRow( object sender, DataGridViewCellEventArgs e )
        {
            // Handle column clicks
            if (e.RowIndex < 0)
                return;

            OnMovieEdit(sender, EventArgs.Empty);            
        }

        private void OnKeyDownGrid( object sender, KeyEventArgs e )
        {
            var selectedMovie = GetSelectedMovie(false);
            if (selectedMovie == null)
                return;

            if (e.KeyCode == Keys.Return)
            {
                OnMovieEdit(sender, EventArgs.Empty);
                e.SuppressKeyPress = true;
            } 
            else if (e.KeyCode == Keys.Delete)
            {
                OnMovieDelete(sender, EventArgs.Empty);
                e.SuppressKeyPress = true; 
            };
        }

        #endregion

        #region Private Members

        private void DisplayError ( string message, string title = "Error")
        {
            MessageBox.Show(this, message, title ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
              
        private void DisplayError ( Exception error, string title = "Error")
        {
            DisplayError(error.Message, title);
        }

        private Movie GetSelectedMovie( bool showErrorIfNone )
        {
            var movie = _bsMovies.Current as Movie;

            if (movie == null && showErrorIfNone)
                DisplayError("No movie selected.", "No Movie");

            return movie;
        }

        private void UpdateMovies()
        {
            // Try and get the currently selected item so we can restore it later
            var selectedMovieId = (_bsMovies.Current as Movie)?.Id ?? 0;

            // Get the movie
            var movies = Enumerable.Empty<Movie>();

            try
            {
                movies = _database.GetAll() ?? Enumerable.Empty<Movie>();
            } catch (Exception e)
            {
                DisplayError(e.Message, "Refresh Failed");
            };

            // Rebind
            _bsMovies.SuspendBinding();
            _bsMovies.DataSource = from m in movies
                                   orderby m.Title
                                   select m;
            _bsMovies.ResumeBinding(); 

            // Restore the movie
            if (selectedMovieId > 0)
            {
                var selectedMovie = movies.FirstOrDefault(m => m.Id == selectedMovieId);
                if (selectedMovie != null)
                    _bsMovies.Position = _bsMovies.IndexOf(selectedMovie);
            };
        }

        private IMovieDatabase _database;

        #endregion
    }
}
