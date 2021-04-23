using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.DAL.Entities;

namespace StudentAttendance.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Discipline> Disciplines { get; }
        public IRepository<Group> Groups { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<Skip> Skips { get; }
        public IRepository<Student> Students { get; }
        public IRepository<Teacher> Teachers { get; }
        public IRepository<User> Users { get; }
    }
}
