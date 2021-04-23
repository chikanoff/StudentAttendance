using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using StudentAttendance.DAL.Entities;
using StudentAttendance.DAL.Interfaces;

namespace StudentAttendance.DAL.Repositories
{
    public class UserDbRepository : IRepository<User>
    {
        private readonly MySqlConnection conn;

        public UserDbRepository(MySqlConnection conn)
        {
            this.conn = conn;
        }
        public void Create(User item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into users(username, password, fullName, roleId) values(@username, @password, @fullName, @roleId)", conn);
                cmd.Parameters.AddWithValue("@username", item.Username);
                cmd.Parameters.AddWithValue("@password", item.Password);
                cmd.Parameters.AddWithValue("@fullName", item.FullName);
                cmd.Parameters.AddWithValue("@roleId", item.RoleId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(User item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"update users set username = @username, password = @password, fullName = @fullName, roleId = @roleId where id = @id", conn);
                cmd.Parameters.AddWithValue("@username", item.Username);
                cmd.Parameters.AddWithValue("@password", item.Password);
                cmd.Parameters.AddWithValue("@fullName", item.FullName);
                cmd.Parameters.AddWithValue("@roleId", item.RoleId);
                cmd.Parameters.AddWithValue("@id", item.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("delete from users where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> Get()
        {
            IList<User> users = new List<User>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from users", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        FullName = reader.GetString(3),
                        RoleId = reader.GetInt32(4)
                    });
                }
            }

            return users;
        }

        public User Get(int id)
        {
            User user = new User();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from users where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        FullName = reader.GetString(3),
                        RoleId = reader.GetInt32(4)
                    };
                }
            }

            return user;
        }
    }
}
