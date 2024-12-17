using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Project_Polished_Version.Classes
{
    public class ConnectionClass
    {
        public static string ConnectionString { get; set; } =
            "Server=localhost;Database=project_database;UserName=root;Password=Cedric1234%%";
    }
}
