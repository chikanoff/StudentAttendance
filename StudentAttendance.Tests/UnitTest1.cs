using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using StudentAttendance.DAL.Entities;
using StudentAttendance.DAL.UnitsOfWork;

namespace StudentAttendance.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string _connString = "database=studentsattendance;server=127.0.0.1;port=3306;user id=root;pwd=admin";
        [TestMethod]
        public void TestMethod1()
        {
            IList<Student> students = new List<Student>();
            using (MySqlConnection conn = new MySqlConnection(_connString))
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
            
        }
    }
}
