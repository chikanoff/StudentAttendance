using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendance.DAL.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}
