using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;
using StudentAttendance.WPF.Services;

namespace StudentAttendance.WPF.Models
{
    public class StudentModel
    {
        private IStudentService _studentService;
        private ITeacherService _teacherService;
        private IUserService _userService;
        private ISkipService _skipService;

        public StudentModel(IStudentService studentService, ITeacherService teacherService, IUserService userService, ISkipService skipService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _userService = userService;
            _skipService = skipService;
        }

        public StudentDTO GetCurrentStudent()
        {
            var user = _studentService.Get().FirstOrDefault(x => x.UserId == AuthentificationService.Id);
            return user;
        }

        public List<SkipDTO> GetSkips(StudentDTO student)
        {
            var skips = _skipService.Get().Where(x => x.StudentId == student.Id);
            return skips.ToList();
        }
    }
}
