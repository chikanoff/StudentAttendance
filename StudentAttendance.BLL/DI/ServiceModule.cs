using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using StudentAttendance.DAL.Interfaces;
using StudentAttendance.DAL.UnitsOfWork;

namespace StudentAttendance.BLL.DI
{
    public class ServiceModule : NinjectModule
    {
        private string _connectionString;

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>()
                .To<StudentsAttendanceDbUnitOfWork>()
                .WithConstructorArgument(_connectionString);
        }
    }
}
