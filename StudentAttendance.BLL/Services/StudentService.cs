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
    public class StudentService : IStudentService
    {
        private IUnitOfWork _storage;

        public StudentService(IUnitOfWork storage)
        {
            _storage = storage;
        }

        public void Create(StudentDTO item)
        {
            try
            {
                Validate(item);
                Student student = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, Student>())
                    .CreateMapper()
                    .Map<Student>(item);
                _storage.Students.Create(student);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно добавить студента. {exception.Message}");
            }
        }

        public void Update(StudentDTO item)
        {
            try
            {
                Validate(item);
                Student student = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, Student>())
                    .CreateMapper()
                    .Map<Student>(item);
                _storage.Students.Update(student);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно обновить студента. {exception.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.Students.Delete(id);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно удалить студента. {exception.Message}");
            }
        }

        public IEnumerable<StudentDTO> Get()
        {
            try
            {
                var users = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>())
                    .CreateMapper()
                    .Map<List<UserDTO>>(_storage.Users.Get());
                var roles = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>())
                    .CreateMapper()
                    .Map<List<RoleDTO>>(_storage.Roles.Get());
                users.ForEach(x => x.Role = roles.FirstOrDefault(k => k.Id == x.RoleId));
                var students = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>())
                    .CreateMapper()
                    .Map<List<StudentDTO>>(_storage.Students.Get());
                var groups = new MapperConfiguration(cfg => cfg.CreateMap<Group, GroupDTO>())
                    .CreateMapper()
                    .Map<List<GroupDTO>>(_storage.Groups.Get());
                students.ForEach(x => x.Group = groups.FirstOrDefault(k => k.Id == x.GroupId));
                students.ForEach(x => x.User = users.FirstOrDefault(k => k.Id == x.UserId));
                return students;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить студентов. {exception.Message}");
            }
        }

        public StudentDTO Get(int id)
        {
            try
            {
                var student = Get().FirstOrDefault(x => x.Id == id);
                return student;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить студента. {exception.Message}");
            }
        }

        public List<UserDTO> GetUsersFromStudents()
        {
            try
            {
                var students = _storage.Students.Get();
                List<User> users = new List<User>();
                students.ToList().ForEach(x => users.Add(_storage.Users.Get(x.UserId)));
                return new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>())
                    .CreateMapper()
                    .Map<List<UserDTO>>(users);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить пользователей из студентов. {exception.Message}");
            }
        }

        private void Validate(StudentDTO item)
        {
            if (string.IsNullOrEmpty(item.Photo))
            {
                throw new ValidationException("Не правильный путь к фото", "Photo path");
            }

            try
            {
                _storage.Groups.Get(item.GroupId);
            }
            catch
            {
                throw new Exception("нет такой группы");
            }

            try
            {
                _storage.Users.Get(item.UserId);
            }
            catch
            {
                throw new Exception("нет такого пользователя");
            }
        }
    }
}
