using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StudentAttendance.BLL.DTO;
using StudentAttendance.DAL.Entities;
using StudentAttendance.WPF.Models;
using StudentAttendance.WPF.MVVMBase;

namespace StudentAttendance.WPF.ViewModels
{
    public class TeachersManagerViewModel : ViewModelBase
    {
        private readonly TeachersManagerModel _model;
        private ObservableCollection<TeacherDTO> _teachers;
        private TeacherDTO _selectedTeacher;
        private string _selectedUsername;
        private string _selectedPassword;
        private string _selectedFullName;
        
        public string SelectedUsername
        {
            get => _selectedUsername;
            set
            {
                _selectedUsername = value;
                RaisePropertyChanged("SelectedUsername");
            }
        }

        public string SelectedPassword
        {
            get => _selectedPassword;
            set
            {
                _selectedPassword = value;
                RaisePropertyChanged("SelectedPassword");
            }
        }

        public string SelectedFullName
        {
            get => _selectedFullName;
            set
            {
                _selectedFullName = value;
                RaisePropertyChanged("SelectedFullName");
            }
        }

        public TeacherDTO SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                RaisePropertyChanged("SelectedTeacher");
            }
        }

        public ObservableCollection<TeacherDTO> Teachers
        {
            get
            {
                _teachers = new ObservableCollection<TeacherDTO>(_model.GetAllTeachers());
                return _teachers;
            }
            set
            {
                _teachers = value;
                RaisePropertyChanged("Teachers");
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public TeachersManagerViewModel(TeachersManagerModel model)
        {
            _model = model;
            CloseCommand = new RelayCommand<IClosable>(Close);
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete);
        }

        private void Close(IClosable window)
        {
            ClearInputs();
            ClearSelected();
            window.Close();
        }

        private void Add()
        {
            if (string.IsNullOrEmpty(_selectedUsername) || string.IsNullOrEmpty(_selectedPassword) ||
                string.IsNullOrEmpty(_selectedFullName))
            {
                MessageBox.Show("Вы заполнили не все поля");
            }
            else
            {
                if (!_model.IsExist(_selectedUsername))
                {
                    _model.Add(_selectedUsername, _selectedPassword, _selectedFullName);
                    ClearInputs();
                    _teachers = new ObservableCollection<TeacherDTO>(_model.GetAllTeachers());
                    RaisePropertyChanged("Teachers");
                }
                else
                {
                    MessageBox.Show("Пользователь с таким именем существует");
                }

            }

        }

        private void Delete()
        {
            if (_selectedTeacher != null)
            {
                _model.Delete(_selectedTeacher);
                ClearSelected();
                _teachers = new ObservableCollection<TeacherDTO>(_model.GetAllTeachers());
                RaisePropertyChanged("Teachers");
            }
            else
            {
                MessageBox.Show("Выберите преподавателя");
            }
        }
        private void ClearInputs()
        {
            _selectedFullName = null;
            _selectedPassword = null;
            _selectedUsername = null;
            RaisePropertyChanged("SelectedFullName");
            RaisePropertyChanged("SelectedUsername");
            RaisePropertyChanged("SelectedPassword");
        }
        private void ClearSelected()
        {
            _selectedTeacher = null;
            RaisePropertyChanged("SelectedTeacher");
        }
    }
}
