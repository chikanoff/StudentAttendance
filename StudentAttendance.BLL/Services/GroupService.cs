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
    public class GroupService : IGroupService
    {
        private IUnitOfWork _storage;

        public GroupService(IUnitOfWork storage)
        {
            _storage = storage;
        }
        public void Create(GroupDTO item)
        {
            try
            {
                Validate(item);
                Group group = new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, Group>())
                    .CreateMapper()
                    .Map<Group>(item);
                _storage.Groups.Create(group);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно добавить одежду. {exception.Message}");
            }
        }

        public void Update(GroupDTO item)
        {
            try
            {
                Validate(item);
                Group group = new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, Group>())
                    .CreateMapper()
                    .Map<Group>(item);
                _storage.Groups.Update(group);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно обновить группу. {exception.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.Groups.Delete(id);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно удалить группу. {exception.Message}");
            }
        }

        public IEnumerable<GroupDTO> Get()
        {
            try
            {
                var groups = _storage.Groups.Get();
                return new MapperConfiguration(cfg => cfg.CreateMap<Group, GroupDTO>())
                    .CreateMapper()
                    .Map<List<GroupDTO>>(groups);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить группы. {exception.Message}");
            }
        }

        public GroupDTO Get(int id)
        {
            try
            {
                var clothingItem = _storage.Groups.Get(id);
                return new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, Group>())
                    .CreateMapper()
                    .Map<GroupDTO>(clothingItem);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить группу. {exception.Message}");
            }
        }

        private void Validate(GroupDTO item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ValidationException("Неверное имя группы", "Group name");
            }
        }
    }
}
