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


        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            if (Product != null)
            {
                _txtName.Text = Product.Name;
                _txtDescription.Text = Product.Description;
                _txtPrice.Text = Product.Price.ToString();
                _txtDiscontinued.Checked = Product.IsDiscontinued;
            };
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
            var product = new Product();
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;

            product.Price = GetPrice();
            product.IsDiscontinued = _txtDiscontinued.Checked;

            //Add Validation
            var error = product.Validate();
            if(!String.IsNullOrEmpty(error))
            {
                //TODO: Show the error
                ShowError(error, "Validation Error");
                return; 
            };

            Product = product;
            this.DialogResult = DialogResult.OK; 
            Close();
        }

        private decimal GetPrice ()
        {
            if (Decimal.TryParse(_txtPrice.Text, out decimal price))
                return price;

            //TODO: Validate Price
            return 0; 
        }

    }
}
