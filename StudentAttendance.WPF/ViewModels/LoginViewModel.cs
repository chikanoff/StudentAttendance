using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StudentAttendance.BLL.DTO;
using StudentAttendance.WPF.Models;
using StudentAttendance.WPF.MVVMBase;
using StudentAttendance.WPF.Services;
using StudentAttendance.WPF.Views;

namespace StudentAttendance.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly LoginModel _model;
        private TeachersViewModel _tvm;
        private AdminViewModel _avm;
        private StudentViewModel _svm;
        private string _selectedUsername;
        private string _selectedPassword;

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

        public ICommand QuitCommand { get; }
        public ICommand LoginCommand { get; }
        public LoginViewModel(LoginModel model, TeachersViewModel tvm, AdminViewModel avm, StudentViewModel svm)
        {
            _model = model;
            _tvm = tvm;
            _avm = avm;
            _svm = svm;
            QuitCommand = new RelayCommand<IClosable>(Quit);
            LoginCommand = new RelayCommand<IClosable>(Login);
        }

        private void Login(IClosable window)
        {
            if (string.IsNullOrEmpty(_selectedUsername) || string.IsNullOrEmpty(_selectedPassword))
            {
                MessageBox.Show("Проверьте поля");
            }
            else
            {
                UserDTO user = _model.GetUser(_selectedUsername, _selectedPassword);
                if (user == null)
                {
                    MessageBox.Show("Такого пользователя не существует");
                }
                else
                {
                    if (user.Role.Name.Equals("Администратор"))
                    {
                        AdminView av = new AdminView(_avm);
                        AuthentificationService.AuthUser(user.Id);
                        av.Show();
                    } else if (user.Role.Name.Equals("Преподаватель"))
                    {
                        TeachersView tv = new TeachersView(_tvm);
                        AuthentificationService.AuthUser(user.Id);
                        tv.Show();
                    }
                    else
                    {
                        StudentView sv = new StudentView(_svm);
                        AuthentificationService.AuthUser(user.Id);
                        sv.Show();
                    }
                }
                _selectedPassword = "";
                _selectedUsername = "";
                RaisePropertyChanged("SelectedPassword");
                RaisePropertyChanged("SelectedUsername");
            }
        }

        private void Quit(IClosable window)
        {
            window.Close();
        }

    }
}
