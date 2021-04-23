using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using StudentAttendance.BLL.DTO;
using StudentAttendance.WPF.Models;
using StudentAttendance.WPF.MVVMBase;

namespace StudentAttendance.WPF.ViewModels
{
    public class AdminReportViewModel : ViewModelBase
    {
        private readonly AdminReportModel _model;
        private ObservableCollection<GroupDTO> _groups;
        private DateTime _selectedStartDate;
        private DateTime _selectedEndDate;
        private GroupDTO _selectedGroup;
        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                RaisePropertyChanged("FileName");
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

        public DateTime SelectedStartDate
        {
            get => _selectedStartDate;
            set
            {
                _selectedStartDate = value;
                RaisePropertyChanged("SelectedStartDate");
            }
        }
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate;
            set
            {
                _selectedEndDate = value;
                RaisePropertyChanged("SelectedEndDate");
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


        public ICommand SaveToXmlCommand { get; }
        public ICommand CloseCommand { get; }

        public AdminReportViewModel(AdminReportModel model)
        {
            _model = model;
            SaveToXmlCommand = new DelegateCommand(Save);
            CloseCommand = new RelayCommand<IClosable>(Close);
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(_fileName) || _selectedGroup == null || _selectedStartDate.CompareTo(new DateTime(2000, 12, 12)) <= 0 || _selectedEndDate.CompareTo(new DateTime(2022, 12, 12)) >= 0)
            {
                MessageBox.Show("Проверьте поля");
            }
            else
            {
                var skips = _model.GetSkips(_selectedStartDate, _selectedEndDate, _selectedGroup);

                XElement root = new XElement("Skips");
                foreach (var skip in skips)
                {
                    root.Add(GetXmlForSkip(skip));
                }

                File.Delete($"{_fileName}.xml");
                root.Save($"{_fileName}.xml");

                Clear();
            }
        }

        private XElement GetXmlForSkip(SkipDTO skip)
        {
            XElement root = new XElement("Skip", new XElement("Discipline", skip.Discipline.Name),
                new XElement("StudentFullName", skip.Student.User.FullName), new XElement("Group", skip.Student.Group.Name), new XElement("Date", skip.Date));
            return root;
        }

        private void Close(IClosable window)
        {
            Clear();
            window.Close();
        }

        private void Clear()
        {
            _fileName = null;
            _selectedGroup = null;
            _selectedEndDate = DateTime.Now;
            _selectedStartDate = DateTime.Now;
            RaisePropertyChanged("FileName");
            RaisePropertyChanged("SelectedStartDate");
            RaisePropertyChanged("SelectedEndDate");
            RaisePropertyChanged("SelectedGroup");
        }
    }
}
