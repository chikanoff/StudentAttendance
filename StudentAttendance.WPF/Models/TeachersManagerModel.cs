using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;
using StudentAttendance.DAL.Entities;

namespace StudentAttendance.WPF.Models
{
    public class TeachersManagerModel
    {
        private ITeacherService _teacherService;
        private IUserService _userService;
        private IRoleService _roleService;
        public TeachersManagerModel(ITeacherService teacherService, IUserService userService, IRoleService roleService)
        {
            _teacherService = teacherService;
            _userService = userService;
            _roleService = roleService;
        }

        public List<TeacherDTO> GetAllTeachers()
        {
            var t = _teacherService.Get();
            return t.ToList();
        }

        public void Add(string username, string password, string fullName)
        {
            _userService.Create(new UserDTO
            {
                Username = username,
                Password = password,
                FullName = fullName,
                RoleId = _roleService.Get().FirstOrDefault(x => x.Name.Equals("Преподаватель")).Id
            });
            int id = _userService.Get().ToList().FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()))
                .Id;
            _teacherService.Create(new TeacherDTO
            {
                UserId = id
            });

        }

        public void Delete(TeacherDTO item)
        {
            _teacherService.Delete(item.Id);
            _userService.Delete(item.UserId);
        }

        public List<RoleDTO> GetRoles()
        {
            return _roleService.Get().Where(x => x.Name.Equals("Преподаватель")).ToList();
        }

        public bool IsExist(string username)
        {
            var u = _userService.Get().ToList().FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()));
            return u != null;
        }
    }
}
