using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using StudentAttendance.BLL.Interfaces.EntityServices;
using StudentAttendance.BLL.Services;

namespace StudentAttendance.BLL.DI
{
    public class DisciplineServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDisciplineService>()
                .To<DisciplineService>();
        }
    }
}
