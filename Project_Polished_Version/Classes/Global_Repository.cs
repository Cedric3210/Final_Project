using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Polished_Version.Classes
{
    public class Global_Repository
    {
        public static List<Jobs> JobFeed { get; set; } = new List<Jobs>();

        public static async Task<List<Jobs>> GetJobsFromDataBase()
        {
            var jobFeedList = new List<Jobs>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT Job_Title, Job_Position, Job_Description, is_filled, Job_id, Company_userNumber, Salary FROM jobs_db";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            jobFeedList.Add(new Jobs
                            {
                                Job_Title = reader["Job_Title"].ToString(),
                                Job_Position = reader["Job_Position"].ToString(),
                                Job_Description = reader["Job_Description"].ToString(),
                                IsFilled = reader["is_filled"].ToString(),
                                Job_id = Convert.ToInt32(reader["Job_id"]),
                                Company_userNumber = Convert.ToInt32(reader["Company_userNumber"]),
                                Job_Salary = reader["Salary"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching jobs: {ex.Message}", "Error", MessageBoxButton.OK);
            }

            return jobFeedList;
        }
    }
}
