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
    public class SkipDbRepository : IRepository<Skip>
    {
        private readonly MySqlConnection conn;

        public SkipDbRepository(MySqlConnection conn)
        {
            this.conn = conn;
        }
        public void Create(Skip item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into skips(disciplineId, studentId, date) values(@disciplineId, @studentId, @date)", conn);
                cmd.Parameters.AddWithValue("@disciplineId", item.DisciplineId);
                cmd.Parameters.AddWithValue("@studentId", item.StudentId);
                cmd.Parameters.AddWithValue("@Date", item.Date);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Skip item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"update skips set disciplineId = @disciplineId, studentId = @studentId, date = @date where id = @id", conn);
                cmd.Parameters.AddWithValue("@disciplineId", item.DisciplineId);
                cmd.Parameters.AddWithValue("@studentId", item.StudentId);
                cmd.Parameters.AddWithValue("@Date", item.Date);
                cmd.Parameters.AddWithValue("@id", item.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("delete from skips where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Skip> Get()
        {
            IList<Skip> skips = new List<Skip>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from skips", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    skips.Add(new Skip
                    {
                        Id = reader.GetInt32(0),
                        DisciplineId = reader.GetInt32(1),
                        StudentId = reader.GetInt32(2),
                        Date = reader.GetDateTime(3)
                    });
                }
            }

            return skips;
        }

        public Skip Get(int id)
        {
            Skip skip = new Skip();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from skips where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    skip = new Skip
                    {
                        Id = reader.GetInt32(0),
                        DisciplineId = reader.GetInt32(1),
                        StudentId = reader.GetInt32(2),
                        Date = reader.GetDateTime(3)
                    };
                }
            }

            return skip;
        }
    }
}
