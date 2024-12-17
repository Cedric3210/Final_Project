using MySql.Data.MySqlClient;
using Project_Polished_Version.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_Polished_Version
{
    /// <summary>
    /// Interaction logic for Create_Jobs_Window.xaml
    /// </summary>
    public partial class Create_Jobs_Window : Window
    {
        private string connectionString = "Server=localhost;Database=project_database;UserName=root;Password=Cedric1234%%";

        public Create_Jobs_Window()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string jobTitle = JobTitleTextBox.Text.Trim();
            string companyName = CompanyNameTextBox.Text.Trim();
            string location = LocationTextBox.Text.Trim();
            string salary = SalaryTextBox.Text.Trim();
            string description = DescriptionTextBox.Text.Trim();

            if (string.IsNullOrEmpty(jobTitle) || string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(location) ||
                string.IsNullOrEmpty(salary) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                {
                    connection.Open();

                    // Fix the table name and column names
                    string query = @"INSERT INTO jobs_db (is_Filled, Company_userNumber, Job_Description, Job_Position, Time_Created, Salary, Job_Title)
                             VALUES (@is_Filled, @Company_userNumber, @Job_Description, @Job_Position, @Time_Created, @Salary, @Job_Title)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Fix parameter names
                        cmd.Parameters.AddWithValue("@is_Filled", "False"); // Use boolean instead of string "False"
                        cmd.Parameters.AddWithValue("@Company_userNumber", MainWindow.CompanyID);
                        cmd.Parameters.AddWithValue("@Job_Description", description);
                        cmd.Parameters.AddWithValue("@Job_Position", jobTitle);
                        cmd.Parameters.AddWithValue("@Time_Created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Salary", salary);
                        cmd.Parameters.AddWithValue("@Job_Title", jobTitle);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Job added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close(); // Close the window after saving
                        }
                        else
                        {
                            MessageBox.Show("Failed to add job. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Remove_Job_Window rjw = new Remove_Job_Window();
            rjw.Show();

        }
        // Handle Cancel Button Click
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the window without saving
        }
    }
}
