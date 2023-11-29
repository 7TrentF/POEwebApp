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
    public partial class Contact : Page
    {
        public string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public string hashedPassword;
        public string username;

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

        private int GetLoggedInUserID(string username, string hashedPassword)
        {
            string query = "SELECT UserID FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    // Retrieve the UserID instead of counting rows
                    var result = command.ExecuteScalar();
                    return (result != null) ? Convert.ToInt32(result) : -1; // Return -1 if user not found
                }
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string enteredPassword = txtPassword.Text;
            hashedPassword = HashPassword(enteredPassword);
            username = txtUsername.Text;

            // Check if username or password is blank
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(enteredPassword))
            {

                MessageBox.Show("Username and password cannot be blank.");
                return; // Exit the method if fields are blank
            }

            // Get the logged-in user's ID
            int userID = GetLoggedInUserID(username, hashedPassword);

            if (userID > 0)
            {
                MessageBox.Show("login successful");
                // Store UserID in a session variable
                Session["UserID"] = userID;

                Response.Redirect("~/ModuleInterface.aspx");
            }
            else
            {
                // User doesn't exist or password doesn't match
                MessageBox.Show("The username and password don't match.");
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx"); // Replace "~/ExitPage.aspx" with the URL of the page you want to redirect to
        }

       
    }
}