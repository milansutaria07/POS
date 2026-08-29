using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAJANI_ERP
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void MainPage_Load(object sender, EventArgs e)
        {
            pg_main.SetPage(0);
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Cmd_sales_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(0);
        }

        private void Cmd_dn_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(2);
        }

        private void Cmd_invoices_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(3);
        }

        private void Cmd_stock_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(4);
        }

        private void Cmd_products_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(5);
        }

        private void Cmd_settings_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(6);
        }

        

        private void Btn_create_create_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(0);
        }

        private void Btn_create_cancel_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(2);
        }

        private void Btn_create_view_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(1);
        }

        private void Btn_view_create_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(0);
        }

        private void Btn_view_cancel_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(2);
        }

        private void Btn_view_view_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(1);
        }

        private void Btn_cancel_create_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(0);
        }

        private void Btn_cancel_cancel_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(2);
        }

        private void Btn_cancel_view_cs_Click(object sender, EventArgs e)
        {
            pg_main.SetPage(1);
            pg_sub_page.SetPage(1);
        }
    }
}
