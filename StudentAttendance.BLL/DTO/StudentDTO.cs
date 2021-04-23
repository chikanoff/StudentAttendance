using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.DAL.Entities;

namespace StudentAttendance.BLL.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public UserDTO User { get; set; }
        public GroupDTO Group { get; set; }
    }
}
