using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StudentAttendance.WPF.MVVMBase;
using StudentAttendance.WPF.ViewModels;

namespace StudentAttendance.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для GroupsManagerView.xaml
    /// </summary>
    public partial class GroupsManagerView : Window, IClosable
    {
        public GroupsManagerView(GroupsManagerViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
