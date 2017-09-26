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
        public ProductDetailForm()
        {
            InitializeComponent();
        }

        /// <summary> Get or set the product being shown</summary>
        public Product Product { get; set;}
        private void OnCancel ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel; // tell parent which button user entered
            Close(); 
        }

        private void OnSave( object sender, EventArgs e )
        {
            var product = new Product();
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;

            product.Price = GetPrice();
            product.IsDiscontinued = _txtDiscontinued.Checked;

            // TODO: Add Validation

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
