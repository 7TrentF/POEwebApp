using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using Date_Calculations;

namespace WebApplication1
{
    public partial class ModuleInterface : System.Web.UI.Page
    {

        ModuleCollection mc = new ModuleCollection();

        public int _userId;
        // Define public class-level variables to store module information.
        public string Code;
        public string Mname;
        public double ModuleCredits = 0;
        public double ClassHrs = 0;
        public double NumWeeksInSemester;
        public double selfStudy;
        public DateTime startDate;
        public DateTime selectedDate ;


        // Declare class-level variables to store information.
        public string modcode;                    // Module code.
        public double remainingHrs;               // Remaining hours for self-study.
        public double remainingHours;
        public DateTime studyDate;                // Date for self-study planning.
        public DateTime workDate;
        public DateTime  Date;
        double remainingSelfStudyHours;          // Remaining self-study hours for the current week.
        public double existingRemainingHrs;
        public int moduleID;
        public DateTime selectedWorkDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if UserID is stored in the session
            if (Session["UserID"] != null)
            {
                // Retrieve the UserID from the session
                _userId = Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                // Redirect to login page if UserID is not found in the session
                Response.Redirect("~/Login.aspx");
            }

            // Only load data if it's not a postback (e.g., not caused by a button click)
            if (!IsPostBack)
            {
                LoadModuleData(); // Call a method to load module data
            }
        }

        private void LoadModuleData()
        {
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // Open a new connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                connection.Open();

                // Define an SQL query to select all records from the Module table for a specific UserID
                string query = "SELECT * FROM Module WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", _userId); // Set the parameter value for the select query

                    SqlDataReader reader = command.ExecuteReader(); // Execute the select query and retrieve the data using a SqlDataReader

                    DataTable dataTable = new DataTable(); // Create a DataTable to store the retrieved data
                    dataTable.Load(reader); // Load the data from the SqlDataReader into the DataTable

