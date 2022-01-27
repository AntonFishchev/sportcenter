using Entities;
using System.Windows;
using Views.MainWindowPages;
using static Data.Enums;

namespace Views
{
    public partial class MainWindow : Window
    {
        private readonly Employee currentUser;

        public MainWindow(Employee currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnMainPage.Focus();
            if (currentUser.RoleName != Role.Директор.ToString())
            {
                btnDirectorControl.Visibility = Visibility.Collapsed;
            }
            mainPage.Content = new MainPage();
        }

        private void btnDirectorControl_Click(object sender, RoutedEventArgs e)
        {
            mainPage.Content = new DirectorControlPage(currentUser);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Window loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        } 

        private void btnAllClients_Click(object sender, RoutedEventArgs e)
        {
            mainPage.Content = new ClientControlPage(currentUser);
        }

        private void btnInventoryItems_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnMainPage_Click(object sender, RoutedEventArgs e)
        {
            mainPage.Content = new MainPage();
        }
    }
}
