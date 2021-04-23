using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace StudentAttendance.DAL.Connections
{
    public class DbConnection
    {
        private readonly string _connectionString;
        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection ConnectToDatabase()
        {
            return new(_connectionString);
        }
    }
}
