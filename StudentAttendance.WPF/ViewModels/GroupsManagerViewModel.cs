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

namespace StudentAttendance.WPF.ViewModels
{
    public class GroupsManagerViewModel : ViewModelBase
    {
        private readonly GroupsManagerModel _model;
        private ObservableCollection<GroupDTO> _groups;
        private GroupDTO _selectedGroup;
        private string _selectedName;

        public GroupDTO SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged("SelectedGroup");
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
        public ObservableCollection<GroupDTO> Groups
        {
            get
            {
                _groups = new ObservableCollection<GroupDTO>(_model.GetAll());
                return _groups;
            }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CloseCommand { get; }
        public GroupsManagerViewModel(GroupsManagerModel model)
        {
            _model = model;
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete);
            CloseCommand = new RelayCommand<IClosable>(Close);
        }

        private void Add()
        {
            if (!string.IsNullOrEmpty(_selectedName))
            {
                if (_model.IsExist(_selectedName))
                {
                    MessageBox.Show("Группа с таким названием уже существует");
                }
                else
                {
                    _model.Add(_selectedName);
                    _groups = new ObservableCollection<GroupDTO>(_model.GetAll());
                    RaisePropertyChanged("Groups");
                }
            }
            else
            {
                MessageBox.Show("Введите название группы");
            }
        }

        private void Delete()
        {
            if (_selectedGroup != null)
            {
                _model.Delete(_selectedGroup.Id);
                _groups = new ObservableCollection<GroupDTO>(_model.GetAll());
                RaisePropertyChanged("Groups");
            }
            else
            {
                MessageBox.Show("Выберите группу");
            }
        }

        private void Close(IClosable window)
        {
            window.Close();
        }
    }
}
