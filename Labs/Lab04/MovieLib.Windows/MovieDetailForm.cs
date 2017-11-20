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

        public MovieDetailForm( string title ) : this()
        {
            Text = title; 
        }

        public MovieDetailForm( string title, Movie movie) : this(title)
        {
            Movie = movie; 
        }

        #endregion
       
        /// <summary> Gets or sets the movie being shown. </summary>
        public Movie Movie { get; set; }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            if (Movie != null)
            {
                _txtTitle.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtLength.Text = Movie.Length.ToString();
                _chkOwned.Checked = Movie.IsOwned;
            }

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

            var movie = new Movie() 
            {
                Id = Movie?.Id ?? 0,
                Title = _txtTitle.Text,
                Description = _txtDescription.Text,
                Length = GetLength(_txtLength),
                IsOwned = _chkOwned.Checked,
            };

            if (!ObjectValidator.TryValidate(movie, out var errors))
            {
                // Show the Error
                ShowError("Not Valid", "Validation Error");
                return;
            };

            Movie = movie;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnValidatingTitle( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (String.IsNullOrEmpty(tb.Text))
                _error.SetError(tb, "Title is required.");
            else
                _error.SetError(tb, "");
        }

        private void OnValidatingLength( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (GetLength(tb) < 0)
            {
                e.Cancel = true;
                _error.SetError(_txtLength, "Length must be >= 0.");
            } else
                _error.SetError(_txtLength, "");
        }

        #endregion

        #region Private Members

        private int GetLength( TextBox control )
        {
            if (Int32.TryParse(control.Text, out int length))
                return length;

            return -1;
        }

        private void ShowError( string message, string title )
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

    }
}
