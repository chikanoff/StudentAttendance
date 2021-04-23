using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StudentAttendance.WPF.Models;
using StudentAttendance.WPF.MVVMBase;
using StudentAttendance.WPF.Views;

namespace StudentAttendance.WPF.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private readonly AdminModel _model;
        private DisciplinesManagerViewModel _dmvm;
        private AdminReportViewModel _arvm;
        private TeachersManagerViewModel _tmvm;
        private GroupsManagerViewModel _gmvm;
        private StudentsManagerViewModel _smvm;

        public ICommand OpenDisciplinesCommand { get; }
        public ICommand OpenTeachersCommand { get; }
        public ICommand OpenGroupsCommand { get; }
        public ICommand OpenStudentsCommand { get; }
        public ICommand MakeAReportCommand { get; }
        public ICommand QuitCommand { get; }
        public ICommand LogoutCommand { get; }
        public AdminViewModel(AdminModel model, DisciplinesManagerViewModel dmvm, TeachersManagerViewModel tmvm, GroupsManagerViewModel gmvm, StudentsManagerViewModel smvm, AdminReportViewModel arvm)
        {
            _model = model;
            _dmvm = dmvm;
            _tmvm = tmvm;
            _gmvm = gmvm;
            _smvm = smvm;
            _arvm = arvm;
            OpenDisciplinesCommand = new DelegateCommand(OpenDisciplines);
            OpenTeachersCommand = new DelegateCommand(OpenTeachers);
            OpenGroupsCommand = new DelegateCommand(OpenGroups);
            OpenStudentsCommand = new DelegateCommand(OpenStudents);
            QuitCommand = new RelayCommand<IClosable>(Quit);
            LogoutCommand = new RelayCommand<IClosable>(Logout);
            MakeAReportCommand = new DelegateCommand(Report);
        }

        private void Report()
        {
            AdminReportView v = new AdminReportView(_arvm);
            v.Show();
        }
        private void OpenTeachers()
        {
            TeachersManagerView tv = new TeachersManagerView(_tmvm);
            tv.Show();
        }
        private void OpenGroups()
        {
            GroupsManagerView gv = new GroupsManagerView(_gmvm);
            gv.Show();
        }
        private void OpenStudents()
        {
            StudentsManagerView sv = new StudentsManagerView(_smvm);
            sv.Show();
        }
        private void OpenDisciplines()
        {
            DisciplinesManagerView dv = new DisciplinesManagerView(_dmvm);
            dv.Show();
        }

        private void Quit(IClosable window)
        {
            window.Close();
        }

        private void Logout(IClosable window)
        {
            window.Close();
        }
    }
}
