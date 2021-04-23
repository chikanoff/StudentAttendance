using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StudentAttendance.BLL.DTO;
using StudentAttendance.BLL.Interfaces.EntityServices;
using StudentAttendance.BLL.Services;
using StudentAttendance.DAL.Entities;
using StudentAttendance.WPF.Models;
using StudentAttendance.WPF.MVVMBase;
using StudentAttendance.WPF.Services;
using StudentAttendance.WPF.Views;

namespace StudentAttendance.WPF.ViewModels
{
    public class TeachersViewModel : ViewModelBase
    {
        private TeachersModel _model;
        private AddSkipViewModel _asvm;
        private TeacherReportViewModel _trvm;
        private StudentsManagerViewModel _smvm;
        private DisciplinesManagerViewModel _dmvm;
        private UserDTO _currentUser;
        private string _disciplines;
        private ObservableCollection<SkipDTO> _skips;
        private SkipDTO _selectedSkip;

        public UserDTO CurrentUser
        {
            get
            {
                _currentUser = _model.GetCurrentUser();
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        public string Disciplines
        {
            get
            {
                if (_disciplines == null)
                {
                    _disciplines = "";
                    _model.GetAllDisciplinesOfTeacher().ForEach(x => _disciplines += x.Name + "; ");
                }
                return _disciplines;
            }
            set
            {
                _disciplines = value;
                RaisePropertyChanged("Disciplines");
            }
        }

        public ObservableCollection<SkipDTO> Skips
        {
            get
            {
                _skips = new ObservableCollection<SkipDTO>(_model.GetSkips());
                return _skips;
            }
            set
            {
                _skips = value;
                RaisePropertyChanged("Skips");
            }
        }

        public SkipDTO SelectedSkip
        {
            get => _selectedSkip;
            set
            {
                _selectedSkip = value;
                RaisePropertyChanged("SelectedSkip");
            }
        }


        public ICommand OpenSkipManagerCommand { get; }
        public ICommand OpenDisciplinesCommand { get; }
        public ICommand UpdateSkipsCommand { get; }
        public ICommand DeleteSkipCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand QuitCommand { get; }
        public ICommand OpenStudentsManagerCommand { get; }
        public ICommand OpenReportManagerCommand { get; }

        public TeachersViewModel(TeachersModel model, AddSkipViewModel asvm, DisciplinesManagerViewModel dmvm, StudentsManagerViewModel smvm, TeacherReportViewModel trvm)
        {
            _model = model;
            _asvm = asvm;
            _dmvm = dmvm;
            _smvm = smvm;
            _trvm = trvm;
            LogoutCommand = new RelayCommand<IClosable>(Logout);
            QuitCommand = new RelayCommand<IClosable>(Quit);
            OpenSkipManagerCommand = new DelegateCommand(OpenSkipManager);
            DeleteSkipCommand = new DelegateCommand(DeleteSkip);
            UpdateSkipsCommand = new DelegateCommand(Update);
            OpenDisciplinesCommand = new DelegateCommand(OpenDisciplines);
            OpenReportManagerCommand = new DelegateCommand(OpenReportManager);
            OpenStudentsManagerCommand = new DelegateCommand(OpenStudentManager);
        }

        private void OpenReportManager()
        {
            TeacherReportView v = new TeacherReportView(_trvm);
            v.Show();
        }

        private void OpenStudentManager()
        {
            StudentsManagerView v = new StudentsManagerView(_smvm);
            v.Show();
        }
        private void OpenSkipManager()
        {
            AddSkipView window = new AddSkipView(_asvm);
            window.Show();
        }
        private void Logout(IClosable window)
        {
            window.Close();
        }
        private void Quit(IClosable window)
        {
            window.Close();
        }

        private void Update()
        {
            _skips = new ObservableCollection<SkipDTO>(_model.GetSkips());
            _disciplines = "";
            _model.GetAllDisciplinesOfTeacher().ForEach(x => _disciplines += x.Name + "; ");
            _selectedSkip = null;
            RaisePropertyChanged("Skips");
            RaisePropertyChanged("Disciplines");
            RaisePropertyChanged("SelectedSkip");
        }

        private void DeleteSkip()
        {
            if (_selectedSkip == null)
            {
                MessageBox.Show("Выберите пропуск");
            }
            else
            {
                _model.DeleteSkip(_selectedSkip);
                Update();
            }
        }

        private void OpenDisciplines()
        {
            DisciplinesManagerView window = new DisciplinesManagerView(_dmvm);
            window.Show();
        }
    }
}
