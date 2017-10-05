using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows
{
    public partial class ProductDetailForm : Form
    {
        #region Construction
        public ProductDetailForm() //: base()
        {
            InitializeComponent();
        }
        public ProductDetailForm(string title) : this()
        {
            Text = title; 
        }
        public ProductDetailForm( string title, Product product ) : this(title)
        {
            Product = product;
        }
        #endregion

        protected override void OnLoad( EventArgs e )   // method that will ultimately call load event
        {
            base.OnLoad(e);

            //if (DesignMode)           // Check designMode // used for constructors
            if (Product != null)
            {
                _txtName.Text = Product.Name;
                _txtDescription.Text = Product.Description;
                _txtPrice.Text = Product.Price.ToString();
                _txtDiscontinued.Checked = Product.IsDiscontinued;
            };

            ValidateChildren(); 
        }
        
        /// <summary> Get or set the product being shown</summary>
        public Product Product { get; set;}

        private void OnCancel ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel; // tell parent which button user entered
            Close(); 
        }

        private void ShowError( string message, string title )
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);   // MessageBox for errors 
        }

        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())        // UI validation
            {
                return; 
            };

            var product = new Product();
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;

            product.Price = GetPrice(_txtPrice);
            product.IsDiscontinued = _txtDiscontinued.Checked;

            //Add Validation
            var error = product.Validate();
            if(!String.IsNullOrEmpty(error))
            {
                //Show the error
                ShowError(error, "Validation Error");
                return; 
            };

            Product = product;
            this.DialogResult = DialogResult.OK; 
            Close();
        }

        private decimal GetPrice ( TextBox control)
        {
            if (Decimal.TryParse(control.Text, out decimal price))
                return price;

            //Validate Price
            return -1; 
        }

        //private void ProductDetailForm_FormClosing( object sender, FormClosingEventArgs e )
        //{
        //    // Please no
        //    //var form = (Form)sender; // problem!, runtime check

        //    // Please yes (type conversion for reference types)
        //    var form = sender as Form; // Safe

        //    // Casting for value type
        //    if (sender is int)
        //    {
        //        var intValue2 = (int)sender;
        //    };

        //    //Pattern Matching
        //    if (sender is int intValue)
        //    {

        //    };

        //    if (MessageBox.Show(this, "Are you sure?", "Closing", MessageBoxButtons.YesNo) == DialogResult.No)
        //        e.Cancel = true;
        //}

        //private void ProductDetailForm_FormClosed( object sender, FormClosedEventArgs e )
        //{

        //}

        private void OnValidatingPrice( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (GetPrice(tb) < 0)
            {
                e.Cancel = true;
                _errors.SetError(_txtPrice, "Price must be >= 0");
            } else
                _errors.SetError(_txtPrice, "");
        }

        private void OnValidName( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;
            if (String.IsNullOrEmpty(tb.Text))
                _errors.SetError(tb, "Name is required");
            else
                _errors.SetError(tb, "");
        }
    }
}
