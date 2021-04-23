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
    public class TeacherDbRepository : IRepository<Teacher>
    {
        private readonly MySqlConnection conn;

        public TeacherDbRepository(MySqlConnection conn)
        {
            this.conn = conn;
        }
        public void Create(Teacher item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into teachers(userId) values(@userId)", conn);
                cmd.Parameters.AddWithValue("@userId", item.UserId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Teacher item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"update teachers set userId = @userId where id = @id", conn);
                cmd.Parameters.AddWithValue("@userId", item.UserId);
                cmd.Parameters.AddWithValue("@id", item.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("delete from teachers where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Teacher> Get()
        {
            IList<Teacher> teachers = new List<Teacher>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from teachers", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teachers.Add(new Teacher
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1)
                    });
                }
            }

            return teachers;
        }

        public Teacher Get(int id)
        {
            Teacher teacher = new Teacher();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from teachers where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    teacher = new Teacher
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                    };
                }
            }

            return teacher;
        }
    }
}
