using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accessibility;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;
using StudentAttendance.WPF.Services;

namespace StudentAttendance.WPF.Models
{
    public class DisciplinesManagerModel
    {
        private readonly IDisciplineService _disciplineService;
        private readonly IUserService _userService;
        private readonly ITeacherService _teacherService;

        public DisciplinesManagerModel(IDisciplineService disciplineService, IUserService userService, ITeacherService teacherService)
        {
            _disciplineService = disciplineService;
            _userService = userService;
            _teacherService = teacherService;
        }

        public List<DisciplineDTO> GetAllDisciplines()
        {
            List<DisciplineDTO> disciplines = new List<DisciplineDTO>();
            var currUser = _userService.Get(AuthentificationService.Id);
            if (currUser.Role.Name.Equals("Администратор"))
            {
                disciplines = _disciplineService.Get().ToList();
            }
            else if(currUser.Role.Name.Equals("Преподаватель"))
            {
                disciplines = _disciplineService.Get().Where(x => x.Teacher.UserId == AuthentificationService.Id)
                    .ToList();
            }

            return disciplines;
        }

        public List<TeacherDTO> GetTeachers()
        {
            List<TeacherDTO> teachers = new List<TeacherDTO>();
            var currUser = _userService.Get(AuthentificationService.Id);
            if (currUser.Role.Name.Equals("Администратор"))
            {
                teachers = _teacherService.Get().ToList();
            }
            else if (currUser.Role.Name.Equals("Преподаватель"))
            {
                teachers = _teacherService.Get().Where(x => x.UserId == AuthentificationService.Id)
                    .ToList();
            }

            return teachers;
        }

        public void AddDiscipline(DisciplineDTO item)
        {
            _disciplineService.Create(item);
        }

        public void EditDiscipline(DisciplineDTO item)
        {
            _disciplineService.Update(item);
        }

        public void DeleteDiscipline(DisciplineDTO item)
        {
            _disciplineService.Delete(item.Id);
        }
    }
}
