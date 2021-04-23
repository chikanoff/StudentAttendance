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
    public class DisciplineDbRepository : IRepository<Discipline>
    {
        private readonly MySqlConnection conn;

        public DisciplineDbRepository(MySqlConnection conn)
        {
            this.conn = conn;
        }

        public void Create(Discipline item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into disciplines(name, teacherId) values(@name, @teacherId)", conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@teacherId", item.TeacherId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Discipline item)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand($"update disciplines set name = @name, teacherId = @teacherId where id = @id", conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@teacherId", item.TeacherId);
                cmd.Parameters.AddWithValue("@id", item.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("delete from disciplines where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Discipline> Get()
        {
            IList<Discipline> disciplines = new List<Discipline>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from disciplines", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    disciplines.Add(new Discipline
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        TeacherId = reader.GetInt32(2)
                    });
                }
            }

            return disciplines;
        }

        public Discipline Get(int id)
        {
            Discipline discipline = new Discipline();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from disciplines where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    discipline = new Discipline 
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        TeacherId = reader.GetInt32(2)
                    };
                }
            }

            return discipline;
        }
    }
}
