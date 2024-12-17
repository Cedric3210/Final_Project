using MySql.Data.MySqlClient;
using Project_Polished_Version.Classes;
using System;
using System.IO;
using System.Windows;

namespace Project_Polished_Version
{
    /// <summary>
    /// Interaction logic for Register_Window.xaml
    /// </summary>
    public partial class Register_Window : Window
    {
        FileInfo fi;
        public Register_Window()
        {
            InitializeComponent();

        }

        private void Add_Row_Columns(int userId)
        {
            //string connectionString = "Server=localhost;Database=project_database;UserID=root;Password=Cedric1234%%;";
            string insertQuery = "INSERT INTO applicant_info (userId, info_type, date_started, content) VALUES (@userId, @info_type, @date_started, @content)";


            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {

                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@info_type", "About_Post");
                        command.Parameters.AddWithValue("@date_started", DateTime.Now);
                        command.Parameters.AddWithValue("@content", DBNull.Value);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@info_type", "Experience_Post");
                        command.Parameters.AddWithValue("@date_started", DBNull.Value);
                        command.Parameters.AddWithValue("@content", DBNull.Value);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@info_type", "Education_Post");
                        command.Parameters.AddWithValue("@date_started", DBNull.Value);
                        command.Parameters.AddWithValue("@content", DBNull.Value);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Three rows created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"1 Error inserting rows: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private int AddAccount()
        {
            int userId = 0;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string defaultImagePath = Path.Combine(baseDirectory, "Images", "Default.jpg");
            fi = new FileInfo(defaultImagePath);

            try
            {

                string First_Name = First_Name_txtbox.Text;
                string Last_Name = Last_Name_txtBox.Text;
                string Email = emal_txtBox.Text;
                string JobTitle = Job_Position_txtBox.Text;
                string Password = PassWord_txtBox.Password;
                string Gender = Male_Option.IsChecked == true ? "Male" : "Female";
                string Mobile_Number = Mobile_Number_txtBox.Text;
                string Address = Address_TxtBox.Text;


                if (string.IsNullOrWhiteSpace(First_Name) || string.IsNullOrWhiteSpace(Last_Name) ||
                    string.IsNullOrWhiteSpace(JobTitle) || string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(Mobile_Number) || string.IsNullOrWhiteSpace(Password))
                {
                    MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }

                if (Mobile_Number.Length != 11 || !Mobile_Number.StartsWith("09"))
                {
                    MessageBox.Show("Invalid phone number. Phone number should start with 09 and be 11 digits long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }

                using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                {
                    string query = "INSERT INTO applicant_accounts (first_name, last_name, email, gender, Phone_Number, Job_Title, `password`,address,Profile_Picture) " +
                                   "VALUES (@first_name, @last_name, @email, @gender, @Phone_Number, @Job_Title, @password, @address,@Profile_Picture); " +
                                   "SELECT LAST_INSERT_ID();";

                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@first_name", First_Name);
                        cmd.Parameters.AddWithValue("@last_name", Last_Name);
                        cmd.Parameters.AddWithValue("@gender", Gender);
                        cmd.Parameters.AddWithValue("@Job_Title", JobTitle);
                        cmd.Parameters.AddWithValue("@email", Email);
                        cmd.Parameters.AddWithValue("@Phone_Number", Mobile_Number);
                        cmd.Parameters.AddWithValue("@password", Password);
                        cmd.Parameters.AddWithValue("@address", Address);
                        cmd.Parameters.AddWithValue("@Profile_Picture", fi.FullName);
                        userId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($" 2 An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return userId;
        }


        private void Next_btn(object sender, RoutedEventArgs e)
        {
            int userId = AddAccount();
            if (userId > 0)
            {
                Add_Row_Columns(userId);
                this.Close();
                MainWindow mn = new MainWindow();
                mn.Show();
            }
        }

        private void Cancl_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}



