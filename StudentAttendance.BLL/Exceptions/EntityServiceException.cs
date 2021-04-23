using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendance.BLL.Exceptions
{
    public class EntityServiceException : Exception
    {
        public EntityServiceException(string message) : base(message)
        {
        }
    }
}
