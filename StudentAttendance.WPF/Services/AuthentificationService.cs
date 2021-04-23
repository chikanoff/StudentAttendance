using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendance.WPF.Services
{
    public class AuthentificationService
    {
        public static int Id { get; set; }

        public static void AuthUser(int id)
        {
            Id = id;
        }
    }
}
