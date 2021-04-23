using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Exceptions;
using StudentAttendance.BLL.Interfaces.EntityServices;
using StudentAttendance.DAL.Entities;
using StudentAttendance.DAL.Interfaces;

namespace StudentAttendance.BLL.Services
{
    public class SkipService : ISkipService
    {
        private IUnitOfWork _storage;

        public SkipService(IUnitOfWork storage)
        {
            _storage = storage;
        }

        public void Create(SkipDTO item)
        {
            try
            {
                Validate(item);
                Skip skip = new MapperConfiguration(cfg => cfg.CreateMap<SkipDTO, Skip>())
                    .CreateMapper()
                    .Map<Skip>(item);
                _storage.Skips.Create(skip);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно добавить пропуск. {exception.Message}");
            }
        }

        public void Update(SkipDTO item)
        {
            try
            {
                Validate(item);
                Skip skip = new MapperConfiguration(cfg => cfg.CreateMap<SkipDTO, Skip>())
                    .CreateMapper()
                    .Map<Skip>(item);
                _storage.Skips.Create(skip);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно обновить пропуск. {exception.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.Skips.Delete(id);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно удалить пропуск. {exception.Message}");
            }
        }

        public IEnumerable<SkipDTO> Get()
        {
            try
            {
                var t = _storage.Teachers.Get();
                var teachers = new MapperConfiguration(cfg => cfg.CreateMap<Teacher, TeacherDTO>())
                    .CreateMapper()
                    .Map<List<TeacherDTO>>(t);
                var u = _storage.Users.Get();
                var users = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>())
                    .CreateMapper()
                    .Map<List<UserDTO>>(u);
                var r = _storage.Roles.Get();
                var roles = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>())
                    .CreateMapper()
                    .Map<List<RoleDTO>>(r);
                users.ForEach(x => x.Role = roles.FirstOrDefault(k => k.Id == x.RoleId));
                teachers.ForEach(x => x.User = users.FirstOrDefault(k => k.Id == x.UserId));
                var d = _storage.Disciplines.Get();
                var disciplines = new MapperConfiguration(cfg => cfg.CreateMap<Discipline, DisciplineDTO>())
                    .CreateMapper()
                    .Map<List<DisciplineDTO>>(d);
                disciplines.ForEach(x => x.Teacher = teachers.FirstOrDefault(k => k.Id == x.TeacherId));
                var s = _storage.Students.Get();
                var students = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>())
                    .CreateMapper()
                    .Map<List<StudentDTO>>(s);
                var g = _storage.Groups.Get();
                var groups = new MapperConfiguration(cfg => cfg.CreateMap<Group, GroupDTO>())
                    .CreateMapper()
                    .Map<List<GroupDTO>>(g);
                students.ForEach(x => x.Group = groups.FirstOrDefault(k => k.Id == x.GroupId));
                students.ForEach(x => x.User = users.FirstOrDefault(k => k.Id == x.UserId));
                var sk = _storage.Skips.Get();
                var skips = new MapperConfiguration(cfg => cfg.CreateMap<Skip, SkipDTO>())
                    .CreateMapper()
                    .Map<List<SkipDTO>>(sk);
                skips.ForEach(x => x.Discipline = disciplines.FirstOrDefault(k => k.Id == x.DisciplineId));
                skips.ForEach(x => x.Student = students.FirstOrDefault(k => k.Id == x.StudentId));
                return skips;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить пропуски. {exception.Message}");
            }
        }

        public SkipDTO Get(int id)
        {
            try
            {
                var skip = Get().FirstOrDefault(x => x.Id == id);
                return skip;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить пропуск. {exception.Message}");
            }
        }

        private void Validate(SkipDTO item)
        {
            if (item.Date.CompareTo(new DateTime(2002, 1, 1)) < 0)
            {
                throw new ValidationException("Неверная дата", "Wrong date");
            }

            try
            {
                _storage.Disciplines.Get(item.DisciplineId);
            }
            catch
            {
                throw new Exception("не существует такой дисциплины");
            }

            try
            {
                _storage.Students.Get(item.StudentId);
            }
            catch
            {
                throw new Exception("не существует такого студента");
            }
        }
    }
}
