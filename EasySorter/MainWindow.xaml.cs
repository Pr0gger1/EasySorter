using System.Windows;

namespace EasySorter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            bool Enable = RegistryWorker.IsRegExist();
            DeactivateBtn.IsEnabled = Enable;
            ActivateBtn.IsEnabled = !Enable;
        }

        private void ActivateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryWorker.Activate();
                ActivateBtn.IsEnabled = false;
                DeactivateBtn.IsEnabled = true; MessageBox.Show("Пункт сортировки добавлен в контекстное меню", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Программу необходимо запустить от имени администратора!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                ActivateBtn.IsEnabled = false;
                DeactivateBtn.IsEnabled = false;
            }
        }

        private void DeactivateBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistryWorker.Deactivate();
            ActivateBtn.IsEnabled = true;
            DeactivateBtn.IsEnabled = false;
            MessageBox.Show("Пункт удален из контекстного меню", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Программа создана Pr0gger1", "О программе", MessageBoxButton.OK, MessageBoxImage.Information
                );
        }
    }
}
