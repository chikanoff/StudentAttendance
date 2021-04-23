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
    public class RoleService : IRoleService
    {
        private IUnitOfWork _storage;

        public RoleService(IUnitOfWork storage)
        {
            _storage = storage;
        }
        public void Create(RoleDTO item)
        {
            throw new NotImplementedException();
        }

        public void Update(RoleDTO item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoleDTO> Get()
        {
            try
            {
                var roles = _storage.Roles.Get();
                return new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>())
                    .CreateMapper()
                    .Map<List<RoleDTO>>(roles);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить роли. {exception.Message}");
            }
        }

        public RoleDTO Get(int id)
        {
            try
            {
                var role = _storage.Roles.Get(id);
                return new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, Role>())
                    .CreateMapper()
                    .Map<RoleDTO>(role);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить роль. {exception.Message}");
            }
        }

        private void Validate(RoleDTO item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {

            }
        }
    }
}
