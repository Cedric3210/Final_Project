using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Project_Polished_Version.Classes
{
    public partial class Applicant_Profile : Window
    {
        public static int windowNumber { get; set; }
        public static int searchUserKey { get; set; }
        private int imageKey;
        public static int searchUserKey_Company { get; set; }
        public static List<int> keys = new List<int>();
        public ApplicantUser CurrentResume { get; private set; }

        public Applicant_Profile()
        {
            imageKey = MainWindow.UserID;
            InitializeComponent();
            InitializeProfile();
            Button_StackPanel.Children.Remove(Connect_Friend_Btn);

        }

        public Applicant_Profile(ApplicantUser userInfo) : this()
        {
            imageKey = Applicant_Search.applicantNumber;
            CurrentResume = userInfo;
            PopulateProfile(userInfo);
            //button name
            Button_StackPanel.Children.Remove(Change_photo_click);
            Button_StackPanel.Children.Add(Connect_Friend_Btn);
        }

        private void InitializeProfile()
        {
            ShowInfo();
            LoadAboutSection();
            LoadExperienceSection();
            LoadEducationSection();
            LoadProfilePicture();
        }
        private void Edit_Profile(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a Profile Picture",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                string imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                Directory.CreateDirectory(imagesDirectory);


                string newFileName = Path.GetFileName(selectedFilePath);
                string newFilePath = Path.Combine(imagesDirectory, newFileName);
                File.Copy(selectedFilePath, newFilePath, true);

                string relativePath = Path.Combine("Images", newFileName);

                MessageBox.Show("Show: " + relativePath);

                try
                {
                    string query = "UPDATE applicant_accounts SET Profile_Picture = @Profile_Picture WHERE id = @UserId";
                    using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Profile_Picture", relativePath);
                            cmd.Parameters.AddWithValue("@UserId", MainWindow.UserID);

                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Upload Successfully");

                                BitmapImage bt = new BitmapImage();
                                bt.BeginInit();
                                bt.UriSource = new Uri(newFilePath, UriKind.Absolute);
                                bt.CacheOption = BitmapCacheOption.OnLoad;
                                bt.EndInit();
                                User_Profile.Source = bt;
                            }
                            else
                            {
                                MessageBox.Show("Nothing Happen");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Show: " + ex.Message);
                }
            }
        }

        private void LoadProfilePicture()
        {
            try
            {
                string imagePath = MainWindow.userAccountsGetID[imageKey].Applicant_Photo;

                string imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagesDirectory, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                User_Profile.Source = bitmap;
                MessageBox.Show("Profile picture loaded successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile picture: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateProfile(ApplicantUser resume)
        {
            try
            {
                DisableButton();

                // Populate other profile data
                Full_Name.Text = $"{resume.First_Name} {resume.Last_Name}";
                Job_Title_txtbox.Text = resume.JobTitle;
                Address_txtbox.Text = resume.Address;
                searchUserKey = resume.Id;
                searchUserKey_Company = resume.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window targetWindow;

                if (windowNumber == 1)
                {
                    targetWindow = new Applicant_DashBoard();
                }
                else if (windowNumber == 2)
                {
                    targetWindow = new Applicant_Search();
                }
                else
                {
                    throw new InvalidOperationException("Invalid window number.");
                }

                this.Close();
                targetWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while navigating back: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }
        private void AddFriend_Btn(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                {
                    connection.Open();
                    int currentUserId = MainWindow.UserType ? MainWindow.CompanyID : MainWindow.UserID;
                    MessageBox.Show("Show: " + currentUserId);
                    MessageBox.Show("Show: " + MainWindow.CompanyID);
                    MessageBox.Show("Show: " + MainWindow.UserType);
                    string query = "INSERT INTO friends (user_id, friend_id, user_type) VALUES (@UserId, @FriendId, @UserType);";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", currentUserId);
                        cmd.Parameters.AddWithValue("@FriendId", searchUserKey);
                        cmd.Parameters.AddWithValue("@UserType", MainWindow.UserType ? "company" : "applicant");

                        int rowsAffected = cmd.ExecuteNonQuery();

                        MessageBox.Show(rowsAffected > 0 ? "Friend request sent successfully!" : "Failed to send friend request.");
                        Connect_Friend_Btn.Content = "Pending";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending friend request: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowInfo()
        {
            try
            {
                int key = MainWindow.UserID;

                if (MainWindow.userAccountsGetID.TryGetValue(key, out ApplicantUser loggedUser))
                {
                    Full_Name.Text = $"{loggedUser.First_Name} {loggedUser.Last_Name}";
                    Job_Title_txtbox.Text = loggedUser.JobTitle;
                    Address_txtbox.Text = loggedUser.Address;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user info: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadAboutSection()
        {
            About_TextBox.Text = await GetSectionDataAsync("About_Post");
        }

        private async void LoadEducationSection()
        {
            Education_TextBox_Copy.Text = await GetSectionDataAsync("Education_Post");
        }

        private async void LoadExperienceSection()
        {
            Experience_TextBox.Text = await GetSectionDataAsync("Experience_Post");
        }
        public void ChangeInfo(string name, string jobTitle, string address)
        {
            Full_Name.Text = name;
            Job_Title_txtbox.Text = jobTitle;
            Address_txtbox.Text = address;
        }
        private async Task<string> GetSectionDataAsync(string infoType)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT content FROM applicant_info WHERE userId = @userId AND info_type = @infoType";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", MainWindow.UserID);
                        cmd.Parameters.AddWithValue("@infoType", infoType);

                        using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader["content"]?.ToString() ?? "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {infoType} data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return string.Empty;
        }
        private void ShowFriendList_btn(object sender, RoutedEventArgs e)
        {
            Friend_List_Window flw = new Friend_List_Window();
            flw.Show();
        }

        private void DisableButton()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM friends WHERE (user_id = @UserId AND friend_id = @FriendId) " +
                                   "OR (user_id = @FriendId AND friend_id = @UserId)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", MainWindow.UserID);
                        cmd.Parameters.AddWithValue("@FriendId", searchUserKey);

                        int friendshipCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (friendshipCount > 0)
                        {
                            Connect_Friend_Btn.IsEnabled = false;
                            Connect_Friend_Btn.Content = "Already Connected";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking friendship: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
