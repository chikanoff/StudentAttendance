using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using StudentAttendance.BLL.DTO;
using StudentAttendance.WPF.Models;
using StudentAttendance.WPF.MVVMBase;

namespace StudentAttendance.WPF.ViewModels
{
    public class StudentsManagerViewModel : ViewModelBase
    {
        private readonly StudentsManagerModel _model;
        private ObservableCollection<StudentDTO> _students;
        private StudentDTO _selectedStudent;
        private ObservableCollection<GroupDTO> _groups;
        private GroupDTO _selectedGroup;
        private string _selectedUsername;
        private string _selectedPassword;
        private string _selectedFullName;
        private string _selectedPhoto;


        public StudentDTO SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                RaisePropertyChanged("SelectedStudent");
            }
        }
        public string SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                _selectedPhoto = value;
                RaisePropertyChanged("SelectedPhoto");
            }
        }
        public GroupDTO SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged("SelectedGroup");
            }
        }
        public ObservableCollection<GroupDTO> Groups
        {
            get
            {
                _groups = new ObservableCollection<GroupDTO>(_model.GetGroups());
                return _groups;
            }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");
            }
        }
        public ObservableCollection<StudentDTO> Students
        {
            get
            {
                _students = new ObservableCollection<StudentDTO>(_model.GetStudents());
                return _students;
            }
            set
            {
                _students = value;
                RaisePropertyChanged("Students");
            }
        }
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

        public ICommand CloseCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ChoosePathCommand { get; }
        public StudentsManagerViewModel(StudentsManagerModel model)
        {
            _model = model;
            CloseCommand = new RelayCommand<IClosable>(Close);
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete);
            ChoosePathCommand = new DelegateCommand(Choose);
        }

        private void Choose()
        {
            string path = null;
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter =
                    "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };
            var show = op.ShowDialog();
            if (show != null && show.Value)
            {
                path = op.FileName;
            }

            if (path != null)
            {
                _selectedPhoto = path;
                RaisePropertyChanged("SelectedPhoto");
            }
            else
            {
                MessageBox.Show("Вы не выбрали фотографию");
            }
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
                string.IsNullOrEmpty(_selectedFullName) || _selectedGroup == null)
            {
                MessageBox.Show("Вы заполнили не все поля");
            }
            else
            {
                if (!_model.IsExist(_selectedUsername))
                {
                    _model.Add(_selectedUsername, _selectedPassword, _selectedFullName, _selectedPhoto, _selectedGroup);
                    ClearInputs();
                    _students = new ObservableCollection<StudentDTO>(_model.GetStudents());
                    RaisePropertyChanged("Students");
                }
                else
                {
                    MessageBox.Show("Студент с таким именем существует");
                }

            }

        }

        private void Delete()
        {
            if (_selectedStudent != null)
            {
                _model.Delete(_selectedStudent);
                ClearSelected();
                _students = new ObservableCollection<StudentDTO>(_model.GetStudents());
                RaisePropertyChanged("Students");
            }
            else
            {
                MessageBox.Show("Выберите студента");
            }
        }
        private void ClearInputs()
        {
            _selectedFullName = null;
            _selectedPhoto = null;
            _selectedPassword = null;
            _selectedUsername = null;
            _selectedGroup = null;
            
            RaisePropertyChanged("SelectedFullName");
            RaisePropertyChanged("SelectedGroup");
            RaisePropertyChanged("SelectedPhoto");
            RaisePropertyChanged("SelectedUsername");
            RaisePropertyChanged("SelectedPassword");
        }
        private void ClearSelected()
        {
            _selectedStudent = null;
            RaisePropertyChanged("SelectedStudent");
        }
    }
}
