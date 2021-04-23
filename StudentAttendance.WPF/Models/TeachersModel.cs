using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;
using StudentAttendance.BLL.Services;
using StudentAttendance.DAL.Entities;
using StudentAttendance.WPF.Services;
using StudentAttendance.WPF.Views;

namespace StudentAttendance.WPF.Models
{
    public class TeachersModel
    {
        private IStudentService _studentService;
        private ITeacherService _teacherService;
        private IDisciplineService _disciplineService;
        private IUserService _userService;
        private ISkipService _skipService;

        public TeachersModel(IStudentService studentService, ITeacherService teacherService, IUserService userService, ISkipService skipService, IDisciplineService disciplineService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _userService = userService;
            _skipService = skipService;
            _disciplineService = disciplineService;
        }

        public UserDTO GetCurrentUser()
        {
            var user = _userService.Get(AuthentificationService.Id);
            return user;
        }

        public List<DisciplineDTO> GetAllDisciplinesOfTeacher()
        {
            var disciplines = _disciplineService.Get().Where(x => x.Teacher.UserId == AuthentificationService.Id);
            return disciplines.ToList();
        }
        public List<StudentDTO> GetStudents()
        {
            var students = _studentService.Get().ToList();
            return students;
        }

        public List<SkipDTO> GetSkips()
        {
            var skips = _skipService.Get();
            return skips.ToList();
        }

        public void DeleteSkip(SkipDTO item)
        {
            _skipService.Delete(item.Id);
        }
    }
}
