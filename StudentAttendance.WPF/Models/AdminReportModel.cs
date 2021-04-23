using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;

namespace StudentAttendance.WPF.Models
{
    public class AdminReportModel
    {
        private ISkipService _skipService;
        private IGroupService _groupService;
        public AdminReportModel(ISkipService skipService, IGroupService groupService)
        {
            _skipService = skipService;
            _groupService = groupService;
        }

        public List<GroupDTO> GetGroups()
        {
            var groups = _groupService.Get();
            return groups.ToList();
        }

        public List<SkipDTO> GetSkips(DateTime startDate, DateTime endDate, GroupDTO group)
        {
            var skips = _skipService.Get();
            var result = skips.Where(x =>
                x.Date.CompareTo(startDate) >= 0 && x.Date.CompareTo(endDate) <= 0 &&
                x.Student.Group.Name.Equals(group.Name));
            return result.ToList();
        }
    }
}
