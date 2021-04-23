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
    public class TeacherService : ITeacherService
    {
        private IUnitOfWork _storage;

        public TeacherService(IUnitOfWork storage)
        {
            _storage = storage;
        }

        public void Create(TeacherDTO item)
        {
            try
            {
                Validate(item);
                Teacher teacher = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, Teacher>())
                    .CreateMapper()
                    .Map<Teacher>(item);
                _storage.Teachers.Create(teacher);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно добавить учителя. {exception.Message}");
            }
        }

        public void Update(TeacherDTO item)
        {
            try
            {
                Validate(item);
                Teacher teacher = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, Teacher>())
                    .CreateMapper()
                    .Map<Teacher>(item);
                _storage.Teachers.Update(teacher);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно обновить учителя. {exception.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.Teachers.Delete(id);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно удалить учителя. {exception.Message}");
            }
        }

        public IEnumerable<TeacherDTO> Get()
        {
            try
            {
                var teachers = new MapperConfiguration(cfg => cfg.CreateMap<Teacher, TeacherDTO>())
                    .CreateMapper()
                    .Map<List<TeacherDTO>>(_storage.Teachers.Get());
                var users = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>())
                    .CreateMapper()
                    .Map<List<UserDTO>>(_storage.Users.Get());
                var roles = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>())
                    .CreateMapper()
                    .Map<List<RoleDTO>>(_storage.Roles.Get());
                users.ForEach(x => x.Role = roles.FirstOrDefault(k => k.Id == x.RoleId));
                teachers.ForEach(x => x.User = users.FirstOrDefault(k => k.Id == x.UserId));

                return teachers;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить учителей. {exception.Message}");
            }
        }

        public TeacherDTO Get(int id)
        {
            try
            {
                var teacher = Get().FirstOrDefault(x => x.Id == id);
                return teacher;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить учителя. {exception.Message}");
            }
        }

        public List<DisciplineDTO> GetAllDisciplinesOfTeacher(int id)
        {
            try
            {
                var disciplines = _storage.Disciplines.Get().ToList().Where(x => x.TeacherId == id);
                return new MapperConfiguration(cfg => cfg.CreateMap<Discipline, DisciplineDTO>())
                    .CreateMapper()
                    .Map<List<DisciplineDTO>>(disciplines);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить дисциплины учителя. {exception.Message}");
            }
        }

        private void Validate(TeacherDTO item)
        {
            try
            {
                _storage.Users.Get(item.UserId);
            }
            catch
            {
                throw new Exception("Нет такого пользователя");
            }
        }
    }
}
