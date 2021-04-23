using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendance.DAL.Entities
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
    }
}
