using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;

namespace StudentAttendance.WPF.Models
{
    public class LoginModel
    {
        private IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserDTO GetUser(string username, string password)
        {
            var k = _userService.Get().FirstOrDefault(x => x.Username == username && x.Password == password);
            return k;
        }
    }
}
