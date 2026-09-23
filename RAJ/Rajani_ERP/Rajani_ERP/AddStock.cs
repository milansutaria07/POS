using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rajani_ERP
{
    public partial class AddStock : Form
    {
        public AddStock()
        {
            InitializeComponent();
        }

        private void BunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BunifuButton1_Click(object sender, EventArgs e)
        {
            // Hide the current form
            this.Hide();

            // Create a new instance of your form (Replace 'Form1' with your actual form class name)
            AddStock freshForm = new AddStock();

            // Show the new form and close the old one
            freshForm.ShowDialog();
            this.Close();
        }

        private void CancleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
