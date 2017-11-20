using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;


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

            _miFileExit.Click += ( o, ea ) => Close();

            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"].ConnectionString;
            _database = new MovieLib.Data.Sql.SqlMovieDatabase(connString);

            _gridMovies.AutoGenerateColumns = false;

            UpdateList();
        }
        
        #region Event Handlers

        private void OnMovieAdd( object sender, EventArgs e )
        {
            var child = new MovieDetailForm("Movie Details");
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save movie
            try
            {
                _database.Add(child.Movie);
            } catch (ValidationException ex)
            {
                DisplayError(ex, "Validation Failed");
            } catch (Exception ex)
            {
                DisplayError(ex, "Add Failed");
            };
            UpdateList(); 
        }

        private void OnMovieDelete( object sender, EventArgs e )
        {
            var movie = GetSelectedMovies();
            if (movie == null)
                return;

            // Delete product
            DeleteMovie(movie);
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            var movie = GetSelectedMovies();
            if (movie == null)
            {
                MessageBox.Show("No movies available.");
                return;
            };

            EditMovie(movie);
        }
  
        private void OnHelpAbout( object sender, EventArgs e )
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }
        
        // Grid
        private void OnEditRow( object sender, DataGridViewCellEventArgs e )
        {
            var grid = sender as DataGridView;

            // Handle column clicks
            if (e.RowIndex < 0)
                return;

            var row = grid.Rows[e.RowIndex];
            var movie = row.DataBoundItem as Movie;

            if (movie != null)
                EditMovie(movie);
        }

        private void OnKeyDownGrid( object sender, KeyEventArgs e )
        {
            if (e.KeyCode != Keys.Delete)
                return;

            var movie = GetSelectedMovies();
            if (movie != null)
                DeleteMovie(movie);

            e.SuppressKeyPress = true;
        }

        #endregion

        #region Private Members

        private void DeleteMovie( Movie movie )
        {
            // Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{movie.Title}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Delete Movie
            try
            {
                _database.Remove(movie.Id);
            } catch (Exception e)
            {
                DisplayError(e, "Delete Failed");
            };
            UpdateList();
        }

        private void DisplayError ( Exception error, string title = "Error")
        {
            DisplayError(error.Message, title);
        }

        private void DisplayError ( string message, string title = "Error")
        {
            MessageBox.Show(this, message, title ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EditMovie( Movie movie )
        {
            var child = new MovieDetailForm("Movie Details");
            child.Movie = movie;
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save edited movie
            try
            {
                _database.Update(child.Movie);
            } catch (Exception e)
            {
                DisplayError(e, "Update Failed");
            }
            UpdateList();
        }

        private Movie GetSelectedMovies()
        {
            if (_gridMovies.SelectedRows.Count > 0)
                return _gridMovies.SelectedRows[0].DataBoundItem as Movie;

            return null;
        }

        private void UpdateList()
        {
            try
            {
                _bsMovies.DataSource = _database.GetAll().ToList();
            } catch (Exception e)
            {
                DisplayError(e, "Refresh Failed");
                _bsMovies.DataSource = null; 
            };
            
        }

        private IMovieDatabase _database; 
        
        #endregion
        
    }
}
