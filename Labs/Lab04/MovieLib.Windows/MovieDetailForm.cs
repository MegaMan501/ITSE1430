using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MovieLib.Windows
{
    public partial class MovieDetailForm : Form
    {
        #region Construction

        public MovieDetailForm()
        {
            InitializeComponent();
        }

        public MovieDetailForm( IMovieDatabase database ) : this()
        {
            _database = database;
        }
            
        #endregion
       
        public Movie Movie { get; set; }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            // Loading the Data
            if (Movie != null)
            {
                _txtTitle.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtLength.Text = Movie.Length.ToString();
                _chkOwned.Checked = Movie.IsOwned;
            }

            // Validate
            ValidateChildren();
        }

        #region Event Handlers

        private void OnCancel( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())
                return;

            var movie = CreateMovie();
            try
            {
                if (movie.Id == 0)
                    movie = _database.Add(movie);
                else
                    movie = _database.Update(movie);
            } catch (Exception ex)
            {
                ShowError(ex.Message, "Error");
                return; 
            }
           
            Movie = movie;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnValidatingTitle( object sender, CancelEventArgs e )
        {
            var control = sender as TextBox;

            if (String.IsNullOrEmpty(control.Text))
            {
                _error.SetError(control, "Title is required.");
                e.Cancel = true; 
            }
            else
                _error.SetError(control, "");
        }

        private void OnValidatingLength( object sender, CancelEventArgs e )
        {
            var control = sender as TextBox;
            var length = ParseInt32(control.Text);

            if (length < 0)
            {
                _error.SetError(control, "Length must be >= 0.");
                e.Cancel = true;
            } else
                _error.SetError(control, "");
        }

        #endregion

        #region Private Members

        private Movie CreateMovie()
        {
            var movie = new Movie() 
            {
                Title = _txtTitle.Text,
                Description = _txtDescription.Text,
                Length = ParseInt32(_txtLength.Text),
                IsOwned = _chkOwned.Checked,
            };

            if (Movie != null)
                movie.Id = Movie.Id;

            return movie; 
        }

        private int ParseInt32( string text )
        {
            if (String.IsNullOrEmpty(text))
                return 0; 

            if (Int32.TryParse(text, out int length))
                return length;

            return -1;
        }

        private void ShowError( string message, string title )
        {
            MessageBox.Show(this, message, title ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private readonly IMovieDatabase _database; 
        #endregion
    }
}
