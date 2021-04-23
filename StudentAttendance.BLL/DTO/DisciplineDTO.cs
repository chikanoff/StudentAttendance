using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendance.BLL.DTO
{
    public class DisciplineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; }
    }
}
