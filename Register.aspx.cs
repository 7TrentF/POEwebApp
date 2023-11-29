using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace WebApplication1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            string username = txtUsername.Text;
            string password = HashPassword(txtPassword.Text);

            // Check if username or password is blank
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be blank.");
                return; // Exit the method if fields are blank
            }

            else
            {
                string query = "INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", password);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }


                MessageBox.Show("Username and password have been registered");
                Response.Redirect("~/Login.aspx");

            }
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

    }
}