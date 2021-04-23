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
    public class DisciplineService : IDisciplineService
    {
        private IUnitOfWork _storage;

        public DisciplineService(IUnitOfWork storage)
        {
            _storage = storage;
        }

        public void Create(DisciplineDTO item)
        {
            try
            {
                 Validate(item);

                 Discipline disc = new MapperConfiguration(cfg => cfg.CreateMap<DisciplineDTO, Discipline>())
                     .CreateMapper().Map<Discipline>(item);
                 _storage.Disciplines.Create(disc);
            }
            catch (Exception e)
            {
                throw new EntityServiceException($"Невозможно добавить дисциплину {e.Message}");
            }
        }

        public void Update(DisciplineDTO item)
        {
            try
            {
                Validate(item);
                Discipline disc = new MapperConfiguration(cfg => cfg.CreateMap<DisciplineDTO, Discipline>())
                    .CreateMapper().Map<Discipline>(item);
                _storage.Disciplines.Update(disc);
            }
            catch (Exception e)
            {
                throw new EntityServiceException($"Невозможно обновить дисциплину {e.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.Disciplines.Delete(id);
            }
            catch (Exception e)
            {
                throw new EntityServiceException($"Невозможно удалить дисциплину {e.Message}");
            }
        }

        public IEnumerable<DisciplineDTO> Get()
        {
            try
            {
                var teachers = new MapperConfiguration(cfg => cfg.CreateMap<Teacher, TeacherDTO>())
                    .CreateMapper()
                    .Map<List<TeacherDTO>>(_storage.Teachers.Get());
                var users = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>())
                    .CreateMapper()
                    .Map<List<UserDTO>>(_storage.Users.Get());
                var roles =  new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>())
                    .CreateMapper()
                    .Map<List<RoleDTO>>(_storage.Roles.Get());
                users.ForEach(x => x.Role = roles.FirstOrDefault(k => k.Id == x.RoleId));
                teachers.ForEach(x => x.User = users.FirstOrDefault(k => k.Id == x.UserId));
                var disciplines =  new MapperConfiguration(cfg => cfg.CreateMap<Discipline, DisciplineDTO>())
                    .CreateMapper()
                    .Map<List<DisciplineDTO>>(_storage.Disciplines.Get());
                disciplines.ForEach(x => x.Teacher = teachers.FirstOrDefault(k => k.Id == x.TeacherId));
                return disciplines;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить дисциплины. {exception.Message}");
            }
        }

        public DisciplineDTO Get(int id)
        {
            try
            {
                var discipline = Get().FirstOrDefault(x => x.Id == id);
                return discipline;

            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить одежду. {exception.Message}");
            }
        }

        private void Validate(DisciplineDTO item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ValidationException("Неверное имя", "discipline name");
            }

            try
            {
                _storage.Disciplines.Get(item.TeacherId);
            }
            catch
            {
                throw new Exception("Нет такого учителя");
            }
        }
    }
}
