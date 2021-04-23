using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;

namespace StudentAttendance.WPF.Models
{
    public class StudentsManagerModel
    {
        private IStudentService _studentService;
        private IGroupService _groupService;
        private IRoleService _roleService;
        private IUserService _userService;

        public StudentsManagerModel(IStudentService studentService, IGroupService groupService,
            IRoleService roleService, IUserService userService)
        {
            _studentService = studentService;
            _groupService = groupService;
            _roleService = roleService;
            _userService = userService;
        }

        public List<GroupDTO> GetGroups()
        {
            var q = _groupService.Get().ToList();
            return q;
        }

        public List<StudentDTO> GetStudents()
        {
            var q = _studentService.Get().ToList();
            return q;
        }

        public void Add(string username, string password, string fullName, string photo, GroupDTO group)
        {
            int roleId = _roleService.Get().FirstOrDefault(x => x.Name.Equals("Студент")).Id;
            _userService.Create(new UserDTO
            {
                Username = username,
                Password = password,
                FullName = fullName,
                RoleId = roleId
            });
            int id = _userService.Get().ToList().FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()))
                .Id;
            _studentService.Create(new StudentDTO
            {
                Photo = photo,
                UserId = id,
                GroupId = group.Id
            });

        }

        public bool IsExist(string username)
        {
            var u = _userService.Get().ToList().FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()));
            return u != null;
        }

        public void Delete(StudentDTO item)
        {
            _studentService.Delete(item.Id);
            _userService.Delete(item.UserId);
        }
    }
}
