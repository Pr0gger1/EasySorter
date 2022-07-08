using System.Windows;

namespace EasySorter
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length != 0)
            {
                foreach (string arg in e.Args)
                {
                    if (arg == "-a")
                    {
                        Sorter Executor = new Sorter();
                        Executor.Run();

                    }
                    else
                    {
                        MessageBox.Show("Некорректный параметр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                }
            }
        }
    }
}
