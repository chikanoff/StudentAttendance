using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using StudentAttendance.DAL.Connections;
using StudentAttendance.DAL.Entities;
using StudentAttendance.DAL.Interfaces;
using StudentAttendance.DAL.Repositories;

namespace StudentAttendance.DAL.UnitsOfWork
{
    public class StudentsAttendanceDbUnitOfWork : IUnitOfWork
    {
        private readonly MySqlConnection _conn;
        private DisciplineDbRepository _disciplines;
        private GroupDbRepository _groups;
        private RoleDbRepository _roles;
        private SkipDbRepository _skips;
        private StudentDbRepository _students;
        private TeacherDbRepository _teachers;
        private UserDbRepository _users;

        public IRepository<Discipline> Disciplines
        {
            get
            {
                if (_disciplines == null)
                {
                    _disciplines = new DisciplineDbRepository(_conn);
                }

                return _disciplines;
            }
        }

        public IRepository<Group> Groups
        {
            get
            {
                if (_groups == null)
                {
                    _groups = new GroupDbRepository(_conn);
                }

                return _groups;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RoleDbRepository(_conn);
                }

                return _roles;
            }
        }

        public IRepository<Skip> Skips
        {
            get
            {
                if (_skips == null)
                {
                    _skips = new SkipDbRepository(_conn);
                }

                return _skips;
            }
        }

        public IRepository<Student> Students
        {
            get
            {
                if (_students == null)
                {
                    _students = new StudentDbRepository(_conn);
                }

                return _students;
            }
        }

        public IRepository<Teacher> Teachers
        {
            get
            {
                if (_teachers == null)
                {
                    _teachers = new TeacherDbRepository(_conn);
                }

                return _teachers;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserDbRepository(_conn);
                }

                return _users;
            }
        }

        public StudentsAttendanceDbUnitOfWork(string connectionString)
        {
            _conn = new DbConnection(connectionString).ConnectToDatabase();
        }
    }
}
