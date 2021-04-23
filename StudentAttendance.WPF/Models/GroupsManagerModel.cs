using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;

namespace StudentAttendance.WPF.Models
{
    public class GroupsManagerModel
    {
        private IGroupService _groupService;
        public GroupsManagerModel(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public List<GroupDTO> GetAll()
        {
            var k = _groupService.Get().ToList();
            return k;
        }

        public void Add(string name)
        {
            _groupService.Create(new GroupDTO
            {
                Name = name
            });
        }

        public void Delete(int id)
        {
            _groupService.Delete(id);
        }

        public bool IsExist(string name)
        {
            var k = _groupService.Get().FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
            return k != null;
        }
    }
}
