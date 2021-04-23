using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendance.DAL.Entities
{
    public class Skip
    {
        public int Id { get; set; }
        public int DisciplineId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
    }
}
