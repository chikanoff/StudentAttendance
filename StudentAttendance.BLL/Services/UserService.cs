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
    public class UserService : IUserService
    {
        private IUnitOfWork _storage;

        public UserService(IUnitOfWork storage)
        {
            _storage = storage;
        }

        public void Create(UserDTO item)
        {
            try
            {
                Validate(item);
                User user = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>())
                    .CreateMapper()
                    .Map<User>(item);
                _storage.Users.Create(user);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно добавить пользователя. {exception.Message}");
            }
        }

        public void Update(UserDTO item)
        {
            try
            {
                Validate(item);
                User user = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>())
                    .CreateMapper()
                    .Map<User>(item);
                _storage.Users.Update(user);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно обновить Пользователя. {exception.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.Users.Delete(id);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно удалить пользователя. {exception.Message}");
            }
        }

        public IEnumerable<UserDTO> Get()
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
                return users;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить пользователей. {exception.Message}");
            }
        }

        public UserDTO Get(int id)
        {
            try
            {
                var user = Get().FirstOrDefault(x => x.Id == id);
                return user;
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить пользователя. {exception.Message}");
            }
        }

        private void Validate(UserDTO item)
        {
            if (string.IsNullOrEmpty(item.FullName))
            {
                throw new ValidationException("Неверно указано поле", "Full Name");
            }
            if (string.IsNullOrEmpty(item.Username))
            {
                throw new ValidationException("Неверно указано поле", "Username");
            }
            if (string.IsNullOrEmpty(item.Password))
            {
                throw new ValidationException("Неверно указано поле", "Password");
            }

            try
            {
                _storage.Roles.Get(item.RoleId);
            }
            catch
            {
                throw new Exception("Нет такой роли");
            }
        }
    }
}
