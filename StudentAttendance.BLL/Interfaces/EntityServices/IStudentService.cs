using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;

namespace StudentAttendance.BLL.Interfaces.EntityServices
{
    public interface IStudentService : IEntityService<StudentDTO>
    {
        public List<UserDTO> GetUsersFromStudents();
    }
}
