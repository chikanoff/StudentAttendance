using Ninject;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;
using StudentAttendance.BLL.DI;
using StudentAttendance.WPF.Services;
using StudentAttendance.WPF.Views;

namespace StudentAttendance.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            //BLL dependencies
            NinjectModule serviceModule = new ServiceModule(GetConnectionString());
            NinjectModule disciplineModule = new DisciplineServiceModule();
            NinjectModule groupModule = new GroupServiceModule();
            NinjectModule roleModule = new RoleServiceModule();
            NinjectModule skipModule = new SkipServiceModule();
            NinjectModule studentModule = new StudentServiceModule();
            NinjectModule teacherModule = new TeacherServiceModule();
            NinjectModule userModule = new UserServiceModule();

            _container = new StandardKernel(serviceModule, disciplineModule, groupModule, roleModule, skipModule, studentModule, 
                teacherModule, userModule);
        }
        private string GetConnectionString()
        {
            var curDir = Directory.GetCurrentDirectory();
            var baseDir = Directory.GetParent(curDir).Parent.Parent;

            var config = new ConfigurationBuilder()
                .AddJsonFile(@$"{baseDir}\config.json")
                .Build();

            return config["ConnectionStrings:DbConnection"];
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<LoginView>();
            Current.MainWindow.Title = "StudentAttendance";
        }
    }
}
