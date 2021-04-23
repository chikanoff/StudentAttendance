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
    public class StudentDbRepository : IRepository<Student>
    {
        private readonly MySqlConnection conn;

        public StudentDbRepository(MySqlConnection conn)
        {
            this.conn = conn;
        }
        public void Create(Student item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into students(photo, userId, groupId) values(@photo, @userId, @groupId)", conn);
                cmd.Parameters.AddWithValue("@photo", item.Photo);
                cmd.Parameters.AddWithValue("@userId", item.UserId);
                cmd.Parameters.AddWithValue("@groupId", item.GroupId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Student item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"update students set photo = @photo, userId = @userId, groupId = @groupId where id = @id", conn);
                cmd.Parameters.AddWithValue("@photo", item.Photo);
                cmd.Parameters.AddWithValue("@userId", item.UserId);
                cmd.Parameters.AddWithValue("@groupId", item.GroupId);
                cmd.Parameters.AddWithValue("@id", item.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("delete from students where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Student> Get()
        {
            IList<Student> students = new List<Student>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from students", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = reader.GetInt32(0),
                        Photo = reader.GetString(1),
                        UserId = reader.GetInt32(2),
                        GroupId = reader.GetInt32(3)
                    });
                }
            }

            return students;
        }

        public Student Get(int id)
        {
            Student student = new Student();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from students where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    student = new Student
                    {
                        Id = reader.GetInt32(0),
                        Photo = reader.GetString(1),
                        UserId = reader.GetInt32(2),
                        GroupId = reader.GetInt32(3)
                    };
                }
            }

            return student;
        }
    }
}
