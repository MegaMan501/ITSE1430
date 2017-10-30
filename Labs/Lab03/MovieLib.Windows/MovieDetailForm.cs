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

        public MovieDetailForm( string title, Movies movie) : this(title)
        {
            Movies = movie; 
        }

        #endregion

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            if (Movies != null)
            {
                _txtTitle.Text = Movies.Title;
                _txtDescription.Text = Movies.Description;
                _txtLength.Text = Movies.Length.ToString();
                _chkOwned.Checked = Movies.IsOwned;
            }

            ValidateChildren();
        }

        /// <summary> Gets or sets the movie being shown. </summary>
        public Movies Movies { get; set; }

        private void OnCancel( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ShowError( string message, string title )
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())
                return;

            var movie = new Movies() {
                Id = Movies?.Id ?? 0,
                Title = _txtTitle.Text,
                Description = _txtDescription.Text,
                Length = GetLength(_txtLength),
                IsOwned = _chkOwned.Checked,
            };

            if (!ObjectValidator.TryValidate(movie, out var errors))
            {
                // Show the Error
                ShowError("Not Valid", "Validation Error");
            }

            Movies = movie;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private int GetLength( TextBox control)
        {
            if (Int32.TryParse(control.Text, out int length))
                return length;

            //Validate Price 
            return -1; 
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

        private void OnValidatingTitle( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (String.IsNullOrEmpty(tb.Text))
                _error.SetError(tb, "Movie Title is required.");
            else
                _error.SetError(tb, "");
        }
    }
}
