using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieLib.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            _gridMovies.AutoGenerateColumns = false;

            UpdateList();
        }

        private Movies GetSelectedMovies()
        {
            if (_gridMovies.SelectedRows.Count > 0)
                return _gridMovies.SelectedRows[0].DataBoundItem as Movies;

            return null;
        }

        private void UpdateList()
        {
            _bsMovies.DataSource = _database.GetAll().ToList();
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnMovieAdd( object sender, EventArgs e )
        {
            var child = new MovieDetailForm("Movie Details");
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save movie
            _database.Add(child.Movies);
            UpdateList();
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

        private void EditMovie( Movies movie)
        {
            var child = new MovieDetailForm("Movie Details");
            child.Movies = movie;
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save edited movie
            _database.Update(child.Movies);
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

        private void DeleteMovie( Movies movie)
        {
            // Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{movie.Title}'?","Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Delete Movie
            _database.Remove(movie.Id);
            UpdateList();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        private IMovieDatabase _database = new MovieLib.Data.Memory.MemoryMovieDatabase(); 

        private void OnEditRow( object sender, DataGridViewCellEventArgs e )
        {
            var grid = sender as DataGridView;

            // Handle column clicks
            if (e.RowIndex < 0)
                return;

            var row = grid.Rows[e.RowIndex];
            var movie = row.DataBoundItem as Movies;

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
    }
}
