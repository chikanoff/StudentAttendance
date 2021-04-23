using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.DAL.Entities;

namespace StudentAttendance.BLL.DTO
{
    public class SkipDTO
    {
        public int Id { get; set; }
        public int DisciplineId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }

        public DisciplineDTO Discipline { get; set; }
        public StudentDTO Student { get; set; }
    }
}
