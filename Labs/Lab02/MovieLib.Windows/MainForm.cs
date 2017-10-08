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
            _movie = child.Movies; 
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            var child = new MovieDetailForm("Movie Details");
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            // Save movie
            _movie = child.Movies;
        }

        private void OnMovieDelete( object sender, EventArgs e )
        {
            if (_movie == null)
                return;

            // Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{_movie.MovieTitle}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Delete product
            _movie = null;
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            
        }

        private Movies _movie; 
    }
}
