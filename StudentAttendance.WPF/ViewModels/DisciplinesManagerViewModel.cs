using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StudentAttendance.BLL.DTO;
using StudentAttendance.WPF.Models;
using StudentAttendance.WPF.MVVMBase;
using StudentAttendance.WPF.Services;

namespace StudentAttendance.WPF.ViewModels
{
    public class DisciplinesManagerViewModel : ViewModelBase
    {
        private DisciplinesManagerModel _model;
        private ObservableCollection<DisciplineDTO> _disciplines;
        private ObservableCollection<TeacherDTO> _teachers;
        private DisciplineDTO _selectedDiscipline;
        private TeacherDTO _selectedTeacher;
        private string _selectedName;
        private bool _visibility;
        private bool _addVisibility;

        public TeacherDTO SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                RaisePropertyChanged("SelectedTeacher");
            }
        }

        public string SelectedName
        {
            get => _selectedName;
            set
            {
                _selectedName = value;
                RaisePropertyChanged("SelectedName");
            }
        }

        public ObservableCollection<DisciplineDTO> Disciplines
        {
            get
            {
                _disciplines = new ObservableCollection<DisciplineDTO>(_model.GetAllDisciplines());

                return _disciplines;
            }
            set
            {
                _disciplines = value;
                RaisePropertyChanged("Disciplines");
            }
        }

        public ObservableCollection<TeacherDTO> Teachers
        {
            get
            {
                _teachers = new ObservableCollection<TeacherDTO>(_model.GetTeachers());
                return _teachers;
            }
            set
            {
                _teachers = value;
                RaisePropertyChanged("Teachers");
            }
        }

        public DisciplineDTO SelectedDiscipline
        {
            get => _selectedDiscipline;
            set
            {
                _selectedDiscipline = value;
                if (!_visibility)
                {
                    ChangeVisibilities();
                }

                _selectedTeacher = _selectedDiscipline.Teacher;
                RaisePropertyChanged("SelectedTeacher");
                RaisePropertyChanged("SelectedDiscipline");
            }
        }

        public bool Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }

        public bool AddVisibility
        {
            get => _addVisibility;
            set
            {
                _addVisibility = value;
                RaisePropertyChanged("AddVisibility");
            }
        }

        public ICommand QuitCommand { get; }
        public ICommand AddDisciplineCommand { get; }
        public ICommand DeleteDisciplineCommand { get; }
        public ICommand EditDisciplineCommand { get; }


        public DisciplinesManagerViewModel(DisciplinesManagerModel model)
        {
            _model = model;
            _visibility = false;
            _addVisibility = true;
            QuitCommand = new RelayCommand<IClosable>(Quit);
            AddDisciplineCommand = new DelegateCommand(Add);
            DeleteDisciplineCommand = new DelegateCommand(Delete);
            EditDisciplineCommand = new DelegateCommand(Edit);
        }

        private void Quit(IClosable window)
        {
            Clear();
            if (_visibility)
            {
                ChangeVisibilities();
            }
            window.Close();
        }

        private void Add()
        {
            if (!string.IsNullOrEmpty(_selectedName) && _selectedTeacher != null)
            {
                _model.AddDiscipline(new DisciplineDTO
                {
                    Name = _selectedName,
                    TeacherId = _selectedTeacher.Id
                });
                Clear();
                Update();
            }
            else
            {
                MessageBox.Show("проверьте поля");
            }
        }

        private void Delete()
        {
            try
            {
                if (_selectedDiscipline == null)
                {
                    MessageBox.Show("Выберите дисциплину");
                }
                else
                {
                    _model.DeleteDiscipline(_selectedDiscipline);
                    Clear();
                    Update();
                    ChangeVisibilities();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно удалить дисциплину");
            }
        }

        private void Edit()
        {
            try
            {
                if (_selectedDiscipline == null)
                {
                    MessageBox.Show("Выберите дисциплину");
                }
                else
                {
                    _model.EditDiscipline(new DisciplineDTO
                    {
                        Id = _selectedDiscipline.Id,
                        Name = _selectedName,
                        TeacherId = _selectedTeacher.Id,
                        Teacher = null
                    });
                    Clear();
                    Update();
                    ChangeVisibilities();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно изменить дисциплину");
            }
        }

        private void Clear()
        {
            _selectedDiscipline = null;
            _selectedTeacher = null;
            _selectedName = null;
            RaisePropertyChanged("SelectedDiscipline");
            RaisePropertyChanged("SelectedTeacher");
            RaisePropertyChanged("SelectedName");
        }

        private void Update()
        {
            _disciplines = new ObservableCollection<DisciplineDTO>(_model.GetAllDisciplines());
            RaisePropertyChanged("Disciplines");
        }

        private void ChangeVisibilities()
        {
            _visibility = !_visibility;
            _addVisibility = !_addVisibility;
            RaisePropertyChanged("Visibility");
            RaisePropertyChanged("AddVisibility");
        }
    }
}
