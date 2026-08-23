using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.UI; // Bunifu UI namespace (adjust based on your installed version)
using System.Configuration;
using Bunifu.UI.WinForms;
using Npgsql;

namespace Rajani_ERP
{
    public partial class Form3 : Form
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

        public Form3()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //show and hide password
        private void ChkShowPassword_OnChange(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.PasswordChar = '●';
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        // ---- Login logic ----
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

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
                                lblError.Text = "Username or password is incorrect.";
                                lblError.Visible = true;
                            }
                        }
                    }
                }
            }
        }


    }
}
