using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class AddSkipViewModel : ViewModelBase
    {
        private readonly AddSkipModel _model;
        private ObservableCollection<DisciplineDTO> _disciplines;
        private DisciplineDTO _selectedDiscipline;
        private ObservableCollection<UserDTO> _students;
        private UserDTO _selectedStudent;
        private DateTime _selectedDate;

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

        public DisciplineDTO SelectedDiscipline
        {
            get => _selectedDiscipline;
            set
            {
                _selectedDiscipline = value;
                RaisePropertyChanged("SelectedDiscipline");
            }
        }

        public ObservableCollection<UserDTO> Students
        {
            get
            {
                _students = new ObservableCollection<UserDTO>(_model.GetStudents());
                return _students;
            }
            set
            {
                _students = value;
                RaisePropertyChanged("Students");
            }
        }
        public UserDTO SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                RaisePropertyChanged("SelectedStudent");
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }

        public ICommand CloseCommand { get; set; }
        public ICommand AddSkipCommand { get; set; }
        public ICommand UpdateCommand { get; }
        public AddSkipViewModel(AddSkipModel model)
        {
            _model = model;
            CloseCommand = new RelayCommand<IClosable>(Close);
            AddSkipCommand = new DelegateCommand(AddSkip);
            UpdateCommand = new DelegateCommand(Update);
        }

        private void Update()
        {
            _students = new ObservableCollection<UserDTO>(_model.GetStudents());
            _disciplines = new ObservableCollection<DisciplineDTO>(_model.GetAllDisciplines());
            RaisePropertyChanged("Disciplines");
            RaisePropertyChanged("Students");
        }

        private void AddSkip()
        {
            try
            {
                if (_selectedDiscipline == null || _selectedStudent == null 
                                                || _selectedDate.CompareTo(new DateTime(2000, 12, 12)) <= 0 
                                                || _selectedDate.CompareTo(new DateTime(2022, 12, 12)) >= 0)
                {
                    MessageBox.Show("Заполните все поля");
                }
                else
                {
                    SkipDTO skip = new SkipDTO
                    {
                        DisciplineId = SelectedDiscipline.Id,
                        StudentId = _model.GetStudentIdFromUserId(SelectedStudent.Id),
                        Date = SelectedDate
                    };
                    _model.AddSkip(skip);
                    ClearAll();
                }
            }
            catch
            {
                MessageBox.Show("Проверьте поля");
            }
        }

        private void Close(IClosable window)
        {
            ClearAll();
            window.Close();
        }

        private void ClearAll()
        {
            _selectedDate = DateTime.Now;
            _selectedStudent = null;
            _selectedDiscipline = null;
            RaisePropertyChanged("SelectedDate");
            RaisePropertyChanged("SelectedStudent");
            RaisePropertyChanged("SelectedDiscipline");
        }
    }
}
