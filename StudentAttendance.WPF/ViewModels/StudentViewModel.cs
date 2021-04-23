using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StudentAttendance.BLL.DTO;
using StudentAttendance.WPF.Models;
using StudentAttendance.WPF.MVVMBase;
using StudentAttendance.WPF.Views;

namespace StudentAttendance.WPF.ViewModels
{
    public class StudentViewModel : ViewModelBase
    {
        private readonly StudentModel _model;
        private ObservableCollection<SkipDTO> _skips;
        private StudentDTO _currentStudent;

        public ObservableCollection<SkipDTO> Skips
        {
            get
            {
                _skips = new ObservableCollection<SkipDTO>(_model.GetSkips(_currentStudent));
                return _skips;
            }
            set
            {
                _skips = value;
                RaisePropertyChanged("Skips");
            }
        }

        public StudentDTO CurrentStudent
        {
            get
            {
                _currentStudent = _model.GetCurrentStudent();
                return _currentStudent;
            }
            set
            {
                _currentStudent = value;
                RaisePropertyChanged("CurrentStudent");
            }
        }

        public ICommand QuitCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand RefreshCommand { get; }
        public StudentViewModel(StudentModel model)
        {
            _model = model;
            QuitCommand = new RelayCommand<IClosable>(Quit);
            LogoutCommand = new RelayCommand<IClosable>(Logout);
            RefreshCommand = new DelegateCommand(Refresh);
        }

        private void Refresh()
        {
            _skips = new ObservableCollection<SkipDTO>(_model.GetSkips(_currentStudent));
            RaisePropertyChanged("Skips");
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
