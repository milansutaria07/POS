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
    public partial class PopUpForm : Form
    {
        public PopUpForm()
        {
            InitializeComponent();
        }

        private void BunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopUpForm_Load(object sender, EventArgs e)
        {

        }
    }
}
