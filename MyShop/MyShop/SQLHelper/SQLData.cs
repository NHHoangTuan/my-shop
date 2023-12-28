using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MyShop.SQLHelper
{
    internal class SQLData
    {

        private string _server;
        private string _databaseName;
        private string _user;
        private string _password;
        public static bool isSelectedDatabase = false;

        private static SQLData? _instance = null;
        SqlConnection? _connection;
        public static SQLData getInstance()
        {
            if (_instance == null)
            {
                _instance = new SQLData();
            }
            return _instance;
        }

        public SQLData()
        {
            _server = "localhost";
            _databaseName = "MyShop";
            _user = "admin";
            _password = "admin";
            _connection = null;
        }

        public SQLData(string server, string databaseName, string user, string password)
        {
            _server = server;
            _databaseName = databaseName;
            _user = user;
            _password = password;

            string? connectionString = AppConfig.AppConfig.ConnectionString();

            _connection = new SqlConnection(connectionString);

            try
            {
                _connection.Open();
                isSelectedDatabase = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Cannot connect to database. Reason: {ex.Message}");
            }

            _instance = this;
        }

        public SqlConnection connection { get { return _connection; } }

    }
}