                    ModuleListView.DataSource = dataTable;
                    ModuleListView.DataBind(); // Bind the data to the ListView
                }
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
           

        }

        protected void AddModule_Click(object sender, EventArgs e)
        {
           

            try
            {
                // Create an instance of the SelfStudyCalc class.
                SelfStudyCalc ssc = new SelfStudyCalc();
                ModuleCollection mc = new ModuleCollection();

                Code = txtModule_Code.Text;
                Mname = Module_Name_textBox.Text;

                // Check if a date is selected
                if (StartDatePicker.Text != "")
                {
                    // Parse and save the selected date
                    selectedDate = DateTime.Parse(StartDatePicker.Text);
                    startDate = selectedDate;
                    MessageBox.Show($"startDate {startDate}");
                }

                // Try to parse and set module credits from the text box.
                if (!string.IsNullOrWhiteSpace(Module_Credits_textBox.Text) && int.TryParse(Module_Credits_textBox.Text, out int credits))
                {
                    ModuleCredits = credits;
                }

                // Try to parse and set class hours per week from the text box.
                if (!string.IsNullOrWhiteSpace(Class_Hours_Per_Week_textBox.Text) && int.TryParse(Class_Hours_Per_Week_textBox.Text, out int classHours))
                {
                    ClassHrs = classHours;
                }

                // Try to parse and set the number of weeks in the semester from the text box.
                if (!string.IsNullOrWhiteSpace(Number_Of_Weeks_Per_Semester_textBox.Text) && int.TryParse(Number_Of_Weeks_Per_Semester_textBox.Text, out int weekZInSemester))
                {
                    NumWeeksInSemester = weekZInSemester;
                }

                // Calculate self-study hours using the SelfStudyCalc class.
                selfStudy = ssc.SsCalc(ModuleCredits, NumWeeksInSemester, ClassHrs);
               
                mc.IsModuleCodeAdded(_userId, Code);
              

                string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();   // Open the database connection

                    string query = "INSERT INTO Module (UserID, ModuleCode, ModuleName, Credits, ClassHoursPerWeek, WeeksInSemester, SelfStudyHrs, StartDate)" +  // Define an SQL query for inserting a new record into the Module table.
                                  " VALUES (@UserID, @ModuleCode, @ModuleName, @Credits, @ClassHoursPerWeek, @WeeksInSemester, @SelfStudyHrs, @StartDate)";
                   
                    using (SqlCommand command = new SqlCommand(query, connection))    // Create a SqlCommand to execute the insert query
                    {
                        command.Parameters.AddWithValue("@UserID", _userId);
                        command.Parameters.AddWithValue("@ModuleCode", Code);
                        command.Parameters.AddWithValue("@ModuleName", Mname);
                        command.Parameters.AddWithValue("@Credits", ModuleCredits);
                        command.Parameters.AddWithValue("@ClassHoursPerWeek", ClassHrs);
                        command.Parameters.AddWithValue("@WeeksInSemester", NumWeeksInSemester);
                        command.Parameters.AddWithValue("@SelfStudyHrs", selfStudy);
                        command.Parameters.AddWithValue("@StartDate", startDate);

                        command.ExecuteNonQuery();        // Execute the insert query to add a new module record
                    }
                    connection.Close();    // Close the database connection
                }


                // Open a new connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // Define an SQL query to select all records from the Module table for a specific UserID
                    string query = "SELECT * FROM Module WHERE UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", _userId); // Set the parameter value for the select query

                        SqlDataReader reader = command.ExecuteReader(); // Execute the select query and retrieve the data using a SqlDataReader

                        DataTable dataTable = new DataTable(); // Create a DataTable to store the retrieved data
                        dataTable.Load(reader); // Load the data from the SqlDataReader into the DataTable

                        ModuleListView.DataSource = dataTable;
                        ModuleListView.DataBind(); // Bind the data to the ListView
                    }
                }
            

            }

            catch (Exception ex)
            {
                // Handle any exceptions and show an error message box.
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }

        protected void ModuleListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            
        }


        protected void ModuleListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void List_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // Open a new connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                connection.Open();

                // Define an SQL query to select all records from the Module table for a specific UserID
                string query = "SELECT * FROM Module WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", _userId); // Set the parameter value for the select query

                    SqlDataReader reader = command.ExecuteReader(); // Execute the select query and retrieve the data using a SqlDataReader

                    DataTable dataTable = new DataTable(); // Create a DataTable to store the retrieved data
                    dataTable.Load(reader); // Load the data from the SqlDataReader into the DataTable

                    ModuleListView.DataSource = dataTable;
                    ModuleListView.DataBind(); // Bind the data to the ListView
                }
            }
        }
        protected void Calculate_Click(object sender, EventArgs e)
        {
           
            try
            {
                RemainingHrsCalc r = new RemainingHrsCalc();     // An instance of the RemainingHrs class.
                modcode = ssModuleCode.Text;

                if (!mc.CheckModuleExists(modcode))   // Check if the module exists in the database
                {

                    MessageBox.Show("Module does not exist.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtHrsSpent.Text) && double.TryParse(txtHrsSpent.Text, out double hrs))  // Try to parse and set the number of hours spent from a text box.
                {
                    remainingHrs = hrs;
                }

                // Check if a date is selected
                if (txtWorkDate.Text != "")
                {
                    // Parse and save the selected date
                    selectedWorkDate = DateTime.Parse(txtWorkDate.Text);
                     studyDate = selectedWorkDate;

                    MessageBox.Show($"sudyDate {studyDate}");

                    //if (StartDatePicker.Text != "")
                    //{
                    //    // Parse and save the selected date
                    //    selectedDate = DateTime.Parse(StartDatePicker.Text);
                    //    startDate = selectedDate;

                    //}
                }

                selfStudy = mc.GetSelfStudyHours(modcode);   // Get the total self-study hours required for the module.

                MessageBox.Show($"id {_userId}");
                DateTime StartDate = mc.GetDate(modcode, _userId);

                MessageBox.Show($"startDate {StartDate}");
                // Calculate the remaining self-study hours for the current week.
                remainingSelfStudyHours = r.CalculateRemainingSelfStudyHoursForCurrentWeek(modcode, selfStudy, remainingHrs, startDate);
             


                string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve the ModuleID for the given modcode from the Module table
                    string moduleIdQuery = "SELECT ModuleID FROM Module WHERE ModuleCode = @ModuleCode AND UserId =@UserId";
                    using (SqlCommand moduleIdCommand = new SqlCommand(moduleIdQuery, connection))
                    {
                        moduleIdCommand.Parameters.AddWithValue("@ModuleCode", modcode);
                        moduleIdCommand.Parameters.AddWithValue("@UserId", _userId);

                        moduleID = (int)moduleIdCommand.ExecuteScalar();
                    }
                }

              
              
                if (mc.CheckModuleCodeInSelfStudyInfo(modcode, _userId))

                {

                    MessageBox.Show($"An2");
                    DateTime WorkDate = mc.GetWorkDate(modcode, _userId);
                 
                    DateTime dateCheck = r.CheckWorkDate(WorkDate, studyDate);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Check if a record with the retrieved ModuleID exists in the SelfStudyInfo table.
                        string checkQuery = "SELECT COUNT(*) FROM SelfStudyInfo WHERE ModuleID = @ModuleID";
                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@ModuleID", moduleID);

                            int existingRecordCount = (int)checkCommand.ExecuteScalar();

                            if (existingRecordCount > 0)
                            {
                                // DateTime workDate = ModuleCollection.Instance.GetWorkDate(modcode);

                                // If a record exists, retrieve the existing remaining hours
                                string existingRemainingHrsQuery = "SELECT RemainingHrs FROM SelfStudyInfo WHERE ModuleID = @ModuleID";
                                using (SqlCommand existingRemainingHrsCommand = new SqlCommand(existingRemainingHrsQuery, connection))
                                {
                                    existingRemainingHrsCommand.Parameters.AddWithValue("@ModuleID", moduleID);
                                    existingRemainingHrs = (int)existingRemainingHrsCommand.ExecuteScalar();
                                    remainingHours = r.CalculateRemainingHours(modcode, remainingHrs, existingRemainingHrs); // Calculate the remaining self-study hours for the current week.
                                }

                                // Update the RemainingHrs value
                                string updateQuery = "UPDATE SelfStudyInfo SET RemainingHrs = @RemainingHrs, workDate = @WorkDate WHERE ModuleID = @ModuleID";

                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@ModuleID", moduleID);
                                    updateCommand.Parameters.AddWithValue("@RemainingHrs", remainingHours);
                                    updateCommand.Parameters.AddWithValue("@workDate", studyDate);
                                    updateCommand.ExecuteNonQuery();
                                }

                                // Clear the SelfStudyInfoList before adding new items
                                //  SelfStudyInfoList.Clear();

                                // Add an instance of SelfStudyInfoDisplay to the list
                                // SelfStudyInfoList.Add(new SelfStudyInfoDisplay { ModuleCode = modcode, RemainingHrs = remainingHours, WorkDate = studyDate });

                            }
                        }
                    }
                }

                //record doesn't exist, insert data
                else
                {
                    MessageBox.Show($"f");
                    MessageBox.Show($"sudyDate {studyDate}");
                    DateTime dateCheck = r.CheckDate(StartDate, studyDate);
                    MessageBox.Show($"f1");
                   
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        // If no record exists, insert a new record
                        string insertQuery = "INSERT INTO SelfStudyInfo (ModuleID, RemainingHrs, workDate) VALUES (@ModuleID, @RemainingHrs, @WorkDate)";
                     
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                          
                            insertCommand.Parameters.AddWithValue("@ModuleID", moduleID);
                            insertCommand.Parameters.AddWithValue("@RemainingHrs", remainingSelfStudyHours);
                            insertCommand.Parameters.AddWithValue("@WorkDate", studyDate);
                          
                            insertCommand.ExecuteNonQuery();
                           
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }

        }

        protected void txtModule_Code_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}