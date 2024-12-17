using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using Project_Polished_Version.Classes;

namespace Project_Polished_Version
{
    public partial class Friend_List_Window : Window
    {
        private string _connection = "Server=localhost;Database=project_database;UserName=root;Password=Cedric1234%%";
        public ObservableCollection<ApplicantUser> ApList { get; set; } = new ObservableCollection<ApplicantUser>();
        public List<int> ConnectList { get; set; } = new List<int>();

        public Friend_List_Window()
        {
            InitializeComponent();
            DataContext = this;
            LoadDataAsync("LOL");
        }

        private async void All_Friends(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch all friends and bind to UI
                await FetchFriendIDsAsync("all");
                BindToUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching all friends: {ex.Message}");
            }
        }

        private async void Pending_Friends(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch pending friends and bind to UI
                await FetchFriendIDsAsync("pending");
                BindToUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching pending friends: {ex.Message}");
            }
        }

        private async void Accepted_Friends(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch accepted friends and bind to UI
                await FetchFriendIDsAsync("accepted");

                BindToUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching accepted friends: {ex.Message}");
            }
        }

        private async void LoadDataAsync(string statusFilter)
        {
            try
            {
                await FetchFriendIDsAsync(statusFilter);
                BindToUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private async Task FetchFriendIDsAsync(string statusFilter)
        {
            ConnectList.Clear();

            using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"SELECT user_id, friend_id 
                                 FROM friends 
                                 WHERE (user_id = @userID OR friend_id = @userID) 
                                 AND status = @StatusFilter";

                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", MainWindow.UserID);
                    command.Parameters.AddWithValue("@StatusFilter", statusFilter);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int userId = reader.GetInt32("user_id");
                            int friendId = reader.GetInt32("friend_id");
                            int targetId = userId == MainWindow.UserID ? friendId : userId;
                            ConnectList.Add(targetId);
                        }
                    }
                }
            }
        }

        private void BindToUI()
        {
            ApList.Clear();

            using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"SELECT id, first_name, last_name, Job_Title, Profile_Picture, 
                                 (SELECT status FROM friends 
                                  WHERE (user_id = id AND friend_id = @CurrentUserId) 
                                  OR (friend_id = id AND user_id = @CurrentUserId)) AS status 
                                 FROM applicant_accounts 
                                 WHERE id = @FriendID";

                connection.Open();
                foreach (var friendID in ConnectList)
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FriendID", friendID);
                        command.Parameters.AddWithValue("@CurrentUserId", MainWindow.UserID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ApList.Add(new ApplicantUser
                                {
                                    Id = reader.GetInt32("id"),
                                    First_Name = reader.GetString("first_name"),
                                    Last_Name = reader.GetString("last_name"),
                                    JobTitle = reader.GetString("Job_Title"),
                                    Applicant_Photo = reader.GetString("Profile_Picture"),
                                    IsAccepted = reader["status"].ToString()
                                });
                            }
                        }
                    }
                }
            }
        }

        private void Friend_Action(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ApplicantUser user)
            {
                switch (user.IsAccepted)
                {
                    case "accepted":
                        Unfriend(user);
                        break;
                    case "pending":
                        AcceptFriend(user);
                        break;
                    default:
                        MessageBox.Show("Invalid action. Please try again.");
                        break;
                }
            }
        }

        private void AcceptFriend(ApplicantUser user)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"UPDATE friends 
                                 SET status = 'Accepted' 
                                 WHERE (user_id = @CurrentUserId AND friend_id = @FriendId) 
                                 OR (user_id = @FriendId AND friend_id = @CurrentUserId)";

                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CurrentUserId", MainWindow.UserID);
                    command.Parameters.AddWithValue("@FriendId", user.Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        MessageBox.Show($"Friend request from {user.First_Name} accepted.");
                    }
                }
            }
        }

        private void Unfriend(ApplicantUser user)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"DELETE FROM friends 
                                 WHERE (user_id = @CurrentUserId AND friend_id = @FriendId) 
                                 OR (user_id = @FriendId AND friend_id = @CurrentUserId)";

                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CurrentUserId", MainWindow.UserID);
                    command.Parameters.AddWithValue("@FriendId", user.Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        ApList.Remove(user);
                        MessageBox.Show($"You have unfriended {user.First_Name}.");
                    }
                }
            }
        }
    }
}
