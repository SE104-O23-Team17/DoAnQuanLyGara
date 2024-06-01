using QuanLyGara.Services;
using QuanLyGara.ViewModels.Windows;
using QuanLyGara.Views.Windows;
using System.Configuration;
using System.Data;
using System.Windows;

namespace QuanLyGara
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Register register = new Register();
            register.Show();

            /*MainWindow mainWindow = new MainWindow();
            mainWindow.Show();*/

            base.OnStartup(e);
        }
    }

}
