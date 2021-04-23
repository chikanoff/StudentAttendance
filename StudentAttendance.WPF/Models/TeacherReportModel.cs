using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;
using StudentAttendance.WPF.Services;

namespace StudentAttendance.WPF.Models
{
    public class TeacherReportModel
    {
        private IDisciplineService _disciplineService;
        private ISkipService _skipService;
        public TeacherReportModel(IDisciplineService disciplineService, ISkipService skipService)
        {
            _disciplineService = disciplineService;
            _skipService = skipService;
        }

        public List<DisciplineDTO> GetTeacherDisciplines()
        {
            List<DisciplineDTO> disciplines = new List<DisciplineDTO>();
            disciplines = _disciplineService.Get().Where(x => x.Teacher.UserId == AuthentificationService.Id)
                .ToList();
            return disciplines;
        }

        public List<SkipDTO> GetSkips(DateTime startDate, DateTime endDate, DisciplineDTO discipline)
        {
            var skips = _skipService.Get();
            var result = skips.Where(x =>
                x.Date.CompareTo(startDate) >= 0 && x.Date.CompareTo(endDate) <= 0 &&
                x.Discipline.Name.Equals(discipline.Name));
            return result.ToList();

        }
    }
}
