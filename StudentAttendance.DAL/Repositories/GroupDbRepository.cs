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
    public class GroupDbRepository : IRepository<Group>
    {
        private readonly MySqlConnection conn;

        public GroupDbRepository(MySqlConnection conn)
        {
            this.conn = conn;
        }
        public void Create(Group item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into `groups`(name) values(@name)", conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Group item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"update `groups` set name = @name where id = @id", conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@id", item.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("delete from `groups` where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Group> Get()
        {
            IList<Group> groups = new List<Group>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from `groups`", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    groups.Add(new Group
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            return groups;
        }

        public Group Get(int id)
        {
            Group group = new Group();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from `groups` where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    group = new Group
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }

            return group;
        }
    }
}
