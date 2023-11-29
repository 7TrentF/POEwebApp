using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Date_Calculations
{
    public class ModuleCollection
    {
        public List<ModuleClass> modules;     // A private list to store module objects.
        private static ModuleCollection instance; // A static instance of the ModuleCollection class.

        // Define a public static property to get an instance of ModuleCollection.
        public static ModuleCollection Instance
        {
            get
            {
                if (instance == null) // Check if the instance is null.
                {
                    instance = new ModuleCollection(); // Create a new instance if it doesn't exist.
                }
                return instance; // Return the instance.
            }
        }

        // Private constructor for the ModuleCollection class.
        public ModuleCollection()
        {
            modules = new List<ModuleClass>(); // Create an empty list to store modules.
        }


        // Method to check if a module code has already been added for a specific user
        public void IsModuleCodeAdded(int userId, string moduleCode)
        {
            // Define the connection string for the SQL Server database
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))     // Establish a connection to the database using the provided connection string
            {
                connection.Open();        // Open the database connection

                string query = "SELECT COUNT(*) FROM Module WHERE UserID = @UserID AND ModuleCode = @ModuleCode";         // Define an SQL query to count the number of records with the given UserID and ModuleCode
                using (SqlCommand command = new SqlCommand(query, connection))         // Create a SqlCommand to execute the count query
                {
                    // Set the parameters for the count query
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@ModuleCode", moduleCode);

                    int count = (int)command.ExecuteScalar();   // Execute the count query and retrieve the count value

                    if (count > 0)  // Check if the moduleCode has already been added for the userId
                    {
                        // The moduleCode has already been added for the userId.
                        throw new ArgumentException("The module code has already been added.");
                    }
                }
            }
        }

        public bool CheckModuleExists(string moduleCode)
        {
            // Query the database to check if a module with the given moduleCode exists.

            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Module WHERE ModuleCode = @ModuleCode";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ModuleCode", moduleCode);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Module found");
                        return true; // Module with the specified moduleCode exists.
                    }
                    else
                    {
                        // Module with the specified moduleCode does not exist. Display a message.
                        MessageBox.Show("Module with the entered code does not exist.", "Error");
                        return false;
                    }
                }
            }
        }

        // Query the database to get the ModuleID for a module with the given moduleCode.
        //In this code, the CheckModuleCodeInSelfStudyInfo function first retrieves the ModuleCode for the given moduleId and userId from the Module table. If the ModuleCode is found, it then checks if the retrieved ModuleCode exists in the SelfStudyInfo table. If the ModuleCode is found in the SelfStudyInfo table, it retrieves the ModuleID from the SelfStudyInfo table and checks if it matches the given moduleId. The function returns true if the ModuleID from the SelfStudyInfo table matches the given moduleId, and false otherwise
        public bool CheckModuleCodeInSelfStudyInfo(string moduleCode, int userId)
        {
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
               
                // Define an SQL query to retrieve the ModuleID from the Module table where the ModuleCode and UserID match the given ModuleCode and UserID
                string moduleIdQuery = "SELECT ModuleID FROM Module WHERE ModuleCode = @ModuleCode AND UserID = @UserID";
                using (SqlCommand moduleIdCommand = new SqlCommand(moduleIdQuery, connection))
                {
                    moduleIdCommand.Parameters.AddWithValue("@ModuleCode", moduleCode);
                    moduleIdCommand.Parameters.AddWithValue("@UserID", userId);

                    object moduleIdResult = moduleIdCommand.ExecuteScalar(); // Execute the ModuleID query and retrieve the ModuleID value
                   
                    // Check if the ModuleID was found
                    if (moduleIdResult != null)
                    {
                        int moduleId = (int)moduleIdResult;
                       
                        // Define an SQL query to check if the retrieved ModuleID exists in the SelfStudyInfo table
                        string selfStudyInfoQuery = "SELECT ModuleID FROM SelfStudyInfo WHERE ModuleID = @ModuleID";
                        using (SqlCommand selfStudyInfoCommand = new SqlCommand(selfStudyInfoQuery, connection))
                        {
                            selfStudyInfoCommand.Parameters.AddWithValue("@ModuleID", moduleId);
                           
                            object selfStudyInfoResult = selfStudyInfoCommand.ExecuteScalar(); // Execute the SelfStudyInfo query and retrieve the ModuleID value
                           
                          

                            // Check if the ModuleID was found in the SelfStudyInfo table
                            if (selfStudyInfoResult != null)
                            {
                               
                                int selfStudyModuleId = (int)selfStudyInfoResult;
                              
                                // Return true if the ModuleID from the SelfStudyInfo table matches the given ModuleID, otherwise false
                                return selfStudyModuleId == moduleId;
                            }
                            else
                            {
                                // The ModuleID was found in the Module table but not in the SelfStudyInfo table
                                return false;
                            }
                        }
                    }

                    else
                    {
                        // The ModuleID was not found in the Module table
                        return false;
                    }
                }
            }
        }

        public int GetExistingRemainingHrs(string moduleCode)
        {
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define an SQL query to retrieve the existing remaining hours from the SelfStudyInfo table
                string existingRemainingHrsQuery = "SELECT RemainingHrs FROM SelfStudyInfo WHERE ModuleID = @ModuleID";
                using (SqlCommand existingRemainingHrsCommand = new SqlCommand(existingRemainingHrsQuery, connection))
                {
                    existingRemainingHrsCommand.Parameters.AddWithValue("@ModuleID", moduleCode);
                    return (int)existingRemainingHrsCommand.ExecuteScalar();
                }
            }
        }




        public void UpdateRemainingHrs(string moduleCode, double remainingHours, DateTime studyDate)
        {
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define an SQL query to update the RemainingHrs value
                string updateQuery = "UPDATE SelfStudyInfo SET RemainingHrs = @RemainingHrs, workDate = @WorkDate WHERE ModuleID = @ModuleID";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@ModuleID", moduleCode);
                    updateCommand.Parameters.AddWithValue("@RemainingHrs", remainingHours);
                    updateCommand.Parameters.AddWithValue("@workDate", studyDate);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        public int GetUserIdFromModule(string moduleCode, int moduleId)
        {
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT UserID FROM Module WHERE ModuleCode = @ModuleCode AND ModuleID = @ModuleID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ModuleCode", moduleCode);
                    command.Parameters.AddWithValue("@ModuleID", moduleId);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return (int)result;
                    }
                    else
                    {
                        throw new ArgumentException("Module with the specified code and ID not found.");
                    }
                }
            }
        }

        public DateTime GetDate(string moduleCode, int userId)
        {
            // Query the database to get the StartDate for a module with the given moduleCode.
            // Example query: "SELECT StartDate FROM Module WHERE ModuleCode = @ModuleCode"
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT StartDate FROM Module WHERE ModuleCode = @ModuleCode  AND UserID = @UserID";
               
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ModuleCode", moduleCode);
                    command.Parameters.AddWithValue("@UserID", userId);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return (DateTime)result; // Return the StartDate value.
                    }
                    else
                    {
                        // Handle the case where the module with the specified moduleCode is not found.
                        throw new ArgumentException("Module with the specified code not found.");
                    }
                }
            }
        }

        public DateTime GetWorkDate(string moduleCode, int userId)
        {
            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define an SQL query to retrieve the ModuleID from the Module table where the ModuleCode and UserID match the given parameters
                string moduleIdQuery = "SELECT ModuleID FROM Module WHERE ModuleCode = @ModuleCode AND UserID = @UserID";
                using (SqlCommand moduleIdCommand = new SqlCommand(moduleIdQuery, connection))
                {
                    moduleIdCommand.Parameters.AddWithValue("@ModuleCode", moduleCode);
                    moduleIdCommand.Parameters.AddWithValue("@UserID", userId);

                    object moduleIdResult = moduleIdCommand.ExecuteScalar(); // Execute the query and retrieve the ModuleID value

                    // Check if the ModuleID was found
                    if (moduleIdResult != null)
                    {
                        int moduleId = (int)moduleIdResult;

                        // Define an SQL query to retrieve the workDate from the SelfStudyInfo table where the ModuleID matches the retrieved ModuleID
                        string workDateQuery = "SELECT workDate FROM SelfStudyInfo WHERE ModuleID = @ModuleID";
                        using (SqlCommand workDateCommand = new SqlCommand(workDateQuery, connection))
                        {
                            workDateCommand.Parameters.AddWithValue("@ModuleID", moduleId);

                            object workDateResult = workDateCommand.ExecuteScalar(); // Execute the query and retrieve the workDate value

                            // Check if the workDate was found in the SelfStudyInfo table
                            if (workDateResult != null && workDateResult != DBNull.Value)
                            {
                                return (DateTime)workDateResult; // Return the workDate value.
                            }
                            else
                            {
                                // Handle the case where no workDate is found for the ModuleID.
                                throw new ArgumentException("No work date found for the specified Module.");
                            }
                        }
                    }
                    else
                    {
                        // Handle the case where no ModuleID is found for the given moduleCode and userId.
                        throw new ArgumentException("No Module found with the specified ModuleCode and UserID.");
                    }
                }
            }
        }




        // Method to get the self-study hours for a specific module using its code.
        public int GetSelfStudyHours(string moduleCode)
        {
            // Query the database to get the SelfStudyHrs for a module with the given moduleCode.

            string connectionString = "Server=tcp:trentzerver.database.windows.net,1433;Initial Catalog=studyPlanner;Persist Security Info=False;User ID=trent;Password=Combi17054845;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT SelfStudyHrs FROM Module WHERE ModuleCode = @ModuleCode";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ModuleCode", moduleCode);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return (int)result; // Return the SelfStudyHrs value.
                    }
                    else
                    {
                        // Handle the case where the module with the specified moduleCode is not found.
                        throw new ArgumentException("Module with the specified code not found.");
                    }
                }
            }
        }
    }
}
