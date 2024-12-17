using MySql.Data.MySqlClient;
using Project_Polished_Version.Classes;
using System;
using System.Linq;
using System.Windows;

namespace Project_Polished_Version
{
    public partial class Remove_Job_Window : Window
    {
        private static string Connection = "Server=localhost;Database=project_database;UserName=root;Password=Cedric1234%%";
        public Remove_Job_Window()
        {
            InitializeComponent();
            PopulateJobListBox();
        }

        private void PopulateJobListBox()
        {
            // Filter jobs by company ID
            var companyJobs = SearchJob_Window.jobFeed
                .Where(job => job.Company_userNumber == MainWindow.CompanyID)
                .ToList();

            // Set the ListBox's ItemsSource to the filtered jobs
            Jobs_ListView.ItemsSource = companyJobs;

            // Optional: Check if no jobs are available for removal
            if (!companyJobs.Any())
            {
                MessageBox.Show("No jobs available for this company.");
            }
        }

        private void RemoveJobButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a job is selected
            if (Jobs_ListView.SelectedItem is Jobs selectedJob)
            {
                // Confirm removal
                if (MessageBox.Show($"Are you sure you want to remove the job: {selectedJob.Job_Position}?",
                                    "Confirm Removal", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Remove the job
                    RemoveJobFromDatabase(selectedJob);
                }
            }
            else
            {
                MessageBox.Show("Please select a job to remove.");
            }
        }

        private void RemoveJobFromDatabase(Jobs selectedJob)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                {
                    connection.Open();

                    // SQL query to delete the job
                    string query = "DELETE FROM jobs_db WHERE Job_id = @Job_id";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@JobID", selectedJob.Job_id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Job '{selectedJob.Job_Position}' removed successfully!");
                            Global_Repository.GetJobsFromDataBase().Remove(selectedJob); // Update the jobFeed
                            PopulateJobListBox(); // Refresh the ListBox
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove the job. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
