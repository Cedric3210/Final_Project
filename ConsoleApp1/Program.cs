// MySQL Connection String
using Bogus;
using ConsoleApp1;
using MySql.Data.MySqlClient;
using System.Globalization;

string connectionString = "Server=localhost;Database=project_database;UserName=root;Password=Cedric1234%%;";
int totalRecords = 200; // Total records to generate

// Faker configuration
var companyInfoFaker = new Faker<Applicant>()
    .RuleFor(c => c.UserId, f => f.Random.Number(1, 500))
    .RuleFor(c => c.InfoType, f => f.Lorem.Word()) 
    .RuleFor(c => c.DateStarted, f => f.Date.Past(5).ToString("yyyy-MM-dd")) 
    .RuleFor(c => c.Content, f => f.Lorem.Paragraph(3)) 
    .RuleFor(c => c.ProfilePicture, _ => "NULL"); 

try
{
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        connection.Open();
        Console.WriteLine("Connected to database.");

        for (int i = 0; i < totalRecords; i++)
        {
            var companyInfo = companyInfoFaker.Generate();

            string insertQuery = @"INSERT INTO company_info 
                                              (userId, info_type, date_started, content, Profile_Picture)
                                              VALUES (@UserId, @InfoType, @DateStarted, @Content, NULL)";

            using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
            {
                // Add parameters to the query
                cmd.Parameters.AddWithValue("@UserId", companyInfo.UserId);
                cmd.Parameters.AddWithValue("@InfoType", "About_Post");
                cmd.Parameters.AddWithValue("@DateStarted", companyInfo.DateStarted);
                cmd.Parameters.AddWithValue("@Content", companyInfo.Content);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Record {i + 1} inserted: UserId {companyInfo.UserId}");
            }
        }

        Console.WriteLine("Data insertion completed successfully!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}