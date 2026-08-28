using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using Npgsql;

namespace RAJANI_ERP
{
    public partial class Form1 : Form
    {
        public static class DatabaseConnection
        {
            public static NpgsqlConnection GetConnection()
            {
                string connectionString =
                    ConfigurationManager
                    .ConnectionStrings["RajaniSuppliers"]
                    .ConnectionString;

                return new NpgsqlConnection(connectionString);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_error.Visible = false;
           
        }

        private void Btn_login_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "SELECT * FROM login.\"ValidateUser\"(@Username, @Password)",
                    connection))
                {
                    command.Parameters.AddWithValue("Username", username);
                    command.Parameters.AddWithValue("Password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool isValid = reader.GetBoolean(reader.GetOrdinal("IsValid"));
                            string userType = reader.IsDBNull(reader.GetOrdinal("UserType"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("UserType"));

                            if (isValid)
                            {
                                MessageBox.Show(
                                    "Login successful for " + userType,
                                    "Login Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                lbl_error.Text = "Username or password is incorrect.";
                                lbl_error.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Btn_show_Click(object sender, EventArgs e)
        {
                
            
        }
    }
}
