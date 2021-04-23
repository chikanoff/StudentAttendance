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
    public class RoleDbRepository : IRepository<Role>
    {
        private readonly MySqlConnection conn;

        public RoleDbRepository(MySqlConnection conn)
        {
            this.conn = conn;
        }
        public void Create(Role item)
        {
            throw new NotImplementedException();
        }

        public void Update(Role item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Get()
        {
            IList<Role> roles = new List<Role>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from roles", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    roles.Add(new Role
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            return roles;
        }

        public Role Get(int id)
        {
            Role role = new Role();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from roles where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    role = new Role
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }

            return role;
        }
    }
}
