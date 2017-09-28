using System;
using System.Windows.Forms;

namespace Nile.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click( object sender, EventArgs e )
        {
            var product = new Product();
            product.Name = "Product A";

            var child = new ProductDetailForm("Product Details");
            if (child.ShowDialog(this) != DialogResult.OK)             // Connecting mainForm to the child in child.ShowDialog();
                return; 

            //TODO: Save product
           // var product = child.Product; 
        }
    }
}
