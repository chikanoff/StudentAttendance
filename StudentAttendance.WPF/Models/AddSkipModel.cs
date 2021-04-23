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
    public class AddSkipModel
    {
        private ISkipService _skipService;
        private IStudentService _studentService;
        private ITeacherService _teacherService;
        private IDisciplineService _disciplineService;
        private IUserService _userService;

        public AddSkipModel(ISkipService skipService, ITeacherService teacherService, IStudentService studentService, IUserService userService, IDisciplineService disciplineService)
        {
            _skipService = skipService;
            _teacherService = teacherService;
            _studentService = studentService;
            _userService = userService;
            _disciplineService = disciplineService;
        }

        public List<DisciplineDTO> GetAllDisciplines()
        {
            List<DisciplineDTO> disciplines = new List<DisciplineDTO>();
            var currUser = _userService.Get(AuthentificationService.Id);
            if (currUser.Role.Name.Equals("Администратор"))
            {
                disciplines = _disciplineService.Get().ToList();
            }
            else if (currUser.Role.Name.Equals("Преподаватель"))
            {
                disciplines = _disciplineService.Get().Where(x => x.Teacher.UserId == AuthentificationService.Id)
                    .ToList();
            }

            return disciplines;
        }

        public List<UserDTO> GetStudents()
        {
            var students = _studentService.GetUsersFromStudents();
            return students;
        }

        public int GetStudentIdFromUserId(int id)
        {
            int studentId = _studentService.Get().ToList().FirstOrDefault(x => x.UserId == id).Id;
            return studentId;
        }

        public void AddSkip(SkipDTO skip)
        {
            _skipService.Create(skip);
        }
    }
}
